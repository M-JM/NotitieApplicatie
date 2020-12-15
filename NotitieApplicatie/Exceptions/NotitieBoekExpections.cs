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
            public DbNotitieBoekExceptions():base($"Er is iets verkeerd gelopen met de Databank gelieve de Administrator hiervoor te contacteren")
            {

            }
        }


        public class GeneralExpection: Exception
        {
            public GeneralExpection():base($"Er is iets verkeerd gelopen tijdens het aanmaken gelieve de Administrator hiervoor te contacteren")
            {

            }
        }

    }
}
