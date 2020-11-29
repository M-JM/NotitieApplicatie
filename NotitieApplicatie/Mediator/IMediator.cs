using NotitieApplicatie.DataAccessLayer;
using System;

namespace NotitieApplicatie.Mediator
{
    public interface IMediator
    {
        event EventHandler<MyEventArgs> NotitieArg;

        event EventHandler<MyEventArgs> NotitieBoekArg;

        void OnNotitieArg(object sender, Notitie notitie);
        void OnNotitieBoekArg(object sender, NotitieBoek notitieBoek);
    }
}