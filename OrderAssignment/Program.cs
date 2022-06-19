using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAssignment
{
    internal class Program
    {
        private void menu()
        {
            Console.WriteLine("**************************************************");
            Console.WriteLine("            Manage Item      Press :1            *");
            Console.WriteLine("            Manage Customres Press :2            *");
            Console.WriteLine("            Close App        Press :3            *");
            Console.WriteLine("**************************************************");

        }
        static void Main(string[] args)
        {
            Program program = new Program();    

            repeedAgain:
            program.menu(); 
            
            int switch_on=int.Parse(Console.ReadLine());
            switch (switch_on)
            {
                case 1:
                    ItemMaster itemMaster=new ItemMaster();
                    itemMaster.ItemMasterPortal();
                    goto repeedAgain;
                case 2:
                    Customer customer=new Customer();
                    customer.portal();
                    goto repeedAgain;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Wrong Choise   ");
                    goto repeedAgain;
            }

            Console.ReadLine(); 
        }
    }
}
