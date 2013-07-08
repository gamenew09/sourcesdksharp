using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Source
{
    public enum EMonoScriptDomain
    {
        ScriptDomainNone = 0,
        ScriptDomainServer = 1,
        ScriptDomainClient = 2,
        ScriptDomainMenu = 4,
    }

    public enum EMonoScriptMsgID
    {
        ScriptMsgIDInvalid = 0,
        ScriptMsgIDInitialize = 1,
    }

    public delegate void MonoMessageDelegate(EMonoScriptDomain target, EMonoScriptMsgID msgid, IntPtr buffer, int length);

    static class NativeFunctions
    {
		[DllImport("__Source", EntryPoint = "set_mono_message_fn")]
        public static extern void SetMonoMessageFn(MonoMessageDelegate del);
    }
}
