using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels.NotitieBoekenViewmodels
{
    public class EditNotitieBoekViewModel : BaseViewModel
    {

        #region Properties and Variables
        private readonly NotitieApplicatieMainViewmodel _vm;
        private NotitieBoek _geselecteerdeNotitieBoek;
        private Boolean _notitieboekGewijzigd;
        private RelayCommand _bewaarCommand;
    
        private FullObservableCollection<Eigenaar> _eigenaars;

        public NotitieBoek GeselecteerdeNotitieBoek
        {
            get { return _geselecteerdeNotitieBoek; }
            set
            {
                SetProperty(ref _geselecteerdeNotitieBoek, value);
                GeselecteerdeNotitieBoek.PropertyChanged += GeselecteerdeNotitieBoek_PropertyChanged;

                /// Noodzakelijk om de huidige eigenaar van mijn Gelesecteerde notitieboek weer te geven als de selecteditem van de ComboBox.
                /// ComboBox sourceItemList is een observable -> de selectedItem moet uit die SourceItemlist komen (Indexvalues)
                /// Dus hier moet ik de waarde van eigenaar in mijn notitieboek gaan instellen met dezelfde waarde uit lijst van Eigenaars
                /// Combobox kent alleen waardes uit de lijst waarmee hij gepopuleerd wordt !!!
                /// Verder opzoeken of hier geen beter manier voor besta ??)

              if(value != null)
                {
                    foreach (Eigenaar item in Eigenaars)
                    {
                        if(item.Id == value.Eigenaar.Id)
                        {
                            GeselecteerdeNotitieBoek.Eigenaar = item;
                        }
                    }
                }
               
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

        public Boolean NotitieBoekGewijzigd
        {
            get { return _notitieboekGewijzigd; }
            set
            {
                SetProperty(ref _notitieboekGewijzigd, value);
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

        private RelayCommand _cancelCommand;

        public RelayCommand CancelCommand
        {
            get { return _cancelCommand; }
            set
            {
                SetProperty(ref _cancelCommand, value);
            }
        }



        #endregion

        #region Constructor
        public EditNotitieBoekViewModel(NotitieApplicatieMainViewmodel vm, NotitieBoek notitieBoek)
        {
            Eigenaars = DbRepository.Eigenaarslijst();
            GeselecteerdeNotitieBoek = notitieBoek;
            _vm = vm;
            Titel = "mijn geselecteerde notitie boek";
            BewaarCommand = new RelayCommand(BewaarNotitieBoek, MagNotitieBoekBewaren);
            CancelCommand = new RelayCommand(CancelNotitieBoek);
        }

        #endregion

        #region Methods
        private void BewaarNotitieBoek(Object parameter = null)
        {
            DbRepository.UpdateNotitieBoek(GeselecteerdeNotitieBoek);
            NotitieBoekGewijzigd = false;
            _vm.SelectedView = new HomeViewModel(_vm);
        }

        private void CancelNotitieBoek(Object parameter = null)
        {
            _vm.SelectedView = new HomeViewModel(_vm);
        }

        private Boolean MagNotitieBoekBewaren(object parameter = null)
        {
            return NotitieBoekGewijzigd;
        }

        private void GeselecteerdeNotitieBoek_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotitieBoekGewijzigd = true;
            Console.WriteLine("i triggered");

        }

        #endregion

    }
}
