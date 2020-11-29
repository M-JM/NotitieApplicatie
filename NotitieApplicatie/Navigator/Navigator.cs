using NotitieApplicatie.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotitieApplicatie.Navigator
{
   public class Navigator : INavigator
    {
    
            private BaseViewModel _currentViewModel;
            public BaseViewModel CurrentViewModel
            {
                get
                {
                    return _currentViewModel;
                }
                set
                {
                    _currentViewModel = value;
                    StateChanged?.Invoke();
                }
            }

            public event Action StateChanged;

        

    }
}
