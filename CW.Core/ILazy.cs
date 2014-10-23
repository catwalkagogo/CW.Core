using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CW {
	public interface ILazy {
		object Value{get;}
		bool IsValueCreated{get;}
	}

	public interface ILazy<T> : ILazy {
		new T Value {
			get;
		}
	}

	public static class LazyFactory {
		public static ILazy<T> Create<T>(){
			return new ResetLazy<T>();
		}

		public static ILazy<T> Create<T>(Func<T> valueFactory){
			return new ResetLazy<T>(valueFactory);
		}

		public static ILazy<T> Create<T>(bool isThreadSafe){
			return new ResetLazy<T>(isThreadSafe);
		}

		public static ILazy<T> Create<T>(Func<T> valueFactory, bool isThreadSafe){
			return new ResetLazy<T>(valueFactory, isThreadSafe);
		}

		public static ILazy<T> Create<T>(LazyThreadSafetyMode mode){
			return new ResetLazy<T>(mode);
		}
		public static ILazy<T> Create<T>(Func<T> valueFactory, LazyThreadSafetyMode mode){
			return new ResetLazy<T>(valueFactory, mode);
		}
	}
}
