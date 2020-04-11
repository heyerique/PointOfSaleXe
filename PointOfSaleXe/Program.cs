using System;
using System.Collections.Generic;
using System.Linq;
using POS = PointOfSale.PointOfSale;
using Terminal = PointOfSale.PointOfSaleTerminal;
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

            var saleOfPoint = new POS(products);
            var terminal = new Terminal(saleOfPoint);

            try
            {
                terminal.SetPricing(productA.Code, (decimal)1.25d);
                terminal.SetPricing(productA.Code, (decimal)3.00d, 3);
                terminal.SetPricing(productB.Code, (decimal)4.25d);
                terminal.SetPricing(productC.Code, (decimal)1.00d);
                terminal.SetPricing(productC.Code, (decimal)5.00d, 6);
                terminal.SetPricing(productD.Code, (decimal)0.75d);

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

                PrintBill(customerA, terminal.CalculateTotal());

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

                PrintBill(customerB, terminal.CalculateTotal());

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

                PrintBill(customerC, terminal.CalculateTotal());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void PrintBill(Customer customer, decimal totalPrice)
        {
            var codeList = customer.ShoppingCart.Select(item => item.Code);
            var codeListString = String.Concat(codeList);

            Console.WriteLine($"Shopping items: {codeListString}");
            Console.WriteLine(string.Format("Total price: ${0:0.00}", totalPrice));
            Console.WriteLine();
        }
    }
}
