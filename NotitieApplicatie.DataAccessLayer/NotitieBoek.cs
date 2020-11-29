using MyOwnLib.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NotitieApplicatie.DataAccessLayer
{
    [Table("Notitieboeken")]
    public class NotitieBoek : ObservableObject
    {
        #region Properties & Fields

        private string _naam;
        private string _beschrijving;

        public int Id { get; set; }

        
       
         public string Naam
        {
            get { return _naam; }
            set
            {
                SetProperty(ref _naam, value);
            }
        }

        public string Beschrijving
        {
            get { return _beschrijving; }
            set
            {
                SetProperty(ref _beschrijving, value);
            }
        }


        public Eigenaar Eigenaar { get; set; }

        public List<Notitie> Notities { get; set; }


        #endregion

        #region Constructors

        private NotitieBoek()
        {

        }

        public NotitieBoek(string naam, string beschrijving, Eigenaar eigenaar)
          :this(0,naam,beschrijving,eigenaar)
        {

        }

        internal NotitieBoek(int id , string naam, string beschrijving, Eigenaar eigenaar)
        {
            Id = id;
            Naam = naam;
            Beschrijving = beschrijving;
            Eigenaar = eigenaar;
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
