using DataInterface;
using DataInterface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop
{
    public partial class EshopConsole
    {
        private bool CartMenu()
        {
            Console.Clear();
            Console.WriteLine("1) Add to cart");
            Console.WriteLine("2) Remove from cart");
            Console.WriteLine("3) View cart");
            Console.WriteLine("4) Checkout cart");
            Console.WriteLine("0) Go back");

            Console.Write("\nSelect an option:");
            switch (Console.ReadLine())
            {
                case "0":
                    return false;
                case "1":
                    Console.Clear();
                    AddProduct();
                    break;
                case "2":
                    Console.Clear();
                    RemoveProduct();
                    break;
                case "3":
                    Console.Clear();
                    View();
                    break;
                case "4":
                    Console.Clear();
                    Checkout();
                    break;
                default:
                    break;
            }

            return true;
        }

        private void AddProduct()
        {
            try
            {
                Console.WriteLine("Please enter the required information to add a product");
                var products = _productService.Get();

                if (!products.Any())
                    throw new Exception("There are no products available");

                products.ForEach(p => Console.WriteLine($"Id: {p.Id} - Name: {p.Name}\t- {p.Price:c}\t{p.Stock} in stock"));
                Console.WriteLine();

                while (true)
                {
                    bool incrementQtyFlag = false;
                    
                    int inputProductId = GetIntInput("Product id: ");

                    var productAux = products.Find(product => product.Id == inputProductId);

                    if (productAux == null)
                    {
                        Console.WriteLine($"Product {inputProductId} doesn't exist!");
                        Console.WriteLine("\n Press enter to continue...");
                        Console.ReadLine();
                        continue;
                    }

                    Product product = null;

                    if (_cartService.ProductInCart(inputProductId))
                    {
                        product = _cartService.GetProductFromCart(inputProductId);
                        incrementQtyFlag = true;
                    }
                    else
                    {
                        product = new Product(productAux.Id, productAux.Name, productAux.Description, productAux.Price, productAux.Brand, productAux.Sku);
                    }

                    int cartProductQty = 0;
                    bool maxedOut = false;

                    while (true)
                    {
                        int maxQtyAvailable = incrementQtyFlag ? productAux.Stock - product.Stock : productAux.Stock;

                        if (maxQtyAvailable <= 0)
                        {
                            if (incrementQtyFlag)
                                Console.WriteLine($"You already have all the available {product.Name} in your cart!");
                            else
                                Console.WriteLine($"There are no {product.Name} in stock!");

                            Console.WriteLine("\n Press any key to continue...");
                            Console.ReadLine();
                            maxedOut = true;

                            break;
                        }

                        cartProductQty = GetIntInput(incrementQtyFlag ? "Product already in cart. Increment Qty by: " : "Hoy many do you want to add?: ");

                        if (cartProductQty <= 0 || cartProductQty > maxQtyAvailable)
                        {
                            Console.WriteLine($"Quantity must be between 1 and {maxQtyAvailable}");
                            Console.WriteLine("\n Press any key to continue...");
                            Console.ReadLine();
                            continue;
                        }

                        break;
                    }

                    if (!maxedOut)
                    {
                        if (incrementQtyFlag)
                            product.AddStock(cartProductQty);
                        else
                            product.UpdateStock(cartProductQty);

                        if (!incrementQtyFlag)
                        {
                            _cartService.AddProduct(product);
                            Console.WriteLine($"Product {product.Id} was added to your cart");
                        }
                        else
                        {
                            Console.WriteLine($"Product {product.Id} quantity successfully updated");
                        }
                    }

                    if (GetStringInput("Do you want to continue shopping? (y/n): ") != "y")
                    {
                        //_cartService.CalculateTotal();
                        break;
                    }
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

        private void RemoveProduct()
        {
            /*var cart = _cartService.GetCart();

            if (cart.Products.Any())
            {
                Console.WriteLine(cart);

                int productToRemove = 0;
                Product product = null;

                while (true)
                {
                    productToRemove = GetIntInput("Product to remove: ");

                    product = cart.Products.Find(product => product.Id == productToRemove);

                    if (product is null)
                    {
                        Console.WriteLine($"Product {productToRemove} is not in your cart!");
                        Console.Write("\nPress any key to continue...");
                        Console.ReadLine();
                        continue;
                    }

                    break;
                }

                int qtyToRemove = 0;

                while (true)
                {
                    qtyToRemove = GetIntInput("Quantity to remove: ");

                    if (qtyToRemove <= 0 || qtyToRemove > product.Stock)
                    {
                        Console.WriteLine($"Qty must be between 1 and {product.Stock}");
                        Console.Write("\nPress any key to continue...");
                        Console.ReadLine();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                if (qtyToRemove == product.Stock)
                {
                    _cartService.RemoveProduct(product.Id);
                }
                else
                {
                    _cartService.UpdateProductQuantity(product.Id, (product.Stock - qtyToRemove));
                }

                Console.WriteLine("\nProduct removed");
                _cartService.CalculateTotal();
            }
            else
            {
                Console.WriteLine("Your Cart is Empty");
            }

            Console.Write("\nPress any key to continue...");
            Console.ReadLine();*/
        }

        private void View()
        {
            /*var cart = _cartService.GetCart();

            if (cart.Products.Any())
                Console.WriteLine(cart);
            else
                Console.WriteLine("Your cart is empty");

            Console.Write("\nPress any key to continue...");
            Console.ReadLine();*/
        }

        private void Checkout()
        {
            /*Console.Clear();

            var cart = _cartService.GetCart();

            if (cart.Products.Any())
            {
                Console.WriteLine(cart);

                string performCheckout;
                while (true)
                {
                    performCheckout = GetStringInput("Do you want to checkout? (y/n): ");
                    if (performCheckout.ToLower() == "y" || performCheckout.ToLower() == "n")
                        break;
                }

                if (performCheckout == "y")
                {
                    List<Product> orderProducts = new();

                    foreach (var p in cart.Products)
                        orderProducts.Add(new Product(p.Id, p.Name, p.Description, p.Price, p.Brand, p.Sku, p.Stock));

                    Order order = new(orderProducts);

                    orderProducts
                        .GroupBy(product => product.Id)
                        .Select(group => new { Id = group.Key, Qty = group.Sum(product => product.Stock) })
                        .ToList()
                        .ForEach(product => _productService.GetProduct(product.Id).AddStock(-product.Qty));

                    _orderService.CreateOrder(order);
                    _cartService.ClearCart();

                    Console.WriteLine($"Your Order Number is: #{order.Id}");
                }
            }
            else
            {
                Console.WriteLine("Your cart is empty");
            }

            Console.Write("\nPress any key to continue...");
            Console.ReadLine();*/
        }
    }
}
