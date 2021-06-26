using System.Windows;
using System.Windows.Controls;

namespace ModernWpf.Controls.Primitives
{
	public static class RowDefinitionHelper
	{
		#region Fields

		public static readonly DependencyProperty PixelHeightProperty =
			DependencyProperty.RegisterAttached(
				"PixelHeight",
				typeof(double),
				typeof(RowDefinitionHelper),
				new PropertyMetadata(double.NaN, OnPixelHeightChanged));

		#endregion Fields

		#region Methods

		private static void OnPixelHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var rowDefinition = (RowDefinition)d;
			var pixels = (double)e.NewValue;
			if (double.IsNaN(pixels) || double.IsInfinity(pixels))
			{
				rowDefinition.ClearValue(RowDefinition.HeightProperty);
			}
			else
			{
				rowDefinition.Height = new GridLength(pixels);
			}
		}

		public static double GetPixelHeight(RowDefinition rowDefinition)
		{
			return (double)rowDefinition.GetValue(PixelHeightProperty);
		}

		public static void SetPixelHeight(RowDefinition rowDefinition, double value)
		{
			rowDefinition.SetValue(PixelHeightProperty, value);
		}

		#endregion Methods
	}
}