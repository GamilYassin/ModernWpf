using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ModernWpf.Controls
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class FontIconFallback : Control
	{
		#region Fields

		public static readonly DependencyProperty DataProperty =
			DependencyProperty.Register(
				nameof(Data),
				typeof(Geometry),
				typeof(FontIconFallback),
				null);

		#endregion Fields

		#region Constructors

		static FontIconFallback()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FontIconFallback), new FrameworkPropertyMetadata(typeof(FontIconFallback)));
			FocusableProperty.OverrideMetadata(typeof(FontIconFallback), new FrameworkPropertyMetadata(false));
		}

		#endregion Constructors

		#region Properties

		public Geometry Data
		{
			get => (Geometry)GetValue(DataProperty);
			set => SetValue(DataProperty, value);
		}

		#endregion Properties
	}
}