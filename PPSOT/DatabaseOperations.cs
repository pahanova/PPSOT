using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Text.Json;
using Newtonsoft.Json;
using PPSOT.Models;

namespace PPSOT
{
    public class DatabaseOperations
    {
        static public void parseJson()
        {
            
            using (DatabaseAdapter db = new DatabaseAdapter())
            {
                List<Tariff> rawTariffs = new List<Tariff>();
                String jsonString = "";
                //FileStream file = File.OpenRead("data.json");
                //byte[] array = new byte[file.Length];
                //file.Read(array, 0, array.Length);
                jsonString = File.ReadAllText("data.json", Encoding.UTF8);
                //var utf8Reader = new Utf8JsonReader(array);
                rawTariffs = JsonConvert.DeserializeObject<List<Tariff>>(jsonString);
                //rawTariffs = tariffs.tariffs;
                foreach (Tariff t in rawTariffs)
                {
                    string nm = t.getName();
                    string op = t.getOper();
                    List<string> featuresList = t.getFeatures();
                    int cp = t.getConnectionPrice();
                    int mp = t.getMounthlyPay();
                    string ft = "";
                    foreach (string feature in featuresList)
                    {
                        ft += feature + "\r\n";
                    }
                    BDTariff trueTariff = new BDTariff()
                    { 
                        connectionprice = cp,
                        features = ft, mounthlypay = mp,
                        name = nm, oper = op 
                    };
                    db.tariff.Add(trueTariff);
                }
                db.SaveChanges();
            }
        }

        public static List<BDTariff> getTariffListFromDb()
        {
            List<BDTariff> tariffs = new List<BDTariff>();
            using (DatabaseAdapter db = new DatabaseAdapter())
            {
                tariffs = db.tariff.ToList<BDTariff>();
                return tariffs; 
            }
            
        }
    }
}
