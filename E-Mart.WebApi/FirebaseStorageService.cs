namespace E_Mart.WebApi;

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
