using System;
using System.Runtime.InteropServices;

namespace Source.Public.Tier0
{
	/// <summary>
	/// Provides access to the Source engine's Tier0 logging functions.
	/// </summary>
	public static class Debug
	{
		[DllImport("__Source")]
		private static extern void dbg_msg(string fmt);

		[DllImport("__Source")]
		private static extern void dbg_warning(string fmt);

		[DllImport("__Source")]
		private static extern void dbg_error(string fmt);

		[DllImport("__Source")]
		private static extern void dbg_devmsg(string fmt);

		[DllImport("__Source")]
		private static extern void dbg_devwarning(string fmt);

		[DllImport("__Source")]
		private static extern void dbg_colormsg(IntPtr color, string fmt);

		/// <summary>
		/// Standard console print.
		/// </summary>
		/// <param name="fmt">The format string, refer to <see cref="System.String.Format(string, object[])" /> for documentation.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		public static void Msg(string fmt, params object[] args)
		{
			var print = String.Format(fmt, args);
			dbg_msg(print);
		}

		/// <summary>
		/// Warning console print.
		/// Use when something has gone wrong, but is non critical.
		/// </summary>
		/// <param name="fmt">The format string, refer to <see cref="System.String.Format(string, object[])" /> for documentation.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		public static void Warning(string fmt, params object[] args)
		{
			var print = String.Format(fmt, args);
			dbg_warning(print);
		}

		/// <summary>
		/// Error console print.
		/// Use when something critical has failed. Will halt application.
		/// </summary>
		/// <param name="fmt">The format string, refer to <see cref="System.String.Format(string, object[])" /> for documentation.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		public static void Error(string fmt, params object[] args)
		{
			var print = String.Format(fmt, args);
			dbg_error(print);
		}

		/// <summary>
		/// Developer console print.
		/// Messages printed with this will only be displayed when in dev mode (started with -dev).
		/// </summary>
		/// <param name="fmt">The format string, refer to <see cref="System.String.Format(string, object[])" /> for documentation.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		public static void DevMsg(string fmt, params object[] args)
		{
			var print = String.Format(fmt, args);
			dbg_devmsg(print);
		}

		/// <summary>
		/// Developer warning console print.
		/// Use when something has gone wrong, but is non critical.
		/// Messages printed with this will only be displayed when in dev mode (started with -dev).
		/// </summary>
		/// <param name="fmt">The format string, refer to <see cref="System.String.Format(string, object[])" /> for documentation.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		public static void DevWarning(string fmt, params object[] args)
		{
			var print = String.Format(fmt, args);
			dbg_devwarning(print);
		}

		/// <summary>
		/// Colored console print.
		/// </summary>
		/// <param name="col">The color to output with.</param>
		/// <param name="fmt">The format string, refer to <see cref="System.String.Format(string, object[])" /> for documentation.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		public static void ColorMsg(Color col, string fmt, params object[] args)
		{
			var print = String.Format(fmt, args);
			dbg_colormsg(col.NativePtr, print);
		}
	}
}
