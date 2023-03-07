using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021183.Web.Models
{
    public class OrderSearchInput : PaginationSearchInput
    {
        /// <summary>
        /// Trạng thái đơn hàng
        /// </summary>
        public int Status { get; set; }
    }
}