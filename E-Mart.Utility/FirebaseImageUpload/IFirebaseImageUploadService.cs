namespace E_Mart.Utility.FirebaseImageUpload;

public interface IFirebaseImageUploadService
{
    Task<string> FirebaseGetUploadImageAsync(FirebaseImageUploadModal firebaseImageUpload);
    Task<string> FirebaseUploadImageAsync(FirebaseImageUploadModal firebaseImageUpload);
    Task FirebaseDeleteUploadImageAsync(FirebaseImageUploadModal firebaseImageUpload);
}