﻿using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels
{
    public class ProfileViewModel : BaseViewModel
    {
        /// <summary>
        /// 
        /// Bedoeling :
        /// 
        /// Input form voor een nieuwe notitieboek
        /// -> Validatie moet gebeuren op velden en de save button moet disabled zijn zolang alle velden niet geldig zijn
        ///  Probleem met IDataError -> kan niet properties van gerelateerde objecten valideren ...
        ///  D
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
        /// 
        private NotitieBoek _notitieBoek;
        private string _naam;
        private string _beschrijving;
        private RelayCommand _maakCommand;
        private Eigenaar _eigenaar;
        private FullObservableCollection<Eigenaar> _eigenaars;

        public NotitieBoek NotitieBoekA
        {
            // kan tevens in de ctor van de viewmodel Notitieboek get als new Notitieboek.
            get { return _notitieBoek ; }
            set
            {
                SetProperty(ref _notitieBoek, value);
                NotitieBoekA.PropertyChanged += Notitieboek_Propertychanged;
               
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


        public ProfileViewModel()
        {
            Titel = "Maak een nieuwe Notitieboek";
            NotitieBoekA = new NotitieBoek("", "", null);
            MaakCommand = new RelayCommand(BewaarNotitieBoek, MagNotitieBewaren);
            Eigenaars = DbRepository.Eigenaarslijst();
            
        }

        private void Notitieboek_Propertychanged(object sender, PropertyChangedEventArgs e)
        {
            // Maakt een check of er geen errormessages meer zijn en indien ok, mag bewaren.
            
            if (NotitieBoekA.HasErrors)
            {
                MagBewaren = true;
            }
            else {
                MagBewaren = false;
            }
        }

        private void BewaarNotitieBoek(Object parameter = null)
        {
         
            //new NotitieBoek(Naam, Beschrijving, Eigenaar);
        
            DbRepository.AddNotitieBoek(NotitieBoekA);
          
            // Command button naar false zodat hij weer disabled is.
            MagBewaren = false;

            // zorgt ervoor dat na update de velden weer leeg zijn.
            NotitieBoekA = new NotitieBoek("", "", null);
                    

            // Idien ik niet de class gebruik -> properties van viewmodel naar eigen method bv. Clear() beter ? 
            //Naam = string.Empty;
            //Beschrijving = string.Empty;
            //Eigenaar = null;
               
        }
        private Boolean MagNotitieBewaren(object parameter = null)
        {
            return MagBewaren;
        }

    }
}
