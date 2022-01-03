using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmplyeesController : ControllerBase
    {

        string connectionString = "Data Source=DESKTOP-1RP86OL;Initial Catalog=EmployeeDetails;User id=sa;Password=tommya;";
        SqlConnection con = new SqlConnection();

        public class Employee
        {
            public int EmployeeID;
            public string Code;
            public string Name;
            public string EmployeeType;
            public string Email;
            public DateTime DOB;
            public string Gender;
            public string Active;
          
        }


     /*   public class EmployeeType
        {
            
            public int ID;
            public string Code;
            public string Description;
            public DateTime SysDate;

        }*/


        public List<Employee> employee = new List<Employee>();
       // public List<EmployeeType> department = new List<EmployeeType>();


        [HttpGet("{id}")]

        public Employee Getting(int id) {
            return employee.Find((emp)=>emp.EmployeeID==id);

        }


        [HttpPost("add")]
        public String Post(Employee x)
        {
            employee.Add(x);
            return x.Name;
        }



        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            employee.Remove(employee.Find((Z)=>Z.EmployeeID == id));
                return " Deleted";
        }



        [HttpPut("{EmployeeID}")]
        public Employee Put(int EmployeeID, Employee n)
        {

            employee.Find((Z) => Z.EmployeeID == n.EmployeeID).Name = n.Name;
            return n;
        }


        [HttpGet("GetAllEmployees")]
        public IEnumerable<Employee> GetAllEmployees()
        {
            DynamicParameters parameter = new DynamicParameters();
            

            con.ConnectionString = connectionString;
            var result = con.Query<Employee>("SelectAllEmployees", parameter, commandType: CommandType.StoredProcedure);


            return result;
        }



        [HttpGet("GetEmployeebyID/{id}")]
        public IEnumerable<Employee> GetEmployee(int id)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@id",id);

            con.ConnectionString = connectionString;
            var result = con.Query<Employee>("SelectEmployeebyID", parameter, commandType: CommandType.StoredProcedure);


            return result;
        }



        [HttpPost("AddEmployees")]

        public IEnumerable<Employee> Adding(Employee e)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@ID", e.EmployeeID);
            parameter.Add("@Code", e.Code);
            parameter.Add("@Name", e.Name);
            parameter.Add("@EmployeeType", e.EmployeeType);
            parameter.Add("@Email", e.Email);
            parameter.Add("@DOB", e.DOB);
            parameter.Add("@Gender", e.Gender);
            parameter.Add("@Active", e.Active);
            
            con.ConnectionString = connectionString;
            var result = con.Query<Employee>("InsertEmployee", parameter, commandType: CommandType.StoredProcedure);

            return result;
        }



        [HttpDelete("DelEmployeebyID/{id}")]
        public IEnumerable<Employee> Remove(int id)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@id", id);

            con.ConnectionString = connectionString;
            var result = con.Query<Employee>("RemoveEmployee", parameter, commandType: CommandType.StoredProcedure);

            return result;

        }



        [HttpPut("UpdateEmployee")]
        public IEnumerable<Employee> Modify(Employee emp)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", emp.EmployeeID);
            parameter.Add("@Code", emp.Code);
            parameter.Add("@Name", emp.Name);
            parameter.Add("@EmployeeType", emp.EmployeeType);
            parameter.Add("@Email", emp.Email);
            parameter.Add("@DOB", emp.DOB);
            parameter.Add("@Gender", emp.Gender);
            parameter.Add("@Active", emp.Active);

            con.ConnectionString = connectionString;
            var result = con.Query<Employee>("UpdateEmployee", parameter, commandType: CommandType.StoredProcedure);


            return result;

        }



      /*  [HttpGet("GetEmp/{DID}")]
        public IEnumerable<EmployeeType> GetEmployeeName(int DID)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@did", DID);

            con.ConnectionString = connectionString;
            var result = con.Query<EmployeeType>("SelectName", parameter, commandType: CommandType.StoredProcedure);


            return result;

        }*/


       /* [HttpPost("InsertEmployeeType")]
        public IEnumerable<EmployeeType> AddingType(EmployeeType e)
        {
            DynamicParameters parameter = new DynamicParameters();

            parameter.Add("@Code", e.Code);
            parameter.Add("@Description", e.Description);
            parameter.Add("@SysDate", e.SysDate);


            con.ConnectionString = connectionString;
            var result = con.Query<EmployeeType>("InsertEmployeeType", parameter, commandType: CommandType.StoredProcedure);

            return result;
        }*/

        

    }
}