using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using Source.Public.Tier0;

namespace Source.Script
{
    class Program
    {
        // Global App Domains
        AppDomain menuDomain; // Persistent client
        AppDomain clientDomain; // Per map client
        AppDomain serverDomain; // Per map server

        static void Main(string[] args)
        {
			Debug.Msg("[Source.Script.exe] Loading shared Mono\n");
			NativeFunctions.SetMonoMessageFn(MonoMessageHandler);
        }

        static void MonoMessageHandler(EMonoScriptDomain target, EMonoScriptMsgID msgid, IntPtr buffer, int length)
        {
            byte[] data = new byte[length];
			if(length != 0)
				Marshal.Copy(buffer, data, 0, length);

			Debug.Msg("[Source.Script.exe] Recieved Mono message, target: {0}, id: {1}, length: {2}\n", target, msgid, length);
        }
    }
}
