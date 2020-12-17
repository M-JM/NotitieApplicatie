using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace NotitieApplicatie.Viewmodels.NotitieViewmodels
{
    public class NotitieViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        SpeechSynthesizer _speechSynthesizerObj;
        private readonly NotitieApplicatieMainViewmodel _vm;
        private Notitie _geselecteerdeNotitie;
        private Notitie _verwijderdeNotitie;
        private RelayCommand _deleteCommand;
        private RelayCommand _editCommand;
        private bool _notitieEditeren;
        private bool _notitieDeleten;

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                ClearErrors(nameof(Email));
                SetProperty(ref _email, value);
                if (!IsValidEmailAddress(value) || String.IsNullOrEmpty(value))
                {
                    AddError(nameof(Email), "Je moet een geldige email address opgeven");
                    MagSturen = false;
                }
                else
                {
                    MagSturen = true;
                }


                OnPropertyChanged(nameof(Email));

            }
        }

        private bool _magSturen;

        public bool MagSturen
        {
            get { return _magSturen; }
            set
            {
                SetProperty(ref _magSturen, value);
            }
        }


        private RelayCommand _emailCommand;

        public RelayCommand EmailCommand
        {
            get { return _emailCommand; }
            set
            {
                SetProperty(ref _emailCommand, value);
            }
        }



        public RelayCommand EditCommand
        {
            get { return _editCommand; }
            set
            {
                SetProperty(ref _editCommand, value);
            }
        }

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand; }
            set
            {
                SetProperty(ref _deleteCommand, value);
            }
        }

        public Notitie GeselecteerdeNotitie
        {
            get { return _geselecteerdeNotitie; }
            set
            {
                SetProperty(ref _geselecteerdeNotitie, value);
                if (GeselecteerdeNotitie != null)
                {
                    NotitieDeleten = true;
                    NotitieEditeren = true;
                    MagAfspelen = true;
                    MagPauzeren = false;
                    MagHervatten = false;
                    MagStoppen = false;
                    Email = "";
                    MagSturen = false;

                    if (GeselecteerdeNotitie.GewijzigdOp == GeselecteerdeNotitie.AangemaaktOp)
                    {
                        GewijzigdDatum = "Is nooit gewijzigd geweest";
                    }
                    else
                    {
                        GewijzigdDatum = GeselecteerdeNotitie.GewijzigdOp.ToString("dd/M/yyyy");
                    }
                }
                else
                {
                    GewijzigdDatum = String.Empty;
                    MagAfspelen = false;
                }
            }
        }

        private string _gewijzigdOp;

        public string GewijzigdDatum
        {
            get { return _gewijzigdOp; }
            set
            {
                SetProperty(ref _gewijzigdOp, value);
            }
        }

        public Boolean NotitieEditeren
        {
            get { return _notitieEditeren; }
            set
            {
                SetProperty(ref _notitieEditeren, value);
            }
        }

        public Boolean NotitieDeleten
        {
            get { return _notitieDeleten; }
            set
            {
                SetProperty(ref _notitieDeleten, value);
            }
        }

        public Notitie VerwijderdeNotitie
        {
            get { return _verwijderdeNotitie; }
            set
            {
                SetProperty(ref _verwijderdeNotitie, value);
                VerwijderdeNotitie.PropertyChanged += VerwijderdeNotitie_PropertyChanged;
            }
        }

        private RelayCommand _speelAf;

        public RelayCommand SpeelAf
        {
            get { return _speelAf; }
            set
            {
                SetProperty(ref _speelAf, value);
            }
        }

        private RelayCommand _hervat;

        public RelayCommand Hervat
        {
            get { return _hervat; }
            set
            {
                SetProperty(ref _hervat, value);
            }
        }

        private RelayCommand _pauze;

        public RelayCommand Pauze
        {
            get { return _pauze; }
            set
            {
                SetProperty(ref _pauze, value);
            }
        }

        private RelayCommand _stop;

        public RelayCommand Stop
        {
            get { return _stop; }
            set
            {
                SetProperty(ref _stop, value);
            }
        }

        private bool _magAfspelen;

        public bool MagAfspelen
        {
            get { return _magAfspelen; }
            set
            {
                SetProperty(ref _magAfspelen, value);
            }
        }
        private bool _magpauzeren;

        public bool MagPauzeren
        {
            get { return _magpauzeren; }
            set
            {
                SetProperty(ref _magpauzeren, value);
            }
        }
        private bool _magStoppen;

        public bool MagStoppen
        {
            get { return _magStoppen; }
            set
            {
                SetProperty(ref _magStoppen, value);
            }
        }
        private bool _magHervatten;



        public bool MagHervatten
        {
            get { return _magHervatten; }
            set
            {
                SetProperty(ref _magHervatten, value);
            }
        }

        public NotitieViewModel(NotitieApplicatieMainViewmodel vm)
        {
            _speechSynthesizerObj = new SpeechSynthesizer();
            _vm = vm;
            Titel = " Mijn Geselecteerde Notitie";
            DeleteCommand = new RelayCommand(DeleteNotitie, MagNotitieDeleten);
            EditCommand = new RelayCommand(EditNotitie, MagNotitieBewaren);
            SpeelAf = new RelayCommand(SpeelNotitieAf, Afspelen);
            Hervat = new RelayCommand(HervatNotitieAf, Hervatten);
            Pauze = new RelayCommand(PauzeNotitieAf, Pauzeren);
            Stop = new RelayCommand(StopNotitieAf, Stoppen);
            EmailCommand = new RelayCommand(SendEmail, MagEmailSturen);
        }

        private bool Afspelen(object arg)
        {
            return MagAfspelen;
        }

        private bool Hervatten(object arg)
        {
            return MagHervatten;
        }

        private bool Pauzeren(object arg)
        {
            return MagPauzeren;
        }

        private bool Stoppen(object arg)
        {
            return MagStoppen;
        }

        private void StopNotitieAf(object obj)
        {
            if (_speechSynthesizerObj != null)
            {
                _speechSynthesizerObj.Dispose();
                MagAfspelen = true;
                MagHervatten = false;
                MagPauzeren = false;
                MagStoppen = false;
            }
        }

        private void PauzeNotitieAf(object obj)
        {
            if (_speechSynthesizerObj != null)
            {
                if (_speechSynthesizerObj.State == SynthesizerState.Speaking)
                {
                    _speechSynthesizerObj.Pause();
                    MagHervatten = true;
                    MagStoppen = true;
                    MagAfspelen = false;
                }
            }
        }

        private void HervatNotitieAf(object obj)
        {
            if (_speechSynthesizerObj != null)
            {
                if (_speechSynthesizerObj.State == SynthesizerState.Paused)
                {
                    _speechSynthesizerObj.Resume();
                    MagHervatten = false;
                    MagStoppen = true;
                    MagAfspelen = false;
                }
            }
        }

        private void SpeelNotitieAf(object obj)
        {
            _speechSynthesizerObj.Dispose();
            if (GeselecteerdeNotitie.Inhoud != "")
            {
                _speechSynthesizerObj = new SpeechSynthesizer();
                var test = _speechSynthesizerObj.GetInstalledVoices();
                Console.WriteLine(test);
                _speechSynthesizerObj.SpeakAsync(GeselecteerdeNotitie.Inhoud);
                MagPauzeren = true;
                MagStoppen = true;
            }
        }

        private void DeleteNotitie(Object parameter = null)
        {
            try
            {
                DbRepository.RemoveNotitie(GeselecteerdeNotitie);
                VerwijderdeNotitie = GeselecteerdeNotitie;
                NotitieDeleten = false;
                NotitieEditeren = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }


        }

        private void EditNotitie(Object parameter = null)
        {
            _vm.SelectedView = new EditNotitieViewModel(_vm, GeselecteerdeNotitie);
        }

        private Boolean MagNotitieBewaren(object parameter = null)
        {
            return NotitieEditeren;
        }

        private Boolean MagNotitieDeleten(object parameter = null)
        {
            return NotitieDeleten;
        }

        private Boolean MagEmailSturen(object parameter = null)
        {
            return MagSturen;
        }

        private void VerwijderdeNotitie_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine($"deleted");
            NotitieEditeren = true;
        }

        private void SendEmail(Object parameter = null)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.mailbox.org", 587)
                {
                    EnableSsl = true,
                    Timeout = 10000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("notitieapplicatietest@mailbox.org", "Aqwxsz12..")
                };

                MailMessage msg = new MailMessage
                {
                    From = new MailAddress("notitieapplicatietest@mailbox.org")
                };
                msg.To.Add(Email);
                msg.Subject = "Gedeelde Notitie van " + GeselecteerdeNotitie.Eigenaar.Naam;
                msg.IsBodyHtml = true;

                msg.Body = "<html><body><p>Beste,</p>" +
                  " Hierbij wenst " + GeselecteerdeNotitie.Eigenaar.Naam + " met jouw zijn notitie te delen."
                    + "<p>Titel:</p>"
                    + "<p>" + GeselecteerdeNotitie.Titel + "</p>"
                    + "<p>Inhoud:</p>"
                    + "<p>" + GeselecteerdeNotitie.Inhoud + "</p>"
                    + "<p>Met vriendelijke groeten,<br>Notitie Applicatie </br></p>" +
                    "</br><p>Dit is een automatische email  , Gelieve hier niet op te antwoorden </p>" + " </body> </html>";

                client.Send(msg);
                MessageBox.Show("Email verstuurd");

            }
            catch (Exception ex)
            {
                MyLogger.GetInstance().Error("Something went wrong with trying to send note by email " + ex.Message);
                MessageBox.Show("Er is een fout opgetreden bij het versturen van de email", "Er is iets verkeerd afgeplopen");
            }
        }

        public static bool IsValidEmailAddress(string email)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(email);
        }

        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors { get { return _propertyErrors.Any(propErrors => propErrors.Value != null && propErrors.Value.Count > 0); } }

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

        private void ClearErrors(string propertyName)
        {
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorschanged(propertyName);
            }

        }

        private void OnErrorschanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

    }
}
