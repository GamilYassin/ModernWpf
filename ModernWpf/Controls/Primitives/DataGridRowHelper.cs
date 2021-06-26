using System.Windows;
using System.Windows.Controls;

namespace ModernWpf.Controls.Primitives
{
	public static class DataGridRowHelper
	{
		#region Fields

		private static readonly DependencyPropertyKey AreRowDetailsFrozenPropertyKey =
			DependencyProperty.RegisterAttachedReadOnly(
				"AreRowDetailsFrozen",
				typeof(bool),
				typeof(DataGridRowHelper),
				new PropertyMetadata(false));

		private static readonly DependencyPropertyKey HeadersVisibilityPropertyKey =
			DependencyProperty.RegisterAttachedReadOnly(
				"HeadersVisibility",
				typeof(DataGridHeadersVisibility),
				typeof(DataGridRowHelper),
				new PropertyMetadata(DataGridHeadersVisibility.All));

		internal static readonly DependencyProperty AreRowDetailsFrozenInternalProperty =
			DependencyProperty.RegisterAttached(
				"AreRowDetailsFrozenInternal",
				typeof(bool),
				typeof(DataGridRowHelper),
				new PropertyMetadata(false, OnAreRowDetailsFrozenInternalChanged));

		internal static readonly DependencyProperty HeadersVisibilityInternalProperty =
			DependencyProperty.RegisterAttached(
				"HeadersVisibilityInternal",
				typeof(DataGridHeadersVisibility),
				typeof(DataGridRowHelper),
				new PropertyMetadata(DataGridHeadersVisibility.All, OnHeadersVisibilityInternalChanged));

		public static readonly DependencyProperty AreRowDetailsFrozenProperty =
									AreRowDetailsFrozenPropertyKey.DependencyProperty;

		public static readonly DependencyProperty HeadersVisibilityProperty =
			HeadersVisibilityPropertyKey.DependencyProperty;

		#endregion Fields

		#region Methods

		private static void OnAreRowDetailsFrozenInternalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SetAreRowDetailsFrozen((DataGridRow)d, (bool)e.NewValue);
		}

		private static void OnHeadersVisibilityInternalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SetHeadersVisibility((DataGridRow)d, (DataGridHeadersVisibility)e.NewValue);
		}

		private static void SetAreRowDetailsFrozen(DataGridRow row, bool value)
		{
			row.SetValue(AreRowDetailsFrozenPropertyKey, value);
		}

		private static void SetHeadersVisibility(DataGridRow row, DataGridHeadersVisibility value)
		{
			row.SetValue(HeadersVisibilityPropertyKey, value);
		}

		public static bool GetAreRowDetailsFrozen(DataGridRow row)
		{
			return (bool)row.GetValue(AreRowDetailsFrozenProperty);
		}

		public static DataGridHeadersVisibility GetHeadersVisibility(DataGridRow row)
		{
			return (DataGridHeadersVisibility)row.GetValue(HeadersVisibilityProperty);
		}

		#endregion Methods
	}
}