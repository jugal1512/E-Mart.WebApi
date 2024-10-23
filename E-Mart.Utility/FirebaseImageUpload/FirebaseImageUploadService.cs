using Firebase.Storage;

namespace E_Mart.Utility.FirebaseImageUpload;

public class FirebaseImageUploadService : IFirebaseImageUploadService
{
    private readonly FirebaseStorageService _firebaseStorageService;
    public FirebaseImageUploadService(FirebaseStorageService firebaseStorageService)
    {
        _firebaseStorageService = firebaseStorageService;
    }

    public async Task<string> FirebaseGetUploadImageAsync(FirebaseImageUploadModal firebaseImageUpload)
    {
        var storage = new FirebaseStorage(_firebaseStorageService.BucketName);
        var downloadUrl = await storage.Child(firebaseImageUpload.fileUploadFolder).Child(firebaseImageUpload.fileName).GetDownloadUrlAsync();
        return downloadUrl;
    }

    public async Task<string> FirebaseUploadImageAsync(FirebaseImageUploadModal firebaseImageUpload)
    {
        var storage = new FirebaseStorage(_firebaseStorageService.BucketName);
        var downloadUrl = await storage
            .Child(firebaseImageUpload.fileUploadFolder)
            .Child(firebaseImageUpload.fileName)
            .PutAsync(File.OpenRead(firebaseImageUpload.filePath));
        return downloadUrl;
    }

    public async Task FirebaseDeleteUploadImageAsync(FirebaseImageUploadModal firebaseImageUpload)
    {
        var storage = new FirebaseStorage(_firebaseStorageService.BucketName);
        await storage.Child(firebaseImageUpload.fileUploadFolder).Child(firebaseImageUpload.fileName).DeleteAsync();
    }
}