using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace MyOwnLib.Common
{
	public class FullObservableCollection<T> : ObservableCollection<T> where T : ObservableObject
	{
		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			base.OnCollectionChanged(e);

			if (e.NewItems != null)
			{
				foreach (T item in e.NewItems)
					item.PropertyChanged += Item_PropertyChanged;
			}
			if (e.OldItems != null)
			{
				foreach (T item in e.OldItems)
					item.PropertyChanged -= Item_PropertyChanged;
			}
		}

		private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}
	}
}
