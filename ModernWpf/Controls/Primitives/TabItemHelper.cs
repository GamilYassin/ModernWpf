using System.Windows;
using System.Windows.Controls;

namespace ModernWpf.Controls.Primitives
{
	public static class TabItemHelper
	{
		#region Fields

		public static readonly DependencyProperty IconProperty =
			DependencyProperty.RegisterAttached(
				"Icon",
				typeof(object),
				typeof(TabItemHelper));

		#endregion Fields

		#region Methods

		public static object GetIcon(TabItem tabItem)
		{
			return tabItem.GetValue(IconProperty);
		}

		public static void SetIcon(TabItem tabItem, object value)
		{
			tabItem.SetValue(IconProperty, value);
		}

		#endregion Methods
	}
}