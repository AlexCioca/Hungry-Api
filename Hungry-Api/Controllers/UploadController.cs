using Azure.Core;
using Hungry_Api.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Hungry_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService uploadService;
        public UploadController(IUploadService uploadService)
        {
            this.uploadService = uploadService ?? throw new ArgumentNullException(nameof(uploadService));
        }
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadAsync()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fileURL = await uploadService.UploadAsync(file.OpenReadStream(), fileName, file.ContentType);
                    return Ok(new { fileURL });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
