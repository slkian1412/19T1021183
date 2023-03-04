using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021183.BusinessLayers;
using _19T1021183.DomainModels;
using _19T1021183.Web.Models;
using System.Reflection;

namespace _19T1021183.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        /*private const int PAGE_SIZ = 10;*/
        private const string PRODUCT_SEARCH = "ProductSearchCondition";
        /// <summary>
        /// 
        /// </summary>
        private const int PAGE_SIZE = 5;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Tìm kiếm, hiển thị mặt hàng dưới dạng phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var input = Session[PRODUCT_SEARCH] as ProductSearchInput ?? new ProductSearchInput
            {
                Page = 1,
                PageSize = PAGE_SIZE,
                SearchValue = "",
                CategoryID = 0,
                SupplierID = 0
            };

            var listOfCategories = CommonDataService.ListOfCategories(0, 0, "", out _);
            var listOfSuppliers = CommonDataService.ListOfSuppliers(0, 0, "", out _);

            ViewBag.ListOfCategories = listOfCategories;
            ViewBag.ListOfSuppliers = listOfSuppliers;

            return View(input);

        }
        public ActionResult Search(ProductSearchInput condition)
        {
            /*int rowCount = 0;*/
            var data = ProductDataService.ListProducts(condition.Page, condition.PageSize, condition.SearchValue, condition.CategoryID, condition.SupplierID, out var rowCount);
            var result = new Models.ProductSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                CategoryID = condition.CategoryID,
                SupplierID = condition.SupplierID,
                RowCount = rowCount,
                Data = data
            };
            Session["ProductSearchCondition"] = condition;
            return View(result);
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
                ProductID = 0,
                CategoryID = 0,
                SupplierID = 0,
                Price = 0,
                Unit = "",
                Photo = ""
            };
            return View(data);
        }
        /// <summary>
        /// Cập nhật thông tin mặt hàng, 
        /// Hiển thị danh sách ảnh và thuộc tính của mặt hàng, điều hướng đến các chức năng
        /// quản lý ảnh và thuộc tính của mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetProduct(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhập mặt hàng";
            return View(data);


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="uploadPhoto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product data, HttpPostedFileBase uploadPhoto)
        {
            // Kiểm soát dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(data.ProductName))
                ModelState.AddModelError(nameof(data.ProductName), "Tên Mặt hàng không được để trống!");
            if (string.IsNullOrEmpty(data.Unit))
                ModelState.AddModelError(nameof(data.Unit), "Đơn vị tính không được để trống!");

            if (ModelState.IsValid == false)    // Kiểm tra dữ liệu đầu vào có hợp lệ hay không
            {
                ViewBag.Title = data.ProductID == 0 ? "Bổ sung Mặt hàng" : "Cập nhật Mặt hàng";
                return View("Edit", data);
            }

            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Products");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Photo = fileName;
            }

            if (data.ProductID == 0)
            {
                CommonDataService.AddProduct(data);
            }
            else
            {
                CommonDataService.UpdateProduct(data);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Xóa mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public ActionResult Delete(int id = 0)
        {
            int ProductID = Convert.ToInt32(id);

            if (Request.HttpMethod == "POST")
            {
                ProductDataService.DeleteProduct(ProductID);
                return RedirectToAction("Index");
            }
            else
            {
                var data = CommonDataService.GetProduct(ProductID);
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
                    var productPhoto = new ProductPhoto
                    {
                        PhotoID = 0,
                        ProductID = productID,
                        DisplayOrder = 1
                    };
                    return View(productPhoto);

                case "edit":
                    ViewBag.Title = "Thay đổi ảnh";
                    var photo = ProductDataService.GetPhoto(photoID);
                    return View(photo);
                case "delete":
                    //ProductDataService.DeletePhoto(photoID);
                    ProductDataService.DeletePhoto(photoID);
                    return RedirectToAction("Edit", new { id = productID }); //return RedirectToAction("Edit", new { productID = productID });
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePhoto(ProductPhoto productPhoto, HttpPostedFileBase uploadPhoto)
        {


            if (string.IsNullOrWhiteSpace(productPhoto.Description))
                ModelState.AddModelError("Description", "Description is required");
            if (productPhoto.DisplayOrder == 0)
                ModelState.AddModelError("DisplayOrder", "DisplayOrder is required");

            if (uploadPhoto != null)
            {
                var path = Server.MapPath("~/Photo/SPhoto");
                var fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                var filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                productPhoto.Photo = fileName;
            }

            if (productPhoto.PhotoID == 0)
            {
                if (uploadPhoto == null)
                    ModelState.AddModelError("Photo", "Photo is required");

                if (!ModelState.IsValid)
                    return View("Photo", productPhoto);

                ProductDataService.AddPhoto(productPhoto);
            }
            else
            {
                if (!ModelState.IsValid)
                    return RedirectToAction("Photo", new { method = "edit", productID = productPhoto.ProductID, photoID = productPhoto.PhotoID });

                ProductDataService.UpdatePhoto(productPhoto);
            }

            return RedirectToAction("Edit", new { id = productPhoto.ProductID });
        }


        [Route("attribute/{method?}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method = "add", int productID = 0, int attributeID = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính";
                    var productAttribute = new ProductAttribute
                    {
                        AttributeID = 0,
                        ProductID = productID,
                        DisplayOrder = 1
                    };
                    return View(productAttribute);
                case "edit":
                    ViewBag.Title = "Thay đổi thuộc tính";
                    var attribute = ProductDataService.GetAttribute(attributeID);
                    return View(attribute);

                case "delete":
                    //ProductDataService.DeleteAttribute(attributeID);
                    ProductDataService.DeleteAttribute(attributeID);
                    return RedirectToAction("Edit", new { id = productID }); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }
        public ActionResult SaveAttribute(ProductAttribute productAttribute)
        {

            if (string.IsNullOrWhiteSpace(productAttribute.AttributeName))
                ModelState.AddModelError("AttributeName", "AttributeName is required");
            if (string.IsNullOrWhiteSpace(productAttribute.AttributeValue))
                ModelState.AddModelError("AttributeValue", "AttributeValue is required");
            if (productAttribute.DisplayOrder <= 0)
                ModelState.AddModelError("DisplayOrder", "DisplayOrder is required");

            if (productAttribute.AttributeID == 0)
            {
                if (!ModelState.IsValid)
                    return View("Attribute", productAttribute);

                ProductDataService.AddAttribute(productAttribute);
            }
            else
            {
                if (!ModelState.IsValid)
                    return RedirectToAction("Attribute", new { method = "edit", productID = productAttribute.ProductID, attributeID = productAttribute.AttributeID });

                ProductDataService.UpdateAttribute(productAttribute);
            }

            return RedirectToAction("Edit", new { id = productAttribute.ProductID });
        }


    }

}