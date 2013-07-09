using System;
using System.Runtime.InteropServices;

namespace Source.Public.Mathlib
{
	/// <summary>
	/// Provides access to the Source engine's QAngle class.
	/// </summary>
	public class Angle : IEquatable<Angle>
	{
		[DllImport("__Source")]
		private static extern IntPtr qangle_create_class(float x, float y, float z);

		[DllImport("__Source")]
		private static extern void qangle_destroy_class(IntPtr qangle);

		[DllImport("__Source")]
		private static extern void qangle_set_qangle(IntPtr qangle, float x, float y, float z);

		[DllImport("__Source")]
		private static extern float qangle_get_x(IntPtr qangle);

		[DllImport("__Source")]
		private static extern float qangle_get_y(IntPtr qangle);

		[DllImport("__Source")]
		private static extern float qangle_get_z(IntPtr qangle);

		[DllImport("__Source")]
		private static extern float qangle_length(IntPtr qangle);

		[DllImport("__Source")]
		private static extern float qangle_lengthsqr(IntPtr qangle);

		[DllImport("__Source")]
		private static extern IntPtr qangle_add(IntPtr qangle, IntPtr other);

		[DllImport("__Source")]
		private static extern IntPtr qangle_sub(IntPtr qangle, IntPtr other);

		[DllImport("__Source")]
		private static extern IntPtr qangle_mul_float(IntPtr qangle, float other);

		[DllImport("__Source")]
		private static extern IntPtr qangle_div_float(IntPtr qangle, float other);

		[DllImport("__Source")]
		private static extern bool qangle_equal(IntPtr qangle, IntPtr other);

		/// <summary>
		/// Initializes a new instance of the <see cref="Angle"/> class with the given angles in degrees.
		/// </summary>
		/// <param name="x">The x angle.</param>
		/// <param name="y">The y angle.</param>
		/// <param name="z">The z angle.</param>
		public Angle(float x, float y, float z)
		{
			NativePtr = qangle_create_class(x, y, z);
		}

		public Angle(IntPtr ptr)
		{
			NativePtr = ptr;
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="Angle"/> class.
		/// </summary>
		~Angle()
		{
			qangle_destroy_class(NativePtr);
		}

		/// <summary>
		/// Sets the angle to the given angles in degrees.
		/// </summary>
		/// <param name="x">The x angle.</param>
		/// <param name="y">The y angle.</param>
		/// <param name="z">The z angle.</param>
		public void SetAngle(float x, float y, float z)
		{
			qangle_set_qangle(NativePtr, x, y, z);
		}

		/// <summary>
		/// Gets or sets the x angle in degrees.
		/// </summary>
		/// <value>
		/// The x angle.
		/// </value>
		public float X
		{
			get { return qangle_get_x(NativePtr); }
			set { qangle_set_qangle(NativePtr, value, Y, Z); }
		}

		/// <summary>
		/// Gets or sets the y angle in degrees.
		/// </summary>
		/// <value>
		/// The y angle.
		/// </value>
		public float Y
		{
			get { return qangle_get_y(NativePtr); }
			set { qangle_set_qangle(NativePtr, X, value, Z); }
		}

		/// <summary>
		/// Gets or sets the z angle in degrees.
		/// </summary>
		/// <value>
		/// The z angle.
		/// </value>
		public float Z
		{
			get { return qangle_get_z(NativePtr); }
			set { qangle_set_qangle(NativePtr, X, Y, value); }
		}

		/// <summary>
		/// Calculates the length of the angle.
		/// </summary>
		/// <returns>The length of the angle.</returns>
		public float Length()
		{
			return qangle_length(NativePtr);
		}

		/// <summary>
		/// Calculates the square of the length of the angle.
		/// This is cheaper than <see cref="Angle">Length</see> as square root is expensive so should be used when possible.
		/// </summary>
		/// <returns>The square of the length of the angle.</returns>
		public float LengthSquared()
		{
			return qangle_lengthsqr(NativePtr);
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return String.Format("[Angle: X:{0} Y:{1} Z:{2}]", X, Y, Z);
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
			return (obj is Angle) ? this == (Angle)obj : false;
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		public bool Equals(Angle other)
		{
			return this == other;
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode()
		{
			return (int)(X + Y + Z);
		}

		/// <summary>
		/// Compares two <see cref="Angle"/>s, the result specifies if their <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties are the same.
		/// </summary>
		/// <param name="value1">An <see cref="Angle"/> to compare.</param>
		/// <param name="value2">An <see cref="Angle"/> to compare.</param>
		/// <returns><c>true</c> if the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties are the same, <c>false</c> otherwise.</returns>
		public static bool operator ==(Angle value1, Angle value2)
		{
			return qangle_equal(value1.NativePtr, value2.NativePtr);
		}

		/// <summary>
		/// Compares two <see cref="Angle"/>s, the result specifies if their <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties are different.
		/// </summary>
		/// <param name="value1">An <see cref="Angle"/> to compare.</param>
		/// <param name="value2">An <see cref="Angle"/> to compare.</param>
		/// <returns><c>false</c> if the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties are the same, <c>true</c> otherwise.</returns>
		public static bool operator !=(Angle value1, Angle value2)
		{
			return !(value1 == value2);
		}

		/// <summary>
		/// Adds two <see cref="Angle"/>s.
		/// </summary>
		/// <param name="value1">An <see cref="Angle"/> to add.</param>
		/// <param name="value2">An <see cref="Angle"/> to add.</param>
		/// <returns>The sum of the two <see cref="Angle"/>s.</returns>
		public static Angle operator +(Angle value1, Angle value2)
		{
			IntPtr newAngle = qangle_add(value1.NativePtr, value2.NativePtr);
			return new Angle(newAngle);
		}

		/// <summary>
		/// Subtracts <paramref name="value2"/> from <paramref name="value1"/> and returns the result./>/>
		/// </summary>
		/// <param name="value1">The <see cref="Angle"/> to be subtracted from.</param>
		/// <param name="value2">The <see cref="Angle"/> to subtract.</param>
		/// <returns><paramref name="value1"/> minus <paramref name="value2"/>.</returns>
		public static Angle operator -(Angle value1, Angle value2)
		{
			IntPtr newAngle = qangle_sub(value1.NativePtr, value2.NativePtr);
			return new Angle(newAngle);
		}

		/// <summary>
		/// Returns the inverse of the <see cref="Angle"/>.
		/// </summary>
		/// <param name="value1">The <see cref="Angle"/> to negate.</param>
		/// <returns>The inverse of the <see cref="Angle"/>.</returns>
		public static Angle operator -(Angle value1)
		{
			return new Angle(-value1.X, -value1.Y, -value1.Z);
		}

		/// <summary>
		/// Mulitplies the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties.
		/// </summary>
		/// <param name="value1">The <see cref="Angle"/> to multiply.</param>
		/// <param name="multiplier">The value to multiply by.</param>
		/// <returns><paramref name="value1"/> multiplied by <paramref name="multiplier"/>.</returns>
		public static Angle operator *(Angle value1, float multiplier)
		{
			IntPtr newAngle = qangle_mul_float(value1.NativePtr, multiplier);
			return new Angle(newAngle);
		}

		/// <summary>
		/// Divides the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties.
		/// </summary>
		/// <param name="value1">The <see cref="Angle"/> to divide.</param>
		/// <param name="divisor">The value to divide by.</param>
		/// <returns><paramref name="value1"/> divided by <paramref name="divisor"/>.</returns>
		public static Angle operator /(Angle value1, float divisor)
		{
			IntPtr newAngle = qangle_div_float(value1.NativePtr, divisor);
			return new Angle(newAngle);
		}

		/// <summary>
		/// Returns a <see cref="Vector"/> with the same <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties as the <see cref="Angle"/>.
		/// </summary>
		/// <param name="value1">The <see cref="Angle"/> to convert.</param>
		/// <returns>A <see cref="Vector"/> with the same <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties as <paramref name="value1"/>.</returns>
		public static implicit operator Vector(Angle value1)
		{
			return new Vector(value1.X, value1.Y, value1.Z);
		}

		/// <summary>
		/// The pointer to the native object.
		/// </summary>
		public IntPtr NativePtr;
	}
}
