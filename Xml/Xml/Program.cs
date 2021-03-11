using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;

namespace Xml
{
    class Stat
    {
        public int CdsCount { get; set; }
        public decimal PricesSum { get; set; }
        public List<string> Countries { get; set; } = new List<string>();
        public int MinYear { get; set; } = int.MaxValue;
        public int MaxYear { get; set; }
        
    }
    
    /*
     * <ARTIST>Dolly Parton</ARTIST>
        <COUNTRY>USA</COUNTRY>
        <COMPANY>RCA</COMPANY>
        <PRICE>9.90</PRICE>
        <YEAR>1982</YEAR>
     */
    class Program
    {
        static void Main(string[] args)
        {
            var doc = XDocument.Load(@"Stat.xml");

            var items = doc.Element("CATALOG")?.Elements("CD");

            if (items != null)
            {
                Stat stat = new Stat();
                foreach (var item in items)
                {
                    stat.CdsCount++;
                    Decimal.TryParse(item.Element("PRICE")?.Value.Replace(".", ","), out decimal sum); // Да, тут  не все так просто, нужно универсальный парсер писать
                    stat.PricesSum += sum;
                    Int32.TryParse(item.Element("YEAR")?.Value, out int year);
                    stat.MinYear = Math.Min(stat.MinYear, year);
                    stat.MaxYear = Math.Max(stat.MaxYear, year);
                    string country = item.Element("COUNTRY")?.Value;
                    if (!string.IsNullOrWhiteSpace(country))
                    {
                        stat.Countries.Add(country); 
                    }
                }

                stat.Countries = stat.Countries.Distinct().ToList();
                Console.WriteLine(JsonSerializer.Serialize(stat));
            }
            else
            {
                Console.WriteLine("Check your xml file");
            }
        }
    }
}