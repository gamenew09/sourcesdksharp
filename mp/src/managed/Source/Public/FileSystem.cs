using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Source.Public
{
	static public class FileSystem
	{
		[DllImport("__Source")]
		private static extern void fs_relative_to_full_path(string path, string pathid, StringBuilder outstr);

		[DllImport("__Source")]
		private static extern void fs_add_search_path(string path, string pathid);

		[DllImport("__Source")]
		private static extern void fs_remove_search_path(string path, string pathid);

		public static string RelativePathToFullPath(string path, string pathid)
		{
			StringBuilder buf = new StringBuilder(260); // 260 as in Source that is the max path length
			fs_relative_to_full_path(path, pathid, buf);
			return buf.ToString();
		}

		public static void AddSearchPath(string path, string pathid)
		{
			fs_add_search_path(path, pathid);
		}

		public static void RemoveSearchPath(string path, string pathid)
		{
			fs_remove_search_path(path, pathid);
		}
	}
}

