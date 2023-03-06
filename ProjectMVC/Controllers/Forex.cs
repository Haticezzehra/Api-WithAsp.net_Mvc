using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Data;
using ProjectMVC.Models.Domain;

namespace ProjectMVC.Controllers
{
    public class ForexController : Controller
    {

        ForexDbContext _context;
        public ForexController(ForexDbContext context)
        {
            _context = context;
        }

        public IActionResult GetListForex()
        {
            AddData add = new AddData(_context);
            add.Add();
            List<Forex> forex = _context.Forex.ToList();

            return View(forex);
        }
    }
}
