using MyOwnLib.Common;
using NotitieApplicatie.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotitieApplicatie.Viewmodels.NotitieViewmodels
{
   public class NotitieViewModel : BaseViewModel
    {
		SpeechSynthesizer _speechSynthesizerObj;
		private readonly NotitieApplicatieMainViewmodel _vm;
		private Notitie _geselecteerdeNotitie;
		private Notitie _verwijderdeNotitie;
		private RelayCommand _deleteCommand;
		private RelayCommand _editCommand;
		private bool _notitieEditeren;
		private bool _notitieDeleten;

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
				if(GeselecteerdeNotitie != null) {
					NotitieDeleten = true;
					NotitieEditeren = true;
					MagAfspelen = true;
					MagPauzeren = false;
					MagHervatten = false;
					MagStoppen = false;

			if (GeselecteerdeNotitie.GewijzigdOp == GeselecteerdeNotitie.AangemaaktOp)
				{
					GewijzigdDatum = "Is nooit gewijzigd geweest";
				}
				else
				{
					GewijzigdDatum = GeselecteerdeNotitie.GewijzigdOp.ToString("dd/M/yyyy");
				}
				} else { 
				GewijzigdDatum = String.Empty;
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
			Titel = " Mijn Geselesecteerde Notitie";
			DeleteCommand = new RelayCommand(DeleteNotitie, MagNotitieDeleten);
			EditCommand = new RelayCommand(EditNotitie, MagNotitieBewaren);
			SpeelAf = new RelayCommand(SpeelNotitieAf,Afspelen);
			Hervat = new RelayCommand(HervatNotitieAf,Hervatten);
			Pauze = new RelayCommand(PauzeNotitieAf,Pauzeren);
			Stop = new RelayCommand(StopNotitieAf,Stoppen);
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
				if(_speechSynthesizerObj.State== SynthesizerState.Speaking)
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

		private void VerwijderdeNotitie_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			Console.WriteLine($"deleted");
			NotitieEditeren = true;
		}
	}
}
