using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PPSOT.Controllers
{
    public class ShowController : Controller
    {
        public List<BDTariff> tariffs = new List<BDTariff>(10);
        [HttpPost]
        public IActionResult Index(string connection_price, string abonent_price, string name, string name_force)
        {
            List<BDTariff> listOfTarrifs = new List<BDTariff>(10);
            listOfTarrifs = createListOfTarrifs(connection_price, abonent_price, name, name_force);
            ViewBag.tarifs = tariffs;

            return View();
        }

        [HttpPost]
        public ActionResult Show(string connection_price, string abonent_price, string name, string name_force)
        {
            List<BDTariff> listOfTarrifs = new List<BDTariff>(10);
            listOfTarrifs = createListOfTarrifs(connection_price, abonent_price, name, name_force);
            ViewBag.tarifs = tariffs;

            return View();
        }

        public List<BDTariff> createListOfTarrifs(string connection_price, string abonent_price, string name, string name_force)
        {
            ViewBag.tarifs = tariffs;
            Sorting sorting = new Sorting();
            int connectionValue = int.Parse(connection_price);
            int mounthlyValue = int.Parse(abonent_price);
            int operValue = int.Parse(name_force);
            Dictionary<string, int> values = new Dictionary<string, int>
            {
                {"oper", operValue },
                { "connectionprice", connectionValue },
                { "mounthlypay", mounthlyValue }
            };
            tariffs = sorting.getTopTen(values, name);
            return tariffs;
        }
    }
}