using System.Windows;
using System.Windows.Media.Animation;

namespace ModernWpf.Controls.Primitives
{
	public static class OpeningAnimationHelper
	{
		#region Fields

		public static readonly DependencyProperty StoryboardProperty =
			DependencyProperty.RegisterAttached(
				"Storyboard",
				typeof(Storyboard),
				typeof(OpeningAnimationHelper),
				new PropertyMetadata(OnStoryboardChanged));

		#endregion Fields

		#region Methods

		private static void OnElementLoaded(object sender, RoutedEventArgs e)
		{
			var element = (FrameworkElement)sender;
			if (element.IsVisible && Helper.IsAnimationsEnabled && !DesignMode.DesignModeEnabled)
			{
				var storyboard = GetStoryboard(element);
				if (storyboard != null)
				{
					storyboard.Begin();
				}
			}
		}

		private static void OnStoryboardChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var element = (FrameworkElement)d;

			if (e.OldValue != null)
			{
				element.Loaded -= OnElementLoaded;
			}

			if (e.NewValue != null)
			{
				element.Loaded += OnElementLoaded;
			}
		}

		public static Storyboard GetStoryboard(FrameworkElement element)
		{
			return (Storyboard)element.GetValue(StoryboardProperty);
		}

		public static void SetStoryboard(FrameworkElement element, Storyboard value)
		{
			element.SetValue(StoryboardProperty, value);
		}

		#endregion Methods
	}
}