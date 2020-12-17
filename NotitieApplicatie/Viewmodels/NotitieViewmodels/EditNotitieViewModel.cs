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
    public class EditNotitieViewModel : BaseViewModel
    {
        private readonly NotitieApplicatieMainViewmodel _vm;
        private RelayCommand _bewaarNotitie;
        private RelayCommand _cancel;
        private FullObservableCollection<Categorie> _categorieen;
        private Boolean _magBewaren;
        private Notitie _geselecteerdeNotitie;
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

        public Notitie GeselecteerdeNotitie
        {
            get { return _geselecteerdeNotitie; }
            set
            {
                SetProperty(ref _geselecteerdeNotitie, value);
                MagBewaren = true;
                GeselecteerdeNotitie.PropertyChanged += GeselecteerdeNotitie_Propertychanged;
                if (value != null)
                {
                    foreach(NotitieBoek item in NotitieBoeken)
                    {
                        if(item.NotitieBoekId == value.NotitieBoekId)
                        {
                            GeselecteerdeNotitie.NotitieBoek = item;
                        }
                    }
                    foreach (Categorie item in Categorieen)
                    {
                        if (item.CategorieId == value.CategorieId)
                        {
                            GeselecteerdeNotitie.Categorie = item;
                        }
                    }
                }
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

        public EditNotitieViewModel(NotitieApplicatieMainViewmodel vm, Notitie notitie)
		{
			_vm = vm;	
            Categorieen = DbRepository.Categorieenlijst();
            NotitieBoeken = DbRepository.Notitieboeklijst();
            GeselecteerdeNotitie = notitie;
            Titel = "U Editeert Notitie met Id " + $"{GeselecteerdeNotitie.Id}";
            BewaarNotitie = new RelayCommand(BewaarEenNotitie, MagNotitieBewaren);
            Cancel = new RelayCommand(CancelAddNotitie);
            MagBewaren = false;
        }

        private void GeselecteerdeNotitie_Propertychanged(object sender, PropertyChangedEventArgs e)
        {
     
            if (GeselecteerdeNotitie.HasErrors)
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
                GeselecteerdeNotitie.GewijzigdOp = DateTime.Now;
                Notitie check = DbRepository.UpdateNotitie(GeselecteerdeNotitie);

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



    }
}
