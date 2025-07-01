using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;

namespace MyApp.Controllers;


[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<StudentsController> _logger;

    public StudentsController(AppDbContext context, ILogger<StudentsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _context.Students
            .Include(s => s.Address)
            .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
            .ToListAsync();

        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var student = await _context.Students
            .Include(s => s.Address)
            .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null) return NotFound();

        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Student updated)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();

        student.Name = updated.Name;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
