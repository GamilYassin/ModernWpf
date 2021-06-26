using System.Windows;
using System.Windows.Controls.Primitives;

namespace ModernWpf.Controls.Primitives
{
	public static class ScrollBarThumbHelper
	{
		#region Fields

		public static readonly DependencyProperty IsExpandedProperty =
			DependencyProperty.RegisterAttached(
				"IsExpanded",
				typeof(bool),
				typeof(ScrollBarThumbHelper),
				new PropertyMetadata(false));

		#endregion Fields

		#region Methods

		public static bool GetIsExpanded(Thumb thumb)
		{
			return (bool)thumb.GetValue(IsExpandedProperty);
		}

		public static void SetIsExpanded(Thumb thumb, bool value)
		{
			thumb.SetValue(IsExpandedProperty, value);
		}

		#endregion Methods
	}
}