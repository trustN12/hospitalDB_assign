
using HospitalDb.DAL;
using HospitalDb.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalDb.Controllers;

public class PatientController : Controller
{
    private readonly DataAccessLayer _dal;

    public PatientController(IConfiguration config)
    {
        _dal = new DataAccessLayer(config);
    }

    // public IActionResult Index()
    // {
    //     var list = _dal.GetPatients();
    //     return View(list);
    // }
    
    public IActionResult Index(string searchText, int? doctorId, int page = 1)
    {
        int pageSize = 5;

        var patients = _dal.GetPatients();

        // SEARCH
        if (!string.IsNullOrEmpty(searchText))
        {
            patients = patients.Where(p =>
                p.Pid.ToString().Contains(searchText) ||
                p.Fname.Contains(searchText) ||
                p.Lname.Contains(searchText) ||
                p.Gender.Contains(searchText) ||
                p.DoctorName.Contains(searchText) ||
                p.Specialisation.Contains(searchText)
            ).ToList();
        }

        // DOCTOR FILTER
        if (doctorId != null)
        {
            patients = patients.Where(p => p.Did == doctorId).ToList();
        }

        // ⭐ STATISTICS (before pagination)
        ViewBag.TotalPatients = patients.Count();
        ViewBag.TotalMale = patients.Count(x => x.Gender == "Male");
        ViewBag.TotalFemale = patients.Count(x => x.Gender == "Female");

        // PAGINATION
        var pagedPatients = patients
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.Page = page;
        ViewBag.TotalPages = (int)Math.Ceiling(ViewBag.TotalPatients / (double)pageSize);

        ViewBag.Doctors = _dal.GetDoctors();
        ViewBag.SearchText = searchText;

        return View(pagedPatients);
    }
   
    
    
    
    public IActionResult Create()
    {
        ViewBag.Doctors = _dal.GetDoctors();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Patient p)
    {
        _dal.InsertPatient(p);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var patient = _dal.GetPatients().FirstOrDefault(x => x.Pid == id);
        ViewBag.Doctors = _dal.GetDoctors();
        return View(patient);
    }

    [HttpPost]
    public IActionResult Edit(Patient p)
    {
        _dal.UpdatePatient(p);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        _dal.DeletePatient(id);
        return RedirectToAction("Index");
    }
    
    public IActionResult Details(int id)
    {
        var patient = _dal.GetPatientById(id);
        return View(patient);
    }
}