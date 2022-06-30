using Medicio.DAL;
using Medicio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Medicio.Controllers
{
    public class HomeController : Controller
    {
        public readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Doctors> doctorList = _context.Doctor.ToList();
            return View(doctorList);
        }
    }
}
