using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.Employees;
using Ssi.TrackTruck.Web.Auth;

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
        [AllowedRoles(Role.Admin, Role.Encoder)]
        public ActionResult All()
        {
            return Json(_employeeService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowedRoles(Role.Admin)]
        public ActionResult Add(AddEmployeeRequest request)
        {
            var response = _employeeService.Add(request);
            return Json(response);
        }

        [HttpPost]
        [AllowedRoles(Role.Admin)]
        public ActionResult Delete(string id)
        {
            var response = _employeeService.Delete(id);
            return Json(response);
        }

        [HttpPost]
        [AllowedRoles(Role.Admin)]
        public ActionResult Save(EditEmployeeRequest request)
        {
            var response = _employeeService.Save(request);
            return Json(response);
        }
	}
}