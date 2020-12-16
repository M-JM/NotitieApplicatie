using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace NotitieApplicatie.Viewmodels.NotitieViewmodels
{
   public class AlleNotitiesViewModel : BaseViewModel
    {
		private readonly NotitieApplicatieMainViewmodel _vm;
		private RelayCommand _addNewNotitie;

		public RelayCommand AddNewNotitie
		{
			get { return _addNewNotitie; }
			set
			{
				SetProperty(ref _addNewNotitie, value);
			}
		}


		private ICollectionView _collectionView;

		public ICollectionView CollectionView
		{
			get { return _collectionView; }
			set
			{
				SetProperty(ref _collectionView, value);
			}
		}

		private FullObservableCollection<Notitie> _notities;

		public FullObservableCollection<Notitie> Notities
		{
			get { return _notities; }
			set

			{
				SetProperty(ref _notities, value);
		
			}

		}

		private Notitie _geselecteerdeNotitie;

		public Notitie GeselecteerdeNotitie
		{
			get { return _geselecteerdeNotitie; }
			set
			{
				SetProperty(ref _geselecteerdeNotitie, value);

			}
		}

		private Notitie _verwijderdeNotitie;

		public Notitie VerwijderdeNotitie
		{
			get { return _verwijderdeNotitie; }
			set
			{
				SetProperty(ref _verwijderdeNotitie, value);
				RemoveNotitie(value);
				CollectionView.Refresh();
			}
		}

		private FullObservableCollection<Eigenaar> _eigenaars;

		public FullObservableCollection<Eigenaar> Eigenaars
		{
			get { return _eigenaars; }
			set
			{
				SetProperty(ref _eigenaars, value);
			}
		}

		private FullObservableCollection<NotitieBoek> _notitieBoeken;

		public FullObservableCollection<NotitieBoek> NotitieBoeken 
		{
			get { return _notitieBoeken; }
			set
			{
				SetProperty(ref _notitieBoeken, value);
			}
		}

		private string _search;
	

		public string  Search
		{
			get { return _search; }
			set
			{
				_search = value;
				OnPropertyChanged(nameof(Search));
				CollectionView.Refresh();
			}
		}

		

		public AlleNotitiesViewModel(NotitieApplicatieMainViewmodel vm)
		{
			_vm = vm;
			Titel = "Al mijn notities";
			Notities = new FullObservableCollection<Notitie>(DbRepository.Notitielijst().ToList());
			CollectionView = CollectionViewSource.GetDefaultView(Notities);
			AddNewNotitie = new RelayCommand(ShowInfoView);
			Search = "";
			CollectionView.Filter = new Predicate<object>(o => Filter(o as Notitie)); ;

		}

		private bool Filter(Notitie notitie)
		{
			return Search == null
		   || notitie.Titel.IndexOf(Search, StringComparison.OrdinalIgnoreCase) != -1
		   || notitie.Inhoud.IndexOf(Search, StringComparison.OrdinalIgnoreCase) != -1
		   || notitie.Eigenaar.Naam.IndexOf(Search, StringComparison.OrdinalIgnoreCase) != -1
		   || notitie.Categorie.Naam.IndexOf(Search, StringComparison.OrdinalIgnoreCase) != -1
		   || notitie.NotitieBoek.Naam.IndexOf(Search, StringComparison.OrdinalIgnoreCase) != -1;
		}

		private void VerwijderdeNotitie_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			Notities.Remove(VerwijderdeNotitie);		
		}

		private void RemoveNotitie(Notitie notitie)
		{
			try
			{
				Notities.Remove(notitie);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex + "You cannot delete nothing");
			}

		}

		private void ShowInfoView(Object parameter = null)
		{
			_vm.SelectedView = new AddNotitieViewModel(_vm);
		}


	}
}
