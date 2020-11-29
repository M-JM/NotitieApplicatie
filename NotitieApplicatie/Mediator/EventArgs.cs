using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Mediator
{
   public class MyEventArgs : EventArgs
    {
        public NotitieBoek NotitieBoek { get; set; }

        public Notitie Notitie { get; set; }

    }
}
