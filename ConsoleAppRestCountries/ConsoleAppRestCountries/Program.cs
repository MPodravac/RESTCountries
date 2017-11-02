using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ConsoleAppRestCountries
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader oSr = new StreamReader("countries.json");
            string sJson = "";
            using (oSr)
            {
                sJson = oSr.ReadToEnd();
            }

            JObject oJson = JObject.Parse(sJson);
            var oCountries = oJson["countries"].ToList();
            List<Country> lCountries = new List<Country>();
            for (int i=0; i < oCountries.Count; i++)
            {
                lCountries.Add(new Country
                {
                    sCode = (string)oCountries[i]["alpha2Code"],
                    sName = (string)oCountries[i]["name"],
                    sCapital = (string)oCountries[i]["capital"],
                    nPopulation = (int)oCountries[i]["population"],
                    fArea = (float)oCountries[i]["area"]
                });
            }
            var OrderByQuery = from c in lCountries.OrderByDescending(o => o.nPopulation) select c;
            List<Country> lSortedCountries = OrderByQuery.ToList();
            for(int i=0;i<lSortedCountries.Count();i++)
            {
                Console.WriteLine(lSortedCountries[i].nPopulation + " " + lSortedCountries[i].sCode + ' ' + lSortedCountries[i].sName + ' ' + lSortedCountries[i].sCapital + ' ' + lSortedCountries[i].fArea);
            }

            Console.ReadKey();
        }
    }
}
