using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels.CategorienViewmodels
{
    class CategorieLijstViewModel : BaseViewModel
    {
        private readonly NotitieApplicatieMainViewmodel _vm;
        private FullObservableCollection<Categorie> _categorieLijst;
        private Categorie _geselecteerdeCategory;
        private RelayCommand _addCategoryCommand;
        private Categorie _verwijderdeCategorie;

        public FullObservableCollection<Categorie> CategorieLijst
        {
            get { return _categorieLijst; }
            set
            {
                SetProperty(ref _categorieLijst, value);

            }
        }

        public Categorie VerwijderdeNotitieboek
        {
            get { return _verwijderdeCategorie; }
            set
            {
                SetProperty(ref _verwijderdeCategorie, value);
                RemoveCategory(value);

            }
        }

   

        public Categorie GeselecteerdeCategory
        {
            get { return _geselecteerdeCategory; }
            set
            {
                SetProperty(ref _geselecteerdeCategory, value);
            }
        }


        public RelayCommand AddCategoryCommand
        {
            get { return _addCategoryCommand; }
            set
            {
                SetProperty(ref _addCategoryCommand, value);
            }
        }



        public CategorieLijstViewModel(NotitieApplicatieMainViewmodel vm)
        {
            _vm = vm;
            Titel = "mijn Categorieen";
            CategorieLijst = DbRepository.Categorieenlijst();
            AddCategoryCommand = new RelayCommand(ShowInfoView);
        }


        private void RemoveCategory(Categorie value)
        {
            throw new NotImplementedException();
        }

        private void ShowInfoView(Object parameter = null)
        {
            _vm.SelectedView = new AddCategoryViewModel(_vm);
        }



    }
}
