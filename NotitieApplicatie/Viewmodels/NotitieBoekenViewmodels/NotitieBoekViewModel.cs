using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using NotitieApplicatie.Mediator;
using NotitieApplicatie.Viewmodels.NotitieBoekenViewmodels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NotitieApplicatie.Viewmodels.NotitieBoekenViewmodels
{
    public class NotitieBoekViewModel : BaseViewModel
    {

        #region Properties & Variables

        private readonly NotitieApplicatieMainViewmodel _vm;
        protected IMediator MediatorInstance;
        private NotitieBoek _geselecteerdeNotitieBoek;
        private FullObservableCollection<Eigenaar> _eigenaars;
        private Boolean _notitieboekGewijzigd;

        private NotitieBoek _verwijderdeNotitieboek;
        private bool _notitieBoekDeleten;

        private RelayCommand _bewaarCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _editCommand;
        private Boolean _notitieBoekEditeren;

        public RelayCommand EditCommand
        {
            get { return _editCommand; }
            set
            {
                SetProperty(ref _editCommand, value);
            }
        }

        public NotitieBoek GeselecteerdeNotitieBoek
        {
            get { return _geselecteerdeNotitieBoek; }
            set
            {
                SetProperty(ref _geselecteerdeNotitieBoek, value);
                if (value != null)
                {
                    GeselecteerdeNotitieBoek.PropertyChanged += GeselecteerdeNotitieBoek_PropertyChanged;
                    NotitieBoekDeleten = true;
                    NotitieBoekEditeren = true;
                }
                NotitieBoekGewijzigd = false;

            }
        }

        public NotitieBoek VerwijderdeNotitieboek
        {
            get { return _verwijderdeNotitieboek; }
            set
            {
                SetProperty(ref _verwijderdeNotitieboek, value);
                VerwijderdeNotitieboek.PropertyChanged += VerwijderdeNotitieboek_PropertyChanged;

            }
        }
        public Boolean NotitieBoekGewijzigd
        {
            get { return _notitieboekGewijzigd; }
            set
            {
                SetProperty(ref _notitieboekGewijzigd, value);
            }
        }

        public Boolean NotitieBoekEditeren
        {
            get { return _notitieBoekEditeren; }
            set
            {
                SetProperty(ref _notitieBoekEditeren, value);
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

        public Boolean NotitieBoekDeleten
        {
            get { return _notitieBoekDeleten; }
            set
            {
                SetProperty(ref _notitieBoekDeleten, value);
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

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand; }
            set
            {
                SetProperty(ref _deleteCommand, value);
            }
        }

        #endregion

        #region Constructor
        public NotitieBoekViewModel(NotitieApplicatieMainViewmodel vm)
        {

            _vm = vm;
            Titel = "mijn geselecteerde notitie boek";
            Eigenaars = DbRepository.Eigenaarslijst();
            BewaarCommand = new RelayCommand(BewaarNotitieBoek, MagNotitieBoekBewaren);
            DeleteCommand = new RelayCommand(DeleteNotitieBoek, MagNotitieBoekDeleten);
            EditCommand = new RelayCommand(EditNotitieBoek, MagNotitieBoekEditeren);

        }



        #endregion

        #region Methods


        private void EditNotitieBoek(Object parameter = null)
        {
            _vm.SelectedView = new EditNotitieBoekViewModel(_vm, GeselecteerdeNotitieBoek);
        }

        private void BewaarNotitieBoek(Object parameter = null)
        {
            DbRepository.UpdateNotitieBoek(GeselecteerdeNotitieBoek);
            NotitieBoekGewijzigd = false;
            Console.WriteLine($"updated");
        }

        private void DeleteNotitieBoek(Object parameter = null)
        {
            try
            {
                 
                DbRepository.RemoveNotitieBoek(GeselecteerdeNotitieBoek);
                VerwijderdeNotitieboek = GeselecteerdeNotitieBoek;
                NotitieBoekDeleten = false;
                NotitieBoekEditeren = false;
            }
            catch (Exception)
            {
            }
           
           

            Console.WriteLine($"deleted");
        }

        private Boolean MagNotitieBoekBewaren(object parameter = null)
        {
            return NotitieBoekGewijzigd;
        }

        private Boolean MagNotitieBoekDeleten(object parameter = null)
        {
            return NotitieBoekDeleten;
        }

        private bool MagNotitieBoekEditeren(object arg)
        {
            return NotitieBoekEditeren;
        }

        private void GeselecteerdeNotitieBoek_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Console.WriteLine($"changed");
            NotitieBoekGewijzigd = true;
         

        }

        private void VerwijderdeNotitieboek_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine($"deleted");
            NotitieBoekGewijzigd = true;
        }


        #endregion


    }
}
