using System;
using System.Collections.Generic;
using PointOfSale;
using POS = PointOfSale.PointOfSale;
using Terminal = PointOfSale.PointOfSaleTerminal;
using PointOfSale.Models;

namespace PointOfSaleXe
{
    class Program
    {
        public static IPointOfSale PointOfSale { get; private set; }
        public static IPointOfSaleTerminal Terminal { get; private set; }

        static void Main(string[] args)
        {
            Init();

            var customerService = new CustomerService(Terminal);
            customerService.Start();
        }

        private static void Init()
        {
            var productA = new ProductA();
            var productB = new ProductB();
            var productC = new ProductC();
            var productD = new ProductD();

            List<Product> products = new List<Product> {
                productA, productB, productC, productD
            };

            PointOfSale = new POS(products);
            Terminal = new Terminal(PointOfSale);

            Utils.DoSafe(() =>
            {
                Terminal.SetPricing(productA.Code, (decimal)1.25d);
            });

            Utils.DoSafe(() =>
            {
                Terminal.SetPricing(productA.Code, (decimal)3.00d, 3);
            });

            Utils.DoSafe(() =>
            {
                Terminal.SetPricing(productB.Code, (decimal)4.25d);
            });

            Utils.DoSafe(() =>
            {
                Terminal.SetPricing(productC.Code, (decimal)1.00d);
            });

            Utils.DoSafe(() =>
            {
                Terminal.SetPricing(productC.Code, (decimal)5.00d, 6);
            });

            Utils.DoSafe(() =>
            {
                Terminal.SetPricing(productD.Code, (decimal)0.75d);
            });
        }
    }
}
