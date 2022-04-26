﻿using BusinessLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop
{
    public partial class EshopConsole
    {
        public bool AdminMenu()
        {
            Console.Clear();
            Console.WriteLine("1) Add a new product");
            Console.WriteLine("2) Edit a product");
            Console.WriteLine("3) Get all products");
            Console.WriteLine("4) Get a product");
            Console.WriteLine("5) Delete a product");
            Console.WriteLine("6) Add Department");
            Console.WriteLine("7) Add Subdepartment");
            Console.WriteLine("8) Reports");
            Console.WriteLine("9) Purchase Orders");
            Console.WriteLine("0) Main Menu");

            Console.Write("\nSelect an option:");
            switch (Console.ReadLine())
            {
                case "0":
                    return false;
                case "6":
                    Console.Clear();
                    CreateDepartment();
                    break;
                case "7":
                    break;
                default:
                    break;
            }

            return true;
        }

        private void AddProduct()
        {
            /*Console.WriteLine("Please indicate de required values for adding a new product:\n");
            int id = GetIntInput("Product Id: ");
            string name = GetStringInput("Name: ");
            decimal price = GetDecimalInput("Price: ");
            string description = GetStringInput("Description: ");
            string brand = GetStringInput("Brand: ");
            string sku = GetStringInput("SKU: ");

            Subdepartment subdepartment = AskForSubdepartment();

            try
            {
                var product = new Product(id, name, description, price, brand, sku);

                subdepartment.AddProduct(product);
                product.SetSubdepartment(subdepartment);

                _productService.AddProduct(product);

                Console.WriteLine("New product added successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/
        }

        private void EditProduct()
        {
            /*int id = GetIntInput("Project Id (to update):");

            Product origProduct = _productService.GetProduct(id);

            if (origProduct != null)
            {
                Console.Write("New Product Name: ");
                string name = Console.ReadLine();
                Console.Write("New Product Description: ");
                string description = Console.ReadLine();
                Console.Write("New Product Brand: ");
                string brand = Console.ReadLine();
                Console.Write("New Product Price: ");
                string price = Console.ReadLine();

                try
                {
                    decimal priceDecimal;
                    if (string.IsNullOrEmpty(price))
                        priceDecimal = origProduct.Price;
                    else
                        if (!decimal.TryParse(price, out priceDecimal))
                        throw new FormatException("The value for Price is invalid");

                    if (string.IsNullOrEmpty(name))
                        name = origProduct.Name;

                    if (string.IsNullOrEmpty(description))
                        description = origProduct.Description;

                    if (string.IsNullOrEmpty(brand))
                        brand = origProduct.Brand;

                    _productService.UpdateProduct(new Product(origProduct.Id, name, description, priceDecimal, brand, origProduct.Sku, origProduct.Stock));
                    Console.WriteLine($"Product {origProduct.Id} was successfully updated");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine($"Product {id} doesn't exist");
            }

            Console.Write("\nPress any key to continue...");
            Console.ReadLine();*/
        }

        private void GetAllProducts()
        {
            /*List<Product> products = _productService.GetProducts();

            if (products.Any())
                products.ForEach(product => Console.WriteLine(product.ToString()));
            else
                Console.WriteLine("Products List is Empty");

            Console.Write("\nPress any key to continue...");
            Console.ReadLine();*/
        }

        private void GetProduct()
        {
            /*int id = GetIntInput("Product Id: ");
            Product product = _productService.GetProduct(id);

            if (product != null)
                Console.WriteLine(product.ToString());
            else
                Console.WriteLine($"Product {id} Was Not Found");

            Console.Write("\nPress any key to continue...");
            Console.ReadLine();*/
        }

        private void DeleteProduct()
        {
            /*int id = GetIntInput("Product Id (to delete): ");

            try
            {
                _productService.DeleteProduct(id);
                Console.WriteLine($"Product {id} deleted successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.Write("\nPress any key to continue...");
            Console.ReadLine();*/
        }

        /*private Subdepartment AskForSubdepartment()
        {
            Console.WriteLine("Choose a Department:");
            List<Department> departmentsList = _departmentService.GetDepartments();

            for (int i = 0; i < departmentsList.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {departmentsList.ElementAt(i).Name}");
            }

            int selectedDepartment = GetIntInput("Deparment: ");
            Department department = departmentsList.ElementAt(selectedDepartment - 1);

            Console.WriteLine($"Choose a Subdepartment for {department.Name}: ");
            for (int i = 0; i < department.Subdepartments.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {department.Subdepartments.ElementAt(i).Name}");
            }

            int selectedSubdepartment = GetIntInput("Subdepartment: ");
            Subdepartment subdepartment = department.Subdepartments.ElementAt(selectedSubdepartment - 1);

            subdepartment.Department = department;
            return subdepartment;
        }*/

        private void CreateDepartment() 
        {
            try
            {
                CreateDepartmentDto department = new(); 
                Console.WriteLine("Please enter the required information to add a new department\n");
                department.Name = GetStringInput("Name: ");
                _departmentService.Create(department);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally 
            {
                Console.Write("\nPress enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
