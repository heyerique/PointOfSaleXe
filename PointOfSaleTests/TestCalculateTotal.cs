using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PointOfSale.Models;
using POS = PointOfSale.PointOfSale;
using Terminal = PointOfSale.PointOfSaleTerminal;

namespace PointOfSaleTests
{
    public class TestCalculateTotal
    {
        private POS _pointOfSale;
        private Terminal _terminal;

        [SetUp]
        public void Setup()
        {
            _pointOfSale = new POS();
            _terminal = new Terminal(_pointOfSale);

            var productA = new ProductA();
            var productB = new ProductB();
            var productC = new ProductC();
            var productD = new ProductD();

            List<Product> products = new List<Product> {
                productA, productB, productC, productD
            };

            try
            {
                foreach (var product in products)
                {
                    _pointOfSale.AddProduct(product);
                }

                _terminal.SetPricing(productA.Code, (decimal)1.25d);
                _terminal.SetPricing(productA.Code, (decimal)3.00d, 3);
                _terminal.SetPricing(productB.Code, (decimal)4.25d);
                _terminal.SetPricing(productC.Code, (decimal)1.00d);
                _terminal.SetPricing(productC.Code, (decimal)5.00d, 6);
                _terminal.SetPricing(productD.Code, (decimal)0.75d);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [TestCase("ABCDABA", 13.25d)]
        [TestCase("CCCCCCC", 6.00d)]
        [TestCase("ABCD", 7.25d)]
        public void TestShoppingCart(string codes, decimal totalPrice)
        {
            var codeList = codes.ToCharArray().ToList();
            var distinctCodeList = codeList.Distinct();

            Assert.DoesNotThrow(() =>
            {
                foreach (var code in codeList)
                {
                    _terminal.ScanProduct(code.ToString());
                }

                foreach (var code in distinctCodeList)
                {
                    var codeCount = codeList.Count(c => c == code);
                    var item = _terminal.Bill.ShoppingList.FirstOrDefault(item => item.Product.Equals(code.ToString()));

                    if (item != null)
                    {
                        Assert.AreEqual(codeCount, item.Count);
                    }
                }

                Assert.IsNotNull(_terminal.Bill);
                Assert.IsNotEmpty(_terminal.Bill.ShoppingList);

                Assert.AreEqual(_terminal.Bill.ShoppingList.Count, distinctCodeList.Count());

                Assert.AreEqual(_terminal.CalculateTotal(), totalPrice);
            });
        }
    }
}
