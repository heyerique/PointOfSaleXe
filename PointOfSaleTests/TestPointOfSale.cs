using System.Collections.Generic;
using NUnit.Framework;
using PointOfSale.Models;
using POS = PointOfSale.PointOfSale;
using System;

namespace PointOfSaleTests
{
    public class TestPointOfSale
    {
        [Test]
        public void TestProducts1()
        {
            var productA = new ProductA();
            var productB = new ProductB();
            var productC = new ProductC();
            var productD = new ProductD();

            List<Product> products = new List<Product> {
                productA, productB, productC, productD
            };

            Assert.DoesNotThrow(() => {
                var pointOfSale = new POS(products);

                Assert.IsNotEmpty(products);
                Assert.AreEqual(products, pointOfSale.Products);
            });
        }

        [Test]
        public void TestProducts2()
        {
            var productA = new ProductA();
            var productB = new ProductB();
            var productC = new ProductC();
            var productD = new ProductD();

            List<Product> products = new List<Product> {
                productA, productB, productC, productD
            };

            var pointOfSale = new POS();

            Assert.DoesNotThrow(() => {
                foreach (var product in products)
                {
                    pointOfSale.AddProduct(product);
                }

                Assert.IsNotEmpty(products);
                Assert.AreEqual(products, pointOfSale.Products);
            });
        }

        [Test]
        public void TestProducts3()
        {
            var productA = new ProductA();
            var pointOfSale = new POS();

            Assert.Throws<InvalidOperationException>(() => {
                pointOfSale.AddProduct(productA);
                pointOfSale.AddProduct(productA);
            });
        }
    }
}
