using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Source
{
    class Program
    {
        // Global App Domains
        AppDomain menuDomain; // Persistent client
        AppDomain clientDomain; // Per map client
        AppDomain serverDomain; // Per map server

        static void Main(string[] args)
        {
            BaseFunctions.Msg("[Source.exe] Loading shared Mono\n");
            BaseFunctions.SetMonoMessageFn(MonoMessageHandler);
        }

        static void MonoMessageHandler(EMonoScriptDomain target, EMonoScriptMsgID msgid, IntPtr buffer, int length)
        {
            byte[] data = new byte[length];
			if(buffer!=null)
            	Marshal.Copy(buffer, data, 0, length);

            BaseFunctions.Msg("[Source.exe] Recieved Mono message, target: {0}, id: {1}, length: {2}\n", target, msgid, length);
        }
    }
}
