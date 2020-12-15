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
    [Table("Notitieboeken")]
    public class NotitieBoek : ObservableObject, INotifyDataErrorInfo
    {
        #region Properties & Fields
        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();
        private string _naam;
        private string _beschrijving;
        public int NotitieBoekId { get; set; }

        public string Naam
        {
            get { return _naam; }
            set
            {
               
                ClearErrors(nameof(Naam));
                SetProperty(ref _naam, value);
                if (String.IsNullOrEmpty(value))
                {
                    AddError(nameof(Naam), "Je moet een Naam opgeven");
                }
                if (Naam.Length < 5)
                {
                    AddError(nameof(Naam), "Mag niet minder dan 5 karakters zijn");

                }
                // Due to property change being triggered by the observable class before validation is performed the Error.count of the dictionary is only updated after the prop. change has occured
                // If i want to use the count to do a check if it can be saved in the propertychange event , i need to trigger it again.
                // This is due to poor implementation of InotifyDataErrorInfo....
                // TODO: This has to be fixed properly -> ask teacher if he can maybe point me in the right direction ??

                OnPropertyChanged(nameof(Naam));
                

            }
        }

        public string Beschrijving
        {
            get { return _beschrijving; }
            set
            {
                ClearErrors(nameof(Beschrijving));
                SetProperty(ref _beschrijving, value);
                if (String.IsNullOrEmpty(value))
                {
                    AddError(nameof(Beschrijving), "Je moet een beschrijving opgeven");
                }
                if(Beschrijving.Length > 50)
                {
                    AddError(nameof(Beschrijving), "Mag maar 50 karakters lang zijn");
                }
                OnPropertyChanged(nameof(Beschrijving));
            }
        }


        //public Eigenaar Eigenaar { get; set; }

        private Eigenaar _eigenaar;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


        [ForeignKey("Eigenaar")]
        public int? EigenaarId { get; set; }
        public Eigenaar Eigenaar
        {
            get { return _eigenaar; }
            set

            {
                ClearErrors(nameof(Eigenaar));
                SetProperty(ref _eigenaar, value);
                if (Eigenaar == null)
                {
                    AddError(nameof(Eigenaar), "Moet een eigenaar hebben");
                }
              
            }
        }
   

        public List<Notitie> Notities { get; set; }

        public bool HasErrors { get
            { return _propertyErrors.Any(propErrors => propErrors.Value != null && propErrors.Value.Count > 0); }}



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
            NotitieBoekId = id;
            Naam = naam;
            Beschrijving = beschrijving;
            Eigenaar = eigenaar;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{NotitieBoekId} - {Naam} ";
        }

      
        #endregion

        #region Validation


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

        #endregion

    }
}


//public class NotitieBoek : ObservableObject, IDataErrorInfo
//{
//    #region Properties & Fields

//    private string _naam;
//    private string _beschrijving;


//    public int Id { get; set; }



//    public string Naam
//    {
//        get { return _naam; }
//        set
//        {
//            SetProperty(ref _naam, value);
//        }
//    }

//    public string Beschrijving
//    {
//        get { return _beschrijving; }
//        set
//        {
//            SetProperty(ref _beschrijving, value);
//        }
//    }


//    //public Eigenaar Eigenaar { get; set; }

//    private Eigenaar _eigenaar;

//    public Eigenaar Eigenaar
//    {
//        get { return _eigenaar; }
//        set
//        {
//            SetProperty(ref _eigenaar, value);
//        }
//    }


//    public List<Notitie> Notities { get; set; }




//    #endregion

//    #region Constructors

    //private NotitieBoek()
    //{

    //}

    //public NotitieBoek(string naam, string beschrijving, Eigenaar eigenaar)
    //  : this(0, naam, beschrijving, eigenaar)
    //{

    //}

    //internal NotitieBoek(int id, string naam, string beschrijving, Eigenaar eigenaar)
    //{
    //    Id = id;
    //    Naam = naam;
    //    Beschrijving = beschrijving;
    //    Eigenaar = eigenaar;
    //}

    //#endregion

    //#region Methods

    //public override string ToString()
    //{
    //    return $"{Id} - {Naam} ";
    //}

    //#endregion

    //#region Validation

    //[NotMapped]
    //public string Error { get { return null; } }

    //[NotMapped]
    //public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

    //[NotMapped]
    //public string this[string columnName]
    //{
    //    get
    //    {
    //        string result = null;

    //        switch (columnName)
    //        {
    //            case "Naam":
    //                if (string.IsNullOrWhiteSpace(Naam))
    //                    result = "Gelieve iets in te vullen";
    //                else if (Naam.Length < 3)
    //                    result = "Titel moet minstens drie karakters lang zijn";

    //                break;
    //            case "Beschrijving":
    //                if (string.IsNullOrWhiteSpace(Beschrijving))
    //                    result = "Gelieve iets in te vullen";
    //                else if (Beschrijving.Length < 5)
    //                    result = "Beschrijving moet minstens vijf karakters lang zijn";
    //                else if (!Regex.IsMatch(Beschrijving, @"^[a-zA-Z]+$"))
    //                    result = "Beschrijving mag geen cijfers bevatten";

    //                break;
    //            case "Eigenaar":
    //                if (Eigenaar.ToString() == null)
    //                    result = "Gelieve een eigenaar te kiezen";
    //                else if (Eigenaar.Id.ToString() == "0")
    //                    result = "Gelieve een eigenaar te kiezen";
    //                break;
    //        }

    //        if (ErrorCollection.ContainsKey(columnName))
    //            ErrorCollection[columnName] = result;
    //        else if (result != null)
    //            ErrorCollection.Add(columnName, result);
    //        OnPropertyChanged("ErrorCollection");


    //        return result;
    //    }
    //}





    //#endregion

//}