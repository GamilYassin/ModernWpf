using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ModernWpf.Controls.Primitives
{
	public static class WindowHelper
	{
		#region Fields

		private const string DefaultWindowStyleKey = "DefaultWindowStyle";

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static readonly DependencyProperty FixMaximizedWindowProperty =
			DependencyProperty.RegisterAttached(
				"FixMaximizedWindow",
				typeof(bool),
				typeof(WindowHelper),
				new PropertyMetadata(false, OnFixMaximizedWindowChanged));

		public static readonly DependencyProperty UseModernWindowStyleProperty =
					DependencyProperty.RegisterAttached(
				"UseModernWindowStyle",
				typeof(bool),
				typeof(WindowHelper),
				new PropertyMetadata(OnUseModernWindowStyleChanged));

		#endregion Fields

		#region Methods

		private static void OnFixMaximizedWindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is Window window)
			{
				if ((bool)e.NewValue)
				{
					MaximizedWindowFixer.SetMaximizedWindowFixer(window, new MaximizedWindowFixer());
				}
				else
				{
					window.ClearValue(MaximizedWindowFixer.MaximizedWindowFixerProperty);
				}
			}
		}

		private static void OnUseModernWindowStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			bool newValue = (bool)e.NewValue;

			if (DesignerProperties.GetIsInDesignMode(d))
			{
				if (d is Control control)
				{
					if (newValue)
					{
						if (control.TryFindResource(DefaultWindowStyleKey) is Style style)
						{
							var dStyle = new Style();

							foreach (Setter setter in style.Setters)
							{
								if (setter.Property == Control.BackgroundProperty ||
									setter.Property == Control.ForegroundProperty)
								{
									dStyle.Setters.Add(setter);
								}
							}

							control.Style = dStyle;
						}
					}
					else
					{
						control.ClearValue(FrameworkElement.StyleProperty);
					}
				}
			}
			else
			{
				var window = (Window)d;
				if (newValue)
				{
					window.SetResourceReference(FrameworkElement.StyleProperty, DefaultWindowStyleKey);
				}
				else
				{
					window.ClearValue(FrameworkElement.StyleProperty);
				}
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool GetFixMaximizedWindow(Window window)
		{
			return (bool)window.GetValue(FixMaximizedWindowProperty);
		}

		public static bool GetUseModernWindowStyle(Window window)
		{
			return (bool)window.GetValue(UseModernWindowStyleProperty);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void SetFixMaximizedWindow(Window window, bool value)
		{
			window.SetValue(FixMaximizedWindowProperty, value);
		}

		public static void SetUseModernWindowStyle(Window window, bool value)
		{
			window.SetValue(UseModernWindowStyleProperty, value);
		}

		#endregion Methods
	}
}