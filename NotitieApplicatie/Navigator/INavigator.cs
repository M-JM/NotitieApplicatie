using NotitieApplicatie.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotitieApplicatie.Navigator
{
    public enum ViewType
    {
        Info,
        NotitieBoek,
        Home,
        Categorien,

    }


    public interface INavigator
    {

        BaseViewModel CurrentViewModel { get; set; }
        event Action StateChanged;



    }
}
