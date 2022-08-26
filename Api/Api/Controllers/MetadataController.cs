using Api.Requests;
using Application.ServiceInterface;
using Core.Domain.DTOS;
using Core.Domain.Models;
using Microsoft.AspNetCore.Http;
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

    [HttpGet("Data/{id}")]
    public async Task<ActionResult> GetOneFile([FromRoute] Guid id)  
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
    public async Task<IActionResult> EditAsync(IFormFile fromFile, [FromRoute] Guid id/*EditRequest request*/) 
    {
        //var file = new MetadataEditDTO(request.Id);
        await _service.EditAsync(fromFile,id);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(IFormFile fromFile)
    {
       // var file = new MetadataPostDTO(request.Name, request.Type);
        await _service.PostAsync(fromFile);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> DownloadFileAsync([FromRoute] Guid id)
    {
        var file = await _service.DownloadFileAsync(id);
        return new FileStreamResult(file.Stream, file.ContentType)
        { 
            FileDownloadName=file.FileName,        
        };

    }
}
