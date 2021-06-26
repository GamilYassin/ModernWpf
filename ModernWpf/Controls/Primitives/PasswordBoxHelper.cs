﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ModernWpf.Controls.Primitives
{
	public class PasswordBoxHelper : DependencyObject
	{
		#region Fields

		private const string ButtonCollapsedState = "ButtonCollapsed";

		private const string ButtonStatesGroup = "ButtonStates";

		private const string ButtonVisibleState = "ButtonVisible";

		private static readonly DependencyProperty HelperInstanceProperty =
			DependencyProperty.RegisterAttached(
				"HelperInstance",
				typeof(PasswordBoxHelper),
				typeof(PasswordBoxHelper),
				new PropertyMetadata(OnHelperInstanceChanged));

		private static readonly DependencyPropertyKey PlaceholderTextVisibilityPropertyKey =
			DependencyProperty.RegisterAttachedReadOnly(
				"PlaceholderTextVisibility",
				typeof(Visibility),
				typeof(PasswordBoxHelper),
				new FrameworkPropertyMetadata(Visibility.Visible));

		private static readonly CommandBinding TextBoxCopyBinding;

		private static readonly CommandBinding TextBoxCutBinding;

		private readonly PasswordBox _passwordBox;

		private bool _hideRevealButton;

		public static readonly DependencyProperty IsEnabledProperty =
			DependencyProperty.RegisterAttached(
				"IsEnabled",
				typeof(bool),
				typeof(PasswordBoxHelper),
				new FrameworkPropertyMetadata(OnIsEnabledChanged));

		/// <summary>
		///  Identifies the PasswordRevealMode dependency property.
		/// </summary>
		public static readonly DependencyProperty PasswordRevealModeProperty =
			DependencyProperty.RegisterAttached(
				"PasswordRevealMode",
				typeof(PasswordRevealMode),
				typeof(PasswordBoxHelper),
				new FrameworkPropertyMetadata(PasswordRevealMode.Peek, OnPasswordRevealModeChanged));

		public static readonly DependencyProperty PlaceholderTextVisibilityProperty = PlaceholderTextVisibilityPropertyKey.DependencyProperty;

		#endregion Fields

		#region Constructors

		static PasswordBoxHelper()
		{
			TextBoxCutBinding = new CommandBinding(ApplicationCommands.Cut);
			TextBoxCutBinding.CanExecute += OnDisabledCommandCanExecute;

			TextBoxCopyBinding = new CommandBinding(ApplicationCommands.Copy);
			TextBoxCopyBinding.CanExecute += OnDisabledCommandCanExecute;
		}

		public PasswordBoxHelper(PasswordBox passwordBox)
		{
			_passwordBox = passwordBox;
		}

		#endregion Constructors

		#region Properties

		private PasswordRevealMode PasswordRevealMode => GetPasswordRevealMode(_passwordBox);

		private TextBox TextBox { get; set; }

		#endregion Properties

		#region Methods

		private static PasswordBoxHelper GetHelperInstance(PasswordBox passwordBox)
		{
			return (PasswordBoxHelper)passwordBox.GetValue(HelperInstanceProperty);
		}

		private static void OnDisabledCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = false;
			e.Handled = true;
		}

		private static void OnHelperInstanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.OldValue is PasswordBoxHelper oldHelper)
			{
				oldHelper.Detach();
			}

			if (e.NewValue is PasswordBoxHelper newHelper)
			{
				newHelper.Attach();
			}
		}

		private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var passwordBox = (PasswordBox)d;
			if ((bool)e.NewValue)
			{
				SetHelperInstance(passwordBox, new PasswordBoxHelper(passwordBox));
			}
			else
			{
				passwordBox.ClearValue(HelperInstanceProperty);
			}
		}

		private static void OnPasswordRevealModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var helper = GetHelperInstance((PasswordBox)d);
			helper?.UpdateVisualState(true);
		}

		private static void SetHelperInstance(PasswordBox passwordBox, PasswordBoxHelper value)
		{
			passwordBox.SetValue(HelperInstanceProperty, value);
		}

		private static void SetPlaceholderTextVisibility(Control control, Visibility value)
		{
			control.SetValue(PlaceholderTextVisibilityPropertyKey, value);
		}

		private void Attach()
		{
			_passwordBox.PasswordChanged += OnPasswordChanged;
			_passwordBox.GotFocus += OnGotFocus;
			_passwordBox.LostFocus += OnLostFocus;

			if (_passwordBox.IsLoaded)
			{
				OnApplyTemplate();
			}
			else
			{
				_passwordBox.Loaded += OnLoaded;
			}
		}

		private void Detach()
		{
			_passwordBox.PasswordChanged -= OnPasswordChanged;
			_passwordBox.GotFocus -= OnGotFocus;
			_passwordBox.LostFocus -= OnLostFocus;
			_passwordBox.Loaded -= OnLoaded;

			if (TextBox != null)
			{
				TextBox.CommandBindings.Remove(TextBoxCutBinding);
				TextBox.CommandBindings.Remove(TextBoxCopyBinding);
				TextBox.TextChanged -= OnTextBoxTextChanged;
				TextBox = null;
			}
		}

		private void OnApplyTemplate()
		{
			_passwordBox.ApplyTemplate();

			TextBox = _passwordBox.GetTemplateChild<TextBox>(nameof(TextBox));

			if (TextBox != null)
			{
				TextBox.IsUndoEnabled = false;
				SpellCheck.SetIsEnabled(TextBox, false);
				TextBox.CommandBindings.Add(TextBoxCutBinding);
				TextBox.CommandBindings.Add(TextBoxCopyBinding);
				TextBox.TextChanged += OnTextBoxTextChanged;
				UpdateTextBox();
			}

			UpdateVisualState(false);
		}

		private void OnGotFocus(object sender, RoutedEventArgs e)
		{
			if (PasswordRevealMode == PasswordRevealMode.Visible && TextBox != null)
			{
				if (e.OriginalSource == _passwordBox)
				{
					TextBox.Focus();
					e.Handled = true;
				}
			}

			if (!string.IsNullOrEmpty(_passwordBox.Password))
			{
				_hideRevealButton = true;
			}

			UpdateVisualState(true);
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			_passwordBox.Loaded -= OnLoaded;
			OnApplyTemplate();
		}

		private void OnLostFocus(object sender, RoutedEventArgs e)
		{
			UpdateVisualState(true);
		}

		private void OnPasswordChanged(object sender, RoutedEventArgs e)
		{
			bool hasPassword = !string.IsNullOrEmpty(_passwordBox.Password);

			if (!hasPassword)
			{
				_hideRevealButton = false;
			}

			SetPlaceholderTextVisibility(_passwordBox, hasPassword ? Visibility.Collapsed : Visibility.Visible);
			UpdateTextBox();
			UpdateVisualState(true);
		}

		private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
		{
			if (PasswordRevealMode == PasswordRevealMode.Visible)
			{
				_passwordBox.Password = ((TextBox)sender).Text;
			}
		}

		private void UpdateTextBox()
		{
			if (TextBox != null)
			{
				TextBox.Text = _passwordBox.Password;
			}
		}

		private void UpdateVisualState(bool useTransitions)
		{
			bool buttonVisible = false;
			if (_passwordBox.IsFocused)
			{
				switch (PasswordRevealMode)
				{
					case PasswordRevealMode.Peek:
						buttonVisible = !_hideRevealButton && !string.IsNullOrEmpty(_passwordBox.Password);
						break;

					case PasswordRevealMode.Hidden:
					case PasswordRevealMode.Visible:
						buttonVisible = false;
						break;
				}
			}

			VisualStateManager.GoToState(_passwordBox, buttonVisible ? ButtonVisibleState : ButtonCollapsedState, useTransitions);
		}

		public static bool GetIsEnabled(PasswordBox passwordBox)
		{
			return (bool)passwordBox.GetValue(IsEnabledProperty);
		}

		/// <summary>
		///  Gets a value that specifies whether the password is always, never, or optionally obscured.
		/// </summary>
		/// <param name="passwordBox">
		///  The element from which to read the property value.
		/// </param>
		/// <returns>
		///  A value of the enumeration that specifies whether the password is always, never, or
		///  optionally obscured. The default is **Peek**.
		/// </returns>
		public static PasswordRevealMode GetPasswordRevealMode(PasswordBox passwordBox)
		{
			return (PasswordRevealMode)passwordBox.GetValue(PasswordRevealModeProperty);
		}

		public static Visibility GetPlaceholderTextVisibility(Control control)
		{
			return (Visibility)control.GetValue(PlaceholderTextVisibilityProperty);
		}

		public static void SetIsEnabled(PasswordBox passwordBox, bool value)
		{
			passwordBox.SetValue(IsEnabledProperty, value);
		}

		/// <summary>
		///  Sets a value that specifies whether the password is always, never, or optionally obscured.
		/// </summary>
		/// <param name="passwordBox">
		///  The element on which to set the attached property.
		/// </param>
		/// <param name="value">
		///  The property value to set.
		/// </param>
		public static void SetPasswordRevealMode(PasswordBox passwordBox, PasswordRevealMode value)
		{
			passwordBox.SetValue(PasswordRevealModeProperty, value);
		}

		#endregion Methods
	}
}