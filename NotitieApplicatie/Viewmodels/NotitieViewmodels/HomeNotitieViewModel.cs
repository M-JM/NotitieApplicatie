using NotitieApplicatie.Viewmodels.NotitieBoekenViewmodels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels.NotitieViewmodels
{
    class HomeNotitieViewModel : BaseViewModel
    {
        private readonly NotitieApplicatieMainViewmodel _vm;
        private BaseViewModel _notitieBoekLijst;

        public BaseViewModel NotitieBoekLijst
        {
            get { return _notitieBoekLijst; }
            set
            {
                SetProperty(ref _notitieBoekLijst, value);
            }
        }

        private BaseViewModel _allenotitieLijst;

        public BaseViewModel AlleNotitieLijst
        {
            get { return _allenotitieLijst; }
            set
            {
                SetProperty(ref _allenotitieLijst, value);
            }
        }

        private BaseViewModel _notitie;

        public BaseViewModel Notitie
        {
            get { return _notitie; }
            set
            {
                SetProperty(ref _notitie, value);
            }
        }


        public HomeNotitieViewModel(NotitieApplicatieMainViewmodel vm)
        {
            _vm = vm;
            NotitieBoekLijst = new NotitieBoekLijstViewModel(_vm);
            AlleNotitieLijst = new AlleNotitiesViewModel(_vm);
            Notitie = new NotitieViewModel(_vm);
            AlleNotitieLijst.PropertyChanged += AlleNotitieLijst_PropertyChanged;
            Notitie.PropertyChanged += Notitie_PropertyChanged;

        }

        private void Notitie_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "VerwijderdeNotitie":
                    (AlleNotitieLijst as AlleNotitiesViewModel).VerwijderdeNotitie = (Notitie as NotitieViewModel).VerwijderdeNotitie;

                    break;
                default:
                    break;
            }
        }

        private void AlleNotitieLijst_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "GeselecteerdeNotitie":
                    (Notitie as NotitieViewModel).GeselecteerdeNotitie = (AlleNotitieLijst as AlleNotitiesViewModel).GeselecteerdeNotitie;
                    break;
                default:
                    break;

            }
        }
    }
}
