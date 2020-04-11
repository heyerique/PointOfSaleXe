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

        [TestCase("ABCDABA", 13.25d)]
        [TestCase("CCCCCCC", 6.00d)]
        [TestCase("ABCD", 7.25d)]
        public void TestShoppingCart(string codes, decimal totalPrice)
        {
            var codeList = codes.ToCharArray().ToList();

            foreach (var code in codeList)
            {
                _terminal.ScanProduct(code.ToString());
            }

            var codeA = codeList.Where(code => code == 'A');
            var codeB = codeList.Where(code => code == 'B');
            var codeC = codeList.Where(code => code == 'C');
            var codeD = codeList.Where(code => code == 'D');

            var itemA = _terminal.Bill.ShoppingList.FirstOrDefault(item => item.Product.Equals("A"));
            var itemB = _terminal.Bill.ShoppingList.FirstOrDefault(item => item.Product.Equals("B"));
            var itemC = _terminal.Bill.ShoppingList.FirstOrDefault(item => item.Product.Equals("C"));
            var itemD = _terminal.Bill.ShoppingList.FirstOrDefault(item => item.Product.Equals("D"));


            if (itemA != null)
            {
                Assert.AreEqual(itemA.Count, codeA.Count());
            }

            if (itemB != null)
            {
                Assert.AreEqual(itemB.Count, codeB.Count());
            }

            if (itemC != null)
            {
                Assert.AreEqual(itemC.Count, codeC.Count());
            }

            if (itemD != null)
            {
                Assert.AreEqual(itemD.Count, codeD.Count());
            }

            Assert.AreEqual(_terminal.CalculateTotal(), totalPrice);
        }
    }
}
