using BusinessLayer.Dtos;
using DataInterface.Entities;
using DataInterface.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop
{
    public partial class EshopConsole
    {
        private bool PurchaseOrderMenu()
        {
            Console.Clear();
            Console.WriteLine("1) Create purchase order");
            Console.WriteLine("2) Get all purchase orders");
            Console.WriteLine("3) Set purchase order status");
            Console.WriteLine("0) Go back");

            Console.Write("\nSelect an option:");
            switch (Console.ReadLine())
            {
                case "0":
                    return false;
                case "1":
                    Console.Clear();
                    CreatePurchaseOrder();
                    break;
                case "2":
                    Console.Clear();
                    GetAllPurchaseOrders();
                    break;
                case "3":
                    Console.Clear();
                    SetPurchaseOrderStatus();
                    break;
                default:
                    break;
            }

            return true;
        }

        private void CreatePurchaseOrder()
        {
            try
            {
                CreatePurchaseOrderDto purchaseOrder = new();
                Console.WriteLine("Please enter the required information to create a purchase order\n");

                // show available providers
                var providers = _providerService.Get();

                if (!providers.Any())
                    throw new Exception("There are no providers");

                Console.WriteLine("Available Providers");
                providers.ForEach(p => Console.WriteLine($"Id: {p.Id}\tName: {p.Name}"));

                Console.WriteLine();
                int providerId = GetIntInput("Provider: ");

                var provider = _productService.GetById(providerId);

                if (provider is null)
                    throw new Exception($"Provider {providerId} does not exist");

                // show available products
                var products = _productService.Get();

                if (!products.Any())
                    throw new Exception("There are no products");

                Console.WriteLine("\nAvailable Products");
                products.ForEach(p => Console.WriteLine($"Id: {p.Id}\tName: {p.Name}\tPrice: {p.Price}\tStock: {p.Stock}"));
                Console.WriteLine();

                int productId;
                var purchaseOrderProducts = new List<PurchaseOrderProductDto>();
                PurchaseOrderProductDto purchaseOrderProduct;

                // ask for products
                while (true)
                {
                    productId = GetIntInput("Product Id: ");

                    var product = _productService.GetById(productId);

                    if (product is null)
                    {
                        Console.WriteLine($"Product {productId} does not exist");
                        Console.WriteLine("\n Press any key to continue...");
                        Console.ReadLine();
                        continue;
                    }

                    purchaseOrderProduct = new();
                    purchaseOrderProduct.Id = productId;

                    int productQuantity = GetIntInput("Quantity: ");

                    if (productQuantity <= 0)
                        throw new Exception("Quantity must be greater than 0");

                    purchaseOrderProduct.Quantity = productQuantity;
                    purchaseOrderProducts.Add(purchaseOrderProduct);
                    Console.WriteLine($"Product {productId} added");

                    if (GetStringInput("Continue adding products? (y/n): ").ToLower() != "y")
                        break;
                }

                // create purchase order
                purchaseOrder.ProviderId = providerId;
                purchaseOrder.Products = purchaseOrderProducts;

                _purchaseOrderService.Create(purchaseOrder);
                Console.WriteLine("Purchase order created");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Console.Write("\nPress enter to continue...");
                Console.ReadLine();
            }
        }

        private void GetAllPurchaseOrders()
        {
            try
            {
                var purchaseOrders = _purchaseOrderService.Get();

                if (!purchaseOrders.Any())
                    throw new Exception("There are no providers");

                foreach (var po in purchaseOrders)
                {
                    Console.WriteLine($"Id: {po.Id}\tProvider: {po.Provider.Name}");

                    foreach (var product in po.Products)
                        Console.WriteLine($" - {product.Name}\tQty: {product.Quantity}");

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Console.Write("\nPress enter to continue...");
                Console.ReadLine();
            }
        }

        private void SetPurchaseOrderStatus()
        {
            try
            {
                Console.WriteLine("Please enter the required information to change PO status");
                int purchaseOrderId = GetIntInput("Purchase order id: ");

                var purchaseOrder = _purchaseOrderService.GetById(purchaseOrderId);

                if (purchaseOrder is null)
                    throw new Exception($"Purchase order {purchaseOrderId} does not exist");

                Console.WriteLine("\nStatus list");
                foreach (var status in Enum.GetNames<PurchaseOrderStatus>())
                    Console.WriteLine($"- {status}");

                Console.WriteLine();
                string inputStatus = GetStringInput("New status: ");

                if (Enum.TryParse(inputStatus, out PurchaseOrderStatus newStatus))
                {
                    _purchaseOrderService.SetStatus(new PurchaseOrderStatusDto { Id = purchaseOrderId, Status = newStatus });

                    /*if (newStatus == PurchaseOrderStatus.Paid)
                    {
                        po.PurchasedProducts
                            .GroupBy(product => product.Id)
                            .Select(group => new { Id = group.Key, Qty = group.Sum(product => product.Stock) })
                            .ToList()
                            .ForEach(product => _productService.GetProduct(product.Id).AddStock(product.Qty));
                    }*/

                    Console.WriteLine($"Status changed");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally 
            {
                Console.Write("\nPress enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
