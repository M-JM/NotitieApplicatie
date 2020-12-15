using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotitieApplicatie.Viewmodels.NotitieViewmodels
{
    public class NotitieLijstViewModel : BaseViewModel
    {
		private NotitieBoek _verwijderdeNotitieboek;
		private ObservableCollection<Notitie> _notities;

		public ObservableCollection<Notitie> Notities
		{
			get { return _notities; }
			set 
				
				{
					SetProperty(ref _notities, value);
				}
						
		}

		public NotitieBoek VerwijderdeNotitieboek
		{
			get { return _verwijderdeNotitieboek; }
			set
			{
				SetProperty(ref _verwijderdeNotitieboek, value);
				RemoveNotities(value);

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

		private void RemoveNotities(NotitieBoek notitieBoek)
		{
			try
			{
				if (notitieBoek != null)
				{
					Notities.Clear();
				}
				else
				{
					throw new Exception();
				}
			}
			catch (Exception ex)
			{
				MyLogger.GetInstance().Warning($"Er is iets verkeerd gegaan by het verwijderen van notitiesObservablecollection + {ex}");

				MessageBox.Show("Something went horribly wrong trying to deleted the notes", "My spaghetti code failed again");
				throw;
			}
			
		}
	}
}
