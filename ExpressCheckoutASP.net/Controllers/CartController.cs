namespace PayPalCodeSample.Controllers {
	using System;
	using System.Web;
	using System.Web.Mvc;
	using System.Collections.Generic;
	using PayPalCodeSample.Models;
	using PayPal.Nvp;
	using PayPal.Enum;

	public class CartController : Controller {
		public ActionResult Index() {
			Cart cart = new Cart();
			
			return View( cart );
		}

		public ActionResult Checkout( string cep , string type ) {
			Cart cart = new Cart();

			return Redirect( cart.Checkout(
				"http://127.0.0.1:8080/Cart/Finalize",
				"http://127.0.0.1:8080/Cart/Cancel",
				cep,
				type
			) );
		}

		public ActionResult Finalize( string token , string PayerId ) {
			Cart cart = new Cart();
			
			ResponseNVP nvp = cart.Finalize(token,PayerId);
			
			if ( nvp.Get("ACK") == "Success" ) {
				string status = nvp.Get("PAYMENTINFO_0_PAYMENTSTATUS");
				
				if ( status == "Completed" || status == "Pending" ) {
					//regras de negócio da aplicação
				}
				
				return Redirect( "/Cart/Success" );
			} else {
				//alguma coisa aconteceu errado, você deverá verificar mensagens de
				//erro para ver o que aconteceu.
				
				return Redirect( "/Cart/Fail" );
			}
		}
		
		public ActionResult Success() {
			return View();
		}
		
		public ActionResult Shipping( string cep ) {
			Cart cart = new Cart();

			return PartialView( cart.GetShipping( cep ) );
		}
		
		public ActionResult Fail() {
			return View();
		}
	}
}