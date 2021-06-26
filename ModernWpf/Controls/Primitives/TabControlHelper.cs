using System.Windows;
using System.Windows.Controls;

namespace ModernWpf.Controls.Primitives
{
	public static class TabControlHelper
	{
		#region Fields

		public static readonly DependencyProperty TabStripFooterProperty =
			DependencyProperty.RegisterAttached(
				"TabStripFooter",
				typeof(object),
				typeof(TabControlHelper));

		public static readonly DependencyProperty TabStripFooterTemplateProperty =
			DependencyProperty.RegisterAttached(
				"TabStripFooterTemplate",
				typeof(DataTemplate),
				typeof(TabControlHelper));

		public static readonly DependencyProperty TabStripHeaderProperty =
							DependencyProperty.RegisterAttached(
				"TabStripHeader",
				typeof(object),
				typeof(TabControlHelper));

		public static readonly DependencyProperty TabStripHeaderTemplateProperty =
			DependencyProperty.RegisterAttached(
				"TabStripHeaderTemplate",
				typeof(DataTemplate),
				typeof(TabControlHelper));

		#endregion Fields

		#region Methods

		public static object GetTabStripFooter(TabControl tabControl)
		{
			return tabControl.GetValue(TabStripFooterProperty);
		}

		public static DataTemplate GetTabStripFooterTemplate(TabControl tabControl)
		{
			return (DataTemplate)tabControl.GetValue(TabStripFooterTemplateProperty);
		}

		public static object GetTabStripHeader(TabControl tabControl)
		{
			return tabControl.GetValue(TabStripHeaderProperty);
		}

		public static DataTemplate GetTabStripHeaderTemplate(TabControl tabControl)
		{
			return (DataTemplate)tabControl.GetValue(TabStripHeaderTemplateProperty);
		}

		public static void SetTabStripFooter(TabControl tabControl, object value)
		{
			tabControl.SetValue(TabStripFooterProperty, value);
		}

		public static void SetTabStripFooterTemplate(TabControl tabControl, DataTemplate value)
		{
			tabControl.SetValue(TabStripFooterTemplateProperty, value);
		}

		public static void SetTabStripHeader(TabControl tabControl, object value)
		{
			tabControl.SetValue(TabStripHeaderProperty, value);
		}

		public static void SetTabStripHeaderTemplate(TabControl tabControl, DataTemplate value)
		{
			tabControl.SetValue(TabStripHeaderTemplateProperty, value);
		}

		#endregion Methods
	}
}