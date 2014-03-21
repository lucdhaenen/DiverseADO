using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdoGemeenschap
{
    public class Brouwer 
    {
        private Int32 brouwersNrValue;
        private string brNaamValue;
        private string adresValue;
        private Int16 postcodeValue;
        private string gemeenteValue;
        private Nullable<Int32> omzetValue;

        public Int32 BrouwerNr
        { get { return brouwersNrValue; } }

        public string BrNaam
        {
            get { return brNaamValue; }
            set { brNaamValue = value; }
        }

        public String Adres
        {
            get { return adresValue; }
            set { adresValue = value; }
        }

        public Int16 Postcode
        {
            get { return postcodeValue; }
            set { postcodeValue = value; }            
        }

        public String Gemeente
        {
            get { return gemeenteValue; }
            set { gemeenteValue = value; }
        }

        public Nullable<Int32> Omzet
        {
            get { return omzetValue; }
            set {
                    if (value.HasValue && Convert.ToInt32(value) < 0)
                    { throw new Exception("Omzet moet positief zijn"); } 
                    else {omzetValue = value; }
                }
        }

        public Brouwer(Int32 brNr, String brNaam, String adres, Int16 postcode, String gemeente, Nullable<Int32> omzet)
        {
            brouwersNrValue = brNr;
            this.BrNaam = brNaam;
            this.Adres = adres;
            this.Postcode = postcode;
            this.Gemeente = gemeente;
            this.Omzet = omzet;
        }
    }
}
