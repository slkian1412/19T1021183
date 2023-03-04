using _19T1021183.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021183.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductSearchInput : PaginationSearchInput
    {
        /// <summary>
        ///     Category Id (Default: 0)
        /// </summary>
        public int CategoryID { get; set; } = 0;

        /// <summary>
        ///     Supplier Id (Default: 0)
        /// </summary>
        public int SupplierID { get; set; } = 0;

    }
}