using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CatWalk {
	public class DisposableLazy<T> : ResetLazy<T>, IDisposable
		where T : IDisposable{
		public DisposableLazy()
			: base(LazyThreadSafetyMode.ExecutionAndPublication) {
		}

		public DisposableLazy(Func<T> valueFactory)
			: base(valueFactory, LazyThreadSafetyMode.ExecutionAndPublication) {
		}

		public DisposableLazy(bool isThreadSafe)
			: base(Activator.CreateInstance<T>, isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None) {
		}

		public DisposableLazy(Func<T> valueFactory, bool isThreadSafe)
			: base(valueFactory, isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None) {
		}

		public DisposableLazy(LazyThreadSafetyMode mode)
			: base(Activator.CreateInstance<T>, mode) {
		}

		public DisposableLazy(Func<T> valueFactory, LazyThreadSafetyMode mode) : base(valueFactory, mode){
		}

		public void Dispose() {
			if(this.IsValueCreated) {
				this.Value.Dispose();
			}
			GC.SuppressFinalize(this);
		}

		~DisposableLazy() {
			this.Dispose();
		}
	}
}
