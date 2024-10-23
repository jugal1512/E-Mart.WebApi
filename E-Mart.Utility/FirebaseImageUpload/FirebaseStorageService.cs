namespace E_Mart.Utility.FirebaseImageUpload;

public class FirebaseStorageService
{
    public string BucketName { get; }
    public string FirebaseStorageUrl { get; }

    public FirebaseStorageService(string bucketName, string firebaseStorageUrl)
    {
        BucketName = bucketName;
        FirebaseStorageUrl = firebaseStorageUrl;
    }
}