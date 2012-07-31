using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpressCheckoutASP.net.Models.Ect;
using ExpressCheckoutASP.net.Models;
using PayPal;
using PayPal.Enum;
using PayPal.Nvp;
using PayPal.ExpressCheckout;
using PayPal.FreteFacil;

namespace ExpressCheckoutASP.net.Models
{    
    public class Cart
    {
        private List<Item> items;
        private ExpressCheckoutApi ec;

        public Cart()
        {
            items = new List<Item>();
            ec = PayPalApiFactory.instance.ExpressCheckout(
            "neto_1306507007_biz_api1.gmail.com",
            "1306507019",
            "Al8n1.tt9Sniswt8UZvcamFvsXYEAegNpyX63HRdtqJVff7rESMSQ3qN"
            );

            populate();
        }

        private void populate()
        {
            Category livros = new Category { Id = 1, Name = "Livros" };

            Add(new Product
            {
                Id = 1,
                Name = "PRIVATE: #1 SUSPECT",
                Description = "by James Patterson and Maxine Paetro",
                Category = livros,
                Price = 33.21,
                Width = 15,
                Height = 1,
                Length = 25,
                Weight = 1
            }, 1);

            Add(new Product
            {
                Id = 2,
                Name = "THE GIRL WITH THE DRAGON TATTOO",
                Description = "by Stieg Larsson",
                Category = livros,
                Price = 17.83,
                Width = 15,
                Height = 2,
                Length = 30,
                Weight = 2
            }, 3);

            Add(new Product
            {
                Id = 3,
                Name = "THE HELP",
                Description = "by Kathryn Stockett",
                Category = livros,
                Price = 31.54,
                Width = 15,
                Height = 2,
                Length = 28,
                Weight = 1
            }, 2);
        }

        public void Add(Product item, int quantity)
        {
            items.Add(new Item(item, quantity));
        }

        public List<Item> ItemList()
        {
            return items;
        }

        public double Total
        {
            get
            {
                double total = 0;

                foreach (Item item in items)
                {
                    total += item.Total;
                }

                return total;
            }
        }

        public string Checkout(string finalize, string cancel, string cep, string type)
        {
            SetExpressCheckoutOperation setEc = ec.SetExpressCheckout(
            finalize, cancel
            );

            using (PaymentRequest request = setEc.PaymentRequest(0))
            {
                request.Action = PaymentAction.SALE;

                foreach (Item item in items)
                {
                    request.addItem(
                    item.Product.Name,
                    item.Quantity,
                    item.Product.Price,
                    item.Product.Description
                    );
                }

                List<Shipping> shipping = GetShipping(cep);

                if (type == "frete-facil")
                {
                    request.ShippingAmount = shipping[0].Value;
                    request.ShippingDiscountAmount = shipping[0].Value - shipping[1].Value;
                }
                else
                {
                    request.ShippingAmount = shipping[1].Value;
                }
            }

            setEc.LocaleCode = LocaleCode.BRAZILIAN_PORTUGUESE;
            setEc.CurrencyCode = CurrencyCode.BRAZILIAN_REAL;
            setEc.HeaderImage = "https://cms.paypal.com/cms_content/US/en_US/images/developer/PP_X_Final_logo_vertical_rgb.gif";
            setEc.BrandName = "PayPal Code Sample";
            setEc.SurveyEnable = true;
            setEc.SurveyQuestion = "Onde ficou sabendo de nossa loja?";
            setEc.SurveyChoice = new string[]{
"Um amigo me contou",
"Mecanismo de pesquisa",
"Anúncio em website",
"Outros"
};

            setEc.sandbox().execute();

            return setEc.RedirectUrl;
        }

        public ResponseNVP Finalize(string token, string PayerId)
        {
            GetExpressCheckoutDetailsOperation getEc = ec.GetExpressCheckoutDetails(
            token
            );

            getEc.sandbox().execute();

            DoExpressCheckoutPaymentOperation doEc = ec.DoExpressCheckoutPayment(
            token, PayerId, PaymentAction.SALE
            );

            doEc.CurrencyCode = CurrencyCode.BRAZILIAN_REAL;
            doEc.PaymentRequest(0).Amount = getEc.ResponseNVP.GetDouble("PAYMENTREQUEST_0_AMT");
            doEc.sandbox().execute();

            return doEc.ResponseNVP;
        }


        public List<Shipping> GetShipping(string cep)
        {
            string origem = "04094-050";
            string destino = cep;
            double peso = 0;
            int largura = 0;
            int altura = 0;
            int comprimento = 0;

            Shipping freteFacil = new Shipping
            {
                Id = "frete-facil",
                Name = "PayPal Frete Fácil"
            };

            Shipping freteECT = new Shipping
            {
                Id = "sedex",
                Name = "SEDEX Correios"
            };

            foreach (Item item in items)
            {
                if (item.Product.Length > comprimento)
                {
                    comprimento = item.Product.Length;
                }

                if (item.Product.Width > largura)
                {
                    largura = item.Product.Width;
                }

                altura += item.Product.Height;
                peso += item.Product.Weight;
            }

            FreteFacilApi wsFreteFacil = PayPalApiFactory.instance.FreteFacil();
            freteFacil.Value = wsFreteFacil.getPreco(
            origem,
            destino,
            largura,
            altura,
            comprimento,
            peso.ToString()
            );

            CalcPrecoPrazoWS wsECT = new CalcPrecoPrazoWS();
            freteECT.Value = Double.Parse(wsECT.CalcPrecoPrazo(
            "", "", "40010",
            origem,
            destino,
            peso.ToString(),
            1,
            comprimento,
            altura,
            largura,
            0, "n", 0, "n"
            ).Servicos[0].Valor);

            return new List<Shipping> {
freteFacil,
freteECT
};
        }
    }

    public class Item
    {
        public Item(Product product, int quantity)
        {
            Quantity = quantity;
            Product = product;
        }

        public int Quantity { get; set; }
        public Product Product { get; set; }
        public double Total
        {
            get { return Product.Price * Quantity; }
        }
    }
}