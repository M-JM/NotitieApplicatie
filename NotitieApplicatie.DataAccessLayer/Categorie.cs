using MyOwnLib.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace NotitieApplicatie.DataAccessLayer
{
    public class Categorie : ObservableObject, INotifyDataErrorInfo
    {
        #region Properties & Fields

        public int Id { get; set; }

        private string _naam;
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
                if (Naam.Length < 2)
                {
                    AddError(nameof(Naam), "Mag niet minder dan 2 karakters zijn");
                }
                OnPropertyChanged(nameof(Naam));

            }
        }

        private string _kleur;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();

        public string Kleur
        {
            get { return _kleur; }
            set
            {
                ClearErrors(nameof(Kleur));
                SetProperty(ref _kleur, value);
                if (String.IsNullOrEmpty(value))
                {
                    AddError(nameof(Kleur), "Je moet een Kleur opgeven");
                }
               
            }
        }

       
        public bool HasErrors { get { return _propertyErrors.Any(propErrors => propErrors.Value != null && propErrors.Value.Count > 0); } }

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
