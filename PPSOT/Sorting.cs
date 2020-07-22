using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPSOT
{
    // Класс для составления списка лучших тарифов
    public class Sorting
    {
        private List<BDTariff> tariffsList;
        public Sorting ()
        {
            tariffsList = DatabaseOperations.getTariffListFromDb();
        }
        
        // Сортировка по оператору. Если mode == false, то все тарифы не этого оператора отрезаются
        private List<BDTariff> sortByTariff(List<BDTariff> tariffs, string oper, bool mode)
        {
            
            List<BDTariff> newTariffs = new List<BDTariff>();
            foreach (BDTariff t in tariffs)
                if (t.oper.Equals(oper))
                    newTariffs.Add(t);
            if (mode)
                foreach (BDTariff t in tariffs)
                    if (!t.oper.Equals(oper))
                        newTariffs.Add(t);
            // Если в обрезанном списке меньше десяти тарифов, дополеяем
            if (newTariffs.Count < 10)
            {
                foreach (BDTariff t in tariffs)
                {
                    if (!t.oper.Equals(oper))
                    {
                        tariffs.Remove(t);
                    }
                }
                var result = from tariff in tariffs
                             orderby tariff.connectionprice, tariff.mounthlypay
                             select tariff;
                tariffs.Clear();
                foreach (BDTariff t  in result)
                {
                    tariffs.Add(t);
                }
                foreach (BDTariff t in tariffs)
                    if (t.oper.Equals(oper))
                        newTariffs.Add(t);
            }
            return newTariffs;
        }

        // Функция, возвращающая список из 10 лучших тарифов. На вход принимает три приоритета
        // values[0] - оператор, values[1] - цена подключения, values[2] - абонентская плата
        public List<BDTariff> getTopTen(Dictionary<string, int> values, string oper)
        {
            List<BDTariff> tariffs = tariffsList;
            int max1 = -1, max2 = -1, max3 = -1;
            string maxs1 = "", maxs2 = "", maxs3 = "";
            foreach (KeyValuePair<string, int>  val in values)
            {
                if (val.Value >= max1)
                {
                    max3 = max2;
                    maxs3 = maxs2;
                    max2 = max1;
                    maxs2 = maxs1;
                    max1 = val.Value;
                    maxs1 = val.Key;
                    continue;
                }
                if (val.Value >= max2)
                {
                    max3 = max2;
                    maxs3 = maxs2;
                    max2 = val.Value;
                    maxs2 = val.Key;
                    continue;
                }
                if (val.Value >= max3)
                {
                    max3 = val.Value;
                    maxs3 = val.Key;
                }
                
            }
            // Отрезаются все тарифы с не указанным оператором
            if (maxs1 == "oper")
            {
                tariffs = sortByTariff(tariffs, oper, false);
                if (maxs2 == "connectionprice")
                {
                    var result = from tariff in tariffs 
                                 orderby tariff.connectionprice, tariff.mounthlypay 
                                 select tariff;
                    tariffs = new List<BDTariff>();
                    foreach (BDTariff t in result)
                        tariffs.Add(t);
                }
                else
                {
                    var result = from tariff in tariffs 
                                 orderby tariff.mounthlypay, tariff.connectionprice
                                 select tariff;
                    tariffs = new List<BDTariff>();
                    foreach (BDTariff t in result)
                        tariffs.Add(t);
                    
                }
            }
            // ну а тут будет рандом какой-то
            else if (maxs2 == "oper")
            {
                if (maxs1 == "connectionprice")
                {
                    var result = from tariff in tariffs
                                 orderby tariff.connectionprice
                                 select tariff;
                    tariffs = new List<BDTariff>();
                    foreach (BDTariff t in result)
                        tariffs.Add(t);
                    tariffs = sortByTariff(tariffs, oper, true);
                    result = from tariff in tariffs
                                 orderby tariff.mounthlypay
                                 select tariff;
                    tariffs = new List<BDTariff>();
                    foreach (BDTariff t in result)
                        tariffs.Add(t);
                }
                else
                {
                    var result = from tariff in tariffs
                                 orderby tariff.mounthlypay
                                 select tariff;
                    tariffs = new List<BDTariff>();
                    foreach (BDTariff t in result)
                        tariffs.Add(t);
                    tariffs = sortByTariff(tariffs, oper, true);
                    result = from tariff in tariffs
                             orderby tariff.connectionprice
                             select tariff;
                    tariffs = new List<BDTariff>();
                    foreach (BDTariff t in result)
                        tariffs.Add(t);
                }
            }
            // По опреатору сортировка не производится
            else if (maxs3 == "oper")
            {
                if (maxs1 == "connectionprice")
                {
                    var result = from tariff in tariffs
                                 orderby tariff.connectionprice, tariff.mounthlypay
                                 select tariff;
                    tariffs = new List<BDTariff>();
                    foreach (BDTariff t in result)
                        tariffs.Add(t);
                }
                else
                {
                    var result = from tariff in tariffs
                                 orderby tariff.mounthlypay, tariff.connectionprice
                                 select tariff;
                    tariffs = new List<BDTariff>();
                    foreach (BDTariff t in result)
                        tariffs.Add(t);
                }
            }
            List<BDTariff> newTariffs = new List<BDTariff>();
            for (int i = 0; i < 10; i++)
            {
                newTariffs.Add(tariffs.ElementAt(i));
            }
            return newTariffs;
        }
    }
}
