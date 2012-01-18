namespace PayPalCodeSample.Models {
	using System;
	using System.Collections.Generic;

	public class Category {
		private List<Product> products;
		
		public Category() {
			products = new List<Product>();
		}
		
		public int Id { get; set; }
		public String Name { get; set; }
		public List<Product> Products {
			get { return products; }
		}
	}
}