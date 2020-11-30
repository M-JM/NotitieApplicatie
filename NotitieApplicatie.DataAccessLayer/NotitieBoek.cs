using MyOwnLib.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

namespace NotitieApplicatie.DataAccessLayer
{
    [Table("Notitieboeken")]
    public class NotitieBoek : ObservableObject, IDataErrorInfo
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


        //public Eigenaar Eigenaar { get; set; }

        private Eigenaar _eigenaar;

        public Eigenaar Eigenaar
        {
            get { return _eigenaar; }
            set
            {
                SetProperty(ref _eigenaar, value);
            }
        }


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

        #region Validation

        [NotMapped]
        public string Error { get { return null; } }

        [NotMapped]
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        [NotMapped]
        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case "Naam":
                        if (string.IsNullOrWhiteSpace(Naam))
                            result = "Gelieve iets in te vullen";
                        else if (Naam.Length < 3)
                            result = "Titel moet minstens drie karakters lang zijn";
                
                    break;
                    case "Beschrijving":
                        if (string.IsNullOrWhiteSpace(Beschrijving))
                            result = "Gelieve iets in te vullen";
                        else if (Beschrijving.Length < 5)
                            result = "Beschrijving moet minstens vijf karakters lang zijn";
                        else if (!Regex.IsMatch(Beschrijving, @"^[a-zA-Z]+$"))
                            result = "Beschrijving mag geen cijfers bevatten";

                        break;
                    case "Eigenaar":
                        if (Eigenaar.ToString() == null)
                            result = "Gelieve een eigenaar te kiezen";
                        else if (Eigenaar.Id.ToString() == "0")
                            result = "Gelieve een eigenaar te kiezen";
                        break;
                }

                if (ErrorCollection.ContainsKey(columnName))
                    ErrorCollection[columnName] = result;
                else if (result != null)
                    ErrorCollection.Add(columnName, result);
                OnPropertyChanged("ErrorCollection");


                return result;
            }
        }





        #endregion

    }
}
