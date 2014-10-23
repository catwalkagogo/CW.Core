/*
	$Id: WeakMulticastDelegate.cs 313 2013-12-04 02:33:30Z catwalkagogo@gmail.com $
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CW {
	public class WeakMulticastDelegate{
		LinkedList<WeakDelegate> _Handlers;
		private LinkedList<WeakDelegate> Handlers{
			get{
				return this._Handlers ?? (this._Handlers = new LinkedList<WeakDelegate>());
			}
		}

		public void Add(Delegate handler){
			this.Handlers.AddLast(new WeakDelegate(handler));
		}
		public void Add(WeakDelegate handler) {
			this.Handlers.AddLast(handler);
		}

		public void Remove(Delegate handler){
			this.RemoveHandler(wd => {
				var d = wd.Delegate;
				return !wd.IsAlive || (d != null && d.Equals(handler));
			});
		}

		public void Remove(WeakDelegate handler) {
			this.RemoveHandler(wd => !wd.IsAlive || wd.Equals(handler));
		}

		public void Invoke(){this.Invoke(null);}
		public void Invoke(params object[] args){
			for(var node = this.Handlers.First; node != null;){
				var next = node.Next;
				var wd = node.Value;
				var d = wd.Delegate;
				if(d != null){
					d.DynamicInvoke(args);
				}else{
					this.Handlers.Remove(node);
				}
				node = next;
			}
		}

		private void RemoveHandler(Predicate<WeakDelegate> pred){
#if DEBUG
			if(pred == null){
				throw new ArgumentNullException("pred");
			}
#endif
			for(var node = this.Handlers.First; node != null;){
				var next = node.Next;
				if(pred(node.Value)){
					this.Handlers.Remove(node);
				}
				node = next;
			}
		}
	}
}
