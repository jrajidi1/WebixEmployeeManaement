using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;

namespace EmpManaement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEmployees()
        {
            return Json(EmpData(), JsonRequestBehavior.AllowGet);
        }

       
        public JsonResult SaveData(List<Employee> emps )
        {
            SaveDataAccess(emps);
            return Json(emps, JsonRequestBehavior.AllowGet);
        }

        public List<Employee> SaveDataAccess(List<Employee> emps)
        {

            List<Employee> Employees = new List<Employee>();


            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + Server.MapPath("~/App_Data/Employees.accdb");

            string strSQL = "SELECT * FROM Employee";
            // Create a connection  
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Create a command and set its connection  
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                // Open the connection and execute the select command.  
                try
                {
                    // Open connecton  
                    connection.Open();
                    command.CommandText = "Delete from Employee";
                    command.ExecuteNonQuery();


                    foreach (var item in emps)
                    {
                        command.CommandText = "Insert into Employee(EmpName,FatherName,Age)Values('" + item.Name + "','" + item.FatherName + "'," + Convert.ToString(item.Age) + ")";
                        command.ExecuteNonQuery();
                    }
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
                return Employees;
                // The connection is automatically closed becasuse of using block.  

            }


        public List<Employee> EmpData()
        {

            List<Employee> Employees = new List<Employee>();

          
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + Server.MapPath("~/App_Data/Employees.accdb");
            

            string strSQL = "SELECT * FROM Employee";
            // Create a connection  
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Create a command and set its connection  
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                // Open the connection and execute the select command.  
                try
                {
                    // Open connecton  
                    connection.Open();
                    // Execute command  
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("------------Original data----------------");
                        while (reader.Read())
                        {
                           
                            Employees.Add(new Employee( Convert.ToInt16( reader["ID"]), reader["EmpName"].ToString(), reader["FatherName"].ToString(), Convert.ToInt16( reader["Age"])));

                        }
                    }                 
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return Employees;
                // The connection is automatically closed becasuse of using block.  
            }

        }
        
    }
    public class Employee
    {
        public Employee()
        {
        }
        public Employee(int id, string name, string fatherName, int age)
        {
            this.id = id;
            Name = name;
            FatherName = fatherName;
            Age = age;

        }
        public long id { get; set; }
        public string Name { get; set; }

        public string FatherName { get; set; }
        public int Age { get; set; }

    }
}