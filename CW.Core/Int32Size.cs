/*
	$Id: Int32Size.cs 230 2011-06-23 09:36:13Z catwalkagogo@gmail.com $
*/
using System;

namespace CatWalk{
	public struct Int32Size : IEquatable<Int32Size>{
		public static readonly Int32Size Empty = new Int32Size();

		public int Width{get; private set;}
		public int Height{get; private set;}

		public Int32Size(int width, int height) : this(){
			this.Width = width;
			this.Height = height;
		}

		public bool Equals(Int32Size size){
			return this.Width.Equals(size.Width) && this.Height.Equals(size.Height);
		}

		public override bool Equals(object obj) {
			if(obj == null){
				return false;
			}else if(obj is Int32Size){
				return this.Equals((Int32Size)obj);
			}else{
				return false;
			}
		}

		public static bool operator==(Int32Size a, Int32Size b){
			return a.Equals(b);
		}

		public static bool operator!=(Int32Size a, Int32Size b){
			return !a.Equals(b);
		}

		public override int GetHashCode() {
			return this.Width.GetHashCode() ^ this.Height.GetHashCode();
		}
	}
}
