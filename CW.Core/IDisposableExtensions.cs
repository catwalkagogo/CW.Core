using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW {
	public static class IDisposableExtensions {
		public static void Dispose(this IEnumerable<IDisposable> source) {
			source.ThrowIfNull("source");
			source.Where(_ => _ != null)
				.ForEach(_ => _.Dispose());
		}

		public static void DisposeLazy<T>(this IEnumerable<T> source){
			source.ThrowIfNull("source");
			source
				.Where(_ => _ != null)
				.Where(_ => {
					dynamic lazy = _;
					return lazy.IsValueCreated;
				})
				.ForEach(_ => {
					dynamic d = _;
					d.Value.Dispose();
				});
		}
	}
}
