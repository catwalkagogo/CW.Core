/*
	$Id: WeakDelegate.cs 313 2013-12-04 02:33:30Z catwalkagogo@gmail.com $
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CatWalk {
	public class WeakDelegate{
		private WeakReference _TargetReference; // null when static method
		private MethodInfo _Method;
		private Type _DelegateType;

		public WeakDelegate(Delegate handler){
			this._TargetReference = (handler.Target != null) ? new WeakReference(handler.Target) : null;
			this._Method = handler.Method;
			this._DelegateType = handler.GetType();
		}

		public Delegate Delegate{
			get{
				if(this._TargetReference != null){
					var target = this._TargetReference.Target;
					if(target != null) {
						return global::System.Delegate.CreateDelegate(this._DelegateType, this._TargetReference.Target, this._Method);
					} else {
						return null;
					}
				}else{
					return global::System.Delegate.CreateDelegate(this._DelegateType, this._Method);
				}
			}
		}

		public bool IsAlive{
			get{
				return this._TargetReference == null || this._TargetReference.IsAlive;
			}
		}

		public Type DelegateType{
			get{
				return this._DelegateType;
			}
		}

		public WeakReference Target{
			get{
				return this._TargetReference;
			}
		}

		public MethodInfo Method{
			get{
				return this._Method;
			}
		}
	}
}
