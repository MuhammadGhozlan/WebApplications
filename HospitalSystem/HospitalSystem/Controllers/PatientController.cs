using HospitalSystem.Data;
using HospitalSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers
{
    public class PatientController : Controller
    {
        private ApplicationDbContext _db;
        public PatientController(ApplicationDbContext db)
        {
            this._db = db;

        }
        public IActionResult Index()
        {
            IEnumerable<Patient> ListOfPatients = _db.Patients;
            return View(ListOfPatients);
        }

        //GET for Create
        public IActionResult CreatePatient()
        {
            return View();
        }
        //POST for Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePatient(Patient patient)
        {
            if (patient.Id.ToString() == patient.Name)
            {
                ModelState.AddModelError("CustomError", "Parient Id Can't be Like Patient Name");
            }
            if(ModelState.IsValid)
            {
                _db.Patients.Add(patient);
                _db.SaveChanges();
                TempData["success"] = "Patient is Added Successfully";
                return RedirectToAction("Index");
            }
            
            return View(patient);
        }

        //GET for Edit
        public IActionResult Edit(int id)
        {
            if(id == 0) 
            {
                return NotFound();
            }
            var PatientFromDb=_db.Patients.FirstOrDefault(x => x.Id == id);
            if (PatientFromDb == null)
            {
                return NotFound();
            }
            
            return View(PatientFromDb);
        }

        //POST for Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Patient patient)
        {
            if (patient.Id.ToString() == patient.Name)
            {
                ModelState.AddModelError("CustomError", "Patient Name should not be similar to Patient Id");
            }
            if (ModelState.IsValid)
            {
                _db.Patients.Update(patient);
                _db.SaveChanges();
                TempData["success"] = "Patient Updated Successfully";
                return RedirectToAction("Index");
            }

            return View(patient);

        }

        //GET Delete
        public IActionResult Delete(int? id)
        {
            if (id == 0|| id==null)
            {
                return NotFound();
            }
            var PatientFromDb= _db.Patients.FirstOrDefault(x => x.Id == id);
            
            if (PatientFromDb == null)
            {
                return NotFound();
            }
            return View(PatientFromDb);
        }

        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Patient patient)
        {
            _db.Patients.Remove(patient);
            _db.SaveChanges();
            TempData["success"] = "Patient Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
