using Microsoft.AspNetCore.Mvc;
using Socios.Application.Interfaces;

namespace Socios.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentsController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadDocument(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No se proporcionó ningún archivo.");
        }

        // Abrimos el archivo en un stream de memoria para pasárselo al servicio
        using var stream = file.OpenReadStream();

        var result = await _documentService.ProcessAndStoreDocumentAsync(file.FileName, stream, file.ContentType);

        if (result)
        {
            return Ok(new { message = "Documento procesado correctamente. Revisa la consola para ver el texto extraído." });
        }

        return StatusCode(500, "Ocurrió un error al procesar el documento. Asegúrate de que sea un PDF válido.");
    }
}