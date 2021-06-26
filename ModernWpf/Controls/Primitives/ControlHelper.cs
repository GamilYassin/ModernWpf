﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ModernWpf.Controls.Primitives
{
	public static class ControlHelper
	{
		#region Fields

		private static readonly DependencyPropertyKey DescriptionVisibilityPropertyKey =
					DependencyProperty.RegisterAttachedReadOnly(
						"DescriptionVisibility",
						typeof(Visibility),
						typeof(ControlHelper),
						new FrameworkPropertyMetadata(Visibility.Collapsed));

		private static readonly DependencyPropertyKey HeaderVisibilityPropertyKey =
					DependencyProperty.RegisterAttachedReadOnly(
						"HeaderVisibility",
						typeof(Visibility),
						typeof(ControlHelper),
						new FrameworkPropertyMetadata(Visibility.Collapsed));

		private static readonly DependencyPropertyKey PlaceholderTextVisibilityPropertyKey =
					DependencyProperty.RegisterAttachedReadOnly(
						"PlaceholderTextVisibility",
						typeof(Visibility),
						typeof(ControlHelper),
						new FrameworkPropertyMetadata(Visibility.Collapsed));

		/// <summary>
		///  Identifies the CornerRadius dependency property.
		/// </summary>
		public static readonly DependencyProperty CornerRadiusProperty =
			DependencyProperty.RegisterAttached(
				"CornerRadius",
				typeof(CornerRadius),
				typeof(ControlHelper),
				null);

		/// <summary>
		///  Identifies the Description dependency property.
		/// </summary>
		public static readonly DependencyProperty DescriptionProperty =
			DependencyProperty.RegisterAttached(
				"Description",
				typeof(object),
				typeof(ControlHelper),
				new FrameworkPropertyMetadata(OnDescriptionChanged));

		public static readonly DependencyProperty DescriptionVisibilityProperty =
					DescriptionVisibilityPropertyKey.DependencyProperty;

		/// <summary>
		///  Identifies the Header dependency property.
		/// </summary>
		public static readonly DependencyProperty HeaderProperty =
			DependencyProperty.RegisterAttached(
				"Header",
				typeof(object),
				typeof(ControlHelper),
				new FrameworkPropertyMetadata(OnHeaderChanged));

		/// <summary>
		///  Identifies the HeaderTemplate dependency property.
		/// </summary>
		public static readonly DependencyProperty HeaderTemplateProperty =
			DependencyProperty.RegisterAttached(
				"HeaderTemplate",
				typeof(DataTemplate),
				typeof(ControlHelper),
				new FrameworkPropertyMetadata(OnHeaderTemplateChanged));

		public static readonly DependencyProperty HeaderVisibilityProperty =
					HeaderVisibilityPropertyKey.DependencyProperty;

		/// <summary>
		///  Identifies the PlaceholderForeground dependency property.
		/// </summary>
		public static readonly DependencyProperty PlaceholderForegroundProperty =
			DependencyProperty.RegisterAttached(
				"PlaceholderForeground",
				typeof(Brush),
				typeof(ControlHelper),
				null);

		/// <summary>
		///  Identifies the PlaceholderText dependency property.
		/// </summary>
		public static readonly DependencyProperty PlaceholderTextProperty =
			DependencyProperty.RegisterAttached(
				"PlaceholderText",
				typeof(string),
				typeof(ControlHelper),
				new FrameworkPropertyMetadata(string.Empty, OnPlaceholderTextChanged));

		public static readonly DependencyProperty PlaceholderTextVisibilityProperty =
					PlaceholderTextVisibilityPropertyKey.DependencyProperty;

		#endregion Fields

		#region Methods

		private static void OnDescriptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UpdateDescriptionVisibility((Control)d);
		}

		private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UpdateHeaderVisibility((Control)d);
		}

		private static void OnHeaderTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UpdateHeaderVisibility((Control)d);
		}

		private static void OnPlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UpdatePlaceholderTextVisibility((Control)d);
		}

		private static void SetDescriptionVisibility(Control control, Visibility value)
		{
			control.SetValue(DescriptionVisibilityPropertyKey, value);
		}

		private static void SetHeaderVisibility(Control control, Visibility value)
		{
			control.SetValue(HeaderVisibilityPropertyKey, value);
		}

		private static void SetPlaceholderTextVisibility(Control control, Visibility value)
		{
			control.SetValue(PlaceholderTextVisibilityPropertyKey, value);
		}

		private static void UpdateDescriptionVisibility(Control control)
		{
			SetDescriptionVisibility(control, IsNullOrEmptyString(GetDescription(control)) ? Visibility.Collapsed : Visibility.Visible);
		}

		private static void UpdateHeaderVisibility(Control control)
		{
			Visibility visibility;

			if (GetHeaderTemplate(control) != null)
			{
				visibility = Visibility.Visible;
			}
			else
			{
				visibility = IsNullOrEmptyString(GetHeader(control)) ? Visibility.Collapsed : Visibility.Visible;
			}

			SetHeaderVisibility(control, visibility);
		}

		private static void UpdatePlaceholderTextVisibility(Control control)
		{
			SetPlaceholderTextVisibility(control, string.IsNullOrEmpty(GetPlaceholderText(control)) ? Visibility.Collapsed : Visibility.Visible);
		}

		internal static bool IsNullOrEmptyString(object obj)
		{
			return obj == null || obj is string s && string.IsNullOrEmpty(s);
		}

		/// <summary>
		///  Gets the radius for the corners of the control's border.
		/// </summary>
		/// <param name="control">
		///  The element from which to read the property value.
		/// </param>
		/// <returns>
		///  The degree to which the corners are rounded, expressed as values of the CornerRadius structure.
		/// </returns>
		public static CornerRadius GetCornerRadius(Control control)
		{
			return (CornerRadius)control.GetValue(CornerRadiusProperty);
		}

		/// <summary>
		///  Gets content that is shown below the control. The content should provide guidance about
		///  the input expected by the control.
		/// </summary>
		/// <param name="control">
		///  The element from which to read the property value.
		/// </param>
		/// <returns>
		///  The content to be displayed below the control. The default is **null**.
		/// </returns>
		public static object GetDescription(Control control)
		{
			return control.GetValue(DescriptionProperty);
		}

		public static Visibility GetDescriptionVisibility(Control control)
		{
			return (Visibility)control.GetValue(DescriptionVisibilityProperty);
		}

		/// <summary>
		///  Gets the content for the control's header.
		/// </summary>
		/// <param name="control">
		///  The element from which to read the property value.
		/// </param>
		/// <returns>
		///  The content of the control's header. The default is **null**.
		/// </returns>
		public static object GetHeader(Control control)
		{
			return control.GetValue(HeaderProperty);
		}

		/// <summary>
		///  Gets the DataTemplate used to display the content of the control's header.
		/// </summary>
		/// <param name="control">
		///  The element from which to read the property value.
		/// </param>
		/// <returns>
		///  The template that specifies the visualization of the header object. The default is **null**.
		/// </returns>
		public static DataTemplate GetHeaderTemplate(Control control)
		{
			return (DataTemplate)control.GetValue(HeaderTemplateProperty);
		}

		public static Visibility GetHeaderVisibility(Control control)
		{
			return (Visibility)control.GetValue(HeaderVisibilityProperty);
		}

		/// <summary>
		///  Gets a brush that describes the color of placeholder text.
		/// </summary>
		/// <param name="control">
		///  The element from which to read the property value.
		/// </param>
		/// <returns>
		///  The brush that describes the color of placeholder text.
		/// </returns>
		public static Brush GetPlaceholderForeground(Control control)
		{
			return (Brush)control.GetValue(PlaceholderForegroundProperty);
		}

		/// <summary>
		///  Gets the text that is displayed in the control until the value is changed by a user
		///  action or some other operation.
		/// </summary>
		/// <param name="control">
		///  The element from which to read the property value.
		/// </param>
		/// <returns>
		///  The text that is displayed in the control when no value is entered. The default is an
		///  empty string ("").
		/// </returns>
		public static string GetPlaceholderText(Control control)
		{
			return (string)control.GetValue(PlaceholderTextProperty);
		}

		public static Visibility GetPlaceholderTextVisibility(Control control)
		{
			return (Visibility)control.GetValue(PlaceholderTextVisibilityProperty);
		}

		/// <summary>
		///  Sets the radius for the corners of the control's border.
		/// </summary>
		/// <param name="control">
		///  The element on which to set the attached property.
		/// </param>
		/// <param name="value">
		///  The property value to set.
		/// </param>
		public static void SetCornerRadius(Control control, CornerRadius value)
		{
			control.SetValue(CornerRadiusProperty, value);
		}

		/// <summary>
		///  Sets content that is shown below the control. The content should provide guidance about
		///  the input expected by the control.
		/// </summary>
		/// <param name="control">
		///  The element on which to set the attached property.
		/// </param>
		/// <param name="value">
		///  The property value to set.
		/// </param>
		public static void SetDescription(Control control, object value)
		{
			control.SetValue(DescriptionProperty, value);
		}

		/// <summary>
		///  Sets the content for the control's header.
		/// </summary>
		/// <param name="control">
		///  The element on which to set the attached property.
		/// </param>
		/// <param name="value">
		///  The property value to set.
		/// </param>
		public static void SetHeader(Control control, object value)
		{
			control.SetValue(HeaderProperty, value);
		}

		/// <summary>
		///  Sets the DataTemplate used to display the content of the control's header.
		/// </summary>
		/// <param name="control">
		///  The element on which to set the attached property.
		/// </param>
		/// <param name="value">
		///  The property value to set.
		/// </param>
		public static void SetHeaderTemplate(Control control, DataTemplate value)
		{
			control.SetValue(HeaderTemplateProperty, value);
		}

		/// <summary>
		///  Sets a brush that describes the color of placeholder text.
		/// </summary>
		/// <param name="control">
		///  The element on which to set the attached property.
		/// </param>
		/// <param name="value">
		///  The property value to set.
		/// </param>
		public static void SetPlaceholderForeground(Control control, Brush value)
		{
			control.SetValue(PlaceholderForegroundProperty, value);
		}

		/// <summary>
		///  Sets the text that is displayed in the control until the value is changed by a user
		///  action or some other operation.
		/// </summary>
		/// <param name="control">
		///  The element on which to set the attached property.
		/// </param>
		/// <param name="value">
		///  The property value to set.
		/// </param>
		public static void SetPlaceholderText(Control control, string value)
		{
			control.SetValue(PlaceholderTextProperty, value);
		}

		#endregion Methods
	}
}