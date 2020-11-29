using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Mediator
{
    public class Mediator : IMediator
    {
        public event EventHandler<MyEventArgs> NotitieBoekArg;

        public event EventHandler<MyEventArgs> NotitieArg;

        public void OnNotitieBoekArg(object sender, NotitieBoek notitieBoek)
        {
            (NotitieBoekArg as EventHandler<MyEventArgs>)?
                .Invoke(sender, new MyEventArgs { NotitieBoek = notitieBoek });
        }

        public void OnNotitieArg(object sender, Notitie notitie)
        {
            (NotitieArg as EventHandler<MyEventArgs>)?
                .Invoke(sender, new MyEventArgs { Notitie = notitie });
        }

    }
}
