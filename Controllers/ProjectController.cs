using COMP2139_ICE.Models;
using Microsoft.AspNetCore.Mvc;

namespace COMP2139_ICE.Controllers;

public class ProjectController : Controller
{

    
    /// <summary>
    /// index action will retrieve a listing of projects (database)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Index()
    {
        var projects = new List<Project>
        {
            new Project { ProjectId = 1, Name = "Project 1", Description = "Last Project " },
        };
        return View(projects);
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Project project)
    {
        //persist new project to the database
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var project = new Project {ProjectId = id, Name = "Project 1", Description = "Last Project " };
        return View(project);
    }
    
}