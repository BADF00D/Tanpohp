#region usings

using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace Tanpohp.Wpf.AttachedProperty
{
	public class ListboxIsAutoscrollDependencyObject : DependencyObject
	{
		protected static readonly DependencyProperty IsAutoscrollProperty
			= DependencyProperty.RegisterAttached("IsAutoscroll", typeof (bool), typeof (ListboxIsAutoscrollDependencyObject),
			                                      new UIPropertyMetadata(default(bool), OnIsAutoscrollChanged));

		public static bool GetIsAutoscroll(DependencyObject obj)
		{
			return (bool) obj.GetValue(IsAutoscrollProperty);
		}

		public static void SetIsAutoscroll(DependencyObject obj, bool value)
		{
			obj.SetValue(IsAutoscrollProperty, value);
		}

		private static void OnIsAutoscrollChanged(DependencyObject dependencyObject,
		                                          DependencyPropertyChangedEventArgs propertyChanged)
		{
			var value = (bool) propertyChanged.NewValue;
			var listBox = (ListBox) dependencyObject;
			var itemCollection = listBox.Items;
			var notifyCollection = (INotifyCollectionChanged) itemCollection.SourceCollection;

			var onCollectionChanged = new NotifyCollectionChangedEventHandler(
				(s1, changedArgs) =>
					{
						var selectedItem = default(object);
						switch (changedArgs.Action)
						{
							case NotifyCollectionChangedAction.Add:
							case NotifyCollectionChangedAction.Move:
								selectedItem = changedArgs.NewItems[changedArgs.NewItems.Count - 1];
								break;
							case NotifyCollectionChangedAction.Remove:
								if (itemCollection.Count < changedArgs.OldStartingIndex)
								{
									selectedItem = itemCollection[changedArgs.OldStartingIndex - 1];
								}
								else if (itemCollection.Count > 0)
									selectedItem = itemCollection[0];
								break;
							case NotifyCollectionChangedAction.Reset:
								if (itemCollection.Count > 0)
									selectedItem = itemCollection[0];
								break;
						}

						if (selectedItem == default(object)) return;

						itemCollection.MoveCurrentTo(selectedItem);
						listBox.ScrollIntoView(selectedItem);
					});

			if (value) notifyCollection.CollectionChanged += onCollectionChanged;
			else notifyCollection.CollectionChanged -= onCollectionChanged;
		}
	}
}