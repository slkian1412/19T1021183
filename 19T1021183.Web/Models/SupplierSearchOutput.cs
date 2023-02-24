using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021183.DomainModels;

namespace _19T1021183.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm, phân trang của nhà cung cấp
    /// </summary>
    public class SupplierSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách các Supplier
        /// </summary>
        public List<Supplier> Data { get; set; }
    }
}