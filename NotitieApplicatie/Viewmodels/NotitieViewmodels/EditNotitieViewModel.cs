using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels.NotitieViewmodels
{
    public class EditNotitieViewModel : BaseViewModel
    {
		private readonly NotitieApplicatieMainViewmodel _vm;
		private Notitie _geselecteerdeNotitie;
		private FullObservableCollection<Eigenaar> _eigenaars;
		private FullObservableCollection<Categorie> _categorieen;
		private FullObservableCollection<NotitieBoek> _notitieBoek;
		private RelayCommand _cancelCommand;
		private RelayCommand _bewaarCommand;
		private Boolean _notitieGewijzigd;
		private FullObservableCollection<NotitieBoek> _notitieBoeken;

		public Notitie GeselecteerdeNotitie
		{
			get { return _geselecteerdeNotitie; }
			set
			{
				SetProperty(ref _geselecteerdeNotitie, value);
				GeselecteerdeNotitie.PropertyChanged += GeselecteerdeNotitie_PropertyChanged;
			}
		}

		public FullObservableCollection<Eigenaar> Eigenaars
		{
			get { return _eigenaars; }
			set
			{
				SetProperty(ref _eigenaars, value);
			}
		}

		public FullObservableCollection<NotitieBoek> NotitieBoeken
		{
			get { return _notitieBoeken; }
			set
			{
				SetProperty(ref _notitieBoeken, value);
			}
		}

		public FullObservableCollection<Categorie> CategorieLijst
		{
			get { return _categorieen; }
			set
			{
				SetProperty(ref _categorieen, value);
			}
		}

		public RelayCommand BewaarCommand
		{
			get { return _bewaarCommand; }
			set
			{
				SetProperty(ref _bewaarCommand, value);
			}
		}

		public RelayCommand CancelCommand
		{
			get { return _cancelCommand; }
			set
			{
				SetProperty(ref _cancelCommand, value);
			}
		}

		public Boolean NotitieGewijzigd
		{
			get { return _notitieGewijzigd; }
			set
			{
				SetProperty(ref _notitieGewijzigd, value);
			}
		}

		public EditNotitieViewModel(NotitieApplicatieMainViewmodel vm, Notitie notitie)
		{
			Eigenaars = DbRepository.Eigenaarslijst();
			GeselecteerdeNotitie = notitie;
			_vm = vm;
			Titel = "U Editeert Notitie met Id " + $"{GeselecteerdeNotitie.Id}";
			BewaarCommand = new RelayCommand(BewaarNotitie, MagNotitieBewaren);
			CancelCommand = new RelayCommand(CancelNotitie);
		}

		private void GeselecteerdeNotitie_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			NotitieGewijzigd = true;
			Console.WriteLine("i triggered");
		}
		private void BewaarNotitie(Object parameter = null)
		{
			DbRepository.UpdateNotitie(GeselecteerdeNotitie);
			NotitieGewijzigd = false;
			_vm.SelectedView = new HomeNotitieViewModel(_vm);
		}

		private void CancelNotitie(Object parameter = null)
		{
			_vm.SelectedView = new HomeNotitieViewModel(_vm);
		}

		private Boolean MagNotitieBewaren(object parameter = null)
		{
			return NotitieGewijzigd;
		}



	}
}
