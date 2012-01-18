namespace PayPalCodeSample.Controllers {
	using System;
	using System.Web;
	using System.Web.Mvc;
	using PayPalCodeSample.Models;

	public class HomeController : Controller {
		public ActionResult Index() {
			return Redirect( "/Cart" );
		}
	}
}