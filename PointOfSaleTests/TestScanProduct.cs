using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PointOfSale.Models;
using POS = PointOfSale.PointOfSale;
using Terminal = PointOfSale.PointOfSaleTerminal;
using Utils = PointOfSale.Utils;

namespace PointOfSaleTests
{
    public class TestScanProduct
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
                Utils.DoSafe(() =>
                {
                    _pointOfSale.AddProduct(product);
                });
            }

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
        }

        [Test]
        public void TestTestScanProduct_WithoutPointOfSale()
        {
            var terminal = new Terminal();
            var codes = "ABCDABA";

            var codeList = codes.ToCharArray().ToList();

            Assert.Throws<NullReferenceException>(() => {
                foreach (var code in codeList)
                {
                    _terminal.ScanProduct(code.ToString());
                }
            });
        }

        [TestCase("A")]
        [TestCase("b")]
        [TestCase("C")]
        public void TestScanProduct_UnitPrice_SingleScanning(string productCode)
        {
            Assert.DoesNotThrow(() => {
                _terminal.ScanProduct(productCode);
            });

            var product = _pointOfSale.Products.FirstOrDefault(item => item.Equals(productCode));

            Assert.IsNotNull(product);
            Assert.IsNotNull(_terminal.Bill);
            Assert.IsNotEmpty(_terminal.Bill.ShoppingList);

            Assert.AreEqual(_terminal.CalculateTotal(), product.Price.UnitPrice);
        }

        [TestCase("C")]
        public void TestScanProduct_UnitPrice_MultipleScanning(string productCode)
        {
            var count = 5;

            Assert.DoesNotThrow(() => {
                for (var i = 0; i < count; i++)
                {
                    _terminal.ScanProduct(productCode);
                }
            });

            var product = _pointOfSale.Products.FirstOrDefault(item => item.Equals(productCode));

            Assert.IsNotNull(product);
            Assert.IsNotNull(_terminal.Bill);
            Assert.IsNotEmpty(_terminal.Bill.ShoppingList);

            Assert.AreEqual(_terminal.CalculateTotal(), product.Price.UnitPrice * count);
        }

        [TestCase("A")]
        public void TestScanProduct_VolumePrice(string productCode)
        {
            Assert.DoesNotThrow(() => {
                _terminal.ScanProduct(productCode);
                _terminal.ScanProduct(productCode);
                _terminal.ScanProduct(productCode);
            });

            var product = _pointOfSale.Products.FirstOrDefault(item => item.Equals(productCode));

            Assert.IsNotNull(product);
            Assert.IsNotNull(_terminal.Bill);
            Assert.IsNotEmpty(_terminal.Bill.ShoppingList);

            Assert.AreEqual(_terminal.CalculateTotal(), product.Price.VolumePrice);
        }

        [TestCase("d")]
        public void TestScanProduct_NoPrice(string productCode)
        {
            Assert.Throws<NullReferenceException>(() => {
                _terminal.ScanProduct(productCode);
            });
        }

        [TestCase("e")]
        public void TestScanProduct_UnavailableProduct(string productCode)
        {
            Assert.Throws<NullReferenceException>(() => {
                _terminal.ScanProduct(productCode);
            });
        }
    }
}
