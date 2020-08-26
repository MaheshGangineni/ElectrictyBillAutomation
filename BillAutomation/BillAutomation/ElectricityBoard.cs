using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillAutomation      //DO NOT change the namespace name
{
    public class ElectricityBoard  //DO NOT change the class name
    {
        public ElectricityBoard()
        {
        }
        //Implement the property as per the description
        public SqlConnection SqlCon { get; set; }
        
        //Implement the methods as per the description   
        public void AddBill(ElectricityBill ebill)
        {
            try
            { 
                string insertBill = "insert into ElectricityBill values(@ConsumerNumber,@ConsumerName,@UnitsConsumed,@BillAmount)";
                using (SqlCon)
                {
                    using (SqlCommand cmdinsertBill = new SqlCommand(insertBill, SqlCon))
                    {
                        SqlCon.Open();
                        cmdinsertBill.Parameters.AddWithValue("@ConsumerNumber", ebill.ConsumerNumber);
                        cmdinsertBill.Parameters.AddWithValue("@ConsumerName", ebill.ConsumerName);
                        cmdinsertBill.Parameters.AddWithValue("@UnitsConsumed", ebill.UnitsConsumed);
                        cmdinsertBill.Parameters.AddWithValue("@BillAmount", ebill.BillAmount);
                        cmdinsertBill.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error:"+e.Message);
            }
        }
        public void CalculateBill(ElectricityBill ebill)
        {
            double billAmount = 0;
            int units = ebill.UnitsConsumed;
            int temp = 0;
            if (units > 1000)
            {
                temp = units - 1000;
                billAmount += ((temp) * 7.5);
                units = units - temp;
            }
            if (units > 600 && units <= 1000)
            {
                temp = units - 600;
                billAmount += ((temp) * 5.5);
                units = units - temp;
            }
            if (units > 300 && units <= 600)
            {
                temp = units - 300;
                billAmount += ((temp) * 3.5);
                units = units - temp;
            }
            if (units > 100 && units <= 300)
            {
                temp = units - 100;
                billAmount += ((temp) * 1.5);
                units = units - temp;
            }
            if (units <= 100)
                billAmount += 0;
            ebill.BillAmount = billAmount;

        }
        
        public List<ElectricityBill> Generate_N_BillDetails(int num)
        {
            string lastNBills = "select TOP "+ num +"* from ElectricityBill order by consumer_number desc";
            DataTable dt = new DataTable();
            List<ElectricityBill> bills = new List<ElectricityBill>();
            try
            {
                using (SqlCon)
                {
                    using (SqlCommand cmdlastNBills = new SqlCommand(lastNBills, SqlCon))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmdlastNBills))
                        {
                            da.Fill(dt);
                        }
                        
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            if(dt.Rows.Count>0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    ElectricityBill ebill = new ElectricityBill()
                    {
                        ConsumerNumber = row["consumer_number"].ToString(),
                        ConsumerName = row["consumer_name"].ToString(),
                        UnitsConsumed = int.Parse(row["units_consumed"].ToString()),
                        BillAmount = double.Parse(row["bill_amount"].ToString())
                    };
                    bills.Add(ebill);

                }
            }
            return bills;
        }
    }
}
