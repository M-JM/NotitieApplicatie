using NotitieApplicatie.DataAccessLayer;
using NotitieApplicatie.Mediator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels
{
    public class HomeViewModel: BaseViewModel
    {

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

        protected IMediator Mediator;

        public HomeViewModel()
        {
            IMediator Mediator = new Mediatora();
            this.Mediator = Mediator;
            
            NotitieBoekLijst = new NotitieBoekLijstViewModel();
            NotitieBoek = new NotitieBoekViewModel();
            NotitieLijst = new NotitieLijstViewModel();
            Mediator.AddParticipants(NotitieBoekLijst);
            Mediator.AddParticipants(NotitieBoek);
            Mediator.AddParticipants(NotitieLijst);
            NotitieBoekLijst.PropertyChanged += NotitieBoekLijst_PropertyChanged;
          //  NotitieBoek.PropertyChanged += NotitieBoek_PropertyChanged;
            NotitieLijst.PropertyChanged += NotitieLijst_PropertyChanged;
        }

        private void NotitieBoekLijst_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Mediator.SendMessageToAllParticipants("test", NotitieBoekLijst);
                                 
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
        private void NotitieBoek_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Mediator.SendMessageToAllParticipants("Blabla", NotitieBoek);

    
        }

        private void NotitieLijst_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Mediator.SendMessageToAllParticipants("aaaaa", NotitieLijst);


        }



    }
}
