using COMP2139_ICE.Data;
using COMP2139_ICE.Models;
using COMP2139_ICE.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_ICE.Areas.ProjectManagement.Controllers;

[Area("ProjectManagement")]
[Route("[area]/[controller][action]")]
public class ProjectController : Controller
{

    
    private readonly ApplicationDbContext _context;
    public ProjectController(ApplicationDbContext context)
    {
     
        _context = context;
    }
    
    
    /// <summary>
    /// index action will retrieve a listing of projects (database)
    /// </summary>
    /// <returns></returns>
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var projects = await _context.Projects.ToListAsync();
        return View(projects);
    }


    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task <IActionResult> Create(Project project)
    {
        if (ModelState.IsValid)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        
        //persist new project to the database
        return View(project);
    }

    [HttpGet("Details/{id:int}")]
    public async Task <IActionResult> Details(int id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);

        if (project == null)
        {
            return NotFound();
        }
        
        
        return View(project);
    }
    
    
    [HttpGet("Edit/{id:int}")]
    public async Task <IActionResult> Edit(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }

    [HttpPost("Edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task <IActionResult> Edit(int id, [Bind("ProjectId, Name, Description")] Project project)
    {
        if (id != project.ProjectId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Projects.Update(project);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProjectExists(project.ProjectId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }
        return View(project);
        
        
    }

    private async  Task <bool>  ProjectExists(int id)
    {
        return await _context.Projects.AnyAsync(e => e.ProjectId == id);
    }
    
    [HttpGet("Delete/{id:int}")]
    public async Task <IActionResult> Delete(int id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);

        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }

    [HttpPost("Delete/{ProjectId}"), ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task <IActionResult> DeleteConfirmed(int ProjectId)
    {
        var project = await _context.Projects.FindAsync(ProjectId);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            
        }
        return NotFound();
    }

    [HttpGet("Search/{searchString}")]
    public async Task<IActionResult> Search(string searchString)
    {
        var projectsQuery = _context.Projects.AsQueryable();

        bool searchPerformed = !string.IsNullOrWhiteSpace(searchString);

        if (!searchPerformed)
        {
            searchString = searchString.ToLower();
            projectsQuery = projectsQuery.Where(p => p.Name.ToLower().Contains(searchString) ||
                                                     p.Description.ToLower().Contains(searchString));
        }
        
        var projects = await projectsQuery.ToListAsync();
        
        ViewData["SearchString"] = searchString;
        ViewData["SearchPerformed"] = searchPerformed;
        
        return View("Index", projects);
        
        
        
    }
    
}