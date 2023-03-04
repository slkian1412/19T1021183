using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021183.DomainModels;

namespace _19T1021183.DataLayers
{
    interface IEmployeeDAL
    {
        int Add(Employee data);
        int Count(string searchValue);
        bool Delete(int id);
        Employee Get(int id);
        bool InUsed(int id);
        IList<Employee> List(int page, int pageSize, string searchValue);
        bool Update(Employee data);

    }
}
