using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PointOfSale;
using PointOfSale.Models;
using POS = PointOfSale.PointOfSale;
using Terminal = PointOfSale.PointOfSaleTerminal;

namespace PointOfSaleTests
{
    public class TestSetPricing
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
        }

        [Test]
        public void TestSetPricing_WithoutPointOfSale()
        {
            var terminal = new Terminal();

            Assert.Throws<NullProductException>(() => {
                terminal.SetPricing("A", (decimal)1.25);
            });
        }

        [TestCase("E", 1.25d)]
        public void TestSetPricing_UnavailableProduct(string productCode, decimal price)
        {
            Assert.Throws<NullProductException>(() => {
                _terminal.SetPricing(productCode, price);
            });
        }

        [TestCase("A", 1.25d)]
        [TestCase("b", 0.75d)]
        [TestCase("C", 3.05d)]
        [TestCase("d", 2d)]
        public void TestSetUnitPrice(string productCode, decimal price)
        {
            Assert.DoesNotThrow(() => {
                _terminal.SetPricing(productCode, price);
            });
            
            var product = _pointOfSale.Products.FirstOrDefault(item => item.Equals(productCode));

            Assert.IsNotNull(product);
            Assert.IsNotNull(product.Price);

            Assert.AreEqual(product.Price.UnitPrice, price);
        }

        [TestCase("A", -100.00d)]
        public void TestSetUnitPrice_InvalidValues(string productCode, decimal price)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                _terminal.SetPricing(productCode, price);
            });
        }

        [TestCase("a", 3.25d, 3)]
        [TestCase("B", 2.00d, 3)]
        [TestCase("D", 6.00d, 6)]
        public void TestSetVolumePrice(string productCode, decimal price, int volume)
        {
            Assert.DoesNotThrow(() => {
                _terminal.SetPricing(productCode, price, volume);
            });

            var product = _pointOfSale.Products.FirstOrDefault(item => item.Equals(productCode));

            Assert.IsNotNull(product);
            Assert.IsNotNull(product.Price);

            Assert.AreEqual(product.Price.VolumePrice, price);
            Assert.AreEqual(product.Price.MaxVolume, volume);
        }

        [TestCase("A", -3.00d, -3)]
        [TestCase("b", 100.00d, 0)]
        public void TestSetVolumePrice_InvalidValues(string productCode, decimal price, int volume)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                _terminal.SetPricing(productCode, price, volume);
            });
        }

        [TestCase("c", 1.25d, 1)]
        public void TestSetVolumePrice_SetSingleVolume(string productCode, decimal price, int volume)
        {
            Assert.DoesNotThrow(() => {
                _terminal.SetPricing(productCode, price, volume);
            });

            var product = _pointOfSale.Products.FirstOrDefault(item => item.Equals(productCode));

            Assert.IsNotNull(product);
            Assert.IsNotNull(product.Price);

            Assert.AreEqual(product.Price.UnitPrice, price);
            Assert.IsNull(product.Price.VolumePrice);
            Assert.IsNull(product.Price.MaxVolume);
        }
    }
}