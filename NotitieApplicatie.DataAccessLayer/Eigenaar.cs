using MyOwnLib.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotitieApplicatie.DataAccessLayer
{
   public class Eigenaar : ObservableObject
    {

        #region Properties & Fields
        public int Id { get; set; }

        
 
        private string _naam;

        public string Naam
        {
            get { return _naam; }
            set
            {
                SetProperty(ref _naam, value);
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value);
            }
        }


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
        public override string ToString()
        {
            return $"{Naam}";
        }


        #endregion


    }


}
