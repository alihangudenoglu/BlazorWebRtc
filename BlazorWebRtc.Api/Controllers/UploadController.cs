using BlazorWebRtc.Application.Features.Commands.Upload;
using BlazorWebRtc.Application.Interface.Services;
using BlazorWebRtc.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebRtc.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly IUploadService _uploadService;
    private IWebHostEnvironment _webHostEnvironment;

    public UploadController(IUploadService uploadService, IWebHostEnvironment webHostEnvironment)
    {
        _uploadService = uploadService;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage([FromForm]UploadFileModel model)
    {

        if (model.File == null || model.File.Length == 0)
            return BadRequest("No file uploaded.");

        // Dosyanın wwwroot altında saklanacağı dizin
        var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "profile-pictures");

        // Eğer dizin yoksa oluşturulur
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        // Benzersiz bir dosya adı oluşturulur
        var fileName = Path.GetRandomFileName() + Path.GetExtension(model.File.FileName);

        var filePath = Path.Combine(uploadPath, fileName);

        // Dosya sistemine kaydetme işlemi
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await model.File.CopyToAsync(stream);
        }

        // Dosyanın tarayıcıda erişilebilir yolu
        var imageUrl = $"images/profile-pictures/{fileName}";

        UploadCommand uploadCommand = new UploadCommand();
        uploadCommand.File = imageUrl;
        uploadCommand.UserId = Guid.Parse(model.UserId);

        var result = await _uploadService.UploadFile(uploadCommand);
        return Ok(result);

    }

}
