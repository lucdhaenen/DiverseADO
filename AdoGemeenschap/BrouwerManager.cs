﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace AdoGemeenschap
{
    public class BrouwerManager
    {
        public ObservableCollection<Brouwer> GetBrouwersBeginNaam(string beginNaam)
        {
            ObservableCollection<Brouwer> brouwers = new ObservableCollection<Brouwer>();
            var manager = new BierenDbManager();
            using (var conBieren = manager.GetConnection())
            {
                using (var comBrouwers = conBieren.CreateCommand())
                {
                    comBrouwers.CommandType = CommandType.Text;
                    if (beginNaam != string.Empty)
                    {
                        comBrouwers.CommandText = "SELECT * FROM Brouwers WHERE BrNaam like @zoals order by BrNaam";
                        var parZoals = comBrouwers.CreateParameter();
                        parZoals.ParameterName = "@zoals";
                        parZoals.Value = beginNaam + "%";
                        comBrouwers.Parameters.Add(parZoals);
                    }
                    else comBrouwers.CommandText = "select * from Brouwers";
                    conBieren.Open();
                    using (var rdrBrouwers = comBrouwers.ExecuteReader())
                    {
                        Int32 brouwerNrPos = rdrBrouwers.GetOrdinal("BrouwerNr");
                        Int32 brNaamPos = rdrBrouwers.GetOrdinal("BrNaam");
                        Int32 adresPos = rdrBrouwers.GetOrdinal("Adres");
                        Int32 postcodePos = rdrBrouwers.GetOrdinal("Postcode");
                        Int32 gemeentePos = rdrBrouwers.GetOrdinal("Gemeente");
                        Int32 omzetPos = rdrBrouwers.GetOrdinal("Omzet");
                        Nullable<Int32> omzet;
                        while (rdrBrouwers.Read())
                        {
                            if (rdrBrouwers.IsDBNull(omzetPos))
                            {
                                omzet = null;
                            }
                            else
                            {
                                omzet = rdrBrouwers.GetInt32(omzetPos);
                            }
                            brouwers.Add(new Brouwer(rdrBrouwers.GetInt32(brouwerNrPos), rdrBrouwers.GetString(brNaamPos),
                                rdrBrouwers.GetString(adresPos), rdrBrouwers.GetInt16(postcodePos), rdrBrouwers.GetString(gemeentePos),
                                omzet));
                        }
                    }
                }
            }
            return brouwers;
        }

        public void SchrijfVerwijderingen(List<Brouwer> brouwers)
        {
            var manager = new BierenDbManager();
            using (var conBieren = manager.GetConnection())
            {
                using (var comDelete = conBieren.CreateCommand())
                {
                    comDelete.CommandType = CommandType.Text;
                    comDelete.CommandText = "delete from brouwers where BrouwerNr=@brouwernr";
                    var parBrouwerNr = comDelete.CreateParameter();
                    parBrouwerNr.ParameterName = "@brouwernr";
                    comDelete.Parameters.Add(parBrouwerNr);
                    conBieren.Open();
                    foreach (Brouwer eenBrouwer in brouwers)
                    {
                        parBrouwerNr.Value = eenBrouwer.BrouwerNr;
                        comDelete.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
