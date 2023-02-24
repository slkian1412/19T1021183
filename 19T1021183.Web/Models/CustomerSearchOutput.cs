using _19T1021183.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021183.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm, phân trang của khách hàng
    /// </summary>
    public class CustomerSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách các khách hàng
        /// </summary>
        public List<Customer> Data { get; set; }
    }
}