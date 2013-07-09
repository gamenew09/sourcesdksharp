using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Source.Host
{
	public enum EMonoScriptDomain
	{
		ScriptDomainServer = 0,
		ScriptDomainClient = 1,
		ScriptDomainMenu = 2,
	}

	public enum EMonoScriptMsgID
	{
		ScriptMsgIDInitialize = 0,
	}

	public delegate void MonoMessageDelegate(EMonoScriptDomain target, EMonoScriptMsgID msgid, IntPtr buffer, int length);

	static class NativeFunctions
	{
		[DllImport("__Source", EntryPoint = "set_mono_message_fn")]
		public static extern void SetMonoMessageFn(MonoMessageDelegate del);
	}
}
