using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NotitieApplicatie.Viewmodels.CategorienViewmodels
{
    public class AddCategoryViewModel : BaseViewModel
    {
        private Categorie _categorie;
        private RelayCommand _maakCommand;
        private RelayCommand _cancelCommand;
        private readonly NotitieApplicatieMainViewmodel _vm;

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

      

        public Categorie Category
        {
            get { return _categorie; }
            set
            {
                SetProperty(ref _categorie, value);
            }
        }


        public AddCategoryViewModel(NotitieApplicatieMainViewmodel vm)
        {
            _vm = vm;
            Titel = "Maak een nieuwe categorie";
            Category = new Categorie("","");
            MaakCommand = new RelayCommand(BewaarCategorie);
            CancelCommand = new RelayCommand(CancelAddCategory);
        }

        private void CancelAddCategory(Object parameter = null)
        {
            _vm.SelectedView = new HomeViewModel(_vm);
        }

        private void BewaarCategorie(Object parameter = null)
        {
         
            DbRepository.AddCategorie(Category);
            _vm.SelectedView = new HomeCategoryViewModel(_vm);
        }
    }
}
