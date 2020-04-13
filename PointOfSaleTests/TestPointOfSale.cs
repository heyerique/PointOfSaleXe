using System.Collections.Generic;
using NUnit.Framework;
using PointOfSale.Models;
using POS = PointOfSale.PointOfSale;
using PointOfSale;

namespace PointOfSaleTests
{
    public class TestPointOfSale
    {
        [Test]
        public void TestPointOfSale_InitialiseWithProductList()
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
        public void TestPointOfSale_AddProducts()
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
        public void TestPointOfSale_AddDuplicatedProducts()
        {
            var productA = new ProductA();
            var pointOfSale = new POS();

            Assert.DoesNotThrow(() => {
                pointOfSale.AddProduct(productA);
            });

            Assert.Throws<DuplicatedProductException>(() => {
                pointOfSale.AddProduct(productA);
            });
        }
    }
}
