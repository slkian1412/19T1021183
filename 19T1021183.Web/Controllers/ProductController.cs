﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021183.BusinessLayers;
using _19T1021183.DomainModels;
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private const int PAGE_SIZE = 5;
        private const string PRODUCT_SEARCH = "ProductCondition";
        public ActionResult Index()
        {
            Models.ProductSearchInput condition = Session[PRODUCT_SEARCH] as Models.ProductSearchInput;

            if (condition == null)
            {
                condition = new Models.ProductSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                };
            }

            return View(condition);
        }

        public ActionResult Search(Models.ProductSearchInput condition)
        {
            var data = ProductDataService.ListProducts(condition.Page, condition.PageSize, condition.SearchValue, condition.CategoryID, condition.SupplierID, out var rowCount);
            var result = new Models.ProductSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                CategoryID = condition.CategoryID,
                SupplierID = condition.SupplierID,
                RowCount = rowCount,
                Data = data,

            };
            Session[PRODUCT_SEARCH] = condition;
            return View(result);
        }

        /// <summary>
        /// Tạo mặt hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Product()
            {
                ProductID = 0,
            };

            ViewBag.Title = "Bổ sung mặt hàng";
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
            if (id <= 0)
                return RedirectToAction("Index");

            var data = CommonDataService.GetProduct(id);

            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật mặt hàng";
            return View(data);
        }

        /// <summary>
        /// Lưu thông tin mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <param name="uploadPhoto"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]  // Kiểm tra Token không hợp lệ
        [HttpPost]  // Chỉ nhận phương thức post
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
                data.Photo = $"Images/Products/{fileName}";
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
            if (id <= 0)
                return RedirectToAction("Index");

            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteProduct(id);
                return RedirectToAction("Index");
            }

            var data = CommonDataService.GetProduct(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Xóa Mặt hàng";
            return View(data);
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
                    var data = new ProductPhoto()
                    {
                        PhotoID = 0,
                        ProductID = productID
                    };
                    ViewBag.Title = "Bổ sung ảnh";
                    return View(data);
                case "edit":
                    if (photoID <= 0)
                        return RedirectToAction("Index");
                    data = ProductDataService.GetPhoto(photoID);
                    if (data == null)
                        return RedirectToAction("Index");
                    ViewBag.Title = "Thay đổi ảnh";
                    return View(data);
                case "delete":
                    ProductDataService.DeletePhoto(photoID);
                    return RedirectToAction($"Edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Lưu thông tin Ảnh trong Mặt hàng
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SavePhoto(ProductPhoto data, HttpPostedFileBase uploadPhoto, string isHidden)
        {
            data.Description = data.Description ?? "";
            data.IsHidden = false;
            if (isHidden == "on")
                data.IsHidden = true;

            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Products/Photos");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Photo = $"Images/Products/Photos/{fileName}";
            }
            else
            {
                data.Photo = "Images/Products/Photos/default.png";
            }

            //
            if (data.PhotoID == 0)
                ProductDataService.AddPhoto(data);
            else
                ProductDataService.UpdatePhoto(data);

            return RedirectToAction($"Edit/{data.ProductID}");
        }

        /// <summary>
        /// Các chức năng quản lý thuộc tính của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method?}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method = "add", int productID = 0, int attributeID = 0)
        {
            switch (method)
            {
                case "add":
                    var data = new ProductAttribute()
                    {
                        AttributeID = 0,
                        ProductID = productID
                    };
                    ViewBag.Title = "Bổ sung thuộc tính";
                    return View(data);
                case "edit":
                    if (attributeID <= 0)
                        return RedirectToAction("Index");
                    data = ProductDataService.GetAttribute(attributeID);
                    if (data == null)
                        return RedirectToAction("Index");
                    ViewBag.Title = "Thay đổi thuộc tính";
                    return View(data);
                case "delete":
                    ProductDataService.DeleteAttribute(attributeID);
                    return RedirectToAction($"Edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="uploadPhoto"></param>
        /// <param name="isHidden"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveAttribute(ProductAttribute data)
        {
            //// check
            if (string.IsNullOrWhiteSpace(data.AttributeName))
                ModelState.AddModelError(nameof(data.AttributeName), "Tên thuộc tính không được để trống!");
            if (string.IsNullOrWhiteSpace(data.AttributeValue))
                ModelState.AddModelError(nameof(data.AttributeValue), "Giá trị Thuộc tính không được để trống!");

            if (ModelState.IsValid == false)    // Kiểm tra dữ liệu đầu vào có hợp lệ hay không
            {
                ViewBag.Title = data.AttributeID == 0 ? "Bổ sung Thuộc tính" : "Thay đổi Thuộc tính";
                return View("Attribute", data);
            }

            //
            if (data.AttributeID == 0)
                ProductDataService.AddAttribute(data);
            else
                ProductDataService.UpdateAttribute(data);

            return RedirectToAction($"Edit/{data.ProductID}");
        }
    }
}