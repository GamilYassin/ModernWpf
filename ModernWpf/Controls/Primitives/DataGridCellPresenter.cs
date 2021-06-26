using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ModernWpf.Controls.Primitives
{
	/// <summary>
	///  Represents the visual elements of a DataGridCell.
	/// </summary>
	public class DataGridCellPresenter : ContentPresenter
	{
		#region Fields

		private readonly BorderHelper _currencyVisualHelper;

		private readonly BorderHelper _focusVisualPrimaryHelper;

		private readonly BorderHelper _focusVisualSecondaryHelper;

		public static readonly DependencyProperty BackgroundProperty =
					Panel.BackgroundProperty.AddOwner(typeof(DataGridCellPresenter),
						new FrameworkPropertyMetadata(
							null,
							FrameworkPropertyMetadataOptions.AffectsRender |
							FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

		public static readonly DependencyProperty CurrencyVisualBrushProperty =
					DependencyProperty.Register(
						nameof(CurrencyVisualBrush),
						typeof(Brush),
						typeof(DataGridCellPresenter),
						new FrameworkPropertyMetadata(
							null,
							FrameworkPropertyMetadataOptions.AffectsRender));

		public static readonly DependencyProperty CurrencyVisualThicknessProperty =
					DependencyProperty.Register(
						nameof(CurrencyVisualThickness),
						typeof(double),
						typeof(DataGridCellPresenter),
						new FrameworkPropertyMetadata(
							0.0,
							FrameworkPropertyMetadataOptions.AffectsRender,
							OnCurrencyVisualThicknessChanged));

		public static readonly DependencyProperty FocusVisualPrimaryBrushProperty =
					DependencyProperty.Register(
						nameof(FocusVisualPrimaryBrush),
						typeof(Brush),
						typeof(DataGridCellPresenter),
						new FrameworkPropertyMetadata(
							null,
							FrameworkPropertyMetadataOptions.AffectsRender));

		public static readonly DependencyProperty FocusVisualPrimaryThicknessProperty =
					DependencyProperty.Register(
						nameof(FocusVisualPrimaryThickness),
						typeof(double),
						typeof(DataGridCellPresenter),
						new FrameworkPropertyMetadata(
							0.0,
							FrameworkPropertyMetadataOptions.AffectsRender,
							OnFocusVisualPrimaryThicknessChanged));

		public static readonly DependencyProperty FocusVisualSecondaryBrushProperty =
					DependencyProperty.Register(
						nameof(FocusVisualSecondaryBrush),
						typeof(Brush),
						typeof(DataGridCellPresenter),
						new FrameworkPropertyMetadata(
							null,
							FrameworkPropertyMetadataOptions.AffectsRender));

		public static readonly DependencyProperty FocusVisualSecondaryThicknessProperty =
					DependencyProperty.Register(
						nameof(FocusVisualSecondaryThickness),
						typeof(double),
						typeof(DataGridCellPresenter),
						new FrameworkPropertyMetadata(
							0.0,
							FrameworkPropertyMetadataOptions.AffectsRender,
							OnFocusVisualSecondaryThicknessChanged));

		public static readonly DependencyProperty IsCurrencyVisualVisibleProperty =
					DependencyProperty.Register(
						nameof(IsCurrencyVisualVisible),
						typeof(bool),
						typeof(DataGridCellPresenter),
						new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

		public static readonly DependencyProperty IsFocusVisualVisibleProperty =
					DependencyProperty.Register(
						nameof(IsFocusVisualVisible),
						typeof(bool),
						typeof(DataGridCellPresenter),
						new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

		#endregion Fields

		#region Constructors

		/// <summary>
		///  Initializes a new instance of the DataGridCellPresenter class.
		/// </summary>
		public DataGridCellPresenter()
		{
			_currencyVisualHelper = new BorderHelper(this);
			_focusVisualPrimaryHelper = new BorderHelper(this);
			_focusVisualSecondaryHelper = new BorderHelper(this);
		}

		#endregion Constructors

		#region Properties

		public Brush Background
		{
			get { return (Brush)GetValue(BackgroundProperty); }
			set { SetValue(BackgroundProperty, value); }
		}

		public Brush CurrencyVisualBrush
		{
			get => (Brush)GetValue(CurrencyVisualBrushProperty);
			set => SetValue(CurrencyVisualBrushProperty, value);
		}

		public double CurrencyVisualThickness
		{
			get => (double)GetValue(CurrencyVisualThicknessProperty);
			set => SetValue(CurrencyVisualThicknessProperty, value);
		}

		public Brush FocusVisualPrimaryBrush
		{
			get => (Brush)GetValue(FocusVisualPrimaryBrushProperty);
			set => SetValue(FocusVisualPrimaryBrushProperty, value);
		}

		public double FocusVisualPrimaryThickness
		{
			get => (double)GetValue(FocusVisualPrimaryThicknessProperty);
			set => SetValue(FocusVisualPrimaryThicknessProperty, value);
		}

		public Brush FocusVisualSecondaryBrush
		{
			get => (Brush)GetValue(FocusVisualSecondaryBrushProperty);
			set => SetValue(FocusVisualSecondaryBrushProperty, value);
		}

		public double FocusVisualSecondaryThickness
		{
			get => (double)GetValue(FocusVisualSecondaryThicknessProperty);
			set => SetValue(FocusVisualSecondaryThicknessProperty, value);
		}

		public bool IsCurrencyVisualVisible
		{
			get => (bool)GetValue(IsCurrencyVisualVisibleProperty);
			set => SetValue(IsCurrencyVisualVisibleProperty, value);
		}

		public bool IsFocusVisualVisible
		{
			get => (bool)GetValue(IsFocusVisualVisibleProperty);
			set => SetValue(IsFocusVisualVisibleProperty, value);
		}

		#endregion Properties

		#region Methods

		private static void OnCurrencyVisualThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((DataGridCellPresenter)d)._currencyVisualHelper.ClearPenCache();
		}

		private static void OnFocusVisualPrimaryThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((DataGridCellPresenter)d)._focusVisualPrimaryHelper.ClearPenCache();
		}

		private static void OnFocusVisualSecondaryThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((DataGridCellPresenter)d)._focusVisualSecondaryHelper.ClearPenCache();
		}

		protected override void OnRender(DrawingContext dc)
		{
			Brush background = Background;
			if (background != null)
			{
				dc.DrawRectangle(background, null, new Rect(RenderSize));
			}

			base.OnRender(dc);

			if (IsCurrencyVisualVisible)
			{
				_currencyVisualHelper.DrawBorder(dc, CurrencyVisualBrush, CurrencyVisualThickness);
			}

			if (IsFocusVisualVisible)
			{
				double focusVisualPrimaryThickness = FocusVisualPrimaryThickness;
				_focusVisualPrimaryHelper.DrawBorder(dc, FocusVisualPrimaryBrush, focusVisualPrimaryThickness);
				_focusVisualSecondaryHelper.DrawBorder(dc, FocusVisualSecondaryBrush, FocusVisualSecondaryThickness, focusVisualPrimaryThickness);
			}
		}

		#endregion Methods

		#region Classes

		private class BorderHelper
		{
			#region Fields

			private readonly UIElement _owner;

			#endregion Fields

			#region Constructors

			public BorderHelper(UIElement owner)
			{
				_owner = owner;
			}

			#endregion Constructors

			#region Properties

			private Pen PenCache { get; set; }

			private Size RenderSize => _owner.RenderSize;

			#endregion Properties

			#region Methods

			public void ClearPenCache()
			{
				PenCache = null;
			}

			public void DrawBorder(
				DrawingContext dc,
				Brush brush,
				double thickness,
				double margin = 0)
			{
				if (thickness > 0 && brush != null)
				{
					Pen pen = PenCache;
					if (pen == null)
					{
						pen = new Pen(brush, thickness);

						if (brush.IsFrozen)
						{
							pen.Freeze();
						}

						PenCache = pen;
					}

					double halfThickness = thickness * 0.5;

					Rect rect = new Rect(
						new Point(margin + halfThickness,
								  margin + halfThickness),
						new Point(RenderSize.Width - margin - halfThickness,
								  RenderSize.Height - margin - halfThickness));

					//GuidelineSet guidelines = new GuidelineSet();
					//guidelines.GuidelinesX.Add(rect.Left + halfThickness);
					//guidelines.GuidelinesX.Add(rect.Right + halfThickness);
					//guidelines.GuidelinesY.Add(rect.Top + halfThickness);
					//guidelines.GuidelinesY.Add(rect.Bottom + halfThickness);

					//dc.PushGuidelineSet(guidelines);
					dc.DrawRectangle(null, pen, rect);

					//dc.Pop();
				}
			}

			#endregion Methods
		}

		#endregion Classes
	}
}