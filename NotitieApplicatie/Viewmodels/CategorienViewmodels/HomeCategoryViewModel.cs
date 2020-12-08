using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels.CategorienViewmodels
{
   public class HomeCategoryViewModel : BaseViewModel
    {


        #region Props & Variables

        private readonly NotitieApplicatieMainViewmodel _vm;

        private BaseViewModel _categorie;

        public BaseViewModel Categorie
        {
            get { return _categorie; }
            set
            {
                SetProperty(ref _categorie, value);
            }
        }

        private BaseViewModel _categorieenlijst;

        public BaseViewModel CategorieenLijst
        {
            get { return _categorieenlijst; }
            set
            {
                SetProperty(ref _categorieenlijst, value);
            }
        }

        #endregion

        public HomeCategoryViewModel(NotitieApplicatieMainViewmodel vm)
        {
            _vm = vm;
            Categorie = new CategorieViewModel();
            CategorieenLijst = new CategorieLijstViewModel(_vm);
            CategorieenLijst.PropertyChanged += CategorieenLijst_PropertyChanged;


        }
        private void CategorieenLijst_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {
                case "GeselecteerdeCategory":
                    (Categorie as CategorieViewModel).GeselecteerdeCategory = (CategorieenLijst as CategorieLijstViewModel).GeselecteerdeCategory;


                    break;

                default:
                    break;
            }
        }

    }
}
