using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021183.DomainModels;
using _19T1021183.BusinessLayers;
using System.Web.Mvc;

namespace _19T1021183.Web//
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "...Chọn quốc gia..."
            });
            foreach (var item in CommonDataService.ListOfCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }
            //ASP.net
            return list;
        }

        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "...Tất cả loại hàng..."
            });
            foreach (var item in CommonDataService.ListOfCategories())
            {
                list.Add(new SelectListItem()
                {
                    Value =Convert.ToString(item.CategoryID),
                    Text = item.CategoryName
                });
            }

            return list;
        }

        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "...Tất cả nhà cung cấp..."
            });
            foreach(var item in CommonDataService.ListOfSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.SupplierID),
                    Text = item.SupplierName
                });
            }
            return list;
        }
        public static List<SelectListItem> Orders()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "...Tất cả trạng thái..."
            });
            foreach(var item in OrderDataService.ListOfOrderStatus())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.Status),
                    Text = item.Description
                }) ;
            }
            return list;
        }

    }
}