using Api.Requests;
using Application.FileModels;
using Application.ServiceInterface;
using Core.Domain.DTOS;
using Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers;

[ApiController]
[Route("api/files/[controller]")]
public class MetadataController : Controller
{
    private readonly IMetadataService _service;
    private const long SizeLimit = 1024 * 1024 * 1024;

    public MetadataController(IMetadataService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [ProducesResponseType(statusCode: 200,Type =typeof(List<FileEntity>))]
    [ProducesResponseType(statusCode: 500)]
    [ProducesResponseType(statusCode: 404)]
    public async Task<ActionResult<FileEntity>> Get()  
    {
        return Ok(await _service.GetFilesAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(statusCode: 200, Type = typeof(FileEntity))]
    [ProducesResponseType(statusCode: 500)]
    [ProducesResponseType(statusCode: 404)]
    public async Task<ActionResult> GetOneFile([FromRoute] Guid id)  
    {
        return Ok(await _service.GetOneFileAsync(id));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(statusCode: 204)]
    [ProducesResponseType(statusCode: 500)]
    [ProducesResponseType(statusCode: 404)]
    public async Task<IActionResult> DeleteAsync([FromRoute]Guid id)  
    {
       await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(statusCode: 204)]
    [ProducesResponseType(statusCode: 500)]
    [ProducesResponseType(statusCode: 404)]
    public async Task<IActionResult> EditAsync(IFormFile fromFile, [FromRoute] Guid id) 
    {
        await _service.EditAsync(fromFile,id);
        return NoContent();
    }

    [HttpPost]
    [RequestSizeLimit(SizeLimit)]
    [RequestFormLimits(MultipartBodyLengthLimit = SizeLimit)]
    [ProducesResponseType(statusCode: 204)]
    [ProducesResponseType(statusCode: 500)]
    [ProducesResponseType(statusCode: 400)]
    public async Task<IActionResult> PostAsync(IFormFile fromFile)
    {
        await _service.PostAsync(fromFile);
        return NoContent();
    }

    [HttpGet("download{id}")]
    [ProducesResponseType(statusCode: 200, Type = typeof(FileStreamResult))]
    [ProducesResponseType(statusCode: 500)]
    [ProducesResponseType(statusCode: 403)]
    [ProducesResponseType(statusCode: 401)]
    public async Task<ActionResult> DownloadFileAsync([FromRoute] Guid id)
    {
        var file = await _service.DownloadFileAsync(id);
        return new FileStreamResult(file.Stream, file.ContentType)
        { 
            FileDownloadName=file.FileName,        
        };

    }
}
