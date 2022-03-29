using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public interface IEmployeesController
    {
        Task<JsonResult> GetAllProfile();
        IActionResult Index();
    }
}