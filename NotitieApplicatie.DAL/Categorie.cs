﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NotitieApplicatie.DAL
{
    public class Categorie
    {
        #region Properties & Fields

        public int Id { get; set; }

        public string Naam { get; set; }

        public string Kleur { get; set; }

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

        #endregion
    }
}
