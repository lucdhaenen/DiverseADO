using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace AdoGemeenschap
{
    public class RekeningenManager
    {
        public Int32 SaldoBonus()
        {
            var dbManager = new BankDbManager();
            using (var conBank = dbManager.GetConnection())
            {
                using (var comBonus = conBank.CreateCommand())
                {
                    comBonus.CommandType = CommandType.Text;
                    comBonus.CommandText = "UPDATE Rekeningen set Saldo=Saldo*1.1";
                    conBank.Open();
                    return comBonus.ExecuteNonQuery();
                }
            }
        }

        public Boolean Storten(Decimal teStorten, string rekeningNr)
        {
            var dbManager = new BankDbManager();
            using (var conBank = dbManager.GetConnection())
            {
                using (var comStorten = conBank.CreateCommand())
                {
                    //comStorten.CommandType = CommandType.Text;
                    comStorten.CommandType = CommandType.StoredProcedure;
                    //comStorten.CommandText = "UPDATE Rekeningen set Saldo=Saldo+@teStorten where RekeningNr=@rekeningNr";
                    comStorten.CommandText = "Storten";
                    DbParameter parTeStorten = comStorten.CreateParameter();
                    parTeStorten.ParameterName = "@teStorten";
                    parTeStorten.Value = teStorten;
                    comStorten.Parameters.Add(parTeStorten);
                    DbParameter parRekeningNr = comStorten.CreateParameter();
                    parRekeningNr.ParameterName = "@rekeningNr";
                    parRekeningNr.Value = rekeningNr;
                    comStorten.Parameters.Add(parRekeningNr);

                    conBank.Open();
                    return comStorten.ExecuteNonQuery() != 0;
                }
            }
        }

        public Decimal SaldoRekeningRaadplegen(string rekeningNr)
        {
            var dbManager = new BankDbManager();
            using (var conBank = dbManager.GetConnection())
            {
                using (var comSaldo = conBank.CreateCommand())
                {
                    comSaldo.CommandType = CommandType.StoredProcedure;
                    comSaldo.CommandText = "SaldoRekeningRaadplegen";
                    var parRekNr = comSaldo.CreateParameter();
                    parRekNr.ParameterName = "@rekeningNr";
                    parRekNr.Value = rekeningNr;
                    comSaldo.Parameters.Add(parRekNr);
                    conBank.Open();
                    Object resultaat = comSaldo.ExecuteScalar();
                    if (resultaat == null)
                    {
                        throw new Exception("Rekening bestaat niet");
                    }
                    else
                    {
                        return (Decimal)resultaat;
                    }
                }
            }
        }
    }
}
