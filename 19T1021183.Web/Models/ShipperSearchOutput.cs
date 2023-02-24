using _19T1021183.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021183.Web.Models
{
    public class ShipperSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Lấy danh sách các shipper
        /// </summary>
        public List<Shipper> Data { get; set; }
    }
}