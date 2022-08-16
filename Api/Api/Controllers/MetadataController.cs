using Application.ServiceInterface;
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
    [HttpDelete("{id}")]
    public async Task<List<FileEntity>> DeleteAsync([FromRoute]Guid id)
    {
        return await _service.DeleteAsync(id);
    }
    [HttpPut]
    public async Task<FileEntity> EditAsync([FromBody] FileEntity obj)
    {
        return await _service.EditAsync(obj);
    }
}
