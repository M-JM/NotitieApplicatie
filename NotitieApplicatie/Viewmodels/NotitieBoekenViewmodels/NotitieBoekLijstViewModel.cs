using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using NotitieApplicatie.Mediator;
using NotitieApplicatie.Viewmodels.NotitieBoekenViewmodels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotitieApplicatie.Viewmodels.NotitieBoekenViewmodels
{
   public class NotitieBoekLijstViewModel : BaseViewModel
    {
        /// <summary>
        /// 
        /// ViewModel voor lijst van notitieboeken 
        /// Deze moet gewoon de lijst van notitieboeken weergeven en
        /// 1 : luisteren naar aanpassing die zouden gebeuren in NotitieboekViewModel
        /// De observable collection moet dan veranderen
        /// 2 : De geselecteerde notitieboek doorsturen naar de notitieboekViewModel
        ///         /// 
        /// </summary>

        #region Properties and variables
        protected IMediator MediatorInstance;
        private readonly NotitieApplicatieMainViewmodel _vm;
        private FullObservableCollection<NotitieBoek> _notitieboeken;
        private NotitieBoek _geselecteerdeNotitieBoek;
        private RelayCommand _addNotitieBoekCommand;
        private NotitieBoek _verwijderdeNotitieboek;

        public FullObservableCollection<NotitieBoek> Notitieboeken
        {
            get { return _notitieboeken; }
            set
            {
                SetProperty(ref _notitieboeken, value);

            }
        }

        public NotitieBoek VerwijderdeNotitieboek
        {
            get { return _verwijderdeNotitieboek; }
            set
            {
                SetProperty(ref _verwijderdeNotitieboek, value);
                RemoveNotitieboek(value);

            }
        }     

        public NotitieBoek GeselecteerdeNotitieBoek
        {
            get { return _geselecteerdeNotitieBoek; }
            set
            {
                SetProperty(ref _geselecteerdeNotitieBoek, value);
            }
        }

    

        public RelayCommand AddNotitieBoekCommand
        {
            get { return _addNotitieBoekCommand; }
            set
            {
                SetProperty(ref _addNotitieBoekCommand, value);
            }
        }


        #endregion

        #region Constructor
        public NotitieBoekLijstViewModel(NotitieApplicatieMainViewmodel vm)
        {
          
            _vm = vm;
            Notitieboeken = DbRepository.Notitieboeklijst();
            AddNotitieBoekCommand = new RelayCommand(ShowInfoView);
            Titel = "mijn notitie boeken";

        }

        #endregion

        #region Methods

        private void VerwijderdeNotitieboek_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Notitieboeken.Remove(VerwijderdeNotitieboek);
            Console.WriteLine($"deleted");
        }

        private void RemoveNotitieboek(NotitieBoek notitie)
        {
            try
            {
                Notitieboeken.Remove(notitie);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "You cannot delete nothing");
            }
           
        }

        private void ShowInfoView(Object parameter = null)
        {
            _vm.SelectedView = new AddNotitieBoekViewModel(_vm);
        }


        #endregion




    }
}
