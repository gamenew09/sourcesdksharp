using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;

using Source.Public.Tier0;
using Source.Public;

namespace Source.Host
{
	class Program
	{
		// Global App Domains
		static AppDomain[] domains;
		static DomainInterface[] domainInterfaces;
		// Map the domains to the respective lib dir
		static readonly string[] domainLib = {"server", "client", "client"};

		static void Main(string[] args)
		{
			// Once this function returns, all native->managed will be handled through messages
			NativeFunctions.SetMonoMessageFn(MonoMessageHandler);

			domains = new AppDomain[3];
			domainInterfaces = new DomainInterface[3];

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
				setup.ApplicationBase = FileSystem.RelativePathToFullPath("mono/lib/" + domainLib[tid], "GAME");
				domains[tid] = AppDomain.CreateDomain(target.ToString(), null, setup);
				domainInterfaces[tid] = (DomainInterface)domains[tid].CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(DomainInterface).FullName);
				break;
			default:
				Debug.DevMsg("[Source.Host.exe] Recieved Mono message, target: {0}, id: {1}, length: {2}\n", target, msgid, length);
				break;
			}
		}
	}
}
