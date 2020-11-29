using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels
{
    public class ProfileViewModel : BaseViewModel
    {

        private NotitieBoek _notitieBoek;
        private string _naam;
        private string _beschrijving;
        private RelayCommand _maakCommand;

        /// <summary>
        /// 
        /// Bedoeling :
        /// 
        /// Input form voor een nieuwe notitieboek
        /// -> Validatie moet gebeuren op velden en de save button moet disabled zijn zolang alle velden niet geldig zijn
        /// 
        /// 
        /// Verder uitzoeken :
        /// 
        /// Twee manieren om te binden naar input velden
        /// Oftewel de Notitieboek object instanceren en de properties ervan updaten door middel van Binding
        /// 
        /// Oftewel de properties van een notitieboek declareren en individueel updaten
        /// De ctor van notitieboek oproepen , properties meegeven als parameter in de ctor en het object doorgeven.
        /// 
        /// -> probleem is door het niet gebruiken van DTO en meteen de models te gebruiken uit datalaag is dat validatie regels daar worden geschreven.
        /// Bij IdataErrorInfo moet men een een sting prop hebben ( gebruik maken van NotMapped dataAnnotation ?)
        /// </summary>

        public NotitieBoek NotitieBoek
        {
            // kan tevens in de ctor van de viewmodel Notitieboek get als new Notitieboek.
            get { return _notitieBoek ?? (_notitieBoek = new NotitieBoek("","",new Eigenaar("",""))); ; }
            set
            {
                SetProperty(ref _notitieBoek, value);

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

        private Eigenaar _eigenaar;

        public Eigenaar Eigenaar
        {
            get { return _eigenaar; }
            set
            {
                SetProperty(ref _eigenaar, value);
            }
        }


        private List<Eigenaar> _eigenaars;

        public List<Eigenaar> Eigenaars
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


        public ProfileViewModel()
        {
            Titel = "Maak een nieuwe Notitieboek";
            MaakCommand = new RelayCommand(BewaarNotitieBoek);
            Eigenaars = DbRepository.Eigenaarslijst();
        }



        private void BewaarNotitieBoek(Object parameter = null)
        {
            NotitieBoek notitieboek = NotitieBoek;

            //new NotitieBoek(Naam, Beschrijving, Eigenaar);


            DbRepository.AddNotitieBoek(notitieboek);

            // zorgt ervoor dat na update de velden weer leeg zijn door de properties leeg te maken
            // naar eigen method bv. Clear() beter ? 
            Naam = string.Empty;
            Beschrijving = string.Empty;
            Eigenaar = null;
               
        }


    }
}
