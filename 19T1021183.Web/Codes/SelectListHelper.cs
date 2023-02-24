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

    }
}