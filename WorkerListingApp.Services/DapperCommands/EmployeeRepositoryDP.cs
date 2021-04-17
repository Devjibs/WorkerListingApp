using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WorkerListingApp.DataAccess.Repository;
using WorkerListingApp.Core.Models;

namespace WorkerListingApp.Services.DapperCommands
{
    public class EmployeeRepositoryDP : IEmployeeRepository
    {
        private IDbConnection dbConnection;

        public EmployeeRepositoryDP(IConfiguration configuration)
        {
            this.dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }





        //Add new Company Object
        public Employee Add(Employee employee)
        {
            var sql = "INSERT INTO Employees(Name, Title, Email, Phone, CompanyId) " +
                "VALUES(@Name, @Title, @Email, @Phone, @CompanyId) SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = dbConnection.Query<int>(sql, employee).Single();
            employee.EmployeeId = id;
            return employee;
        }






        //Get details in Company Table by Id
        public Employee Find(int id)
        {
            var sql = "SELECT * FROM Employees WHERE EmployeeId = @EmployeeId";
            return dbConnection.Query<Employee>(sql, new { @EmployeeId = id }).Single();
        }





        //Get all Company details
        public List<Employee> GetAll()
        {
            var sql = "SELECT * FROM Employees";
            return dbConnection.Query<Employee>(sql).ToList();
        }





        public void Remove(int id)
        {
            var sql = "DELETE FROM Employees WHERE EmployeeId = @Id";
            dbConnection.Execute(sql, new { id });
        }





        public Employee Update(Employee employee)
        {
            var sql = "UPDATE Employees SET " +
                "Name = @Name, Title = @Title, " +
                "Email = @Email, Phone = @Phone, " +
                "CompanyId = @CompanyId WHERE EmployeeId = @EmployeeId";

            dbConnection.Execute(sql, employee);
            return employee;
        }
    }
}
