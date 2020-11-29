using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels
{
    public class NotitieBoekViewModel : BaseViewModel
    {
   
        /// <summary>
        /// Viewmodel voor één Notitieboek -> Moet tevens Viewmodel van notitielijst weergeven.
        /// 
        /// </summary>
     
        private NotitieBoek _geselecteerdeNotitieBoek;

        public NotitieBoek GeselecteerdeNotitieBoek
        {
            get { return _geselecteerdeNotitieBoek; }
            set
            {
                SetProperty(ref _geselecteerdeNotitieBoek, value);
                GeselecteerdeNotitieBoek.PropertyChanged += GeselecteerdeNotitieBoek_PropertyChanged;
                NotitieBoekGewijzigd = false;
            }
        }

        private ObservableCollection<Notitie> _notities;

        public ObservableCollection<Notitie> Notities
        {
            get { return _notities; }
            set
            {
                SetProperty(ref _notities, value);
            }
        }


        private Boolean _notitieboekGewijzigd;

        public Boolean NotitieBoekGewijzigd
        {
            get { return _notitieboekGewijzigd; }
            set
            {
                SetProperty(ref _notitieboekGewijzigd, value);
            }
        }


        private RelayCommand _bewaarCommand;

        public RelayCommand BewaarCommand
        {
            get { return _bewaarCommand; }
            set
            {
                SetProperty(ref _bewaarCommand, value);
            }
        }

        public NotitieBoekViewModel()
        {
            Titel = "mijn geselecteerde notitie boek";
            BewaarCommand = new RelayCommand(BewaarNotitieBoek, MagNotitieBoekBewaren);
        }



        #region Methods
        private void BewaarNotitieBoek(Object parameter = null)
        {
            DbRepository.UpdateNotitieBoek(GeselecteerdeNotitieBoek);
            NotitieBoekGewijzigd = false;
        }

        private Boolean MagNotitieBoekBewaren(object parameter = null)
        {
            return NotitieBoekGewijzigd;
        }

        private void GeselecteerdeNotitieBoek_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotitieBoekGewijzigd = true;
        }

        #endregion


    }
}
