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

namespace NotitieApplicatie.Viewmodels.NotitieViewmodels
{
    public class AddNotitieViewModel : BaseViewModel
    {
        #region Properties and Variables


        private readonly NotitieApplicatieMainViewmodel _vm;
        private RelayCommand _bewaarNotitie;
        private RelayCommand _cancel;
        private FullObservableCollection<Categorie> _categorieen;
        private Boolean _magBewaren;
        private Notitie _notitie;
        private FullObservableCollection<NotitieBoek> _notitieBoeken;

        public RelayCommand BewaarNotitie
        {
            get { return _bewaarNotitie; }
            set
            {
                SetProperty(ref _bewaarNotitie, value);
            }
        }

        public RelayCommand Cancel
        {
            get { return _cancel; }
            set
            {
                SetProperty(ref _cancel, value);
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

        public Notitie Notitie
        {
            get { return _notitie; }
            set
            {
                SetProperty(ref _notitie, value);
             
                Notitie.PropertyChanged += Notitie_Propertychanged;
            }
        }

        public FullObservableCollection<NotitieBoek> NotitieBoeken
        {
            get { return _notitieBoeken; }
            set
            {
                SetProperty(ref _notitieBoeken, value);
            }
        }

        public FullObservableCollection<Categorie> Categorieen
        {
            get { return _categorieen; }
            set
            {
                SetProperty(ref _categorieen, value);
            }
        }

        #endregion

        #region Constructor
        public AddNotitieViewModel(NotitieApplicatieMainViewmodel vm)
        {
            _vm = vm;
            Titel = "Maak een nieuwe notitie aan";
            Categorieen = DbRepository.Categorieenlijst();
            NotitieBoeken = DbRepository.Notitieboeklijst();
            Notitie = new Notitie("","",DateTime.Now,DateTime.Now,0,null,null,null);
            BewaarNotitie = new RelayCommand(BewaarEenNotitie, MagNotitieBewaren);
            Cancel = new RelayCommand(CancelAddNotitie);
        }

        #endregion

        #region Methods

        private void Notitie_Propertychanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(Notitie.HasErrors);
            Console.WriteLine(Notitie.Rating);
            if (Notitie.HasErrors)
            {
                MagBewaren = false;
            }
            else
            {
                MagBewaren = true;
            }
        }

        private void BewaarEenNotitie(Object parameter = null)
        {
            try
            {
                Notitie check = DbRepository.AddNotitie(Notitie);

                if (check != null)
                {
                    MagBewaren = false;
                    _vm.SelectedView = new HomeNotitieViewModel(_vm);
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
                MyLogger.GetInstance().Error("General Exception uit BewaarNotitie : " + ex.Message);
                MessageBox.Show("Er is een onbekende fout opgetreden, gelieve contact op te nemen met de Administrator", "Er is een fout opgetreden");
            }
        }

        private void CancelAddNotitie(Object parameter = null)
        {
            _vm.SelectedView = new HomeNotitieViewModel(_vm);
        }

        private Boolean MagNotitieBewaren(object parameter = null)
        {
            return MagBewaren;
        }




        #endregion

    }
}
