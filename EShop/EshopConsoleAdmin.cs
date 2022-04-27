using BusinessLayer.Dtos;
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
            Console.WriteLine("1) Create product");
            Console.WriteLine("2) Update product");
            Console.WriteLine("3) Get all products");
            Console.WriteLine("4) Get product by id");
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
                case "1":
                    Console.Clear();
                    CreateProduct();
                    break;
                case "2":
                    Console.Clear();
                    UpdateProduct();
                    break;
                case "3":
                    Console.Clear();
                    GetProducts();
                    break;
                case "4":
                    Console.Clear();
                    GetProductById();
                    break;
                case "5":
                    Console.Clear();
                    DeleteProduct();
                    break;
                case "6":
                    Console.Clear();
                    CreateDepartment();
                    break;
                case "7":
                    Console.Clear();
                    CreateSubdepartment();
                    break;
                case "9":
                    bool showPurchaseOrderMenu = true;
                    while (showPurchaseOrderMenu)
                    {
                        showPurchaseOrderMenu = PurchaseOrderMenu();
                    }
                    break;
                default:
                    break;
            }

            return true;
        }

        private void CreateProduct()
        {
            try
            {
                CreateProductDto product = new();
                Console.WriteLine("Please enter the required information to add a new product:\n");

                product.Name = GetStringInput("Name: ");
                product.Price = GetDecimalInput("Price: ");
                product.Description = GetStringInput("Description: ");
                product.Brand = GetStringInput("Brand: ");
                product.Sku = GetStringInput("SKU: ");
                product.Stock = GetIntInput("Stock: ");

                Console.WriteLine("\nChoose a Department:");
                List<DepartmentDto> departments = _departmentService.Get();

                foreach (var dep in departments)
                    Console.WriteLine($"Id: {dep.Id}\tName: {dep.Name}");

                int departmentId = GetIntInput("Deparment Id: ");
                var department = _departmentService.GetById(departmentId);

                if (department is null)
                    throw new Exception($"Department {departmentId} does not exist");

                Console.WriteLine($"\nChoose a Subdepartment for {department.Name}: ");
                var subdepartments = _subdepartmentService.GetByDepartmentId(departmentId);

                foreach (var subdep in subdepartments)
                    Console.WriteLine($"Id: {subdep.Id}\tName: {subdep.Name}");

                product.SubdepartmentId = GetIntInput("Subdepartment Id: ");
                
                _productService.CreateProduct(product);
                Console.WriteLine("Product created");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally 
            {
                Console.Write("\nPress enter to continue...");
                Console.ReadLine();
            }
        }

        private void UpdateProduct()
        {
            try
            {
                Console.WriteLine("Please enter the required information to update a product\n");

                var products = _productService.Get();

                if (!products.Any())
                    throw new Exception("There are no products to update");

                foreach (var p in products)
                    Console.WriteLine($"Id: {p.Id}\n - ({p.Brand}) {p.Name}\n - {p.Description}\n - {p.Price:c}");

                Console.WriteLine();
                int productId = GetIntInput("Product Id:");

                var product = _productService.GetById(productId);

                if (product is null)
                    throw new Exception($"Product {productId} does not exist");

                decimal price = product.Price;

                Console.Write("Name: ");
                string inputName = Console.ReadLine().Trim();
                Console.Write("Description: ");
                string inputDescription = Console.ReadLine().Trim();
                Console.Write("Brand: ");
                string inputBrand = Console.ReadLine().Trim();
                Console.Write("Price: ");
                string inputPrice = Console.ReadLine().Trim();

                if (!string.IsNullOrEmpty(inputPrice))
                    if (!decimal.TryParse(inputPrice, out price))
                        throw new FormatException("The value for Price must be decimal");

                Console.Write("Sku: ");
                string inputSku = Console.ReadLine().Trim();


                _productService.UpdateProduct(new UpdateProductDto 
                { 
                    Id = productId,
                    Name = inputName,
                    Description = inputDescription,
                    Brand = inputBrand,
                    Price = price,
                    Sku = inputSku
                });
                Console.WriteLine("Product updated");
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

        private void GetProducts()
        {
            try
            {
                var products = _productService.Get();

                if (!products.Any())
                    throw new Exception("There are no products");

                products
                    .ForEach(product => Console.WriteLine($"Id: {product.Id}\n - ({product.Brand}) {product.Name}\n - {product.Description}\n - {product.Price:c}"));
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

        private void GetProductById()
        {
            try
            {
                int id = GetIntInput("Product Id: ");
                var product = _productService.GetById(id);

                if (product is null)
                    throw new Exception($"Product {id} does not exist");

                Console.WriteLine($"\nId: {product.Id}\n - ({product.Brand}) {product.Name}\n - {product.Description}\n - {product.Price:c}");
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

        private void DeleteProduct()
        {
            try
            {
                Console.WriteLine("Please enter the following information to delete a product\n");
                int productId = GetIntInput("Product Id: ");

                _productService.DeleteProduct(productId);
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

        private void CreateDepartment() 
        {
            try
            {
                CreateDepartmentDto department = new(); 
                Console.WriteLine("Please enter the required information to add a new department\n");
                department.Name = GetStringInput("Name: ");
                _departmentService.Create(department);
                Console.WriteLine("Department created");
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

        private void CreateSubdepartment()
        {
            try
            {
                var departments = _departmentService.Get();

                if (!departments.Any())
                    throw new Exception("There are no departments, create one in order to create a subdeparment");

                CreateSubdepartmentDto subdepartment = new();
                Console.WriteLine("Please enter the required information to add a new subdepartment\n");
                Console.WriteLine("Current Departments");

                foreach (var department in departments)
                    Console.WriteLine($"Id: {department.Id}\tName: {department.Name}");

                Console.WriteLine();
                subdepartment.Name = GetStringInput("Name: ");
                subdepartment.DepartmentId = GetIntInput("Department Id: ");

                _subdepartmentService.Create(subdepartment);
                Console.WriteLine("Subdepartment created");
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
