using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static NotitieApplicatie.Exceptions.NotitieBoekExpections;

namespace NotitieApplicatie.Viewmodels.NotitieBoekenViewmodels
{
    public class AddNotitieBoekViewModel : BaseViewModel
    {

        #region Properties & Variables

        private readonly NotitieApplicatieMainViewmodel _vm;
        private NotitieBoek _notitieBoek;
        private RelayCommand _maakCommand;
        private RelayCommand _cancelCommand;
        private FullObservableCollection<Eigenaar> _eigenaars;
        private Boolean _magBewaren;

        public NotitieBoek NotitieBoek
        {
          
            get { return _notitieBoek; }
            set
            {
                SetProperty(ref _notitieBoek, value);
              NotitieBoek.PropertyChanged += Notitieboek_Propertychanged;
                Console.WriteLine("I raised prop change");

            }
        }

        public Boolean MagBewaren
        {
            get { return _magBewaren; }
            set
            {
                SetProperty(ref _magBewaren, value);
            }
        }

        public FullObservableCollection<Eigenaar> Eigenaars
        {
            get { return _eigenaars; }
            set
            {
                SetProperty(ref _eigenaars, value);
            }
        }
        
        public RelayCommand MaakCommand
        {
            get { return _maakCommand; }
            set
            {
                SetProperty(ref _maakCommand, value);
            }
        }

        public RelayCommand CancelCommand
        {
            get { return _cancelCommand; }
            set
            {
                SetProperty(ref _cancelCommand, value);
            }
        }

        #endregion

        #region Constructor
        public AddNotitieBoekViewModel(NotitieApplicatieMainViewmodel vm)
        {
            _vm = vm;
            Titel = "Maak een nieuwe NotitieBoek";
            NotitieBoek = new NotitieBoek("", "", null);
            MaakCommand = new RelayCommand(BewaarNotitieBoek, MagNotitieBewaren);
            CancelCommand = new RelayCommand(CancelAddBoek);
            Eigenaars = DbRepository.Eigenaarslijst();
        }

        #endregion

        #region Methods

        private void Notitieboek_Propertychanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(NotitieBoek.HasErrors);
            if (NotitieBoek.HasErrors)
            {
                MagBewaren = false;
            }
            else
            {
                MagBewaren = true;
            }
        }

        private void BewaarNotitieBoek(Object parameter = null)
        {
            try
            {
                
               NotitieBoek check = DbRepository.AddNotitieBoek(NotitieBoek);
            
                if(check != null) { 
                MagBewaren = false;
                _vm.SelectedView = new HomeViewModel(_vm);
                }
                else
                {
                    throw new DbNotitieBoekExceptions();
                }
            }
            catch (DbNotitieBoekExceptions ex)
            {
                MessageBox.Show($"{ex.Message}", "Er is een fout opgetreden");
            }
           catch(Exception ex)
            {
                MyLogger.GetInstance().Error("General Exception uit BewaarNotitieBoek : " + ex.Message);
                MessageBox.Show("Er is een onbekende fout opgetreden, gelieve contact op te nemen met de Administrator","Er is een fout opgetreden");
            }

         

     

        }

        private void CancelAddBoek(Object parameter = null)
        {
            _vm.SelectedView = new HomeViewModel(_vm);

        }

        private Boolean MagNotitieBewaren(object parameter = null)
        {
            return MagBewaren;
        }

        #endregion


    }
}
