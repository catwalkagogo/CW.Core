/*
	$Id: Int32Point.cs 230 2011-06-23 09:36:13Z catwalkagogo@gmail.com $
*/
using System;
using System.Net;

namespace CW{
	public struct Int32Point : IEquatable<Int32Point>{
		public static readonly Int32Point Empty = new Int32Point();

		public int X{get; private set;}
		public int Y{get; private set;}

		public Int32Point(int x, int y) : this(){
			this.X = x;
			this.Y = y;
		}

		public bool Equals(Int32Point point){
			return this.X.Equals(point.X) && this.Y.Equals(point.Y);
		}

		public override bool Equals(object obj) {
			if(obj == null){
				return false;
			}else if(obj is Int32Point){
				return this.Equals((Int32Point)obj);
			}else{
				return false;
			}
		}

		public static bool operator==(Int32Point a, Int32Point b){
			return a.Equals(b);
		}

		public static bool operator!=(Int32Point a, Int32Point b){
			return !a.Equals(b);
		}

		public override int GetHashCode() {
			return this.X.GetHashCode() ^ this.Y.GetHashCode();
		}

		#region operators

		public static Int32Point operator+(Int32Point a, Int32Point b){
			return new Int32Point(a.X + b.X, a.Y + b.Y);
		}

		public static Int32Point operator-(Int32Point a, Int32Point b){
			return new Int32Point(a.X - b.X, a.Y - b.Y);
		}

		public static Int32Point operator*(Int32Point a, Int32Point b){
			return new Int32Point(a.X * b.X, a.Y * b.Y);
		}

		public static Int32Point operator/(Int32Point a, Int32Point b){
			return new Int32Point(a.X / b.X, a.Y / b.Y);
		}

		public static Int32Point operator%(Int32Point a, Int32Point b){
			return new Int32Point(a.X % b.X, a.Y % b.Y);
		}

		#endregion

		#region operators with vector

		public static Int32Point operator+(Int32Point a, Int32Vector b){
			return new Int32Point(a.X + b.X, a.Y + b.Y);
		}

		public static Int32Point operator-(Int32Point a, Int32Vector b){
			return new Int32Point(a.X - b.X, a.Y - b.Y);
		}

		public static Int32Point operator*(Int32Point a, Int32Vector b){
			return new Int32Point(a.X * b.X, a.Y * b.Y);
		}

		public static Int32Point operator/(Int32Point a, Int32Vector b){
			return new Int32Point(a.X / b.X, a.Y / b.Y);
		}

		public static Int32Point operator%(Int32Point a, Int32Vector b){
			return new Int32Point(a.X % b.X, a.Y % b.Y);
		}
		#endregion
	}
}
