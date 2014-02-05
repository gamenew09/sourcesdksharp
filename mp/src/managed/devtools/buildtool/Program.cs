using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Mono.Options;
using Mono.Cecil;
using Mono.Cecil.Mdb;

namespace buildtool
{
	class MainClass
	{
		private static void ProcessDirectory(string libDir, string target, string entryPointPrefix, bool debug)
		{
			var di = new DirectoryInfo(libDir);
			if(!di.Exists)
				Directory.CreateDirectory(libDir);

			var targetPath = target + "/" + entryPointPrefix;

			if(!new DirectoryInfo(targetPath).Exists)
				Directory.CreateDirectory(targetPath);
			
			var moduleRef = new ModuleReference(entryPointPrefix + ".dll");

			entryPointPrefix += "_";

			// We need to do this as it's looking for the assembly when use an enum constant
			// So lets tell it where the assemblies are coming from so it can load them
			var assemblyResolve = new DefaultAssemblyResolver();
			assemblyResolve.AddSearchDirectory(libDir);

			var readParams = new ReaderParameters { AssemblyResolver = assemblyResolve };

			var writeParams = new WriterParameters { SymbolWriterProvider = new MdbWriterProvider(), WriteSymbols = debug };

			foreach (var file in di.EnumerateFiles("*.dll").Union(di.EnumerateFiles("*.exe")))
			{
				ModuleDefinition md;

				// Only load the pdb if it exists, stops issues with Nuget packages, eg Newtonsoft.Json
				if (new FileInfo(file.FullName.Replace(file.Extension, "pdb")).Exists)
					readParams.ReadSymbols = true;
				else
					readParams.ReadSymbols = false;

				// If there are mdbs load them too, but make sure we know we're looking for mdbs
				if (new FileInfo(file.FullName+".mdb").Exists)
				{
					readParams.ReadSymbols = true;
					readParams.SymbolReaderProvider = new MdbReaderProvider ();
				}

				md = ModuleDefinition.ReadModule(file.FullName, readParams);

				md.ModuleReferences.Add(moduleRef);
				Console.WriteLine("Type count: {0}", md.Types.Count);
				// Process P/Invokes
				foreach (var mdType in md.Types)
				{
					Console.WriteLine("Method count: {0}", mdType.Methods.Count);
					foreach (var method in mdType.Methods)
					{
						if (method.IsPInvokeImpl)
						{
							if (method.PInvokeInfo.Module.Name == "__Source")
							{
								Console.WriteLine("Fixing entry point for: {0}", method.FullName);
								Console.WriteLine("From: {0}", method.PInvokeInfo.EntryPoint);
								method.PInvokeInfo.EntryPoint = entryPointPrefix + method.PInvokeInfo.EntryPoint;
								Console.WriteLine("To: {0}", method.PInvokeInfo.EntryPoint);

								method.PInvokeInfo.Module = moduleRef;
							}
						}
					}
				}

				md.Write(targetPath + "/" + file.Name, writeParams);
			}
		}

		public static void Main(string[] args)
		{
			bool showHelp = false;
			string inputDir = "";
			string outputDir = "";
			List<string> prefixes = new List<string>();
			bool debug = false;
			var p = new OptionSet()
			{
				{ "i|input=", "Input directory",
					v => inputDir = v },
				{ "o|output=", "Output directory",
					v => outputDir = v },
				{ "p|prefix=", "Library prefixes",
					v => prefixes.Add(v) },
				{ "d|debug", "Debug",
					v => debug = v != null },
				{ "h|help", "Show this help",
					v => showHelp = v != null },
			};

			try
			{
				p.Parse(args);
			}
			catch(OptionException e)
			{
				Console.Write("buildtool: ");
				Console.WriteLine(e.Message);
				Console.WriteLine("Try 'buildtool --help' for more information");
				return;
			}

			if(showHelp)
			{
				ShowHelp(p);
				return;
			}

			if(inputDir == "" || outputDir == "" || prefixes.Count == 0)
			{
				Console.WriteLine("Try 'buildtool --help' for more information");
				return;
			}

			foreach(var prefix in prefixes)
			{
				ProcessDirectory(inputDir, outputDir, prefix, debug);
			}
		}

		static void ShowHelp(OptionSet p)
		{
			Console.WriteLine("Usage: buildtool [OPTIONS]");
			Console.WriteLine("Options:");
			p.WriteOptionDescriptions(Console.Out);
		}
	}
}
