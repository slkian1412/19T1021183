using _19T1021183.DataLayers;
using _19T1021183.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021183.DataLayers.FakeDB
{
    /// <summary>
    /// 
    /// </summary>
    public class CountryDAL : ICountryDAL
    {
        public IList<Country> List()
        {
            List<Country> Data = new List<Country>();
            Data.Add(new Country() { CountryName = "Việt Nam" });
            Data.Add(new Country() { CountryName = "Trung Quốc" });
            Data.Add(new Country() { CountryName = "Lào"});
            return Data;
        }
    }
}
