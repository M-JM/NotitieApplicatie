using MyOwnLib.Common;
using NotitieApplicatie.Mediator;
using NotitieApplicatie.Navigator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels
{
   public class NotitieApplicatieMainViewmodel : ObservableObject
    {
        /// <summary>
        /// Dit is de ViewModel van de Mainwindow van de Applicatie.
        /// Hier gaan de routing configuren van RelayCommands om onze gebruiker te laten navigeren tussen de verschillende views.
        /// Indien men van View naar nieuwe view (waarvoor geen knop in MainWindow ter beschikking wordt gesteld) gaat die RelayCommand geimplementeerd worden
        /// in de relevante ViewModel ( Denk -> NotitieDetailView met een Edit knop die naar EditNotitieView gaat)
        /// </summary>

        #region Properties & Fields

        private RelayCommand _viewCommand;

        public RelayCommand ViewCommand
        {
            get { return _viewCommand; }
            set
            {
                SetProperty(ref _viewCommand, value);
            }
        }

        private BaseViewModel _selectedView;

        public BaseViewModel SelectedView
        {
            get { return _selectedView; }
            set
            {
                SetProperty(ref _selectedView, value);
            }
        }

        #endregion

        #region Constructor

        public NotitieApplicatieMainViewmodel()
        {
            ViewCommand = new RelayCommand(Routing);
        }

        #endregion


        #region Methods

        //private void ShowInfoView(Object parameter = null)
        //{
        //    SelectedView = new InfoViewModel();
        //}

        //private void ShowNotitieBoekView(Object parameter = null)
        //{
        //    SelectedView = new NotitieBoekViewModel();
        //}
        protected IMediator mediator;
        /// <summary>
        /// Parameter komt van de View door gebruik te maken van RelayCommandParameter die aan de Radiobutton van mijn navigatie zijn meegegeven.
        /// Dit komt overeen met de ViewType van mijn Navigator Klasse
        /// 
        /// </summary>
        /// <param name="parameter"></param>

        private void Routing(object parameter)
        {
            if(parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                switch (viewType)
                {
                    case ViewType.Info:
                        SelectedView = new InfoViewModel();
                        break;
                    case ViewType.NotitieBoek:
                        SelectedView = new NotitieBoekLijstViewModel();
                        break;
                    case ViewType.Profiel:
                        SelectedView = new ProfileViewModel();
                        break;
                    case ViewType.Home:
                        SelectedView = new HomeViewModel();
                        break;
                    // Default
                    default:
                         SelectedView = new HomeViewModel();
                        break;

                }
            }
        }

        #endregion

    }
}
