using Medicio.DAL;
using Medicio.Models;
using Medicio.Utilize;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Medicio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DoctorController : Controller
    {
        public readonly IWebHostEnvironment _env;
        public readonly AppDbContext _context;

        public DoctorController(IWebHostEnvironment env, AppDbContext context)
        {
            _env = env;
            _context = context;
        }
         
        // GET: DoctorController
        public ActionResult Index()
        {
            List<Doctors> doctorList = _context.Doctor.ToList();
            return View(doctorList);
        }

        // GET: DoctorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DoctorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Doctors doctor)
        {
            if (!ModelState.IsValid) return View();
            if(doctor.Img.CheckType("/images"))
            {
                ModelState.AddModelError("Type", "Wrong Type");
            }
            if (doctor.Img.CheckSize(1000))
            {
                ModelState.AddModelError("Size", "Wrong Size");
            }
            if (doctor.Img != null)
            {
                doctor.ImgUrl = doctor.Img.SaveImg(Path.Combine(_env.WebRootPath, "admin/Images"));
            }
            _context.Doctor.Add(doctor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: DoctorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DoctorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Doctors doctor)
        {
            if (doctor.Id != id) return View();
            Doctors DbDoctor = _context.Doctor.Find(id);

            if (doctor.Img.CheckType("/images"))
            {
                ModelState.AddModelError("Type", "Wrong Type");
            }
            if (doctor.Img.CheckSize(1000))
            {
                ModelState.AddModelError("Size", "Wrong Size");
            }
            if(doctor.Img != null)
            {
                FileManager.DeleteImg(Path.Combine(_env.WebRootPath, "images", DbDoctor.ImgUrl));
                DbDoctor.ImgUrl = doctor.Img.SaveImg(Path.Combine(_env.WebRootPath, "admin/Images"));
            }
            DbDoctor.Name = doctor.Name;
            DbDoctor.Duty = doctor.Duty;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        // GET: DoctorController/Delete/5
        public ActionResult Delete(int id)
        {
            Doctors doctor = _context.Doctor.Find(id);
            if (doctor == null) return View();
            FileManager.DeleteImg(Path.Combine(_env.WebRootPath, doctor.ImgUrl));
            _context.Doctor.Remove(doctor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: DoctorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
