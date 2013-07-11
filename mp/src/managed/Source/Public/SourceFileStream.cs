using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Source.Public
{
	public class SourceFileStream : Stream
	{
		public SourceFileStream(string filename, string options, string pathid)
		{
			NativePtr = FSNative.fs_file_open(filename, options, pathid);
			// Check if something strange has happened
			if(!FSNative.fs_file_is_ok(NativePtr))
			{
				Close();
				throw new FileNotFoundException();
			}

			// Test for writable file AND writable file mode
			writable = FSNative.fs_file_writable(filename, pathid) && (options.Contains("w") || options.Contains("+"));
		}

		public override void Close()
		{
			FSNative.fs_file_close(NativePtr);
			base.Close();
		}

		#region implemented abstract members of Stream

		public override void Flush()
		{
			FSNative.fs_file_flush(NativePtr);
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			IntPtr buf = Marshal.AllocHGlobal(count);
			int retsize = FSNative.fs_file_read(buf, count, NativePtr);
			Marshal.Copy(buf, buffer, offset, retsize);
			Marshal.FreeHGlobal(buf);
			return retsize;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			FSNative.fs_file_seek(NativePtr, (int)offset, origin);
			return Position;
		}

		public override void SetLength(long value)
		{
			FSNative.fs_file_set_buffer_size(NativePtr, (uint)value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			IntPtr buf = Marshal.AllocHGlobal(count);
			Marshal.Copy(buffer, offset, buf, count);
			FSNative.fs_file_write(buf, count, NativePtr);
			Marshal.FreeHGlobal(buf);
		}

		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return writable;
			}
		}

		public override long Length
		{
			get
			{
				return FSNative.fs_file_size(NativePtr);
			}
		}

		public override long Position
		{
			get
			{
				return FSNative.fs_file_tell(NativePtr);
			}
			set
			{
				FSNative.fs_file_seek(NativePtr, (int)value, SeekOrigin.Begin);
			}
		}

		#endregion

		private IntPtr NativePtr;
		private bool writable;
	}
}

