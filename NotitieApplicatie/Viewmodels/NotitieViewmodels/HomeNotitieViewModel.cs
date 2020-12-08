using NotitieApplicatie.Viewmodels.NotitieBoekenViewmodels;
using System;
using System.Collections.Generic;
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

        private BaseViewModel _notitieLijst;

        public BaseViewModel NotitieLijst
        {
            get { return _notitieLijst; }
            set
            {
                SetProperty(ref _notitieLijst, value);
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
            NotitieLijst = new NotitieLijstViewModel();
            Notitie = new NotitieViewModel();

        }

    }
}
