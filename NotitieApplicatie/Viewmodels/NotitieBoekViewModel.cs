using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NotitieApplicatie.Viewmodels
{
    public class NotitieBoekViewModel : BaseViewModel
    {
   
        /// <summary>
        /// Viewmodel voor één Notitieboek -> Moet tevens Viewmodel van notitielijst weergeven.
        /// 
        /// </summary>
     
        private NotitieBoek _geselecteerdeNotitieBoek;

        private List<Eigenaar> _eigenaars;

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
        public List<Eigenaar> Eigenaars
        {
            get { return _eigenaars; }
            set
            {
                SetProperty(ref _eigenaars, value);
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
            Eigenaars = DbRepository.Eigenaarslijst();
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
            Console.WriteLine($"changed");
            Console.WriteLine($"{GeselecteerdeNotitieBoek.Eigenaar.Naam}");
            NotitieBoekGewijzigd = true;
         

        }

        #endregion


    }
}
