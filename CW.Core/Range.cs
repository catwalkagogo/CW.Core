/*
 $Id: Range.cs 212 2011-04-26 10:26:50Z cs6m7y@bma.biglobe.ne.jp $
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatWalk{
	public struct Range<T> : IEquatable<Range<T>>{
		private T lowerBound;
		private T upperBound;
		private bool isExcludingLowerBound;
		private bool isExcludingUpperBound;
		private IComparer<T> comparer;

		public Range(T lower, T upper) : this(lower, upper, false, false, Comparer<T>.Default){}
		public Range(T lower, T upper, bool excludeLower, bool excludeUpper) : this(lower, upper, false, false, Comparer<T>.Default){}
		public Range(T lower, T upper, bool excludeLower, bool excludeUpper, IComparer<T> comparer){
			this.lowerBound = lower;
			this.upperBound = upper;
			this.isExcludingLowerBound = excludeLower;
			this.isExcludingUpperBound = excludeUpper;
			this.comparer = comparer;
		}

		public bool Contains(T value){
			bool lower = 
				(this.lowerBound == null) ? true :
				(this.isExcludingLowerBound) ? this.comparer.Compare(this.lowerBound, value) < 0 : this.comparer.Compare(this.lowerBound, value) <= 0;
			bool upper =
				(this.upperBound == null) ? true :
				(this.isExcludingUpperBound) ? this.comparer.Compare(this.upperBound, value) > 0 : this.comparer.Compare(this.upperBound, value) >= 0;
			return lower && upper;
		}

		public T LowerBound{
			get{
				return this.lowerBound;
			}
			set{
				this.lowerBound = value;
			}
		}

		public T UpperBound{
			get{
				return this.upperBound;
			}
			set{
				this.upperBound = value;
			}
		}

		public bool IsExcludingLowerBound{
			get{
				return this.isExcludingLowerBound;
			}
			set{
				this.isExcludingLowerBound = value;
			}
		}

		public bool IsExcludingUpperBound{
			get{
				return this.isExcludingUpperBound;
			}
			set{
				this.isExcludingUpperBound = value;
			}
		}

		#region IEquatable

		public bool Equals(Range<T> other){
			return this.upperBound.Equals(other.upperBound) &&
				this.lowerBound.Equals(other.lowerBound) &&
				this.isExcludingLowerBound.Equals(other.isExcludingLowerBound) &&
				this.isExcludingUpperBound.Equals(other.isExcludingUpperBound);
		}

		public override bool Equals(object obj){
			if(!(obj is Range<T>)) {
				return false;
			}
			return this.Equals((Range<T>)obj);
		}

		public override int GetHashCode(){
			return this.upperBound.GetHashCode() ^ this.lowerBound.GetHashCode() ^
				this.isExcludingLowerBound.GetHashCode() ^ this.isExcludingUpperBound.GetHashCode();
		}

		public static bool operator ==(Range<T> a, Range<T> b){
			return a.Equals(b);
		}

		public static bool operator !=(Range<T> a, Range<T> b){
			return !a.Equals(b);
		}

		#endregion

		#region Interset

		public bool IsIntersetWith(Range<T> range){
			return this.Contains(range.lowerBound) || this.Contains(range.upperBound) || range.Contains(this.lowerBound) || range.Contains(this.upperBound);
		}

		#endregion
	}
}
