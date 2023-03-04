using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021183.Web.Models
{
    public class CategorySearchInput
    {
        public int Page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SearchValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SupplierID { get; set; }
    }
}