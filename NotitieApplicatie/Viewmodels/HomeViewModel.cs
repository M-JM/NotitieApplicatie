﻿using NotitieApplicatie.DataAccessLayer;
using NotitieApplicatie.Mediator;
using NotitieApplicatie.Viewmodels.NotitieBoekenViewmodels;
using NotitieApplicatie.Viewmodels.NotitieViewmodels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels
{
    public class HomeViewModel: BaseViewModel
    {

        #region Props & Variables

        private readonly NotitieApplicatieMainViewmodel _vm;

        private BaseViewModel _notitieBoek;

        public BaseViewModel NotitieBoek
        {
            get { return _notitieBoek; }
            set
            {
                SetProperty(ref _notitieBoek, value);
            }
        }

        private BaseViewModel _notitieBoekLijst;

        public BaseViewModel NotitieBoekLijst
        {
            get { return _notitieBoekLijst; }
            set
            {
                SetProperty(ref _notitieBoekLijst, value);
            }
        }

        private BaseViewModel _notitieLijst;

        public BaseViewModel NotitieLijst
        {
            get { return _notitieLijst; }
            set
            {
                SetProperty(ref _notitieLijst, value);
            }
        }
        #endregion

        #region Constructor
        public HomeViewModel(NotitieApplicatieMainViewmodel vm)
        {

            _vm = vm;
            NotitieBoekLijst = new NotitieBoekLijstViewModel(_vm);
            NotitieBoek = new NotitieBoekViewModel(_vm);
            NotitieLijst = new NotitieLijstViewModel();

            NotitieBoekLijst.PropertyChanged += NotitieBoekLijst_PropertyChanged;
            NotitieBoek.PropertyChanged += NotitieBoek_PropertyChanged;
        }
        #endregion

        #region Methods

        private void NotitieBoek_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "VerwijderdeNotitieboek":
                    (NotitieBoekLijst as NotitieBoekLijstViewModel).VerwijderdeNotitieboek = (NotitieBoek as NotitieBoekViewModel).VerwijderdeNotitieboek;

                    break;
                default:
                    break;
            }
        }

        private void NotitieBoekLijst_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {
                case "GeselecteerdeNotitieBoek":
                    (NotitieBoek as NotitieBoekViewModel).GeselecteerdeNotitieBoek = (NotitieBoekLijst as NotitieBoekLijstViewModel).GeselecteerdeNotitieBoek;
                    (NotitieLijst as NotitieLijstViewModel).GeselecteerdeNotitieBoek = (NotitieBoekLijst as NotitieBoekLijstViewModel).GeselecteerdeNotitieBoek;
                    break;

                default:
                    break;
            }
        }

        #endregion







    }
}
