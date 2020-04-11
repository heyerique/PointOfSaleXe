using System;
using System.Collections.Generic;
using System.Linq;
using PointOfSale;
using PointOfSale.Models;

namespace PointOfSaleXe
{
    public class CustomerService
    {
        private readonly IPointOfSaleTerminal _terminal;

        public CustomerService(IPointOfSaleTerminal terminal)
        {
            _terminal = terminal;
        }

        public void ProcessCustomers()
        {
            var productA = new ProductA();
            var productB = new ProductB();
            var productC = new ProductC();
            var productD = new ProductD();

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

            var customerC = new Customer();
            customerC.ShoppingCart = new List<IProduct> {
                    productA,
                    productB,
                    productC,
                    productD,
                };

            ProcessCustomer(customerA);
            ProcessCustomer(customerB);
            ProcessCustomer(customerC);
        }

        private void ProcessCustomer(Customer customer)
        {
            if (customer == null)
            {
                return;
            }

            try
            {
                foreach (var product in customer.ShoppingCart)
                {
                    Utils.DoSafe(() => {
                        _terminal.ScanProduct(product.Code);
                    });
                }

                customer.Bill = _terminal.Bill;
                _terminal.CalculateTotal();

                PrintBill(customer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void PrintBill(Customer customer)
        {
            if (customer == null)
            {
                return;
            }

            var codeList = customer.ShoppingCart.Select(item => item.Code);
            var codeListString = string.Concat(codeList);
            var totalPrice = customer.Bill?.TotalPrice ?? 0;

            Console.WriteLine($"Shopping items: {codeListString}");
            Console.WriteLine(string.Format("Total price: ${0:0.00}", totalPrice));
            Console.WriteLine();
        }
    }
}
