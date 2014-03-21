using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdoGemeenschap
{
    public class Figuur
    {
        private Int32 IDValue;
        private string naamValue;
        private System.Object versieValue;

        public Int32 ID
        {
            get { return IDValue; }
            set { IDValue = value; }
        }
        public string Naam
        {
            get { return naamValue; }
            set { naamValue = value; }
        }
        public object Versie
        {
            get { return versieValue; }
            set { versieValue = value; }
        }
        public Figuur(Int32 id, string naam, object versie)
        {
            this.IDValue = id;
            this.Naam = naam;
            this.Versie = versie;
        }
        public Figuur() { }
    }
}
