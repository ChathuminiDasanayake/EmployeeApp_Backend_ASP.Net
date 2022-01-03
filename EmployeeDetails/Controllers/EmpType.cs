using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeDetails.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpType : Controller
    {

        public class EmployeeType
        {

            public int ID;
            public string Code;
            public string Description;
            public DateTime SysDate;

        }

        public List<EmployeeType> employeetype = new List<EmployeeType>();

        string connectionString = "Data Source=DESKTOP-1RP86OL;Initial Catalog=EmployeeDetails;User id=sa;Password=tommya;";
        SqlConnection con = new SqlConnection();

        [HttpPost("InsertEmployeeType")]
        public IEnumerable<EmployeeType> AddingType(EmployeeType e)
        {
            DynamicParameters parameter = new DynamicParameters();


            parameter.Add("@Code", e.Code);
            parameter.Add("@Description", e.Description);
            parameter.Add("@SysDate", e.SysDate);
          

            con.ConnectionString = connectionString;
            var result = con.Query<EmployeeType>("InsertEmployeeType", parameter, commandType: CommandType.StoredProcedure);

            return result;
        }

        [HttpGet("GetEmpOfType/{DID}")]
        public IEnumerable<EmployeeType> GetEmployeeName(int DID)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@did", DID);

            con.ConnectionString = connectionString;
            var result = con.Query<EmployeeType>("SelectNameandType", parameter, commandType: CommandType.StoredProcedure);


            return result;

        }

        [HttpGet("GetbyID/{id}")]
        public IEnumerable<EmployeeType> GetEmployeebyID(int id)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@id", id);

            con.ConnectionString = connectionString;
            var result = con.Query<EmployeeType>("SelectEmployeeTypebyID", parameter, commandType: CommandType.StoredProcedure);


            return result;
        }

        [HttpGet("GetAllEmployeeTypes")]
        public IEnumerable<EmployeeType> GetAllEmployees()
        {
            DynamicParameters parameter = new DynamicParameters();


            con.ConnectionString = connectionString;
            var result = con.Query<EmployeeType>("SelectAllEmployeeTypes", parameter, commandType: CommandType.StoredProcedure);


            return result;
        }


        [HttpPut("UpdateEmployeeType")]
        public IEnumerable<EmployeeType> Modify(EmployeeType emp)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@id", emp.ID);
            parameter.Add("@code", emp.Code);
            parameter.Add("@description", emp.Description);

            con.ConnectionString = connectionString;
            var result = con.Query<EmployeeType>("UpdateEmployeeType", parameter, commandType: CommandType.StoredProcedure);


            return result;

        }

        [HttpDelete("DeleteEmployeeType/{id}")]
        public IEnumerable<EmployeeType> Remove(int id)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@id", id);

            con.ConnectionString = connectionString;
            var result = con.Query<EmployeeType>("RemoveEmployeeType", parameter, commandType: CommandType.StoredProcedure);

            return result;

        }



    }
}
