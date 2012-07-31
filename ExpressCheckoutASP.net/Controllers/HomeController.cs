using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpressCheckoutASP.net.Models;
using PayPal.Nvp;
using PayPal.Enum;

namespace ExpressCheckoutASP.net.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/Cart");
        }
        
    }
}

