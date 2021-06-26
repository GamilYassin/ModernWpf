using System;

namespace ModernWpf
{
	internal abstract class EventRevoker<TSource, TDelegate>
		where TSource : class
		where TDelegate : Delegate
	{
		#region Fields

		private WeakReference<TDelegate> _handler;

		private WeakReference<TSource> _source;

		#endregion Fields

		#region Constructors

		protected EventRevoker(TSource source, TDelegate handler)
		{
			_source = new WeakReference<TSource>(source);
			_handler = new WeakReference<TDelegate>(handler);
			AddHandler(source, handler);
		}

		#endregion Constructors

		#region Methods

		protected abstract void AddHandler(TSource source, TDelegate handler);

		protected abstract void RemoveHandler(TSource source, TDelegate handler);

		public void Revoke()
		{
			if (_source != null && _handler != null &&
				_source.TryGetTarget(out var source) &&
				_handler.TryGetTarget(out var handler))
			{
				RemoveHandler(source, handler);
			}

			_source = null;
			_handler = null;
		}

		#endregion Methods
	}
}