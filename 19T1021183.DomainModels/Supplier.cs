using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021183.DomainModels
{
    /// <summary>
    /// Nhà cung cấp
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// 
        /// </summary>
        public int SupplierID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SupplierName { get; set;}
        /// <summary>
        /// 
        /// </summary>
        public string ContactName { get; set;}
        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set;}
        ///
        public string City { get; set;}
        ///
        public string PostalCode { get; set;}
        ///
        public string Country { get; set;}  
        ///
        public string Phone { get; set;}    
    }
}
