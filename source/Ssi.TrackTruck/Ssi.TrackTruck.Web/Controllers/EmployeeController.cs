using System.Collections.Generic;
using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.DAL.Entities;
using Ssi.TrackTruck.Bussiness.Employees;

namespace Ssi.TrackTruck.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult GetByDesignations(IList<string> designations)
        {
            var employees = _employeeService.GetByDesignations(designations);
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult All()
        {
            return Json(_employeeService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(Employee request)
        {
            var response = _employeeService.Add(request);
            return Json(response);
        }
	}
}