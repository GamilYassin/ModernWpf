using System.Windows;
using System.Windows.Controls;

namespace ModernWpf.Controls.Primitives
{
	public static class TextBoxHelper
	{
		#region Fields

		private const string ButtonCollapsedState = "ButtonCollapsed";

		private const string ButtonStatesGroup = "ButtonStates";

		private const string ButtonVisibleState = "ButtonVisible";

		private static readonly DependencyPropertyKey HasTextPropertyKey =
			DependencyProperty.RegisterAttachedReadOnly(
				"HasText",
				typeof(bool),
				typeof(TextBoxHelper),
				null);

		public static readonly DependencyProperty HasTextProperty =
			HasTextPropertyKey.DependencyProperty;

		public static readonly DependencyProperty IsDeleteButtonProperty =
			DependencyProperty.RegisterAttached(
				"IsDeleteButton",
				typeof(bool),
				typeof(TextBoxHelper),
				new PropertyMetadata(OnIsDeleteButtonChanged));

		public static readonly DependencyProperty IsDeleteButtonVisibleProperty =
			DependencyProperty.RegisterAttached(
				"IsDeleteButtonVisible",
				typeof(bool),
				typeof(TextBoxHelper),
				new PropertyMetadata(OnIsDeleteButtonVisibleChanged));

		public static readonly DependencyProperty IsEnabledProperty =
			DependencyProperty.RegisterAttached(
				"IsEnabled",
				typeof(bool),
				typeof(TextBoxHelper),
				new PropertyMetadata(OnIsEnabledChanged));

		#endregion Fields

		#region Methods

		private static void OnDeleteButtonClick(object sender, RoutedEventArgs e)
		{
			var button = (Button)sender;
			if (button.TemplatedParent is TextBox textBox)
			{
				textBox.SetCurrentValue(TextBox.TextProperty, null);
			}
		}

		private static void OnIsDeleteButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var button = (Button)d;

			if ((bool)e.OldValue)
			{
				button.Click -= OnDeleteButtonClick;
			}

			if ((bool)e.NewValue)
			{
				button.Click += OnDeleteButtonClick;
			}
		}

		private static void OnIsDeleteButtonVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UpdateVisualStates((TextBox)d, (bool)e.NewValue);
		}

		private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var textBox = (TextBox)d;

			if ((bool)e.NewValue)
			{
				textBox.Loaded += OnLoaded;
				textBox.TextChanged += OnTextChanged;
				UpdateHasText(textBox);
			}
			else
			{
				textBox.Loaded -= OnLoaded;
				textBox.TextChanged -= OnTextChanged;
				textBox.ClearValue(HasTextPropertyKey);
			}
		}

		private static void OnLoaded(object sender, RoutedEventArgs e)
		{
			var textBox = (TextBox)sender;
			UpdateVisualStates(textBox, GetIsDeleteButtonVisible(textBox));
		}

		private static void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var textBox = (TextBox)sender;
			UpdateHasText(textBox);
		}

		private static void SetHasText(TextBox textBox, bool value)
		{
			textBox.SetValue(HasTextPropertyKey, value);
		}

		private static void UpdateHasText(TextBox textBox)
		{
			SetHasText(textBox, !string.IsNullOrEmpty(textBox.Text));
		}

		private static void UpdateVisualStates(TextBox textBox, bool isDeleteButtonVisible)
		{
			VisualStateManager.GoToState(textBox, isDeleteButtonVisible ? ButtonVisibleState : ButtonCollapsedState, true);
		}

		public static bool GetHasText(TextBox textBox)
		{
			return (bool)textBox.GetValue(HasTextProperty);
		}

		public static bool GetIsDeleteButton(Button button)
		{
			return (bool)button.GetValue(IsDeleteButtonProperty);
		}

		public static bool GetIsDeleteButtonVisible(TextBox textBox)
		{
			return (bool)textBox.GetValue(IsDeleteButtonVisibleProperty);
		}

		public static bool GetIsEnabled(TextBox textBox)
		{
			return (bool)textBox.GetValue(IsEnabledProperty);
		}

		public static void SetIsDeleteButton(Button button, bool value)
		{
			button.SetValue(IsDeleteButtonProperty, value);
		}

		public static void SetIsDeleteButtonVisible(TextBox textBox, bool value)
		{
			textBox.SetValue(IsDeleteButtonVisibleProperty, value);
		}

		public static void SetIsEnabled(TextBox textBox, bool value)
		{
			textBox.SetValue(IsEnabledProperty, value);
		}

		#endregion Methods
	}
}