using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace CW {
	public class ResetLazy<T> : ILazy<T> {
		private Func<T> valueFactory;
		private LazyThreadSafetyMode mode;
		private Lazy<T> _Lazy;

		public ResetLazy()
			: this(LazyThreadSafetyMode.ExecutionAndPublication) {
		}

		public ResetLazy(Func<T> valueFactory)
			: this(valueFactory, LazyThreadSafetyMode.ExecutionAndPublication) {
		}

		public ResetLazy(bool isThreadSafe)
			: this(Activator.CreateInstance<T>, isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None) {
		}

		public ResetLazy(Func<T> valueFactory, bool isThreadSafe)
			: this(valueFactory, isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None) {
		}

		public ResetLazy(LazyThreadSafetyMode mode)
			: this(Activator.CreateInstance<T>, mode) {
		}

		public ResetLazy(Func<T> valueFactory, LazyThreadSafetyMode mode) {
			this.valueFactory = valueFactory;
			this.mode = mode;

			this.CreateLazy();
		}

		private void CreateLazy() {
			this._Lazy = new Lazy<T>(valueFactory, mode);
		}

		public void Reset() {
			if(this._Lazy.IsValueCreated) {
				var disposable = this._Lazy.Value as IDisposable;
				if(disposable != null) {
					disposable.Dispose();
				}
				this.CreateLazy();
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public T Value {
			get {
				return this._Lazy.Value;
			}
		}

		public bool IsValueCreated {
			get {
				return this._Lazy.IsValueCreated;
			}
		}

		public override string ToString() {
			return this._Lazy.ToString();
		}

		#region ILazy Members

		object ILazy.Value {
			get {
				return this._Lazy.Value;
			}
		}

		#endregion
	}
}
