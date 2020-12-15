using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using NotitieApplicatie.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Mediator
{
    public class Mediatora : IMediator 
    { 
        private List<BaseViewModel> _participants;

        public Mediatora()
        {
            _participants = new List<BaseViewModel>();

        }

        public void AddParticipants(BaseViewModel participant)
        {
            _participants.Add(participant);
        }

        // Verander de eerste parameter in een Object zodat ik objecten door kan geven ?)

        public void SendMessageToAllParticipants(string message, BaseViewModel SenderParticipant)
        {
            _participants.ForEach(participant =>
            {
                if (participant != SenderParticipant)    //don't send message to sender
                {
                    participant.ReceiveMessage(message);
                }
            });
        }

        }
    }

