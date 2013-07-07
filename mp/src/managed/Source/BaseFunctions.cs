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

    public delegate void MonoMessageDelegate( EMonoScriptDomain target, EMonoScriptMsgID msgid, IntPtr buffer, int length );

    static class BaseFunctions
    {
        [DllImport("__Internal")]
        private static extern void monoscript_msg(string str);

        [DllImport("__Internal")]
        private static extern void monoscript_devmsg(string str);

        [DllImport("__Internal", EntryPoint = "monoscript_set_mono_message_fn")]
        public static extern void SetMonoMessageFn(MonoMessageDelegate del);

        public static void Msg(string format, params object[] args)
        {
            monoscript_msg(String.Format(format, args));
        }

        public static void DevMsg(string format, params object[] args)
        {
            monoscript_devmsg(String.Format(format, args));
        }
    }
}
