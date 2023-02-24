using _19T1021183.BusinessLayers;
using _19T1021183.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021183.DataLayers;
using System.Web.UI;

namespace _19T1021183.Web.Controllers
{
    public class CategoryController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string CATEGORY_SEARCH = "CategorySearchCondition";


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public ActionResult Index(int page = 1, string searchValue = "")
        //{
        //    int rowCount = 0;
        //    var data = CommonDataService.ListOfCategories(page, PAGE_SIZE, searchValue, out rowCount);
        //    int pageCount = rowCount / PAGE_SIZE;
        //    if (rowCount % PAGE_SIZE > 0)
        //        rowCount += 1;
        //    ViewBag.Page = page;
        //    ViewBag.RowCount = rowCount;
        //    ViewBag.PageCount = pageCount;
        //    ViewBag.SeachValue = searchValue;
        //    return View(data);
        //}
        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[CATEGORY_SEARCH] as Models.PaginationSearchInput;
            if (condition == null)
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
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung loại hàng";
            Category data = new Category()
            {
                CategoryID = 0
            };

            return View("Edit", data);
        }
        /// <summary>
        /// Tìm kiếm
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCategories(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new Models.CategorySearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session["CategorySearchCondition"] = condition;
            return View(result);
        }
        /// <summary>
        /// Sửa Loại hàng
        /// </summary>
        /// <returns></returns>

        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetCategory(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Sửa loại hàng";
            return View(data);

        }
        public ActionResult Save(Category data)//save(int SupplierID, string SupplierName,string ContactName,...
        {
            if (string.IsNullOrWhiteSpace(data.CategoryName))
                ModelState.AddModelError(nameof(data.CategoryName), "Tên loại hàng không được để trống");
            data.Description = data.Description ?? "";
            data.ParentCategoryId = data.ParentCategoryId ?? "";
            
            if (!ModelState.IsValid)
            {
                ViewBag.Tille = data.CategoryID == 0 ? "Bổ sung loại hàng" : "Cập nhât loại hàng ";
                return View("Edit", data);
            }
            if (data.CategoryID == 0)
            {
                CommonDataService.AddGetCategory(data);
            }
            else
            {
                CommonDataService.UpdateCategory(data);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Xóa Loại hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            else
            {
                var data = CommonDataService.GetCategory(id);
                if (data == null)
                    return RedirectToAction("Index");
                return View(data);
            }
        }
    }
}