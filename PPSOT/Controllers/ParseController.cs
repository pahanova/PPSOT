using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Newtonsoft.Json;
using PPSOT.Models;

namespace PPSOT.Controllers
{
    public class ParseController : Controller
    {
        public IActionResult Index()
        {
            IWebDriver driver = new ChromeDriver();
            // Страницы, с которой парсим
            driver.Url = @"https://www.topnomer.ru/tarifs/";

            Reveal(driver);

            string writePath = @"C:\Users\User\Desktop\СиПИнженерия\Информация.json";
            StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default);

            Tariff tarif = new Tariff();

            var elements1 = driver.FindElements(By.XPath(@".//div[@class='price_tarif_row one']"));
            var elements2 = driver.FindElements(By.XPath(@".//div[@class='price_tarif_row two']"));

            Console.WriteLine(elements1.Count + elements2.Count);

            List<Tariff> tarifs = new List<Tariff>();

            int counter = 0;

            using (sw)
            {
                //sw.WriteLine("[");
                // Перевод спаршеных данных в JSON
                foreach (IWebElement element in elements2)
                {
                    counter += 1;

                    tarif.clearFeatures();
                    SetAllPrices(element, tarif);
                    SetOperator(element, tarif);
                    SetName(element, tarif);
                    SetAttributes(element, tarif);
                    tarif.setId(counter);

                    Tariff newTarif = new Tariff();
                    newTarif.setId(tarif.getId());
                    newTarif.setOper(tarif.getOper());
                    newTarif.setName(tarif.getName());
                    newTarif.setMounthlyPay(tarif.getMounthlyPay());
                    newTarif.setConnectionPrice(tarif.getConnectionPrice());
                    foreach (String feature in tarif.getFeatures())
                        newTarif.addFeature(feature);

                    tarifs.Add(newTarif);


                    /*string json = JsonConvert.SerializeObject(tarif, Formatting.Indented);
                    sw.Write(json);
                    if (counter != elements1.Count + elements2.Count)
                        sw.WriteLine(",");*/

                    Console.WriteLine(counter);
                }

                // Перевод спаршеных данных в JSON
                foreach (IWebElement element in elements1)
                {
                    counter += 1;

                    tarif.clearFeatures();
                    SetAllPrices(element, tarif);
                    SetOperator(element, tarif);
                    SetName(element, tarif);
                    SetAttributes(element, tarif);
                    tarif.setId(counter);

                    Tariff newTarif = new Tariff();
                    newTarif.setId(tarif.getId());
                    newTarif.setOper(tarif.getOper());
                    newTarif.setName(tarif.getName());
                    newTarif.setMounthlyPay(tarif.getMounthlyPay());
                    newTarif.setConnectionPrice(tarif.getConnectionPrice());
                    foreach (String feature in tarif.getFeatures())
                        newTarif.addFeature(feature);

                    tarifs.Add(newTarif);

                    /*string json = JsonConvert.SerializeObject(tarif, Formatting.Indented);
                    sw.Write(json);
                    if (counter!= elements1.Count + elements2.Count)
                        sw.WriteLine(",");*/

                    Console.WriteLine(counter);
                }

                string json = JsonConvert.SerializeObject(tarifs, Formatting.Indented);

                sw.WriteLine(json);

                //sw.WriteLine("]");
            }

            sw.Close();

            return View();
        }

        // Нажатие на нопку "показать ещё"
        public static void Reveal(IWebDriver driver)
        {
            System.Threading.Thread.Sleep(10000);
            driver.FindElement(By.XPath(@".//span[@class='show-more-wrap']")).Click();
            System.Threading.Thread.Sleep(10000);
            driver.FindElement(By.XPath(@".//span[@class='show-more-wrap']")).Click();
            System.Threading.Thread.Sleep(10000);
        }

        // Считывание цен
        public static void SetAllPrices(IWebElement element, Tariff tarif)
        {
            int counter = 0;
            IWebElement price = element.FindElement(By.XPath(@".//div[@class='right_section']"));
            IWebElement price_info = price.FindElement(By.XPath(@".//div[@class='price_info']"));
            var holders = price_info.FindElements(By.XPath(@".//div[@class='holder']"));
            foreach (IWebElement holder in holders)
            {
                IWebElement bmoney = holder.FindElement(By.XPath(@".//i[@class='b-money']"));
                String mon = bmoney.Text.Split('\n')[0];
                mon = String.Join("", mon.Split(' ', '\r'));
                int mone = Convert.ToInt32(mon);
                if (counter == 0)
                    tarif.setMounthlyPay(mone);
                else
                    tarif.setConnectionPrice(mone);
                counter += 1;
            }
        }

        // Считывание операторов
        public static void SetOperator(IWebElement element, Tariff tarif)
        {
            IWebElement section = element.FindElement(By.XPath(@".//div[@class='left_section']"));

            IWebElement b = null;
            IWebElement mf = null;
            IWebElement mts = null;
            IWebElement rt = null;
            IWebElement mt = null;
            IWebElement y = null;
            IWebElement t2 = null;
            IWebElement wf = null;
            IWebElement kt = null;
            IWebElement mg = null;
            IWebElement ez = null;
            IWebElement ss = null;
            IWebElement mtt = null;
            try
            {
                b = section.FindElement(By.XPath(".//div[@class='beeline section_head']"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                mf = section.FindElement(By.XPath(".//div[@class='megafon section_head']"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                mts = section.FindElement(By.XPath(".//div[@class='mts section_head']"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                rt = section.FindElement(By.XPath(".//div[@class='rostelecom section_head']"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                mt = section.FindElement(By.XPath(".//div[@class='matriks-telekom section_head']"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                y = section.FindElement(By.XPath(".//div[@class='yota section_head']"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                t2 = section.FindElement(By.XPath(".//div[@class='tele2 section_head']"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                wf = section.FindElement(By.XPath(".//div[@class='wifire section_head']"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                kt = section.FindElement(By.XPath(".//div[@class='kantrikom section_head']"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                mg = section.FindElement(By.XPath(".//div[@class='mango-telekom section_head']"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                ez = section.FindElement(By.XPath(".//div[@class='ezmobile section_head']"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                ss = section.FindElement(By.XPath(".//div[@class='simsim section_head']"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                mtt = section.FindElement(By.XPath(".//div[@class='mtt section_head']"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            if (mf != null)
                tarif.setOper("Мегафон");
            if (b != null)
                tarif.setOper("Билайн");
            if (mts != null)
                tarif.setOper("МТС");
            if (rt != null)
                tarif.setOper("Ростелеком");
            if (mt != null)
                tarif.setOper("Матрикс телеком");
            if (y != null)
                tarif.setOper("Йота");
            if (t2 != null)
                tarif.setOper("Теле2");
            if (wf != null)
                tarif.setOper("Wifire");
            if (kt != null)
                tarif.setOper("КантриКом");
            if (mg != null)
                tarif.setOper("Манго телеком");
            if (ez != null)
                tarif.setOper("EZ Mobile");
            if (ss != null)
                tarif.setOper("Sim Sim");
            if (mtt != null)
                tarif.setOper("МТТ");
        }

        // Считывание названий тарифов
        public static void SetName(IWebElement element, Tariff tarif)
        {
            IWebElement section = element.FindElement(By.XPath(@".//div[@class='left_section']"));
            IWebElement oper = section.FindElement(By.XPath(@".//div[@class]"));
            IWebElement refer = section.FindElement(By.XPath(@".//a[@class='upper']"));
            tarif.setName(refer.Text);
        }

        // Считывание свойств
        public static void SetAttributes(IWebElement element, Tariff tarif)
        {
            IWebElement section = element.FindElement(By.XPath(@".//div[@class='left_section']"));
            IWebElement attr = section.FindElement(By.XPath(@".//div[@class='price_tarif_row_txt typo grey']"));
            attr = attr.FindElement(By.XPath(@".//ul"));
            var att = attr.FindElements(By.XPath(@".//li"));
            foreach (IWebElement attribute in att)
            {
                tarif.addFeature(attribute.Text);
            }
        }
    }
}