using System.Windows;

namespace ModernWpf.Controls.Primitives
{
	public class VisualStateGroupListener : FrameworkElement
	{
		#region Fields

		private static readonly DependencyPropertyKey CurrentStateNamePropertyKey =
			DependencyProperty.RegisterReadOnly(
				nameof(CurrentStateName),
				typeof(string),
				typeof(VisualStateGroupListener),
				null);

		public static readonly DependencyProperty CurrentStateNameProperty =
			CurrentStateNamePropertyKey.DependencyProperty;

		public static readonly DependencyProperty GroupProperty =
			DependencyProperty.Register(
				nameof(Group),
				typeof(VisualStateGroup),
				typeof(VisualStateGroupListener),
				new PropertyMetadata(OnGroupChanged));

		public static readonly DependencyProperty ListenerProperty =
			DependencyProperty.RegisterAttached(
				"Listener",
				typeof(VisualStateGroupListener),
				typeof(VisualStateGroupListener),
				new PropertyMetadata(OnListenerChanged));

		#endregion Fields

		#region Constructors

		static VisualStateGroupListener()
		{
			VisibilityProperty.OverrideMetadata(typeof(VisualStateGroupListener), new FrameworkPropertyMetadata(Visibility.Collapsed));
		}

		public VisualStateGroupListener()
		{
		}

		#endregion Constructors

		#region Properties

		public string CurrentStateName
		{
			get => (string)GetValue(CurrentStateNameProperty);
			private set => SetValue(CurrentStateNamePropertyKey, value);
		}

		public VisualStateGroup Group
		{
			get => (VisualStateGroup)GetValue(GroupProperty);
			set => SetValue(GroupProperty, value);
		}

		#endregion Properties

		#region Methods

		private static void OnGroupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((VisualStateGroupListener)d).OnGroupChanged((VisualStateGroup)e.OldValue, (VisualStateGroup)e.NewValue);
		}

		private static void OnListenerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.OldValue is VisualStateGroupListener oldListener)
			{
				oldListener.ClearValue(GroupProperty);
			}

			if (e.NewValue is VisualStateGroupListener newListener)
			{
				newListener.Group = (VisualStateGroup)d;
			}
		}

		private void OnCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
		{
			UpdateCurrentStateName(e.NewState);
		}

		private void OnGroupChanged(VisualStateGroup oldGroup, VisualStateGroup newGroup)
		{
			if (oldGroup != null)
			{
				oldGroup.CurrentStateChanged -= OnCurrentStateChanged;
			}

			if (newGroup != null)
			{
				newGroup.CurrentStateChanged += OnCurrentStateChanged;
			}

			UpdateCurrentStateName(newGroup?.CurrentState);
		}

		private void UpdateCurrentStateName(VisualState currentState)
		{
			if (currentState != null)
			{
				CurrentStateName = currentState.Name;
			}
			else
			{
				ClearValue(CurrentStateNamePropertyKey);
			}
		}

		public static VisualStateGroupListener GetListener(VisualStateGroup group)
		{
			return (VisualStateGroupListener)group.GetValue(ListenerProperty);
		}

		public static void SetListener(VisualStateGroup group, VisualStateGroupListener value)
		{
			group.SetValue(ListenerProperty, value);
		}

		#endregion Methods
	}
}