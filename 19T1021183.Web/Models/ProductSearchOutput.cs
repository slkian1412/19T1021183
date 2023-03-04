using _19T1021183.DomainModels;
using _19T1021183.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace _19T1021183.Web.Models
{
    public class ProductSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Product> Data { get; set; }
        /// <summary>
        ///     Category Id
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        ///     Supplier Id
        /// </summary>
        public int SupplierID { get; set; }

        /// <summary>
        ///     From
        /// </summary>
        public int From => (Page - 1) * PageSize + 1;

        /// <summary>
        ///     To
        /// </summary>
        public int To => (Page - 1) * PageSize + Data.Count;
    }


}