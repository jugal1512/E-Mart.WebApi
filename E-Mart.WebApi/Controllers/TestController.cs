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
        public TestController(FirebaseStorageService firebaseStorageService)
        {
            _firebaseStorageService = firebaseStorageService;
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
                var storage = new FirebaseStorage(_firebaseStorageService.BucketName);
                var downloadUrl = await storage.Child("uploads").Child(fileName).GetDownloadUrlAsync();

                using (var httpClient = new HttpClient())
                {
                    var fileBytes = await httpClient.GetByteArrayAsync(downloadUrl);

                    // Determine content type based on file extension
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
                            // Add more cases as needed for other file types
                    }

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



        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Path.GetTempPath(), fileName);

            try
            {
                // Save the file temporarily
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Upload to Firebase Storage
                var storage = new FirebaseStorage(_firebaseStorageService.BucketName);
                var downloadUrl = await storage
                    .Child("uploads")
                    .Child(fileName)
                    .PutAsync(System.IO.File.OpenRead(filePath));

                return Ok(new { FileUrl = downloadUrl });
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
                var storage = new FirebaseStorage(_firebaseStorageService.BucketName);
                await storage.Child("uploads").Child(fileName).DeleteAsync();

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

        // Example of a method to serve a file
        [HttpGet("download/{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {
            var filePath = Path.Combine(Path.GetTempPath(), fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }
    }
}