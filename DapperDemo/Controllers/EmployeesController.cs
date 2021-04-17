using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkerListingApp.Core.Models;
using WorkerListingApp.DataAccess.Repository;



namespace WorkerListingApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly ICompanyRepository _companyRepo;

        public EmployeesController(IEmployeeRepository employeeRepo, ICompanyRepository companyRepo)
        {
            _employeeRepo = employeeRepo;
            _companyRepo = companyRepo;
        }






        [BindProperty]
        public Employee Employee { get; set; }








        // GET: Employees
        public IActionResult Index()
        {
            return View(_employeeRepo.GetAll());
        }





        //// GET: Employees/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var employee =_employeeRepo.Find(id.GetValueOrDefault());
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(employee);
        //}








        // GET: Employees/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> companyList = _companyRepo.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.CompanyId.ToString()
                });
            ViewBag.CompanyList = companyList;                                                                      
            return View();
        }







        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.








        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost()
        {
            if (ModelState.IsValid)
            {
                _employeeRepo.Add(Employee);
                return RedirectToAction(nameof(Index));
            }
            return View(Employee);
        }









        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Employee = _employeeRepo.Find(id.GetValueOrDefault());
            IEnumerable<SelectListItem> companyList = _companyRepo.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.CompanyId.ToString()
            });
            ViewBag.CompanyList = companyList;

            if (Employee == null)
            {
                return NotFound();
            }
            return View(Employee);
        }







        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != Employee.EmployeeId)
            {
                return NotFound();
            }
            _employeeRepo.Update(Employee);

            return View(Employee);
        }









        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _employeeRepo.Remove(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index));
        }






    }
}
