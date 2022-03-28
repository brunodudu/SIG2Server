using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIG2Server.Models;

namespace SIG2Server.Controllers;

[ApiController]
[Route("sig2server/[controller]")]
public class SoftwareOMController : ControllerBase
{
    private readonly ILogger<SoftwareOMController> _logger;
    private readonly ModelsContext _context;

    public SoftwareOMController(ILogger<SoftwareOMController> logger, ModelsContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<SoftwareOM>>> Get(long id)
    {
        var softwareOM = await _context.SoftwareOMs.FindAsync(id);
        if (softwareOM == null)
        {
            return NotFound();
        }

        softwareOM.OM = await _context.OMs.FindAsync(softwareOM.OMId);
        softwareOM.Software = await _context.Softwares.FindAsync(softwareOM.SoftwareId);
        return Ok(softwareOM);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SoftwareOM>>> GetAll()
    {
        var softwareOMs = await _context.SoftwareOMs.Select(x => x).ToListAsync();
        if (softwareOMs == null)
        {
            return NotFound();
        }

        foreach (var softwareOM in softwareOMs)
        {
            softwareOM.OM = await _context.OMs.FindAsync(softwareOM.OMId);
            softwareOM.Software = await _context.Softwares.FindAsync(softwareOM.SoftwareId);
        }
        return Ok(softwareOMs);
    }

    [HttpGet("SoftwareName")]
    public async Task<ActionResult<IEnumerable<SoftwareOM>>> GetAllWithSoftwareName(string softwareName)
    {
        var softwareOMs = await _context.SoftwareOMs.Where(x => x.Software.Nome == softwareName).ToListAsync();
        if (softwareOMs == null)
        {
            return NotFound();
        }

        foreach (var softwareOM in softwareOMs)
        {
            softwareOM.OM = await _context.OMs.FindAsync(softwareOM.OMId);
            softwareOM.Software = await _context.Softwares.FindAsync(softwareOM.SoftwareId);
        }
        return Ok(softwareOMs);
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<SoftwareOM>>> Post(SoftwareOM softwareOM)
    {
        try
        {
            if (softwareOM.OM != null)
            {
                _context.OMs.AddOrUpdate<OM>(softwareOM.OM, softwareOM.OMId);
            } else
            {
                softwareOM.OMId = 0;
                softwareOM.OM = null;
            }

            if (softwareOM.Software != null)
            {
                _context.Softwares.AddOrUpdate<Software>(softwareOM.Software, softwareOM.SoftwareId);
            } else
            {
                softwareOM.SoftwareId = 0;
                softwareOM.Software = null;
            }

            _context.SoftwareOMs.AddOrUpdate<SoftwareOM>(softwareOM, softwareOM.Id);
            await _context.SaveChangesAsync();
        } catch (Exception e)
        {
            throw;
        }

        return CreatedAtAction(nameof(Get), new { id = softwareOM.Id }, softwareOM);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(long id, SoftwareOM softwareOM)
    {
        softwareOM.Id = id;

        try
        {
            if (softwareOM.OM != null)
            {
                _context.OMs.AddOrUpdate<OM>(softwareOM.OM, softwareOM.OMId);
            } else
            {
                softwareOM.OMId = 0;
                softwareOM.OM = null;
            }

            if (softwareOM.Software != null)
            {
                _context.Softwares.AddOrUpdate<Software>(softwareOM.Software, softwareOM.SoftwareId);
            } else
            {
                softwareOM.SoftwareId = 0;
                softwareOM.Software = null;
            }

            _context.SoftwareOMs.Update(softwareOM);
            await _context.SaveChangesAsync();
        } catch (Exception e)
        {
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var softwareOM = await _context.SoftwareOMs.FindAsync(id);
        if (softwareOM == null)
        {
            return NotFound();
        }

        _context.SoftwareOMs.Remove(softwareOM);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
