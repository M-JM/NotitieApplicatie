using MyOwnLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NotitieApplicatie.Viewmodels
{
    public class InfoViewModel : BaseViewModel
    {

        private RelayCommand _return;

        public RelayCommand Return
        {
            get { return _return; }
            set
            {
                SetProperty(ref _return, value);
            }
        }


        public InfoViewModel()
        {

        }
      
    }
}
