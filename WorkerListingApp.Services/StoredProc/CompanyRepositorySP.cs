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

namespace WorkerListingApp.Services.StoredProc
{
    public class CompanyRepositorySP : ICompanyRepository
    {
        private IDbConnection dbConnection;

        public CompanyRepositorySP(IConfiguration configuration)
        {
            this.dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }





        //Add new Company Object
        public Company Add(Company company)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", 0,
                DbType.Int32, direction: ParameterDirection.Output);

            parameters.Add("@Name", company.Address);
            parameters.Add("@Name", company.City);
            parameters.Add("@Name", company.State);
            parameters.Add("@Name", company.PostalCode);
            this.dbConnection.Execute("usp_AddCompany",
                parameters, commandType: CommandType.StoredProcedure);

            company.CompanyId = parameters.Get<int>("CompanyId");
            return company;
        }





        //Get details in Company Table by Id
        public Company Find(int id)
        {
            return dbConnection.Query<Company>("usp_GetCompany",
                new { CompanyId = id },
                commandType: CommandType.StoredProcedure).SingleOrDefault();
        }





        //Get all Company details
        public List<Company> GetAll()
        {
            return dbConnection.Query<Company>("usp_GetAllCompany", 
                commandType: CommandType.StoredProcedure).ToList();
        }





        public void Remove(int id)
        {
            dbConnection.Execute("usp_RemoveCompany",
                new { ComanyId = id },
                commandType: CommandType.StoredProcedure);
        }





        public Company Update(Company company)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", company.CompanyId,
                DbType.Int32);

            parameters.Add("@Name", company.Address);
            parameters.Add("@Name", company.City);
            parameters.Add("@Name", company.State);
            parameters.Add("@Name", company.PostalCode);
            this.dbConnection.Execute("usp_UpdateCompany",
                parameters, commandType: CommandType.StoredProcedure);

            company.CompanyId = parameters.Get<int>("CompanyId");
            return company;
        }
    }
}
