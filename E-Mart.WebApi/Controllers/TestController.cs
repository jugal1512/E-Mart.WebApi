using E_Mart.WebApi.Utilities.FirebaseImageUpload;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;

namespace E_Mart.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        //private static string apiKey = "AIzaSyD7740AvtJdRskX1sMEWjQkYCr8YtqT6IM";
        //private static string bucket = "practice-bdcd1.appspot.com";
        //private static string authEmail = "jugallotwala@gmail.com";
        //private static string authPassword = "Jugal15012002";

        private readonly FirebaseStorageService _firebaseStorageService;
        private readonly IFirebaseImageUploadService _firebaseImageUploadService;
        private readonly string _fileUploadFolder;
        public TestController(FirebaseStorageService firebaseStorageService,IFirebaseImageUploadService firebaseImageUploadService,IConfiguration configuration)
        {
            _firebaseStorageService = firebaseStorageService;
            _firebaseImageUploadService = firebaseImageUploadService;
            _fileUploadFolder = configuration["FileUploadSettings:TestPage"];
        }

        [HttpGet("downloadUploadImage/{fileName}")]
        public async Task<IActionResult> DownloadUploadImage(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return BadRequest("Invalid file name.");
            }

            try
            {
                var fileUploadFolder = _fileUploadFolder;
                var firebaseGetImage = new FirebaseImageUploadModal
                {
                    fileUploadFolder = fileUploadFolder,
                    fileName = fileName,
                };
                var downloadUrl = await _firebaseImageUploadService.FirebaseGetUploadImageAsync(firebaseGetImage);

                using (var httpClient = new HttpClient())
                {
                    var fileBytes = await httpClient.GetByteArrayAsync(downloadUrl);
                    var contentType = GetContentType(fileName);
                    return File(fileBytes, contentType, fileName);
                }
            }
            catch (FirebaseStorageException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Firebase error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving file: {ex.Message}");
            }
        }

        [HttpGet("getUploadImage/{fileName}")]
        public async Task<IActionResult> GetUploadImage(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return BadRequest("Invalid file name.");
            }

            try
            {
                var fileUploadFolder = _fileUploadFolder;
                var firebaseGetImage = new FirebaseImageUploadModal
                {
                    fileUploadFolder = fileUploadFolder,
                    fileName = fileName,
                };
                var downloadUrl = await _firebaseImageUploadService.FirebaseGetUploadImageAsync(firebaseGetImage);
                return Ok(new { FileUrl = downloadUrl });
            }
            catch (FirebaseStorageException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Firebase error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving file: {ex.Message}");
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Path.GetTempPath(), fileName);
            var fileUploadFolder = _fileUploadFolder;

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var firebaseImageUpload = new FirebaseImageUploadModal
                { 
                    fileUploadFolder = fileUploadFolder,
                    fileName = fileName,
                    filePath = filePath,
                };

                var downloadUrl = await _firebaseImageUploadService.FirebaseUploadImageAsync(firebaseImageUpload);
                return Ok(new { FileUrl = downloadUrl, Message = "Image Upload Successfully." });
            }
            catch (FirebaseStorageException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Firebase error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file: {ex.Message}");
            }
        }

        [HttpDelete("delete/{fileName}")]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return BadRequest("Invalid file name.");
            }

            try
            {
                var fileUploadFolder = _fileUploadFolder;
                var firebaseGetImage = new FirebaseImageUploadModal
                {
                    fileUploadFolder = fileUploadFolder,
                    fileName = fileName,
                };
                await _firebaseImageUploadService.FirebaseDeleteUploadImageAsync(firebaseGetImage);

                return Ok(new { Message = "File deleted successfully." });
            }
            catch (FirebaseStorageException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Firebase error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting file: {ex.Message}");
            }
        }

        private string GetContentType(string fileName)
        {
            var contentType = "application/octet-stream"; // Default content type
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".pdf":
                    contentType = "application/pdf";
                    break;
            }
            return contentType;
        }
    }
}