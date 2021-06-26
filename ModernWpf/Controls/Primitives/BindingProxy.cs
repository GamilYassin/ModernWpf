using System.Windows;

namespace ModernWpf.Controls.Primitives
{
	public class BindingProxy : Freezable
	{
		#region Fields

		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register(
				nameof(Value),
				typeof(object),
				typeof(BindingProxy));

		#endregion Fields

		#region Properties

		public object Value
		{
			get => GetValue(ValueProperty);
			set => SetValue(ValueProperty, value);
		}

		#endregion Properties

		#region Methods

		protected override Freezable CreateInstanceCore()
		{
			return new BindingProxy();
		}

		#endregion Methods
	}
}