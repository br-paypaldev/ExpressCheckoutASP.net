namespace PayPalCodeSample.Models {
	using System;

	public class Product {
		private Category c;
		
		public Category Category {
			get { return c; }
			set {
				c = value;
				c.Products.Add( this );
			}
		}

		public int Id { get; set; }
		public String Name { get; set; }
		public String Description { get; set; }
		public double Price { get; set; }
		public double Weight { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int Length { get; set; }
	}
}