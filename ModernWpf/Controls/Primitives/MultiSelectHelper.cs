using System.Windows;
using System.Windows.Controls;

namespace ModernWpf.Controls.Primitives
{
	public static class MultiSelectHelper
	{
		#region Fields

		private const string MultiSelectDisabledState = "MultiSelectDisabled";

		private const string MultiSelectEnabledState = "MultiSelectEnabled";

		private const string MultiSelectStatesGroup = "MultiSelectStates";

		public static readonly DependencyProperty SelectionModeProperty =
			DependencyProperty.RegisterAttached(
				"SelectionMode",
				typeof(SelectionMode),
				typeof(MultiSelectHelper),
				new PropertyMetadata(SelectionMode.Single, OnSelectionModeChanged));

		#endregion Fields

		#region Methods

		private static void OnSelectionModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var container = (ListBoxItem)d;
			UpdateVisualState(container, (SelectionMode)e.NewValue, container.IsVisible);
		}

		private static void UpdateVisualState(ListBoxItem control, SelectionMode selectionMode, bool useTransitions)
		{
			bool multiSelectEnabled = selectionMode == SelectionMode.Multiple;
			VisualStateManager.GoToState(control, multiSelectEnabled ? MultiSelectEnabledState : MultiSelectDisabledState, useTransitions);
		}

		public static SelectionMode GetSelectionMode(ListBoxItem container)
		{
			return (SelectionMode)container.GetValue(SelectionModeProperty);
		}

		public static void SetSelectionMode(ListBoxItem container, SelectionMode value)
		{
			container.SetValue(SelectionModeProperty, value);
		}

		#endregion Methods
	}
}