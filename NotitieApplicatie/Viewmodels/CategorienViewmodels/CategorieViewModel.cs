using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using NotitieApplicatie.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NotitieApplicatie.Exceptions.CategorieExpections;

namespace NotitieApplicatie.Viewmodels.CategorienViewmodels
{
    public class CategorieViewModel : BaseViewModel
    {

        private Categorie _geselecteerdeCategorie;
        private Categorie  _onGewijzigdeCategorie;
        private Boolean _categorieGewijzigd;
        private Boolean _categorieBewaren;
        private Boolean _categorieVerwijderen;

        public Boolean CategorieBewaren
        {
            get { return _categorieBewaren; }
            set
            {
                SetProperty(ref _categorieBewaren, value);
            }
        }

        public Boolean CategorieVerwijderen
        {
            get { return _categorieVerwijderen; }
            set
            {
                SetProperty(ref _categorieVerwijderen, value);
            }
        }

        public Boolean CategorieGewijzigd
        {
            get { return _categorieGewijzigd; }
            set
            {
                SetProperty(ref _categorieGewijzigd, value);
            }
        }

        private RelayCommand _bewaar;

        public RelayCommand Bewaar
        {
            get { return _bewaar; }
            set
            {
                SetProperty(ref _bewaar, value);
            }
        }

        private RelayCommand _delete;

        public RelayCommand Delete
        {
            get { return _delete; }
            set
            {
                SetProperty(ref _delete, value);
            }
        }

        public Categorie OnGewijzigdeCategorie
        {
            get { return _onGewijzigdeCategorie; }
            set
            {
                SetProperty(ref _onGewijzigdeCategorie, value);
               if(GeselecteerdeCategorie != null)
                {
                 
                }
            }
        }


        public Categorie GeselecteerdeCategorie
        {
            get { return _geselecteerdeCategorie; }
            set
            {
                SetProperty(ref _geselecteerdeCategorie, value);
                if(value != null)
                {
                    GeselecteerdeCategorie.PropertyChanged += GeselecteerdeCategorie_PropertyChanged;
                    CategorieBewaren = false;
                    CategorieVerwijderen = true;
                }
                

            }
        }

        public CategorieViewModel()
        {
            Titel = "mijn geselecteerde categorie";
            Bewaar = new RelayCommand(BewaarCategorie, MagCategorieBewaren);
            Delete = new RelayCommand(DeleteCategorie, MagCategorieDeleten);
            
        }

        private void DeleteCategorie(object obj)
        {
            try
            {
              Boolean test =  DbRepository.RemoveCategorie(GeselecteerdeCategorie);
                if (test == false)
                {
                    throw new CategorieDbException();
                }

            }
            catch (CategorieDbException ex)
            {
                MessageBox.Show($"{ex.Message}");
             
            }

        }

        private void BewaarCategorie(object obj)
        {
            DbRepository.UpdateCategorie(GeselecteerdeCategorie);
            CategorieGewijzigd = false;
        }

        private Boolean MagCategorieBewaren(object parameter = null)
        {
            return CategorieBewaren;
        }

        private Boolean MagCategorieDeleten(object parameter = null)
        {
            return CategorieVerwijderen;
        }


        private void GeselecteerdeCategorie_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
           if(GeselecteerdeCategorie.HasErrors)
            {
            CategorieBewaren = false;
            }
            else {
            CategorieBewaren = true;
            }
            
                                   
            CategorieGewijzigd = true;



        }

    }
}
