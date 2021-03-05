using AutoMapper;
using BustadirForm.BLL;
using BustadirForm.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using Telerik.Reporting.Services.WebApi;

namespace BustadirForm
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //ReportsControllerConfiguration.RegisterRoutes(GlobalConfiguration.Configuration);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AddGlobalFilters(GlobalFilters.Filters);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Handle i18n decimal separators
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            CultureConfig.LoadCultures(new string[] { "en", "fo", "da" }, "~/Content/files/cultures.json");
            //DatabaseSetupConfig.Execute(false);

            AutoMapperConfiguration.Configure();
            Mapper.AssertConfigurationIsValid();


            //var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //jsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;
            //var xmlFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //xmlFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            //var dcs = new DataContractSerializer(typeof(Website), null, int.MaxValue, false, /* preserveObjectReferences: */ true, null);
            //xmlFormatter.CreateDefaultSerializerSettings(<Website>(dcs);

            
        }

        public static void AddGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new BootstrapCore.Attributes.LoggingFilterAttribute());
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["init"] = 0;
        }
    }
}
