using AHD.Models;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AHD
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            BsonClassMap.RegisterClassMap<NeuLeaveCancelation>();
            BsonClassMap.RegisterClassMap<NeuLeavePastApply>();
            BsonClassMap.RegisterClassMap<NeuLeaveWFHApply>();
            BsonClassMap.RegisterClassMap<RequestLog>();
            BsonClassMap.RegisterClassMap<AttachmentLog>();
            

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
