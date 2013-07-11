using System;
using System.Runtime.InteropServices;

namespace Source.Public.Tier0
{
	public static class CommandLine
	{
		[DllImport("__Source")]
		private static extern IntPtr commandline_get();
		
		[DllImport("__Source")]
		private static extern IntPtr commandline_check_parm(string parm, out IntPtr value);

		[DllImport("__Source")]
		private static extern int commandline_parm_count();

		[DllImport("__Source")]
		private static extern int commandline_find_parm(string parm);

		[DllImport("__Source")]
		private static extern IntPtr commandline_get_parm(int index);

		public static string CommandLineString
		{
			get
			{
				return Marshal.PtrToStringAnsi(commandline_get());
			}
		}

		public static string CheckParm(string parm, out string value)
		{
			IntPtr valueptr;
			IntPtr parmptr = commandline_check_parm(parm, out valueptr);
			value = "";

			if(valueptr != IntPtr.Zero)
				value = Marshal.PtrToStringAnsi(valueptr);

			if(parmptr != IntPtr.Zero)
				return Marshal.PtrToStringAnsi(parmptr);

			return "";
		}

		public static int ParmCount
		{
			get
			{
				return commandline_parm_count();
			}
		}

		public static int FindParm(string parm)
		{
			return commandline_find_parm(parm);
		}

		public static string GetParm(int index)
		{
			IntPtr valptr = commandline_get_parm(index);
			return Marshal.PtrToStringAnsi(valptr);
		}
	}
}

