using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static NotitieApplicatie.Exceptions.NotitieBoekExpections;

namespace NotitieApplicatie.Viewmodels.NotitieBoekenViewmodels
{
    public class EditNotitieBoekViewModel : BaseViewModel
    {

        #region Properties and Variables
        private readonly NotitieApplicatieMainViewmodel _vm;
        private NotitieBoek _geselecteerdeNotitieBoek;
        private Boolean _notitieboekGewijzigd;
        private RelayCommand _bewaarCommand;
      

        private FullObservableCollection<Eigenaar> _eigenaars;

        public NotitieBoek GeselecteerdeNotitieBoek
        {
            get { return _geselecteerdeNotitieBoek; }
            set
            {
                SetProperty(ref _geselecteerdeNotitieBoek, value);
                GeselecteerdeNotitieBoek.PropertyChanged += GeselecteerdeNotitieBoek_PropertyChanged;

                /// Noodzakelijk om de huidige eigenaar van mijn Gelesecteerde notitieboek weer te geven als de selecteditem van de ComboBox.
                /// ComboBox sourceItemList is een observable -> de selectedItem moet uit die SourceItemlist komen 
                /// Dus hier moet ik de waarde van eigenaar in mijn notitieboek gaan instellen met dezelfde waarde uit lijst van Eigenaars
                /// Combobox kent alleen waardes uit de lijst waarmee hij gepopuleerd wordt !!!
                /// Verder opzoeken of hier geen beter manier voor besta ??)

              if(value != null)
                {
                    foreach (Eigenaar item in Eigenaars)
                    {
                        if(item.EigenaarId == value.Eigenaar.EigenaarId)
                        {
                            GeselecteerdeNotitieBoek.Eigenaar = item;
                        }
                    }
                }
               
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

        public Boolean NotitieBoekGewijzigd
        {
            get { return _notitieboekGewijzigd; }
            set
            {
                SetProperty(ref _notitieboekGewijzigd, value);
            }
        }

        public RelayCommand BewaarCommand
        {
            get { return _bewaarCommand; }
            set
            {
                SetProperty(ref _bewaarCommand, value);
            }
        }

        private RelayCommand _cancelCommand;

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
        public EditNotitieBoekViewModel(NotitieApplicatieMainViewmodel vm, NotitieBoek notitieBoek)
        {
            Eigenaars = DbRepository.Eigenaarslijst();
            GeselecteerdeNotitieBoek = notitieBoek;
            _vm = vm;
            Titel = "U Editeert Notitieboek met Id " + $"{GeselecteerdeNotitieBoek.NotitieBoekId}";
            BewaarCommand = new RelayCommand(BewaarNotitieBoek, MagNotitieBoekBewaren);
            CancelCommand = new RelayCommand(CancelNotitieBoek);
            NotitieBoekGewijzigd = false;
        }

        #endregion

        #region Methods
        private void BewaarNotitieBoek(Object parameter = null)
        {
            try
            {
               NotitieBoek check = DbRepository.UpdateNotitieBoek(GeselecteerdeNotitieBoek);
             if(check != null)
                {
                    NotitieBoekGewijzigd = false;
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
            catch (Exception ex)
            {
                MyLogger.GetInstance().Error("General Exception uit EditNotitieBoek : " + ex.ToString());
                // ex.ToString
                //The default implementation of ToString obtains the name of the class that threw the current exception, the message, the result of calling ToString on the inner exception, and the result of calling Environment.StackTrace.If any of these members is null, its value is not included in the returned string.
                //If there is no error message or if it is an empty string (""), then no error message is returned.The name of the inner exception and the stack trace are returned only if they are not null.
                MessageBox.Show("Er is een onbekende fout opgetreden, gelieve contact op te nemen met de Administrator", "Er is een fout opgetreden");
            }

        }

        private void CancelNotitieBoek(Object parameter = null)
        {
            _vm.SelectedView = new HomeViewModel(_vm);
        }

        private Boolean MagNotitieBoekBewaren(object parameter = null)
        {
            return NotitieBoekGewijzigd;
        }

        private void GeselecteerdeNotitieBoek_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (GeselecteerdeNotitieBoek.HasErrors)
            {
                NotitieBoekGewijzigd = false;
            }
            else
            {
                NotitieBoekGewijzigd = true;
            }         
        }

        #endregion

    }
}
