using _19T1021183.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021183.Web.Models
{
    public class OrderSearchOutput : PaginationSearchOutput
    {
        public List<Order> Data { get; set; }
        /// <summary>
        /// Trạng thái đơn hàng
        /// </summary>
        public int status { get; set; }
    }
}