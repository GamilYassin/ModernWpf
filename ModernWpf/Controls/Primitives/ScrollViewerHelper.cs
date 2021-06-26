using System.Windows;
using System.Windows.Controls;

namespace ModernWpf.Controls.Primitives
{
	public static class ScrollViewerHelper
	{
		#region Fields

		public static readonly DependencyProperty AutoHideScrollBarsProperty =
			DependencyProperty.RegisterAttached(
				"AutoHideScrollBars",
				typeof(bool),
				typeof(ScrollViewerHelper),
				new PropertyMetadata(false, OnAutoHideScrollBarsChanged));

		public static readonly DependencyProperty IsEnabledProperty =
					DependencyProperty.RegisterAttached(
				"IsEnabled",
				typeof(bool),
				typeof(ScrollViewerHelper),
				new PropertyMetadata(false, OnIsEnabledChanged));

		#endregion Fields

		#region Methods

		private static void OnAutoHideScrollBarsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is ScrollViewer sv)
			{
				UpdateVisualState(sv);
			}
		}

		private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var sv = (ScrollViewer)d;
			if ((bool)e.NewValue)
			{
				sv.Loaded += OnLoaded;
			}
			else
			{
				sv.Loaded -= OnLoaded;
			}
		}

		private static void OnLoaded(object sender, RoutedEventArgs e)
		{
			var sv = (ScrollViewer)sender;
			sv.ApplyTemplate();
			UpdateVisualState(sv, false);
		}

		private static void UpdateVisualState(ScrollViewer sv, bool useTransitions = true)
		{
			string stateName = GetAutoHideScrollBars(sv) ? "NoIndicator" : "MouseIndicator";
			VisualStateManager.GoToState(sv, stateName, useTransitions);
		}

		public static bool GetAutoHideScrollBars(DependencyObject element)
		{
			return (bool)element.GetValue(AutoHideScrollBarsProperty);
		}

		public static bool GetIsEnabled(ScrollViewer scrollViewer)
		{
			return (bool)scrollViewer.GetValue(IsEnabledProperty);
		}

		public static void SetAutoHideScrollBars(DependencyObject element, bool value)
		{
			element.SetValue(AutoHideScrollBarsProperty, value);
		}

		public static void SetIsEnabled(ScrollViewer scrollViewer, bool value)
		{
			scrollViewer.SetValue(IsEnabledProperty, value);
		}

		#endregion Methods
	}
}