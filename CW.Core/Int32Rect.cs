/*
	$Id: Int32Rect.cs 316 2013-12-26 10:16:12Z catwalkagogo@gmail.com $
*/
using System;

namespace CW{
//#if SILVERLIGHT
	public struct Int32Rect : IEquatable<Int32Rect>{
		public int X{get; private set;}
		public int Y{get; private set;}
		public int Width{get; private set;}
		public int Height{get; private set;}
		public int Left{get{return this.X;}}
		public int Top{get{return this.Y;}}
		public int Right{get{return this.X + this.Width;}}
		public int Bottom{get{return this.Y + this.Height;}}

		public Int32Rect(int x, int y, int width, int height) : this(){
			if(width < 0){
				throw new ArgumentOutOfRangeException("width");
			}
			if(height < 0){
				throw new ArgumentOutOfRangeException("height");
			}
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		#region Equals

		public bool Equals(Int32Rect rect){
			return this.X.Equals(rect.X) && this.Y.Equals(rect.Y) && this.Width.Equals(rect.Width) && this.Height.Equals(this.Height);
		}

		public override bool Equals(object obj) {
			if(obj == null){
				return false;
			}else if(obj is Int32Rect){
				return this.Equals((Int32Rect)obj);
			}else{
				return false;
			}
		}

		public override int GetHashCode() {
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Width.GetHashCode() ^ this.Height.GetHashCode();
		}

		#endregion

		#region Operators

		public static bool operator ==(Int32Rect a, Int32Rect b) {
			return a.Equals(b);
		}

		public static bool operator !=(Int32Rect a, Int32Rect b) {
			return !a.Equals(b);
		}

		public static Int32Rect operator +(Int32Rect a, Int32Vector v) {
			return new Int32Rect(a.Left + v.X, a.Top + v.Y, a.Width, a.Height);
		}

		public static Int32Rect operator +(Int32Vector v, Int32Rect a) {
			return new Int32Rect(a.Left + v.X, a.Top + v.Y, a.Width, a.Height);
		}

		#endregion

		#region Contains

		public bool Contains(Int32Point pt) {
			return this.ContainsInternal(pt.X, pt.Y);
		}

		public bool Contains(int x, int y) {
			return this.ContainsInternal(x, y);
		}

		public bool Contains(Int32Rect rect) {
			return (this.X <= rect.X &&
					this.Y <= rect.Y &&
					this.X + this.Width >= rect.X + rect.Width &&
					this.Y + this.Height >= rect.Y + rect.Height);
		} 

		#endregion

		#region Intersect

		public bool IsIntersect(Int32Rect rect) {
			return !(this.Left > rect.Right || this.Right < rect.Left || this.Top > rect.Bottom || this.Bottom < rect.Top);
		}

		public Int32Rect Intersect(Int32Rect rect){
			if(this.Left > rect.Right || this.Right < rect.Left || this.Top > rect.Bottom || this.Bottom < rect.Top){
				return Empty;
			}else{
				var left = Math.Max(this.Left, rect.Left);
				var top = Math.Max(this.Top, rect.Top);
				var right = Math.Min(this.Right, rect.Right);
				var bottom = Math.Min(this.Bottom, rect.Bottom);
				var rect2 = new Int32Rect(left, top, bottom - top, right -left);
				return rect2;
			}
		}

		#endregion

		#region Property
		public long Area {
			get {
				return this.Width * this.Height;
			}
		}

		public static readonly Int32Rect Empty = new Int32Rect();

		public bool IsEmpty {
			get {
				return Empty.Equals(this);
			}
		}

		#endregion	

		public Int32Rect Union(Int32Rect rect) {
			if(IsEmpty) {
				return rect;
			} else if(!rect.IsEmpty) {
				var left = (int)Math.Min(Left, rect.Left);
				var top = (int)Math.Min(Top, rect.Top);

				var maxRight = (int)Math.Max(Right, rect.Right);
				var width = (int)Math.Max(maxRight - left, 0);

				var maxBottom = (int)Math.Max(Bottom, rect.Bottom);
				var height = (int)Math.Max(maxBottom - top, 0);

				var x = left;
				var y = top;
				return new Int32Rect(x, y, width, height);
			} else {
				return rect;
			}
		}

		private bool ContainsInternal(int x, int y) {
			return ((x >= this.X) && (x - this.Width <= this.X) &&
					(y >= this.Y) && (y - this.Height <= this.Y));
		}
	}
//#endif
}
