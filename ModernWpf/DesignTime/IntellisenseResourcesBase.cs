using System;
using System.ComponentModel;
using System.Windows;

namespace ModernWpf.DesignTime
{
	public abstract class IntellisenseResourcesBase : ResourceDictionary, ISupportInitialize
	{
		#region Constructors

		protected IntellisenseResourcesBase()
		{
		}

		#endregion Constructors

		#region Properties

		public new Uri Source
		{
			get => base.Source;

			set
			{
				if (DesignMode.DesignModeEnabled)
				{
					base.Source = value;
				}
			}
		}

		#endregion Properties

		#region Methods

		public new void EndInit()
		{
			Clear();
			MergedDictionaries.Clear();
			base.EndInit();
		}

		void ISupportInitialize.EndInit()
		{
			EndInit();
		}

		#endregion Methods
	}
}