using System.Windows;
using System.Windows.Input;

namespace ModernWpf.Controls.Primitives
{
	public static class PressHelper
	{
		#region Fields

		private static readonly DependencyPropertyKey IsPressedPropertyKey =
			DependencyProperty.RegisterAttachedReadOnly(
				"IsPressed",
				typeof(bool),
				typeof(PressHelper),
				null);

		public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
			"IsEnabled",
			typeof(bool),
			typeof(PressHelper),
			new PropertyMetadata(OnIsEnabledChanged));

		public static readonly DependencyProperty IsPressedProperty =
			IsPressedPropertyKey.DependencyProperty;

		#endregion Fields

		#region Methods

		private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var element = (UIElement)d;
			if ((bool)e.NewValue)
			{
				element.MouseLeftButtonDown += OnMouseLeftButtonDown;
				element.MouseLeftButtonUp += OnMouseLeftButtonUp;
				element.MouseEnter += OnMouseEnter;
				element.MouseLeave += OnMouseLeave;
				UpdateIsPressed(element);
			}
			else
			{
				element.MouseLeftButtonDown -= OnMouseLeftButtonDown;
				element.MouseLeftButtonUp -= OnMouseLeftButtonUp;
				element.MouseEnter -= OnMouseEnter;
				element.MouseLeave -= OnMouseLeave;
				element.ClearValue(IsPressedPropertyKey);
			}
		}

		private static void OnMouseEnter(object sender, MouseEventArgs e)
		{
			UpdateIsPressed((UIElement)sender);
		}

		private static void OnMouseLeave(object sender, MouseEventArgs e)
		{
			UpdateIsPressed((UIElement)sender);
		}

		private static void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			UpdateIsPressed((UIElement)sender);
		}

		private static void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			UpdateIsPressed((UIElement)sender);
		}

		private static void SetIsPressed(UIElement element, bool value)
		{
			element.SetValue(IsPressedPropertyKey, value);
		}

		private static void UpdateIsPressed(UIElement element)
		{
			Rect itemBounds = new Rect(new Point(), element.RenderSize);

			if ((Mouse.LeftButton == MouseButtonState.Pressed) &&
				element.IsMouseOver &&
				itemBounds.Contains(Mouse.GetPosition(element)))
			{
				SetIsPressed(element, true);
			}
			else
			{
				element.ClearValue(IsPressedPropertyKey);
			}
		}

		public static bool GetIsEnabled(UIElement element)
		{
			return (bool)element.GetValue(IsEnabledProperty);
		}

		public static bool GetIsPressed(UIElement element)
		{
			return (bool)element.GetValue(IsPressedProperty);
		}

		public static void SetIsEnabled(UIElement element, bool value)
		{
			element.SetValue(IsEnabledProperty, value);
		}

		#endregion Methods
	}
}