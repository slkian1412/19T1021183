using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021183.DomainModels;
using _19T1021183.DataLayers;
using System.Data.SqlClient;
using _19T1021183.DataLayers.FakeDB;
using System.Configuration;
using _19T1021183.DataLayers.SQL_Server;

namespace _19T1021183.BusinessLayers
{
    /// <summary>
    /// cung cấp các chức năng nghiệp vụ liên quan đến: quốc gia nhà cung cấp khách hàng
    /// người giao hàng nhân viên loại hàng
    /// </summary>

    public static class CommonDataService
    {
        private static ICountryDAL countryDB;
        private static ICommonDAL<Supplier> supplierDB;
        private static ICommonDAL<Customer> customerDB;
        private static ICommonDAL<Category> categoryDB;
        private static ICommonDAL<Employee> employeeDB;
        private static ICommonDAL<Shipper> shipperDB;

        /// <summary>
        /// Ctor
        /// </summary>
        static CommonDataService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            //   countryDB = new DataLayers.FakeDB.CountryDAL();
            countryDB = new DataLayers.SQL_Server.CountryDAL(connectionString);
            supplierDB = new DataLayers.SQL_Server.SupplierDAL(connectionString);
            customerDB = new DataLayers.SQL_Server.CustomerDAL(connectionString);
            categoryDB = new DataLayers.SQL_Server.CategoryDAL(connectionString);
            shipperDB = new DataLayers.SQL_Server.ShipperDAL(connectionString);
            employeeDB = new DataLayers.SQL_Server.EmployeeDAL(connectionString);
        }
        #region chức năng tác nghiệp liên quan đến quốc gia
        /// <summary>
        /// Danh sách các quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountries()
        {
            return countryDB.List().ToList();
        }

       


        #endregion

        #region chức năng tác nghiệp liên quan đến nhà cung cấp
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page"> trang cần xem</param>
        /// <param name="pageSize"> Số dòng hiển thị trên mỗi trang (0 nếu không phân trang)</param>
        /// <param name="searchValue">Giá trị tìm kiếm(chuỗi rỗng nếu lấy toàn bộ dữ liệu)</param>
        /// <param name="rowCount">tham số đầu ra: số dòng dữ liệu truy vấn được</param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(int page,
                int pageSize,
                string searchValue,
                out int rowCount)
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy dánh sách nhà cung cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(string searchValue = "")
        {
            return supplierDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm 1 nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return supplierDB.Get(supplierID);
        }
        /// <summary>
        /// Thêm nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static int AddGetSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }
        /// <summary>
        /// Cập nhật nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }
        /// <summary>
        /// Xoá nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int supplierID)
        {
            return supplierDB.Delete(supplierID);
        }
        /// <summary>
        /// Kiểm tra xem nhà cung cấp có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUserSupplier(int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }

        #endregion
        #region chức năng tác nghiệp liên quan đến khách hàng
        /// <summary>
        /// Tìm kiếm và lấy danh sách khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page"> trang cần xem</param>
        /// <param name="pageSize"> Số dòng hiển thị trên mỗi trang (0 nếu không phân trang)</param>
        /// <param name="searchValue">Giá trị tìm kiếm(chuỗi rỗng nếu lấy toàn bộ dữ liệu)</param>
        /// <param name="rowCount">tham số đầu ra: số dòng dữ liệu truy vấn được</param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page,
            int pageSize,
            string searchValue,
            out int rowCount)
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(string searchValue = "")
        {
            return customerDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int customerID)
        {
            return customerDB.Get(customerID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddGetCustomer(Customer data)
        {
            return customerDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int customerID)
        {
            return customerDB.Delete(customerID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool InUserCustomer(int customerID)
        {
            return customerDB.InUsed(customerID);
        }

        #endregion

        #region chức năng tác nghiệp liên quan đến loaij hàng
        /// <summary>
        /// Tìm kiếm và lấy danh sách loại hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page"> trang cần xem</param>
        /// <param name="pageSize"> Số dòng hiển thị trên mỗi trang (0 nếu không phân trang)</param>
        /// <param name="searchValue">Giá trị tìm kiếm(chuỗi rỗng nếu lấy toàn bộ dữ liệu)</param>
        /// <param name="rowCount">tham số đầu ra: số dòng dữ liệu truy vấn được</param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(int page,
            int pageSize,
            string searchValue,
            out int rowCount)
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }

        public static List<Category> ListOfCategories(string searchValue = "")
        {
            return categoryDB.List(1, 0, searchValue).ToList();
        }
       
        public static Category GetCategory(int categoryID)
        {
            return categoryDB.Get(categoryID);
        }
        
        public static int AddGetCategory(Category data)
        {
            return categoryDB.Add(data);
        }
        
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }
    
        public static bool DeleteCategory(int categoryID)
        {
            return categoryDB.Delete(categoryID);
        }
        
        public static bool InUserCategory(int categoryID)
        {
            return categoryDB.InUsed(categoryID);
        }

        #endregion
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhân viên
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(int page,
            int pageSize,
            string searchValue,
            out int rowCount)
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }

        public static List<Employee> ListOfEmployees(string searchValue = "")
        {
            return employeeDB.List(1, 0, searchValue).ToList();
        }

        public static Employee GetEmployee(int employeeID)
        {
            return employeeDB.Get(employeeID);
        }

        public static int AddGetEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }

        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }

        public static bool DeleteEmployee(int employeeID)
        {
            return categoryDB.Delete(employeeID);
        }

        public static bool InUsedEmployee(int employeeID)
        {
            return categoryDB.InUsed(employeeID);
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách shipper
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Shipper> ListOfShipper(int page,
           int pageSize,
           string searchValue,
           out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }

        public static List<Shipper> ListOfShipper(string searchValue = "")
        {
            return shipperDB.List(1, 0, searchValue).ToList();
        }

        public static Shipper GetShipper(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }

        public static int AddGetShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }

        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }

        public static bool DeleteShipper(int shipperID)
        {
            return shipperDB.Delete(shipperID);
        }

        public static bool InUsedShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }
    }
}


