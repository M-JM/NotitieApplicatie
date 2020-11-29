using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.DataAccessLayer
{
   public class Eigenaar
    {

        #region Properties & Fields
        public int Id { get; set; }
        
        public string Naam { get; set; }

        public string Email { get; set; }

        #endregion

        #region Constructors
        private Eigenaar()
        {


        }

        public Eigenaar(string naam, string email)
            :this(0,naam,email)
        {

        }


        internal Eigenaar(int id, string naam, string email)
        {
            Id = id;
            Naam = naam;
            Email = email;

        }

        #endregion

        #region Methods



        #endregion


    }


}
