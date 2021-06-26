using System.Windows;
using System.Windows.Media;

namespace ModernWpf.Controls
{
	public class RatingItemPathInfo : RatingItemInfo
	{
		#region Fields

		public static readonly DependencyProperty DataProperty =
			DependencyProperty.Register(
				nameof(Data),
				typeof(Geometry),
				typeof(RatingItemPathInfo),
				null);

		public static readonly DependencyProperty DisabledDataProperty =
			DependencyProperty.Register(
				nameof(DisabledData),
				typeof(Geometry),
				typeof(RatingItemPathInfo),
				null);

		public static readonly DependencyProperty PlaceholderDataProperty =
			DependencyProperty.Register(
				nameof(PlaceholderData),
				typeof(Geometry),
				typeof(RatingItemPathInfo),
				null);

		public static readonly DependencyProperty PointerOverDataProperty =
			DependencyProperty.Register(
				nameof(PointerOverData),
				typeof(Geometry),
				typeof(RatingItemPathInfo),
				null);

		public static readonly DependencyProperty PointerOverPlaceholderDataProperty =
			DependencyProperty.Register(
				nameof(PointerOverPlaceholderData),
				typeof(Geometry),
				typeof(RatingItemPathInfo),
				null);

		public static readonly DependencyProperty UnsetDataProperty =
			DependencyProperty.Register(
				nameof(UnsetData),
				typeof(Geometry),
				typeof(RatingItemPathInfo),
				null);

		#endregion Fields

		#region Constructors

		public RatingItemPathInfo()
		{
		}

		#endregion Constructors

		#region Properties

		public Geometry Data
		{
			get => (Geometry)GetValue(DataProperty);
			set => SetValue(DataProperty, value);
		}

		public Geometry DisabledData
		{
			get => (Geometry)GetValue(DisabledDataProperty);
			set => SetValue(DisabledDataProperty, value);
		}

		public Geometry PlaceholderData
		{
			get => (Geometry)GetValue(PlaceholderDataProperty);
			set => SetValue(PlaceholderDataProperty, value);
		}

		public Geometry PointerOverData
		{
			get => (Geometry)GetValue(PointerOverDataProperty);
			set => SetValue(PointerOverDataProperty, value);
		}

		public Geometry PointerOverPlaceholderData
		{
			get => (Geometry)GetValue(PointerOverPlaceholderDataProperty);
			set => SetValue(PointerOverPlaceholderDataProperty, value);
		}

		public Geometry UnsetData
		{
			get => (Geometry)GetValue(UnsetDataProperty);
			set => SetValue(UnsetDataProperty, value);
		}

		#endregion Properties

		#region Methods

		protected override Freezable CreateInstanceCore()
		{
			return new RatingItemPathInfo();
		}

		#endregion Methods
	}
}