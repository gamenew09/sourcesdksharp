using System;
using System.Runtime.InteropServices;

namespace Source.Public
{
	public class SourceFileFind
	{
		private int handle;
		private bool first = true;
		private bool ex = false;
		private string wildcard;
		private string pathid;

		public SourceFileFind(string wildcard)
		{
			this.wildcard = wildcard;
		}

		public SourceFileFind(string wildcard, string pathid)
		{
			this.wildcard = wildcard;
			this.pathid = pathid;
			ex = true;
		}

		public string FindNext()
		{
			IntPtr strptr;
			if(first)
			{
				if(ex)
					strptr = FSNative.fs_find_first_ex(wildcard, pathid, out handle);
				else
					strptr = FSNative.fs_find_first(wildcard, out handle);

				first = false;
			}
			else
			{
				strptr = FSNative.fs_find_next(handle);
			}

			return Marshal.PtrToStringAnsi(strptr);
		}

		public void Close()
		{
			FSNative.fs_find_close(handle);
		}
	}
}

