using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace BillAutomation    //DO NOT change the namespace name
{
    public class ElectricityBill         //DO NOT change the class name
    {
        //Implement the fields and properties as per description
        private string consumerNumber;
        private string consumerName;
        private int unitsConsumed;
        private double billAmount;
        private const string Msg = "Invalid Consumer Number";

        public ElectricityBill()
        {
        }

        public ElectricityBill(string consumerNumber, string consumerName, int unitsConsumed, double billAmount)
        {
            this.consumerNumber = consumerNumber;
            this.consumerName = consumerName;
            this.unitsConsumed = unitsConsumed;
            this.billAmount = billAmount;
        }
        public string pattern = @"[EB]{2}[0-9]{5}$";
        public string ConsumerNumber
        {
            
            get
            {
                return this.consumerNumber;
            }
            set
            {
                if (Regex.IsMatch(value,pattern))
                {
                    this.consumerNumber = value;
                }
                else
                {
                    throw new FormatException();
                }
            }

        }
        public string ConsumerName { get => consumerName; set => consumerName = value; }
        public int UnitsConsumed { get => unitsConsumed; set => unitsConsumed = value; }
        public double BillAmount { get => billAmount; set => billAmount = value; }
    }
}
