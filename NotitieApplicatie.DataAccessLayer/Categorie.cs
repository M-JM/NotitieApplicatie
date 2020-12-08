using MyOwnLib.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotitieApplicatie.DataAccessLayer
{
    public class Categorie : ObservableObject
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

        private string _kleur;

        public string Kleur
        {
            get { return _kleur; }
            set
            {
                SetProperty(ref _kleur, value);
            }
        }


        // -> kleuren -> Color datatype. -> Hexademical/RGB/String -> black / orange
        //-> color picker -> HEXADEMICAL -> RGB

        #endregion

        #region Constructors

        private Categorie()
        {

        }

        public Categorie(string naam, string kleur)
            :this(0,naam,kleur)
        {

        }

        internal Categorie(int id,string naam,string kleur)
        {
            Id = id;
            Naam = naam;
            Kleur = kleur;
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
