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
        private static IPointOfSale _pointOfSale;
        private static IPointOfSaleTerminal _terminal;

        static void Main(string[] args)
        {
            Init();

            var customerService = new CustomerService(_terminal);
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

            _pointOfSale = new POS(products);
            _terminal = new Terminal(_pointOfSale);

            Utils.DoSafe(() =>
            {
                _terminal.SetPricing(productA.Code, (decimal)1.25d);
            });

            Utils.DoSafe(() =>
            {
                _terminal.SetPricing(productA.Code, (decimal)3.00d, 3);
            });

            Utils.DoSafe(() =>
            {
                _terminal.SetPricing(productB.Code, (decimal)4.25d);
            });

            Utils.DoSafe(() =>
            {
                _terminal.SetPricing(productC.Code, (decimal)1.00d);
            });

            Utils.DoSafe(() =>
            {
                _terminal.SetPricing(productC.Code, (decimal)5.00d, 6);
            });

            Utils.DoSafe(() =>
            {
                _terminal.SetPricing(productD.Code, (decimal)0.75d);
            });
        }
    }
}
