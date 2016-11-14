using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Data;

namespace Transaction.EditingMoneyOperation
{	
	public class CategorysListViewModel
	{
		public CategorysListViewModel()
		{
			SelectedItems = new ObservableCollection<string>();

#if DEBUG
			((ObservableCollection<string>) SelectedItems).CollectionChanged += (sender, args) => Debug.WriteLine($"Collection changed: {args.Action}");
#endif
		}

		public void Initialize(ObservableCollection<string> categorys, IEnumerable<string> selectedCategories)
		{
			if (Categorys != null)
			{
				((ListCollectionView) Categorys).DetachFromSourceCollection();
				Categorys = null;
			}

			SelectedItems.Clear();
			foreach (var item in selectedCategories)
				SelectedItems.Add(item);

			Categorys = new ListCollectionView(categorys);
		}

		public ICollectionView Categorys { get; private set; }

		public ICollection<string> SelectedItems { get;}
	}
}
