using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace Source
{
	internal static class FSNative
	{
		[DllImport("__Source")]
		internal static extern void fs_relative_to_full_path(string path, string pathid, StringBuilder outstr);

		[DllImport("__Source")]
		internal static extern void fs_add_search_path(string path, string pathid);

		[DllImport("__Source")]
		internal static extern void fs_remove_search_path(string path, string pathid);

		[DllImport("__Source")]
		internal static extern IntPtr fs_file_open(string filename, string options, string pathid);

		[DllImport("__Source")]
		internal static extern bool fs_file_exists(string filename, string pathid);

		[DllImport("__Source")]
		internal static extern bool fs_file_writable(string filename, string pathid);

		[DllImport("__Source")]
		internal static extern void fs_file_close(IntPtr file);

		[DllImport("__Source")]
		internal static extern int fs_file_read(IntPtr outbuf, int size, IntPtr file);

		[DllImport("__Source")]
		internal static extern int fs_file_write(IntPtr inbuf, int size, IntPtr file);

		[DllImport("__Source")]
		internal static extern void fs_file_seek(IntPtr file, int pos, SeekOrigin origin);

		[DllImport("__Source")]
		internal static extern uint fs_file_tell(IntPtr file);

		[DllImport("__Source")]
		internal static extern uint fs_file_size(IntPtr file);

		[DllImport("__Source")]
		internal static extern void fs_file_flush(IntPtr file);

		[DllImport("__Source")]
		internal static extern void fs_file_delete(string filename, string pathid);

		[DllImport("__Source")]
		internal static extern void fs_file_set_buffer_size(IntPtr file, uint size);

		[DllImport("__Source")]
		internal static extern bool fs_file_is_ok(IntPtr file);
	}
}

