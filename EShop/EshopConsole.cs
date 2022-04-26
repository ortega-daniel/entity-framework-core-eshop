using BusinessLayer.Services.Abstractions;
using BusinessLayer.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop
{
    public partial class EshopConsole
    {
        private readonly IDepartmentService _departmentService;
        private readonly ISubdepartmentService _subdepartmentService;

        public EshopConsole()
        {
            _departmentService = new DepartmentService();
            _subdepartmentService = new SubdepartmentService();
        }

        public bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("1) Admin Menu");
            Console.WriteLine("2) Customer Menu");
            Console.WriteLine("0) Exit");

            Console.Write("\nSelect an option:");
            switch (Console.ReadLine())
            {
                case "0":
                    return false;
                case "1":
                    Console.Clear();
                    bool showAdminMenu = true;
                    while (showAdminMenu)
                    {
                        showAdminMenu = AdminMenu();
                    }
                    break;
                case "2":
                    /*bool showClientMenu = true;
                    while (showClientMenu)
                    {
                        showClientMenu = ClientMenu();
                    }*/
                    break;
                default:
                    break;
            }

            return true;
        }

        #region UserInput
        private int GetIntInput(string msg)
        {
            while (true)
            {
                Console.Write(msg);
                if (int.TryParse(Console.ReadLine(), out int result))
                    return result;
            }
        }

        private decimal GetDecimalInput(string msg)
        {
            while (true)
            {
                Console.Write(msg);
                if (decimal.TryParse(Console.ReadLine(), out decimal result))
                    return result;
            }
        }

        private string GetStringInput(string msg)
        {
            while (true)
            {
                Console.Write(msg);
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                    return input;
            }
        }
        #endregion
    }
}
