using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace MyOwnLib.Common
{
	public class ObservableObject : INotifyPropertyChanged
	{
		private Boolean _isModified;

		public Boolean IsModified
		{
			get { return _isModified; }
			set { _isModified = value; }
		}

		public void SetProperty<T>(ref T field, T value, [CallerMemberName] String propertyName = null)
		{
			if (!Object.Equals(field, value))
			{
				field = value;
				OnPropertyChanged(propertyName);
				IsModified = true;
			}
		}

		protected void OnPropertyChanged(String propertyName)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			PropertyChanged?.Invoke(this, e);
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
