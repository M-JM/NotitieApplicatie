using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels.CategorienViewmodels
{
    public class CategorieViewModel : BaseViewModel
    {
        private Categorie _geselecteerdeCategory;

        public Categorie GeselecteerdeCategory
        {
            get { return _geselecteerdeCategory; }
            set
            {
                SetProperty(ref _geselecteerdeCategory, value);
                GeselecteerdeCategory.PropertyChanged += GeselecteerdeCategory_PropertyChanged;

            }
        }

        public CategorieViewModel()
        {
            Titel = "mijn geselecteerde Category";
        }


        private void GeselecteerdeCategory_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Console.WriteLine($"changed");
           


        }

    }
}
