using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Transaction.EditingMoneyOperation
{
	public class ListBoxSeledtItemsBehavior : Behavior<ListBox>
	{
		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;			
		}

		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs args)
		{
			base.OnPropertyChanged(args);
			if (args.Property.Name == nameof(SelectedItems))
			{
				if (args.OldValue != null && args.OldValue is INotifyCollectionChanged)
					((INotifyCollectionChanged)args.OldValue).CollectionChanged -= ListBoxSeledtItemsBehavior_CollectionChanged;

				if (args.NewValue != null && args.NewValue is INotifyCollectionChanged)
					((INotifyCollectionChanged)args.NewValue).CollectionChanged += ListBoxSeledtItemsBehavior_CollectionChanged;
			}
		}

		private void ListBoxSeledtItemsBehavior_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (var item in e.NewItems)
						AssociatedObject.SelectedItems.Add(item);

					break;

				case NotifyCollectionChangedAction.Remove:
					foreach (var item in e.OldItems)
						AssociatedObject.SelectedItems.Remove(item);

					break;

				case NotifyCollectionChangedAction.Replace:
					break;
				case NotifyCollectionChangedAction.Move:
					break;
				case NotifyCollectionChangedAction.Reset:
					break;
				default:
					break;
			}
		}

		private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs args)
		{
			if (args.AddedItems.Count != 0)
			{
				foreach (var item in args.AddedItems.OfType<object>())
					SelectedItems.Add(item);
			}
			else if (args.RemovedItems.Count != 0)
			{
				foreach (var item in args.RemovedItems.OfType<object>())
					SelectedItems.Remove(item);
			}

			args.Handled = true;
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
		}

		public IList SelectedItems
		{
			get { return (IList) GetValue(SelectedItemsProperty); }
			set { SetValue(SelectedItemsProperty, value); }
		}

		// Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty SelectedItemsProperty =
			DependencyProperty.Register("SelectedItems", typeof(IList), typeof(ListBoxSeledtItemsBehavior), new PropertyMetadata());
	}
}
