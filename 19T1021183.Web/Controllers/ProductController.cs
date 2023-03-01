using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021183.DomainModels;
using _19T1021183.BusinessLayers;
using _19T1021183.Web.Models;

namespace _19T1021183.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string SUPPLIER_SEARCH = "ProductSearchCondition";
        /// <summary>
        /// Tìm kiếm, hiển thị mặt hàng dưới dạng phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[SUPPLIER_SEARCH] as Models.PaginationSearchInput;
            if(condition == null)
            {
                condition = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                };
            };
            return View(condition);
        }
        /// <summary>
        /// Tạo mặt hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung mặt hàng";
            Product data = new Product()
            {
                ProductID = 0
            };

            return View("Edit", data);

        }
        /// <summary>
        /// Cập nhật thông tin mặt hàng, 
        /// Hiển thị danh sách ảnh và thuộc tính của mặt hàng, điều hướng đến các chức năng
        /// quản lý ảnh và thuộc tính của mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>    
        public ActionResult Search(Models.PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = ProductDataService.ListOfProducts(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new Models.ProductSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session["ProductSearchCondition"] = condition;
            return View(result);
        }
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var data = ProductDataService.GetProduct(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhập nhà cung cấp";
            return View(data);
        }
        /// <summary>
        /// Xóa mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>     
        public ActionResult Save(Product data)//save(int SupplierID, string SupplierName,string ContactName,...
        {
            if (string.IsNullOrWhiteSpace(data.ProductName))
                ModelState.AddModelError(nameof(data.ProductName), "Tên mặt hàng không được để trống");
            if (string.IsNullOrWhiteSpace(data.Unit))
                ModelState.AddModelError(nameof(data.Unit), "Giá không được để trống");
            

            if (!ModelState.IsValid)
            {
                ViewBag.Tille = data.SupplierID == 0 ? "Bổ sung nhà cung cấp" : "Cập nhât nhà cung cấp ";
                return View("Edit", data);
            }
            if (data.SupplierID == 0)
            {
                ProductDataService.AddProduct(data);
            }
            else
            {
                ProductDataService.UpdateProduct(data);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            if (Request.HttpMethod == "POST")
            {
                ProductDataService.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            else
            {
                var data = ProductDataService.GetProduct(id);
                if (data == null)
                    return RedirectToAction("Index");
                return View(data);
            }
        }

        /// <summary>
        /// Các chức năng quản lý ảnh của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method?}/{productID?}/{photoID?}")]
        public ActionResult Photo(string method = "add", int productID = 0, long photoID = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh";
                    return View();
                case "edit":
                    ViewBag.Title = "Thay đổi ảnh";
                    return View();
                case "delete":
                    //ProductDataService.DeletePhoto(photoID);
                    return RedirectToAction($"Edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Các chức năng quản lý thuộc tính của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method?}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method = "add", int productID = 0, long attributeID = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính";
                    return View();
                case "edit":
                    ViewBag.Title = "Thay đổi thuộc tính";
                    return View();
                case "delete":
                    //ProductDataService.DeleteAttribute(attributeID);
                    return RedirectToAction($"Edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }
    }
}