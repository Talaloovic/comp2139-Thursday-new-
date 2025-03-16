using COMP2139_ICE.Data;
using COMP2139_ICE.Models;
using COMP2139_ICE.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_ICE.Areas.ProjectManagement.Controllers;
[Area("ProjectManagement")]
[Route("[area]/[controller]/[action]")]
public class ProjectTaskController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProjectTaskController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{ProjectId:int}")]
    public async Task <IActionResult> Index(int projectId)
    {
        var tasks =await _context.Tasks.
            Where(t => t.ProjectId == projectId).ToListAsync();
        
        ViewBag.ProjectId = projectId;
        return View(tasks);
    }

    [HttpGet("Details/{id:int}")]
    public async Task <IActionResult> Details(int id)
    {
        var task = await _context.
            Tasks.
            Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.ProjectId == id);

        if (task == null)
        {
            return NotFound();
        }
        return View(task);
    }

    [HttpGet("Create/{ProjectId:int}")]
    public async Task <IActionResult> Create(int projectId)
    {
        var project = await _context.Projects.FindAsync(projectId);
        if (project == null)
        {
            return NotFound();
        }

        var task = new ProjectTask
        {
            ProjectId = projectId,
            Title = "",
            Description = "",
        };
        
        return View();
    }

    [HttpPost("Create/{ProjectId:int}")]
    [ValidateAntiForgeryToken]
    public async Task <IActionResult> Create([Bind("Title", "Description", "ProjectId")]ProjectTask task)
    {
        if (ModelState.IsValid)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { projectId = task.ProjectId });
        }
        
        return View(task);
        
    }

    [HttpGet("Edit/{id:int}")]
    public async Task <IActionResult> Edit(int id)
    {
        var task = await _context.
            Tasks.
            Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.ProjectId == id);

        if (task == null)
        {
            return NotFound();
        }
        return View(task);
    }

    [HttpPost("Edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task <IActionResult> Edit(int id, [Bind("ProjectTaskId", "Title", "Description", "ProjectId")] ProjectTask task)
    {
        if (id != task.Id) 
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { projectId = task.ProjectId });
        }
        return View(task);
    }

    [HttpGet("Delete/{id:int}")]
    public async Task <IActionResult> Delete(int id)
    {
        var task = await _context.
            Tasks.
            Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.ProjectId == id);

        if (task == null)
        {
            return NotFound();
        }
        return View(task);
    }

    [HttpPost("Delete/{projectId:int}"), ActionName("Delete")]
    public async Task <IActionResult> DeleteConfirmed(int Id)
    {
        var task = _context.Tasks.Find(Id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { projectId = task.ProjectId });
        }
        return View(task);
    }
    
    [HttpGet("Search/{searchString}")]
    public async Task<IActionResult> Search(int? projectId, string searchString)
    {
        var tasksQuery = _context.Tasks.AsQueryable();

        bool searchPerformed = !string.IsNullOrWhiteSpace(searchString);

        if (projectId.HasValue)
        {
            tasksQuery = tasksQuery.Where(t => t.ProjectId == projectId);
        }
        

        if (searchPerformed)
        {
            searchString = searchString.ToLower();
            tasksQuery = tasksQuery.Where(t => t.Title.ToLower().Contains(searchString) ||
                                               t.Description.ToLower().Contains(searchString));
        }
        
        var tasks = await tasksQuery.ToListAsync();
        ViewBag.ProjectId = projectId;
        ViewData["SearchString"] = searchString;
        ViewData["SearchPerformed"] = searchPerformed;
        
        return View("Index", tasks);
        
        
        
    }
}