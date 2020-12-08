using MyOwnLib.Common;
using NotitieApplicatie.BusinessAccessLayer;
using NotitieApplicatie.Mediator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Viewmodels
{
    public class BaseViewModel : ObservableObject
    {
  
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
        private IMediator _mediator;
        
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

        public BaseViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public string ReceiveMessage(string message, object @object)
        {
            return message;
        }

        internal string ReceiveMessage(string message)
        {
            return message;
        }

     
    }
}
