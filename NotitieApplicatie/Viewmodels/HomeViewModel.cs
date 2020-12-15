using MyOwnLib.Common;
using NotitieApplicatie.Viewmodels.NotitieBoekenViewmodels;
using NotitieApplicatie.Viewmodels.NotitieViewmodels;
using System.ComponentModel;


namespace NotitieApplicatie.Viewmodels
{
    public class HomeViewModel: BaseViewModel
    {



        #region Props & Variables


        private readonly NotitieApplicatieMainViewmodel _vm;
        private BaseViewModel _notitieBoek;
        private BaseViewModel _notitieBoekLijst;
        private BaseViewModel _notitieLijst;


        public BaseViewModel NotitieBoek
        {
            get { return _notitieBoek; }
            set
            {
                SetProperty(ref _notitieBoek, value);
            }
        }

        public BaseViewModel NotitieBoekLijst
        {
            get { return _notitieBoekLijst; }
            set
            {
                SetProperty(ref _notitieBoekLijst, value);
            }
        }

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
            MyLogger.GetInstance().Info("I came from HomeViewModel");

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
