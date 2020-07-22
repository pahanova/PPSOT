using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPSOT.Models
{
    [JsonObject(MemberSerialization.Fields)]
    public class Tariff
    {
        private int id;
        private string name;
        private string oper;
        List<string> features;
        private int connectionPrice;
        private int mounthlyPay;

        public Tariff()
        {
            features = new List<string>();
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return this.name;
        }

        public void setOper(string oper)
        {
            this.oper = oper;
        }

        public string getOper()
        {
            return this.oper;
        }

        public void addFeature(string feature)
        {
            this.features.Add(feature);
        }

        public List<string> getFeatures()
        {
            return this.features;
        }

        public void setFeatures(List<string> features)
        {
            this.features = features;
        }

        public void clearFeatures()
        {
            this.features.Clear();
        }

        public void setConnectionPrice(int connectionPrice)
        {
            this.connectionPrice = connectionPrice;
        }

        public int getConnectionPrice()
        {
            return this.connectionPrice;
        }

        public void setMounthlyPay(int mounthlyPay)
        {
            this.mounthlyPay = mounthlyPay;
        }

        public int getMounthlyPay()
        {
            return this.mounthlyPay;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return this.id;
        }
    }
}