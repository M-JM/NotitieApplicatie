using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels.NotitieViewmodels
{
    public class NotitieLijstViewModel : BaseViewModel
    {

		private ObservableCollection<Notitie> _notities;

		public ObservableCollection<Notitie> Notities
		{
			get { return _notities; }
			set 
				
				{
					SetProperty(ref _notities, value);
				}
						
		}

		private NotitieBoek _geselecteerdeNotitieBoek;

		public NotitieBoek GeselecteerdeNotitieBoek
		{
			get { return _geselecteerdeNotitieBoek; }
			set
			{
				SetProperty(ref _geselecteerdeNotitieBoek, value);
				if(value != null) { 
				Notities =  new ObservableCollection<Notitie>(DbRepository.Notitielijst().Where(x => x.NotitieBoek.NotitieBoekId == GeselecteerdeNotitieBoek.NotitieBoekId));
				}
			}
		}

		public NotitieLijstViewModel()
		{
			Titel = "Mijn Notities";
			Notities = new ObservableCollection<Notitie>();

		}


	}
}
