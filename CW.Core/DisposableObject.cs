/*
	$Id: DisposableObject.cs 320 2014-01-03 07:41:05Z catwalkagogo@gmail.com $
*/
using System;

namespace CW{
	public abstract class DisposableObject : IDisposable{
		private bool _Disposed = false;
		protected bool IsDisposed{
			get{
				return this._Disposed;
			}
		}

		public void Dispose(){
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		
		protected virtual void Dispose(bool disposing){
			this._Disposed = true;
		}

		protected void ThrowIfDisposed(){
			throw new ObjectDisposedException("this");
		}
		
		~DisposableObject(){
			this.Dispose(false);
		}
	}
}