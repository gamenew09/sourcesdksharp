using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseFunctions.Msg("[Source.exe] Loading shared Mono\n");
            BaseFunctions.SetMonoMessageFn(MonoMessageHandler);
        }

        static void MonoMessageHandler(EMonoScriptDomain target, EMonoScriptMsgID msgid, IntPtr buffer, int length)
        {
            BaseFunctions.Msg("[Source.exe] Recieved Mono message, target: {0}, id: {1}, length: {2}\n", target, msgid, length);
        }
    }
}
