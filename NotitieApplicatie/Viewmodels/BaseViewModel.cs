using MyOwnLib.Common;
using NotitieApplicatie.BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels
{
    public class BaseViewModel : ObservableObject
    {
        /// <summary>
        ///     Base Klasse die alle ViewModels gaan erven.
        ///     Zorgt ervoor dat bij  ViewModels wij toegang hebben tot de DbRepository en de facto een property Titel definieren die waarde default krijgen.
        ///     Het is wel de bedoeling deze waarde te setten naar iets deftig , maar indien ik het vergeet dat er toch een default titel is.
        ///        
        /// 
        /// </summary>

        private IDbRepository _dbRepository;

        public IDbRepository DbRepository
        {
            get { return _dbRepository; }
            set
            {
                SetProperty(ref _dbRepository, value);
            }
        }

        private String _titel;

        public String Titel
        {
            get { return _titel; }
            set
            {
                SetProperty(ref _titel, value);
            }
        }

        public BaseViewModel()
        {
            DbRepository = new DbRepositoryDisconnected();
            Titel = "Default Titel";
        }

    }
}
