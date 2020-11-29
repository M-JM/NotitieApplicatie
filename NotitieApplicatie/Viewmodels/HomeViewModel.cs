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

       

        public HomeViewModel()
        {
           
            NotitieBoekLijst = new NotitieBoekLijstViewModel();
            NotitieBoek = new NotitieBoekViewModel();
            NotitieBoekLijst.PropertyChanged += NotitieBoekLijst_PropertyChanged;
            

        }

        private void NotitieBoekLijst_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
                                 
            switch (e.PropertyName)
            {
                case "GeselecteerdeNotitieBoek":
                    (NotitieBoek as NotitieBoekViewModel).GeselecteerdeNotitieBoek = (NotitieBoekLijst as NotitieBoekLijstViewModel).GeselecteerdeNotitieBoek;
                    break;
                default:
                    break;
            }
        }



    }
}
