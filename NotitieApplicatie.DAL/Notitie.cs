using System;

namespace NotitieApplicatie.DAL
{
    public class Notitie
    {
        #region Properties & Fields

        public int Id { get; set; }

        public string Titel { get; set; }

        public string Inhoud { get; set; }

        public DateTime AangemaaktOp { get; set; }

        #endregion

        #region Constructors

        // Parameterloos ctor nodig wegens hoe EF is opgebouwd
        private Notitie()
        {

        }

        public Notitie(string titel, string inhoud, DateTime aangemaaktOp)
            : this(0, titel, inhoud,aangemaaktOp)
        {

        }

        internal Notitie(int id, string titel, string inhoud, DateTime aangemaaktOp)
        {
            Id = id;
            Titel = titel;
            Inhoud = inhoud;
            AangemaaktOp = aangemaaktOp;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{Id} - {Titel} - {Inhoud} - {AangemaaktOp.ToShortDateString()} ";
        }

        #endregion
    }
}
