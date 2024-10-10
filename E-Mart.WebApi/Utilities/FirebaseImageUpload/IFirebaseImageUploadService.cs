namespace E_Mart.WebApi.Utilities.FirebaseImageUpload;

public interface IFirebaseImageUploadService
{
    Task<string> FirebaseGetUploadImageAsync(FirebaseImageUploadModal firebaseImageUpload);
    Task<string> FirebaseUploadImageAsync(FirebaseImageUploadModal firebaseImageUpload);
    Task FirebaseDeleteUploadImageAsync(FirebaseImageUploadModal firebaseImageUpload);
}