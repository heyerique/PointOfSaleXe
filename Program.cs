using System;
using System.Collections.Generic;
using PointOfSale;
using PointOfSale.Models;

namespace PointOfSaleXe
{
    class Program
    {
        static void Main(string[] args)
        {
            var productA = new ProductA();
            var productB = new ProductB();
            var productC = new ProductC();
            var productD = new ProductD();

            List<Product> products = new List<Product> {
                productA, productB, productC, productD
            };

            var saleOfPoint = new PointOfSale.PointOfSale(products);
            var terminal = new PointOfSaleTerminal(saleOfPoint);

            terminal.SetPricing(productA.Code, (decimal)1.25);
            terminal.SetPricing(productA.Code, (decimal)3.00, 3);
            terminal.SetPricing(productB.Code, (decimal)4.25);
            terminal.SetPricing(productC.Code, (decimal)1.00);
            terminal.SetPricing(productC.Code, (decimal)5.00, 6);
            terminal.SetPricing(productD.Code, (decimal)0.75);

            var customerA = new Customer();
            customerA.ShoppingCart = new List<IProduct> {
                productA,
                productB,
                productC,
                productD,
                productA,
                productB,
                productA
            };

            foreach (var product in customerA.ShoppingCart)
            {
                terminal.ScanProduct(product.Code);
            }

            var printTemp = "Verify that the total price is: {0:0.00}";
            Console.WriteLine(String.Format(printTemp, terminal.CalculateTotal()));

            var customerB = new Customer();
            customerB.ShoppingCart = new List<IProduct> {
                productC,
                productC,
                productC,
                productC,
                productC,
                productC,
                productC
            };

            foreach (var product in customerB.ShoppingCart)
            {
                terminal.ScanProduct(product.Code);
            }

            Console.WriteLine(String.Format(printTemp, terminal.CalculateTotal()));

            var customerC = new Customer();
            customerC.ShoppingCart = new List<IProduct> {
                productA,
                productB,
                productC,
                productD,
            };

            foreach (var product in customerC.ShoppingCart)
            {
                terminal.ScanProduct(product.Code);
            }

            Console.WriteLine(String.Format(printTemp, terminal.CalculateTotal()));
        }
    }
}
