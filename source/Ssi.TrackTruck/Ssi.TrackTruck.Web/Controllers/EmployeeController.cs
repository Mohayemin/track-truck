using System.Web.Mvc;
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
        public ActionResult All()
        {
            return Json(_employeeService.GetAll(), JsonRequestBehavior.AllowGet);
        }
	}
}