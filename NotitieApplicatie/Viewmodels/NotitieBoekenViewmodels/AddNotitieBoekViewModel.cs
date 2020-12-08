using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels.NotitieBoekenViewmodels
{
    public class AddNotitieBoekViewModel : BaseViewModel
    {

        #region Properties & Variables

        private readonly NotitieApplicatieMainViewmodel _vm;
        private NotitieBoek _notitieBoek;
        private string _naam;
        private string _beschrijving;
        private RelayCommand _maakCommand;
        private RelayCommand _cancelCommand;
        private Eigenaar _eigenaar;
        private FullObservableCollection<Eigenaar> _eigenaars;

        public NotitieBoek NotitieBoekA
        {
          
            get { return _notitieBoek; }
            set
            {
                SetProperty(ref _notitieBoek, value);
              NotitieBoekA.PropertyChanged += Notitieboek_Propertychanged;
                Console.WriteLine("I raised prop change");

            }
        }

        private Boolean _magBewaren;

        public Boolean MagBewaren
        {
            get { return _magBewaren; }
            set
            {
                SetProperty(ref _magBewaren, value);
            }
        }

        public string Naam
        {
            get { return _naam; }
            set
            {
                SetProperty(ref _naam, value);
            }
        }

        public string Beschrijving
        {
            get { return _beschrijving; }
            set
            {
                SetProperty(ref _beschrijving, value);
            }
        }

        public Eigenaar Eigenaar
        {
            get { return _eigenaar; }
            set
            {
                SetProperty(ref _eigenaar, value);
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
        
        public RelayCommand MaakCommand
        {
            get { return _maakCommand; }
            set
            {
                SetProperty(ref _maakCommand, value);
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

        #endregion

        #region Constructor
        public AddNotitieBoekViewModel(NotitieApplicatieMainViewmodel vm)
        {
            _vm = vm;
            Titel = "Maak een nieuwe Notitieboek";
            NotitieBoekA = new NotitieBoek("", "", null);
            MaakCommand = new RelayCommand(BewaarNotitieBoek, MagNotitieBewaren);
            CancelCommand = new RelayCommand(CancelAddBoek);
            Eigenaars = DbRepository.Eigenaarslijst();
        }

        #endregion

        #region Methods

        private void Notitieboek_Propertychanged(object sender, PropertyChangedEventArgs e)
        {

            Console.WriteLine("I came from the actual prop change");
            Console.WriteLine(NotitieBoekA.Naam);
            Console.WriteLine(NotitieBoekA.HasErrors);
       
            // Maakt een check of er geen errormessages meer zijn en indien ok, mag bewaren.

            if (NotitieBoekA.HasErrors)
            {
                MagBewaren = false;
            }
            else
            {
                MagBewaren = true;
            }
        }

        private void BewaarNotitieBoek(Object parameter = null)
        {

            //new NotitieBoek(Naam, Beschrijving, Eigenaar);

            DbRepository.AddNotitieBoek(NotitieBoekA);

            // Command button naar false zodat hij weer disabled is.
            MagBewaren = false;

            // zorgt ervoor dat na update de velden weer leeg zijn.
            //NotitieBoekA = new NotitieBoek("", "", null);
            _vm.SelectedView = new HomeViewModel(_vm);

            // Idien ik niet de class gebruik -> properties van viewmodel naar eigen method bv. Clear() beter ? 
            //Naam = string.Empty;
            //Beschrijving = string.Empty;
            //Eigenaar = null;

        }

        private void CancelAddBoek(Object parameter = null)
        {
            _vm.SelectedView = new HomeViewModel(_vm);

        }

        private Boolean MagNotitieBewaren(object parameter = null)
        {
            return MagBewaren;
        }

        #endregion


    }
}
