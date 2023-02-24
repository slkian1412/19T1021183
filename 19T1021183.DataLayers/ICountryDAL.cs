using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021183.DomainModels;



namespace _19T1021183.DataLayers
{
    /// <summary>
    /// Định ngĩa phép xử lý dữ liệu liên quan đến quốc gia
    /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// Lấy danh sách quốc gia
        /// </summary>
        /// <returns></returns>
        IList<Country> List();
    }
}
