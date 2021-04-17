﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerListingApp.Core.Models;

namespace WorkerListingApp.DataAccess.Repository
{
    public interface IEmployeeRepository
    {
        Employee Find(int id);
        List<Employee> GetAll();
        Employee Add(Employee employee);

        Employee Update(Employee employee);

        void Remove(int id);
    }
}
