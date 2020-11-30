using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels
{
   public class NotitieBoekLijstViewModel : BaseViewModel
    {
        // Used to as DataSource for the list 

        private ObservableCollection<NotitieBoek> _notitieboeken;

        public ObservableCollection<NotitieBoek> Notitieboeken
        {
            get { return _notitieboeken; }
            set
            {
                SetProperty(ref _notitieboeken, value);
                
            }
        }

        // Used to know which notebook has been selected

        private NotitieBoek _geselecteerdeNotitieBoek;

        public NotitieBoek GeselecteerdeNotitieBoek
        {
            get { return _geselecteerdeNotitieBoek; }
            set
            {
                SetProperty(ref _geselecteerdeNotitieBoek, value);
            }
        }



        public NotitieBoekLijstViewModel()
        {
            Notitieboeken = new ObservableCollection<NotitieBoek>(DbRepository.Notitieboeklijst());
            Titel = "mijn notitie boeken";
            
        }



    }
}
