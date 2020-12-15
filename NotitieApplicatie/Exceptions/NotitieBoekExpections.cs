using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Exceptions
{
  public class NotitieBoekExpections : Exception
    {
        public NotitieBoekExpections(String Message) : base(Message)
        {

        }


        public class DbNotitieBoekExceptions : Exception
        {
            public DbNotitieBoekExceptions()
            {

            }
        }


        public class GeneralExpection: Exception
        {
            public GeneralExpection()
            {

            }
        }

    }
}
