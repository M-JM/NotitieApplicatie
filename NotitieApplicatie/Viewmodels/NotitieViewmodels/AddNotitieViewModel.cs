using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels.NotitieViewmodels
{
    public class AddNotitieViewModel : BaseViewModel
    {

        private Notitie _notitie;

        public Notitie Notitie
        {
            get { return _notitie; }
            set
            {
                SetProperty(ref _notitie, value);
            }
        }

        private FullObservableCollection<Categorie> _categorieen;

        public FullObservableCollection<Categorie> Categorieen
        {
            get { return _categorieen; }
            set
            {
                SetProperty(ref _categorieen, value);
            }
        }


        public AddNotitieViewModel()
        {
            Categorieen = DbRepository.Categorieenlijst();
            Notitie = new Notitie();
        }




    }
}
