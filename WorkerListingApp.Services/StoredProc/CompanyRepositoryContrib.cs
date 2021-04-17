using Dapper;
using Dapper.Contrib.Extensions;
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
    public class CompanyRepositoryContrib : ICompanyRepository
    {
        private IDbConnection dbConnection;

        public CompanyRepositoryContrib(IConfiguration configuration)
        {
            this.dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }





        //Add new Company Object
        public Company Add(Company company)
        {
            var id = dbConnection.Insert(company);
            company.CompanyId = (int)id;
            return company;
        }





        //Get details in Company Table by Id
        public Company Find(int id)
        {
            return dbConnection.Get<Company>(id);
        }





        //Get all Company details
        public List<Company> GetAll()
        {
            return dbConnection.GetAll<Company>().ToList();
        }





        public void Remove(int id)
        {
            dbConnection.Delete(new Company(){ CompanyId = id });

        }






        public Company Update(Company company)
        {
            dbConnection.Update(company);
            return company;
        }
    }
}
