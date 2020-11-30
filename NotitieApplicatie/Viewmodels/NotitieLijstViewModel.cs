using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels
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
				Console.WriteLine("I changed from notitielijst");
				}
						
		}


		private NotitieBoek _geselecteerdeNotitieBoek;

		public NotitieBoek GeselecteerdeNotitieBoek
		{
			get { return _geselecteerdeNotitieBoek; }
			set
			{
				SetProperty(ref _geselecteerdeNotitieBoek, value);
				Notities =  new ObservableCollection<Notitie>(DbRepository.Notitielijst().Where(x => x.NotitieBoek.Id == GeselecteerdeNotitieBoek.Id));
			}
		}

	

		public NotitieLijstViewModel()
		{

			Notities = new ObservableCollection<Notitie>();

		}

	

	}
}
