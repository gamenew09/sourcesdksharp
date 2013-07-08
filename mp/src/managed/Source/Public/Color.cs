using System;
using System.Runtime.InteropServices;

namespace Source.Public
{
	/// <summary>
	/// Provides access to the Source engine's color class.
	/// </summary>
	public class Color : IEquatable<Color>
	{
		[DllImport("__Source")]
		private static extern IntPtr color_create_class(int r, int g, int b, int a);

		[DllImport("__Source")]
		private static extern void color_destroy_class(IntPtr color);

		[DllImport("__Source")]
		private static extern void color_set_color(IntPtr color, int r, int g, int b, int a);

		[DllImport("__Source")]
		private static extern int color_get_r(IntPtr color);

		[DllImport("__Source")]
		private static extern int color_get_g(IntPtr color);

		[DllImport("__Source")]
		private static extern int color_get_b(IntPtr color);

		[DllImport("__Source")]
		private static extern int color_get_a(IntPtr color);

		public Color(IntPtr nativePtr)
		{
			NativePtr = nativePtr;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Color"/> class with the given RGBA values.
		/// </summary>
		/// <param name="r">The red channel value.</param>
		/// <param name="g">The green channel value.</param>
		/// <param name="b">The blue channel value.</param>
		/// <param name="a">The alpha channel value.</param>
		public Color(byte r, byte g, byte b, byte a = 255)
		{
			NativePtr = color_create_class(r, g, b, a);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Color"/> class with the given ARGB value.
		/// </summary>
		/// <param name="packedValue">The packed color in ARGB.</param>
		public Color(uint packedValue)
		{
			var alpha = (byte)(packedValue >> 24 & 0xFF);
			var red = (byte)(packedValue >> 16 & 0xFF);
			var green = (byte)(packedValue >> 8 & 0xFF);
			var blue = (byte)(packedValue & 0xFF);

			NativePtr = color_create_class(red, green, blue, alpha);
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="Color"/> class.
		/// </summary>
		~Color()
		{
			color_destroy_class(NativePtr);
		}

		/// <summary>
		/// Sets the <see cref="Color"/> to the given RGBA values.
		/// </summary>
		/// <param name="r">The red channel.</param>
		/// <param name="g">The green channel.</param>
		/// <param name="b">The blue channel.</param>
		/// <param name="a">The alpha channel.</param>
		public void SetColor(byte r, byte g, byte b, byte a = 255)
		{
			color_set_color(NativePtr, r, g, b, a);
		}

		/// <summary>
		/// Sets the <see cref="Color"/> to the given ARGB value.
		/// </summary>
		/// <param name="packedValue">The packed color in ARGB.</param>
		public void SetColor(int packedValue)
		{
			var alpha = (byte)(packedValue >> 24 & 0xFF);
			var red = (byte)(packedValue >> 16 & 0xFF);
			var green = (byte)(packedValue >> 8 & 0xFF);
			var blue = (byte)(packedValue & 0xFF);

			SetColor(red, green, blue, alpha);
		}

		/// <summary>
		/// Gets or sets the red channel.
		/// </summary>
		/// <value>
		/// The red channel.
		/// </value>
		public byte R
		{
			get { return (byte)color_get_r(NativePtr); }
			set { SetColor(value, G, B, A); }
		}

		/// <summary>
		/// Gets or sets the green channel.
		/// </summary>
		/// <value>
		/// The green channel.
		/// </value>
		public byte G
		{
			get { return (byte)color_get_g(NativePtr); }
			set { SetColor(R, value, B, A); }
		}

		/// <summary>
		/// Gets or sets the green channel.
		/// </summary>
		/// <value>
		/// The blue channel.
		/// </value>
		public byte B
		{
			get { return (byte)color_get_b(NativePtr); }
			set { SetColor(R, G, value, A); }
		}

		/// <summary>
		/// Gets or sets the alpha channel.
		/// </summary>
		/// <value>
		/// The alpha channel.
		/// </value>
		public byte A
		{
			get { return (byte)color_get_a(NativePtr); }
			set { SetColor(R, G, B, value); }
		}

		/// <summary>
		/// Gets or sets the packed color in ARGB format.
		/// </summary>
		/// <value>
		/// The packed color, ARGB format.
		/// </value>
		public int PackedValue
		{
			get { return (A << 24) + (R << 16) + (G << 8) + B; }
			set { SetColor(value); }
		}

		/// <summary>
		/// Compares two colors, the result specifies if the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> properties are the same.
		/// </summary>
		/// <param name="a">A color to compare.</param>
		/// <param name="b">A color to compare.</param>
		/// <returns><c>true</c> if the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> properties are the same, <c>false</c> otherwise.</returns>
		public static bool operator ==(Color a, Color b)
		{
			return a.PackedValue == b.PackedValue;
		}

		/// <summary>
		/// Compares two colors, the specifies if any of the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> properties are different.
		/// </summary>
		/// <param name="a">A <see cref="Color"/> to compare.</param>
		/// <param name="b">A <see cref="Color"/> to compare.</param>
		/// <returns><c>false</c> if the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> properties are the same, <c>true</c> otherwise.</returns>
		public static bool operator !=(Color a, Color b)
		{
			return !(a == b);
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			return ((obj is Color) && Equals((Color)obj));
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode()
		{
			return PackedValue.GetHashCode();
		}

		/// <summary>
		/// Multiplies the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> properties by the same value.
		/// </summary>
		/// <param name="col">The <see cref="Color"/> to multiply.</param>
		/// <param name="scale">The amount each channel should be multiplied to.</param>
		/// <returns>The <see cref="Color"/> multipled, each channel clamped between <c>0</c> and <c>255</c>.</returns>
		public static Color operator *(Color col, float scale)
		{
			var red = (byte)Math.Min(col.R * scale, 255.0f);
			var green = (byte)Math.Min(col.G * scale, 255.0f);
			var blue = (byte)Math.Min(col.B * scale, 255.0f);
			var alpha = (byte)Math.Min(col.A * scale, 255.0f);

			return new Color(red, green, blue, alpha);
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return String.Format("[Color: R={0}, G={1}, B={2}, A={3}, PackedValue={4}]", R, G, B, A, PackedValue);
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.
		/// </returns>
		public bool Equals(Color other)
		{
			return PackedValue == other.PackedValue;
		}

		/// <summary>
		/// The pointer to the native object.
		/// </summary>
		public IntPtr NativePtr;
	}
}
