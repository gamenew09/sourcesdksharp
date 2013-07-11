using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace Source.Public
{
	public class SourceFileInfo : FileSystemInfo
	{

		public SourceFileInfo(string filename, string pathid)
		{
			this.filename = filename.Replace("\\", "/");
			this.pathid = pathid;

			string[] nameparts = this.filename.Split('/');
			name = nameparts[nameparts.Length - 1];
		}

		public bool IsDirectory
		{
			get
			{
				return FSNative.fs_file_is_directory(filename, pathid);
			}
		}

		#region implemented abstract members of FileSystemInfo
		public override void Delete()
		{
			if(Exists && FSNative.fs_file_writable(filename, pathid))
				FSNative.fs_file_delete(filename, pathid);
			else
				throw new FileNotFoundException();
		}

		public override bool Exists
		{
			get
			{
				return FSNative.fs_file_exists(filename, pathid);
			}
		}

		public override string Name
		{
			get
			{
				return name;
			}
		}

		public override string FullName
		{
			get
			{
				if(fullpath == "")
				{
					StringBuilder buf = new StringBuilder(260); // 260 as in Source that is the max path length
					FSNative.fs_relative_to_full_path(filename, pathid, buf);
					fullpath = buf.ToString();
				}

				return fullpath;
			}
		}

		#endregion
		private string filename;
		private string name;
		private string pathid;
		private string fullpath;
	}
}

