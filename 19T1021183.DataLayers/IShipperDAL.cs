using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021183.DomainModels;

namespace _19T1021183.DataLayers
{
    interface IShipperDAL
    {
        int Add(Shipper data);
        int Count(string searchValue = "");
        bool Delete(int id);
        Shipper Get(int id);
        bool InUsed(int id);
        IList<Shipper> List(int page, int pageSize, string searchValue);
        bool Update(Shipper data);

    }
}
