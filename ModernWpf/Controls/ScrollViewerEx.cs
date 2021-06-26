using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ModernWpf.Controls
{
	public class ScrollViewerEx : ScrollViewer
	{
		#region Methods

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			if (Style == null && ReadLocalValue(StyleProperty) == DependencyProperty.UnsetValue)
			{
				SetResourceReference(StyleProperty, typeof(ScrollViewer));
			}
		}

		protected override void OnMouseWheel(MouseWheelEventArgs e)
		{
			if (e.Handled) { return; }

			if (ScrollableHeight > 0)
			{
				base.OnMouseWheel(e);
			}
		}

		#endregion Methods

		/*private bool CanScrollVerticallyInDirection(bool inPositiveDirection)
        {
            bool canScrollInDirection = false;
            if (VerticalScrollBarVisibility != ScrollBarVisibility.Disabled)
            {
                var extentHeight = ExtentHeight;
                var viewportHeight = ViewportHeight;
                if (extentHeight > viewportHeight)
                {
                    if (inPositiveDirection)
                    {
                        var maxVerticalOffset = extentHeight - viewportHeight;
                        if (VerticalOffset < maxVerticalOffset)
                        {
                            canScrollInDirection = true;
                        }
                    }
                    else
                    {
                        if (VerticalOffset > 0)
                        {
                            canScrollInDirection = true;
                        }
                    }
                }
            }

            return canScrollInDirection;
        }*/
	}
}