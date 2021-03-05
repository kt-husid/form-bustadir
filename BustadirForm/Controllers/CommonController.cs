using BustadirForm.BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BustadirForm.Controllers
{
    internal class Postal
    {
        public int Code { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public int CallingCode { get; set; }
    }

    public class CommonController : Controller
    {
        [HttpGet]
        public ActionResult ParsePhoneNumber(string number)
        {
            if (!string.IsNullOrEmpty(number))
            {
                try
                {
                    number = number.Trim().Replace("_", "");
                    //var countriesTextFile = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/App_Data/countries.json"), System.Text.Encoding.UTF8);
                    //var countriesJson = JsonConvert.DeserializeObject<List<Kthusid.Helpers.CultureInfo>>(countriesTextFile);
                    var phone = PhoneHandler.Instance.Parse(number);
                    //var countryInfo = countriesJson.Where(x => x.CallingCode == phone.CountryCode && x.CountryName.Trim().Length > 0).FirstOrDefault();
                    return Json(new { Phone = phone }, JsonRequestBehavior.AllowGet); //, CountryInfo = countryInfo
                }
                catch (Exception ex)
                {
                    return Json(ex, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FindCity(string postal, string countryCode)
        {
            if (!string.IsNullOrEmpty(postal))
            {
                try
                {

                    var file = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/App_Data/postalcodes.json"), System.Text.Encoding.UTF8);
                    var json = JsonConvert.DeserializeObject<List<Postal>>(file);
                    //var city = json.Where(x => x.Code.ToString().Equals(postal.ToLower()) && (!string.IsNullOrEmpty(countryCode) && x.CountryCode.ToLower().Equals(countryCode.ToLower()))).FirstOrDefault();
                    var city = json.Where(x => x.Code.ToString().Equals(postal.ToLower())).FirstOrDefault();
                    return Json(city, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(ex, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}