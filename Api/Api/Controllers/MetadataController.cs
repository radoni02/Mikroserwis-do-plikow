using Api.Requests;
using Application.ServiceInterface;
using Core.Domain.DTOS;
using Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MetadataController : Controller
{
    private readonly IMetadataService _service;

    public MetadataController(IMetadataService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetFiles")]
    public async Task<ActionResult<FileEntity>> Get()
    {
        return Ok(await _service.GetFilesAsync());
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> GetOneFile(Guid id)
    {
        return Ok(await _service.GetOneFileAsync(id));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute]Guid id)
    {
       await _service.DeleteAsync(id);
        return NoContent();
    }
    [HttpPut]
    public async Task<IActionResult> EditAsync([FromBody] EditRequest request)
    {
        var file = new MetadataEditDTO(request.Id, request.Name, request.Type);
        await _service.EditAsync(file);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody]PostRequest request )
    {
        var file = new MetadataPostDTO(request.Name, request.Type);
        await _service.PostAsync(file);
        return NoContent();
    }
}
