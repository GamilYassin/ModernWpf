using System.ComponentModel;
using System.Windows;

namespace ModernWpf.DesignTime
{
	public static class Design
	{
		#region Fields

		public static readonly DependencyProperty RequestedThemeProperty = DependencyProperty.RegisterAttached(
			"RequestedTheme",
			typeof(ElementTheme),
			typeof(Design),
			new PropertyMetadata(ElementTheme.Default, OnRequestedThemeChanged));

		#endregion Fields

		#region Methods

		private static void OnRequestedThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (DesignerProperties.GetIsInDesignMode(d))
			{
				var element = (FrameworkElement)d;
				ThemeManager.SetRequestedTheme(element, (ElementTheme)e.NewValue);
			}
		}

		public static ElementTheme GetRequestedTheme(FrameworkElement element)
		{
			return (ElementTheme)element.GetValue(RequestedThemeProperty);
		}

		public static void SetRequestedTheme(FrameworkElement element, ElementTheme value)
		{
			element.SetValue(RequestedThemeProperty, value);
		}

		#endregion Methods
	}
}