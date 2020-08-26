using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

namespace BillAutomation         //DO NOT change the namespace name
{
    public class Program        //DO NOT change the class name
    {

        static void Main(string[] args)  //DO NOT change the 'Main' method signature
        {
            //Implement the code here
            ElectricityBoard eb = null;
            DBHandler db = new DBHandler();
            int noOfBills;
            List<ElectricityBill> li = new List<ElectricityBill>();
            Console.WriteLine("Enter Number of Bills To Be Added :");
            noOfBills = int.Parse(Console.ReadLine());
            BillValidator b = new BillValidator();
            for (int i = 0; i < noOfBills; i++)
            {
                string consumerNumber, consumerName;
                int units;
                Console.WriteLine("Enter Consumer Number:");
                consumerNumber = Console.ReadLine();
                Console.WriteLine("Enter Consumer Name:");
                consumerName = Console.ReadLine();
                do
                {
                    Console.WriteLine("Enter Units Consumed:");
                    units = int.Parse(Console.ReadLine());
                    Console.WriteLine(b.ValidateUnitsConsumed(units));
                } while (b.ValidateUnitsConsumed(units) != null);
                ElectricityBill ebill = new ElectricityBill();
                try
                {
                    ebill.ConsumerNumber = consumerNumber;
                    ebill.ConsumerName = consumerName;
                    ebill.UnitsConsumed = units;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Consumer Number");
                    continue;
                }
                    eb = new ElectricityBoard();
                    eb.CalculateBill(ebill);
                    eb.SqlCon = db.GetConnection();
                    eb.AddBill(ebill);
                    li.Add(ebill);
            }
                    eb.SqlCon = db.GetConnection();
                    Console.WriteLine();
                    Console.WriteLine();
                    foreach (var bill in li)
                    {
                        Console.WriteLine(bill.ConsumerName);
                        Console.WriteLine(bill.ConsumerNumber);
                        Console.WriteLine(bill.UnitsConsumed);
                        Console.WriteLine("Bill Amount:" + bill.BillAmount);
                    }
                    Console.WriteLine("Enter Last 'N' Number of Bills To Generate:");
                    int num = int.Parse(Console.ReadLine());
                    List<ElectricityBill> li2 = eb.Generate_N_BillDetails(num);
                    Console.WriteLine("Details of last ‘N’ bills:");
                    foreach (var j in li2)
                    {
                        Console.WriteLine("EB Bill for " + j.ConsumerName + " is " + j.BillAmount);
                    }
            Console.ReadKey();
        }
    }

    public class BillValidator
    {      //DO NOT change the class name

        public String ValidateUnitsConsumed(int UnitsConsumed)      //DO NOT change the method signature
        {
            //Implement code here
            return (UnitsConsumed>0  ? null:"Given units is invalid");
        }
    }
}
