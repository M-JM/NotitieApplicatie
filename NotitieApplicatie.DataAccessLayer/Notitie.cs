using MyOwnLib.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotitieApplicatie.DataAccessLayer
{
    [Table("Notities")]
    public class Notitie : ObservableObject, IDataErrorInfo
    {
        #region Properties & Fields

        private string _titel;
        private string _inhoud;
        private DateTime _aangemaaktOp;
        private DateTime _gewijzigdOp;

        public int Id { get; set; }

        [Required]
        public string Titel
        {
            get { return _titel; }
            set
            {
                SetProperty(ref _titel, value);
            }
        }

        [Required]
        public string Inhoud
        {
            get { return _inhoud; }
            set
            {
                SetProperty(ref _inhoud, value);
            }
        }

        [Column(TypeName = "datetime2")]
        public DateTime AangemaaktOp
        {
            get { return _aangemaaktOp; }
            set
            {
                SetProperty(ref _aangemaaktOp, value);
            }
        }

      
        [Column(TypeName = "datetime2")]
        public DateTime GewijzigdOp
        {
            get { return _gewijzigdOp; }
            set
            {
                SetProperty(ref _gewijzigdOp, value);
            }
        }



        public Categorie Categorie { get; set; }
        public Eigenaar Eigenaar { get; set; }
        
        public NotitieBoek NotitieBoek { get; set; }

       

        #endregion

        #region Constructors

        // Parameterloos ctor nodig wegens hoe EF is opgebouwd
        private Notitie()
        {

        }

        public Notitie(string titel, string inhoud, DateTime aangemaaktOp, DateTime gewijzigdOp, Categorie categorie, Eigenaar eigenaar, NotitieBoek notitieBoek)
            : this(0, titel, inhoud,aangemaaktOp,gewijzigdOp, categorie, eigenaar,notitieBoek)
        {

        }

        internal Notitie(int id, string titel, string inhoud, DateTime aangemaaktOp, DateTime gewijzigdOp, Categorie categorie, Eigenaar eigenaar, NotitieBoek notitieBoek)
        {
            Id = id;
            Titel = titel;
            Inhoud = inhoud;
            AangemaaktOp = aangemaaktOp;
            GewijzigdOp = gewijzigdOp;
            Categorie = categorie;
            Eigenaar = eigenaar;
            NotitieBoek = notitieBoek;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{Id} - {Titel} - {Inhoud} - {AangemaaktOp.ToShortDateString()} ";
        }

        #endregion



        #region Validation

     
        [NotMapped]
        public string Error { get { return null; } }

        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case "Titel":
                        if (string.IsNullOrWhiteSpace(Titel))
                            result = "Gelieve iets in te vullen";
                        else if (Titel.Length < 3)
                            result = "Titel moet minstens drie karakters lang zijn";
                    break;
                }
                return result;
            }
        }




        #endregion
    }
}
