using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;

using Source.Tier0;
using Source.Filesystem;

namespace Source.Host
{
	class Program
	{
		class ScriptDomain
		{
			public AppDomain Domain { get; set; } 
			public DomainInterface Interface { get; set; }
		}

		// Global App Domains
		static ScriptDomain[] scriptDomains;

		// Map the domains to the respective lib dir
		static readonly string[] domainName = {"server", "client", "menu"};

		static void Main(string[] args)
		{
			// Once this function returns, all native->managed will be handled through messages
			NativeFunctions.SetMonoMessageFn(MonoMessageHandler);

			scriptDomains = new ScriptDomain[3];

			Debug.Msg("[Source.Host.exe] Loading shared Mono (Version: {0})\n", GetMonoVersion());
		}

		static string GetMonoVersion()
		{
			Type type = Type.GetType("Mono.Runtime");
			if (type != null)
			{
				MethodInfo displayName = type.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static); 
				if(displayName != null)
					return (string)displayName.Invoke(null, null);
			}
			return "Unknown";
		}

		static void MonoMessageHandler(EMonoScriptDomain target, EMonoScriptMsgID msgid, IntPtr buffer, int length)
		{
			byte[] data = new byte[length];
			if(length != 0)
				Marshal.Copy(buffer, data, 0, length);

			int tid = (int)target;

			switch(msgid)
			{
			case EMonoScriptMsgID.ScriptMsgIDInitialize:
				AppDomainSetup setup = new AppDomainSetup();
				setup.ApplicationBase = new SourceFileInfo("mono/lib/" + (target == EMonoScriptDomain.ScriptDomainServer ? "server" : "client"), "GAME").FullName;
				AppDomain aDomain = AppDomain.CreateDomain(domainName[tid], null, setup);
				DomainInterface dInterface = (DomainInterface)aDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(DomainInterface).FullName);
				scriptDomains[tid] = new ScriptDomain() { Domain = aDomain, Interface = dInterface };
				break;
			default:
				Debug.DevMsg("[Source.Host.exe] Recieved Mono message, target: {0}, id: {1}, length: {2}\n", target, msgid, length);
				break;
			}
		}
	}
}
