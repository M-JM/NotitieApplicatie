using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using NotitieApplicatie.Viewmodels;
using System;

namespace NotitieApplicatie.Mediator
{
    public interface IMediator
    {
        void AddParticipants(BaseViewModel participant);
        void SendMessageToAllParticipants(string message, BaseViewModel SenderParticipant);
   

    }
}