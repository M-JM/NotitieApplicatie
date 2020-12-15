using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.Exceptions
{
   public class CategorieExpections : Exception
    {
        public CategorieExpections(String Message) : base(Message)
        {

        }

    public class CategorieDbException : Exception
        {
        public CategorieDbException() : base($"Er is iets verkeerd gelopen met de Databank gelieve de Administrator hiervoor te contacteren")
            {
            }
        }

        public class CategorieException : Exception
        {
            public CategorieException(): base($"Er is iets verkeerd gelopen met de gelieve de Administrator hiervoor te contacteren")
            {
            }
        }




    }
}
