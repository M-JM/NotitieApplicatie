using System;
using System.Collections.Generic;
using System.Text;

namespace NotitieApplicatie.DAL
{
    public class NotitieBoek
    {
        #region Properties & Fields

        public int Id { get; set; }

        public string Naam { get; set; }

        #endregion

        #region Constructors

        private NotitieBoek()
        {

        }

        public NotitieBoek(string naam)
          :this(0,naam)
        {

        }

        internal NotitieBoek(int id , string naam)
        {
            Id = id;
            Naam = naam;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{Id} - {Naam} ";
        }

        #endregion

    }
}
