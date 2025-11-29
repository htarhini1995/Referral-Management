using FleetManagement.EF.Models;
using FleetManagement.EF.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentService _documentService;

        public DocumentsController(DocumentService documentService)
        {
            _documentService = documentService;
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken ct)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty.");

            var username = User.Identity?.Name ?? "unknown";
            await using var stream = file.OpenReadStream();

            var doc = await _documentService.UploadAsync(
                fileName: file.FileName,
                contentType: file.ContentType,
                sizeBytes: file.Length,
                fileStream: stream,
                uploadedBy: username,
                ct: ct);

            return Ok(new
            {
                doc.Id,
                doc.OriginalFileName,
                doc.ContentType,
                doc.SizeBytes,
                doc.UploadedAtUtc,
                doc.UploadedBy
            });
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetContent(long id, CancellationToken ct)
        {
            var result = await _documentService.GetContentAsync(id, ct);
            if (result == null)
                return NotFound();

            var base64 = Convert.ToBase64String(result.Content);

            return Ok(new
            {
                Id = id,
                FileName = result.FileName,
                ContentType = result.ContentType,
                ContentBase64 = base64
            });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            var ok = await _documentService.DeleteAsync(id, ct);
            if (!ok)
                return NotFound();

            return NoContent();
        }
    }
}
