using System.Windows;

namespace ModernWpf.Controls
{
	/// <summary>
	///  Default styles for controls.
	/// </summary>
	public class XamlControlsResources : ResourceDictionary
	{
		#region Fields

		private static ResourceDictionary _compactResources;

		private static ResourceDictionary _controlsResources;

		private static ResourceDictionary _uiSettingsResources;

		private bool _useCompactResources;

		#endregion Fields

		#region Constructors

		/// <summary>
		///  Initializes a new instance of the XamlControlsResources class.
		/// </summary>
		public XamlControlsResources()
		{
			MergedDictionaries.Add(ControlsResources);
			MergedDictionaries.Add(UISettingsResources);

			if (DesignMode.DesignModeEnabled)
			{
				_ = CompactResources;
			}
		}

		#endregion Constructors

		#region Properties

		internal static ResourceDictionary CompactResources
		{
			get
			{
				if (_compactResources == null)
				{
					_compactResources = new ResourceDictionary { Source = PackUriHelper.GetAbsoluteUri("DensityStyles/Compact.xaml") };
				}
				return _compactResources;
			}
		}

		internal static ResourceDictionary ControlsResources
		{
			get
			{
				if (_controlsResources == null)
				{
					_controlsResources = new ResourceDictionary { Source = PackUriHelper.GetAbsoluteUri("ControlsResources.xaml") };
				}
				return _controlsResources;
			}
		}

		internal static ResourceDictionary UISettingsResources
		{
			get
			{
				return _uiSettingsResources ??= new UISettingsResources();
			}
		}

		public bool UseCompactResources
		{
			get => _useCompactResources;

			set
			{
				if (_useCompactResources != value)
				{
					_useCompactResources = value;

					if (UseCompactResources)
					{
						MergedDictionaries.Add(CompactResources);
					}
					else
					{
						MergedDictionaries.Remove(CompactResources);
					};
				}
			}
		}

		#endregion Properties
	}
}