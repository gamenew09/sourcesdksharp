using System;
using System.Runtime.InteropServices;

namespace Source.Public.Mathlib
{
	/// <summary>
	/// Provides access to the Source engine's vector class.
	/// </summary>
	public class Vector : IEquatable<Vector>
	{
		[DllImport("__Source")]
		private static extern IntPtr vector_create_class(float x, float y, float z);

		[DllImport("__Source")]
		private static extern void vector_destroy_class(IntPtr vector);

		[DllImport("__Source")]
		private static extern void vector_set_vector(IntPtr vector, float x, float y, float z);

		[DllImport("__Source")]
		private static extern float vector_get_x(IntPtr vector);

		[DllImport("__Source")]
		private static extern float vector_get_y(IntPtr vector);

		[DllImport("__Source")]
		private static extern float vector_get_z(IntPtr vector);

		[DllImport("__Source")]
		private static extern void vector_set_x(IntPtr vector, float val);

		[DllImport("__Source")]
		private static extern void vector_set_y(IntPtr vector, float val);

		[DllImport("__Source")]
		private static extern void vector_set_z(IntPtr vector, float val);

		[DllImport("__Source")]
		private static extern float vector_length(IntPtr vector);

		[DllImport("__Source")]
		private static extern float vector_lengthsqr(IntPtr vector);

		[DllImport("__Source")]
		private static extern float vector_length2d(IntPtr vector);

		[DllImport("__Source")]
		private static extern float vector_length2dsqr(IntPtr vector);

		[DllImport("__Source")]
		private static extern float vector_dotproduct(IntPtr vector, IntPtr other);

		[DllImport("__Source")]
		private static extern IntPtr vector_crossproduct(IntPtr vector, IntPtr other);

		[DllImport("__Source")]
		private static extern float vector_distto(IntPtr vector, IntPtr other);

		[DllImport("__Source")]
		private static extern float vector_disttosqr(IntPtr vector, IntPtr other);

		[DllImport("__Source")]
		private static extern bool vector_withinaabox(IntPtr vector, IntPtr othera, IntPtr otherb);

		[DllImport("__Source")]
		private static extern IntPtr vector_normalized(IntPtr vector);

		[DllImport("__Source")]
		private static extern IntPtr vector_add(IntPtr vector, IntPtr other);

		[DllImport("__Source")]
		private static extern IntPtr vector_sub(IntPtr vector, IntPtr other);

		[DllImport("__Source")]
		private static extern IntPtr vector_mul(IntPtr vector, IntPtr other);

		[DllImport("__Source")]
		private static extern IntPtr vector_div(IntPtr vector, IntPtr other);

		[DllImport("__Source")]
		private static extern IntPtr vector_mul_float(IntPtr vector, float other);

		[DllImport("__Source")]
		private static extern IntPtr vector_div_float(IntPtr vector, float other);

		[DllImport("__Source")]
		private static extern bool vector_equal(IntPtr vector, IntPtr other);

		/// <summary>
		/// Initializes a new instance of the <see cref="Vector"/> class with the given coordinates.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public Vector(float x, float y, float z)
		{
			NativePtr = vector_create_class(x, y, z);
		}

		public Vector(IntPtr ptr)
		{
			NativePtr = ptr;
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="Vector"/> class.
		/// </summary>
		~Vector()
		{
			vector_destroy_class(NativePtr);
		}

		/// <summary>
		/// Sets the vector to the given coordinates.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public void SetVector(float x, float y, float z)
		{
			vector_set_vector(NativePtr, x, y, z);
		}

		/// <summary>
		/// Gets or sets the x coordinate.
		/// </summary>
		/// <value>
		/// The x coordinate.
		/// </value>
		public float X
		{
			get { return vector_get_x(NativePtr); }
			set { vector_set_x(NativePtr, value); }
		}

		/// <summary>
		/// Gets or sets the y coordinate.
		/// </summary>
		/// <value>
		/// The y coordinate.
		/// </value>
		public float Y
		{
			get { return vector_get_y(NativePtr); }
			set { vector_set_y(NativePtr, value); }
		}

		/// <summary>
		/// Gets or sets the z coordinate.
		/// </summary>
		/// <value>
		/// The z coordinate.
		/// </value>
		public float Z
		{
			get { return vector_get_z(NativePtr); }
			set { vector_set_z(NativePtr, value); }
		}

		/// <summary>
		/// Calculates the length of the vector.
		/// </summary>
		/// <returns>The length.</returns>
		public float Length()
		{
			return vector_length(NativePtr);
		}

		/// <summary>
		/// Calculates the square of the length of the <see cref="Vector"/>.
		/// This is cheaper than <see cref="Length" /> as square root is expensive so should be used when possible.
		/// </summary>
		/// <returns>The square of the length.</returns>
		public float LengthSquared()
		{
			return vector_lengthsqr(NativePtr);
		}

		/// <summary>
		/// Calculates the length of the <see cref="X"/> and <see cref="Y"/> properties of the <see cref="Vector"/>.
		/// </summary>
		/// <returns>The length of the <see cref="X"/> and <see cref="Y"/> properties.</returns>
		public float Length2D()
		{
			return vector_length2d(NativePtr);
		}

		/// <summary>
		/// Calculates the square of the length of the <see cref="X"/> and <see cref="Y"/> properties of the vector.
		/// This is cheaper than <see cref="Length2D" /> as square root is expensive so should be used when possible.
		/// </summary>
		/// <returns>The square of the length of the <see cref="X"/> and <see cref="Y"/> properties.</returns>
		public float Length2DSquared()
		{
			return vector_length2dsqr(NativePtr);
		}

		/// <summary>
		/// Calculates the dot product of two <see cref="Vector"/>s.
		/// </summary>
		/// <param name="other">The other <see cref="Vector"/>.</param>
		/// <returns>The dot product of the two <see cref="Vector"/>s.</returns>
		public float Dot(Vector other)
		{
			return vector_dotproduct(NativePtr, other.NativePtr);
		}

		/// <summary>
		/// Calculates the cross product of two <see cref="Vector"/>s.
		/// </summary>
		/// <param name="other">The other <see cref="Vector"/>.</param>
		/// <returns>The cross product of the two <see cref="Vector"/>s.</returns>
		public Vector Cross(Vector other)
		{
			IntPtr newVector = vector_crossproduct(NativePtr, other.NativePtr);
			return new Vector(newVector);
		}

		/// <summary>
		/// Calculates the distance between two <see cref="Vector"/>s.
		/// </summary>
		/// <param name="other">The other <see cref="Vector"/>.</param>
		/// <returns>The distance between the two <see cref="Vector"/>s.</returns>
		public float Distance(Vector other)
		{
			return vector_distto(NativePtr, other.NativePtr);
		}

		/// <summary>
		/// Calculates the square of the distance between two <see cref="Vector"/>s.
		/// This is cheaper than <see cref="Distance" /> as as square root is expensive so should be used when possible.
		/// </summary>
		/// <param name="other">The other <see cref="Vector"/>.</param>
		/// <returns>The distance between the two <see cref="Vector"/>s.</returns>
		public float DistanceSquared(Vector other)
		{
			return vector_disttosqr(NativePtr, other.NativePtr);
		}

		/// <summary>
		/// Checks if the vector is within the box defined by the <paramref name="min"/> and <paramref name="max"/> vectors.
		/// </summary>
		/// <param name="min">The minimum <see cref="Vector"/>.</param>
		/// <param name="max">The maximum <see cref="Vector"/>.</param>
		/// <returns>Whether or not the <see cref="Vector"/> instance lies within the box.</returns>
		public bool WithinAABox(Vector min, Vector max)
		{
			return vector_withinaabox(NativePtr, min.NativePtr, max.NativePtr);
		}

		/// <summary>
		/// Returns the normalized form of the <see cref="Vector"/>.
		/// </summary>
		/// <returns>The normalized form of the <see cref="Vector"/> instance.</returns>
		public Vector Normalized()
		{
			IntPtr newVector = vector_normalized(NativePtr);
			return new Vector(newVector);
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return String.Format("[Vector: X:{0} Y:{1} Z:{2}]", X, Y, Z);
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
			return (obj is Vector) ? this == (Vector)obj : false;
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		public bool Equals(Vector other)
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
		/// Compares two <see cref="Vector"/>s, the result specifies if their <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties are the same.
		/// </summary>
		/// <param name="value1">An <see cref="Vector"/> to compare.</param>
		/// <param name="value2">A <see cref="Vector"/> to compare.</param>
		/// <returns><c>true</c> if the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties are the same, <c>false</c> otherwise.</returns>
		public static bool operator ==(Vector value1, Vector value2)
		{
			return vector_equal(value1.NativePtr, value2.NativePtr);
		}

		/// <summary>
		/// Compares two <see cref="Vector"/>s, the result specifies if any of their <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties are different.
		/// </summary>
		/// <param name="value1">An <see cref="Vector"/> to compare.</param>
		/// <param name="value2">A <see cref="Vector"/> to compare.</param>
		/// <returns><c>false</c> if the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties are the same, <c>true</c> otherwise.</returns>
		public static bool operator !=(Vector value1, Vector value2)
		{
			return !(value1 == value2);
		}

		/// <summary>
		/// Adds two <see cref="Vector"/>s.
		/// </summary>
		/// <param name="value1">A <see cref="Vector"/> to add.</param>
		/// <param name="value2">A <see cref="Vector"/> to add.</param>
		/// <returns>The sum of the two <see cref="Vector"/>s.</returns>
		public static Vector operator +(Vector value1, Vector value2)
		{
			IntPtr newVector = vector_add(value1.NativePtr, value2.NativePtr);
			return new Vector(newVector);
		}

		/// <summary>
		/// Subtracts <paramref name="value2"/> from <paramref name="value1"/> and returns the result./>/>
		/// </summary>
		/// <param name="value1">The <see cref="Vector"/> to be subtracted from.</param>
		/// <param name="value2">The <see cref="Vector"/> to subtract.</param>
		/// <returns><paramref name="value1"/> minus <paramref name="value2"/>.</returns>
		public static Vector operator -(Vector value1, Vector value2)
		{
			IntPtr newVector = vector_sub(value1.NativePtr, value2.NativePtr);
			return new Vector(newVector);
		}

		/// <summary>
		/// Returns the inverse of the <see cref="Vector"/>.
		/// </summary>
		/// <param name="value1">The <see cref="Vector"/> to negate.</param>
		/// <returns>The inverse of the <see cref="Vector"/>.</returns>
		public static Vector operator -(Vector value1)
		{
			return new Vector(-value1.X, -value1.Y, -value1.Z);
		}

		/// <summary>
		/// Mulitplies the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties.
		/// </summary>
		/// <param name="value1">A <see cref="Vector"/> to multiply.</param>
		/// <param name="value2">A <see cref="Vector"/> to multiply.</param>
		/// <returns><paramref name="value1"/> multiplied by <paramref name="value2"/>.</returns>
		public static Vector operator *(Vector value1, Vector value2)
		{
			IntPtr newVector = vector_mul(value1.NativePtr, value2.NativePtr);
			return new Vector(newVector);
		}

		/// <summary>
		/// Mulitplies the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties.
		/// </summary>
		/// <param name="value1">The <see cref="Vector"/> to multiply.</param>
		/// <param name="multiplier">The value to multiply by.</param>
		/// <returns><paramref name="value1"/> multiplied by <paramref name="multiplier"/>.</returns>
		public static Vector operator *(Vector value1, float multiplier)
		{
			IntPtr newVector = vector_mul_float(value1.NativePtr, multiplier);
			return new Vector(newVector);
		}

		/// <summary>
		/// Divides the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties by <paramref name="divisor"/>.
		/// </summary>
		/// <param name="value1">A <see cref="Vector"/> to divide.</param>
		/// <param name="divisor">The <see cref="Vector"/> to divide by.</param>
		/// <returns><paramref name="value1"/> divided by <paramref name="divisor"/>.</returns>
		public static Vector operator /(Vector value1, Vector divisor)
		{
			IntPtr newVector = vector_div(value1.NativePtr, divisor.NativePtr);
			return new Vector(newVector);
		}

		/// <summary>
		/// Divides the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties by <paramref name="divisor"/>.
		/// </summary>
		/// <param name="value1">A <see cref="Vector"/> to divide.</param>
		/// <param name="divisor">The number to divide by.</param>
		/// <returns><paramref name="value1"/> divided by <paramref name="divisor"/>.</returns>
		public static Vector operator /(Vector value1, float divisor)
		{
			IntPtr newVector = vector_div_float(value1.NativePtr, divisor);
			return new Vector(newVector);
		}

		/// <summary>
		/// Returns an <see cref="Angle"/> with the same <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties as the <see cref="Vector"/>.
		/// </summary>
		/// <param name="value1">The <see cref="Vector"/> to convert.</param>
		/// <returns>An <see cref="Angle"/> with the same <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> properties as <paramref name="value1"/>.</returns>
		public static implicit operator Angle(Vector value1)
		{
			return new Angle(value1.X, value1.Y, value1.Z);
		}

		/// <summary>
		/// The pointer to the native object.
		/// </summary>
		public IntPtr NativePtr;
	}
}