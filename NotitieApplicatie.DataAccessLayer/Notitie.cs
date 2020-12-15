using MyOwnLib.Common;
using System;
using System.Collections;
using System.Collections.Generic;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace NotitieApplicatie.DataAccessLayer
{
    [Table("Notities")]
    public class Notitie : ObservableObject, INotifyDataErrorInfo
    {
        #region Properties & Fields

        private string _titel;
        private string _inhoud;
        private DateTime _aangemaaktOp;
        private DateTime _gewijzigdOp;
        

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();


        public int Id { get; set; }

        [Required]
        public string Titel
        {
            get { return _titel; }
            set
            {
                ClearErrors(nameof(Titel));
                SetProperty(ref _titel, value);
                if (String.IsNullOrEmpty(value))
                {
                    AddError(nameof(Titel), "Je moet een Titel opgeven");
                }
                if(Titel.Length < 5)
                {
                    AddError(nameof(Titel), "Mag niet minder dan 2 karakters zijn");
                }
            }
        }

        [Required]
        public string Inhoud
        {
            get { return _inhoud; }
            set
            {
                ClearErrors(nameof(Inhoud));
                SetProperty(ref _inhoud, value);
                if (String.IsNullOrEmpty(value))
                {
                    AddError(nameof(Inhoud), "Inhoud mag niet leeg zijn");
                }
                if (Inhoud.Length > 100)
                {
                    AddError(nameof(Titel), "Inhoud mag niet groter zijn als 100 karakters");
                }
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

        private double _rating;

        public double Rating
        {
            get { return _rating; }
            set
            {
                SetProperty(ref _rating, value);
            }
        }



        public Categorie Categorie { get; set; }

        [ForeignKey("Categorie")]
        public int? CategorieId { get; set; }

        [ForeignKey("Eigenaar")]
        public int? EigenaarId { get; set; }
        public Eigenaar Eigenaar { get; set; }

        [ForeignKey("NotitieBoek")]
        public int? NotitieBoekId { get; set; }
        public NotitieBoek NotitieBoek { get; set; }


        public bool HasErrors{ get { return _propertyErrors.Any(propErrors => propErrors.Value != null && propErrors.Value.Count > 0); } }



        #endregion

        #region Constructors

        // Parameterloos ctor nodig wegens hoe EF is opgebouwd
        public Notitie()
        {

        }

        public Notitie(string titel, string inhoud, DateTime aangemaaktOp, DateTime gewijzigdOp, double rating, Categorie categorie, Eigenaar eigenaar, NotitieBoek notitieBoek)
            : this(0, titel, inhoud,aangemaaktOp,gewijzigdOp,rating, categorie, eigenaar,notitieBoek)
        {

        }

        internal Notitie(int id, string titel, string inhoud, DateTime aangemaaktOp, DateTime gewijzigdOp, double rating, Categorie categorie, Eigenaar eigenaar, NotitieBoek notitieBoek)
        {
            Id = id;
            Titel = titel;
            Inhoud = inhoud;
            AangemaaktOp = aangemaaktOp;
            GewijzigdOp = gewijzigdOp;
            Rating = rating;
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

        public IEnumerable GetErrors(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (_propertyErrors.ContainsKey(propertyName) && (_propertyErrors[propertyName] != null) && _propertyErrors[propertyName].Count > 0)
                    return _propertyErrors[propertyName].ToList();
                else
                    return null;
            }
            else
         return _propertyErrors.SelectMany(err => err.Value.ToList());
        }



        public void AddError(string propertyName, string errorMessage)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }
            _propertyErrors[propertyName].Add(errorMessage);
            OnErrorschanged(propertyName);
    }


        private void OnErrorschanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ClearErrors(string propertyName)
        {
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorschanged(propertyName);
            }

        }

    }


    


    #endregion


}











//{
//    [Table("Notities")]
//public class Notitie : ObservableObject, IDataErrorInfo
//{
//    #region Properties & Fields

//    private string _titel;
//    private string _inhoud;
//    private DateTime _aangemaaktOp;
//    private DateTime _gewijzigdOp;

//    public int Id { get; set; }

//    [Required]
//    public string Titel
//    {
//        get { return _titel; }
//        set
//        {
//            SetProperty(ref _titel, value);
//        }
//    }

//    [Required]
//    public string Inhoud
//    {
//        get { return _inhoud; }
//        set
//        {
//            SetProperty(ref _inhoud, value);
//        }
//    }

//    [Column(TypeName = "datetime2")]
//    public DateTime AangemaaktOp
//    {
//        get { return _aangemaaktOp; }
//        set
//        {
//            SetProperty(ref _aangemaaktOp, value);
//        }
//    }


//    [Column(TypeName = "datetime2")]
//    public DateTime GewijzigdOp
//    {
//        get { return _gewijzigdOp; }
//        set
//        {
//            SetProperty(ref _gewijzigdOp, value);
//        }
//    }



//    public Categorie Categorie { get; set; }
//    public Eigenaar Eigenaar { get; set; }
//    public NotitieBoek NotitieBoek { get; set; }



//    #endregion

//    #region Constructors

//    Parameterloos ctor nodig wegens hoe EF is opgebouwd
//    private Notitie()
//    {

//    }

//    public Notitie(string titel, string inhoud, DateTime aangemaaktOp, DateTime gewijzigdOp, Categorie categorie, Eigenaar eigenaar, NotitieBoek notitieBoek)
//        : this(0, titel, inhoud, aangemaaktOp, gewijzigdOp, categorie, eigenaar, notitieBoek)
//    {

//    }

//    internal Notitie(int id, string titel, string inhoud, DateTime aangemaaktOp, DateTime gewijzigdOp, Categorie categorie, Eigenaar eigenaar, NotitieBoek notitieBoek)
//    {
//        Id = id;
//        Titel = titel;
//        Inhoud = inhoud;
//        AangemaaktOp = aangemaaktOp;
//        GewijzigdOp = gewijzigdOp;
//        Categorie = categorie;
//        Eigenaar = eigenaar;
//        NotitieBoek = notitieBoek;
//    }

//    #endregion

//    #region Methods

//    public override string ToString()
//    {
//        return $"{Id} - {Titel} - {Inhoud} - {AangemaaktOp.ToShortDateString()} ";
//    }

//    #endregion

//    #region Validation


//    [NotMapped]
//    public string Error { get { return null; } }

//    public string this[string columnName]
//    {
//        get
//        {
//            string result = null;

//            switch (columnName)
//            {
//                case "Titel":
//                    if (string.IsNullOrWhiteSpace(Titel))
//                        result = "Gelieve iets in te vullen";
//                    else if (Titel.Length < 3)
//                        result = "Titel moet minstens drie karakters lang zijn";
//                    break;
//            }
//            return result;
//        }
//    }




//    #endregion
//}
//}

