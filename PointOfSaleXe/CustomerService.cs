using System;
using System.Collections.Generic;
using System.Linq;
using PointOfSale;
using PointOfSale.Models;

namespace PointOfSaleXe
{
    public class CustomerService
    {
        private static IPointOfSaleTerminal _terminal;

        public CustomerService(IPointOfSaleTerminal terminal)
        {
            _terminal = terminal;
        }

        public void Start()
        {
            var customer = new Customer();

            while (string.IsNullOrWhiteSpace(customer.ShoppingList))
            {
                Console.WriteLine("Please input product codes: ");
                customer.ShoppingList = Console.ReadLine();
            }

            ProcessCustomer(customer);
        }

        private void ProcessCustomer(Customer customer)
        {
            if (customer == null
                || string.IsNullOrWhiteSpace(customer.ShoppingList))
            {
                return;
            }

            var productCodes = customer.ShoppingList.ToCharArray();

            try
            {
                foreach (var code in productCodes)
                {
                    Utils.DoSafe(() => {
                        _terminal.ScanProduct(code.ToString());
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

            var totalPrice = customer.Bill?.TotalPrice ?? 0;

            Console.WriteLine(string.Format("Total price: ${0:0.00}", totalPrice));
            Console.WriteLine();
        }
    }
}
