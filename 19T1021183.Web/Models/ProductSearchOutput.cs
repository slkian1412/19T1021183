using _19T1021183.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021183.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm, phân trang của mặt hàng
    /// </summary>
    public class ProductSearchOutput :PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách các product
        /// </summary>
        public List<Product> Data { get; set; }
    }
}