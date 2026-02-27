using Amazon.S3;
using Amazon.S3.Model;
using DotNetEnv;
using KAppraisal.TicketModule.Helpers;

namespace KAppraisal.TicketModule.Clients;

public class FileSystemClient : IFileSystemClient
{
    private const string BucketName = "ticket-attachments";

    private IAmazonS3 CreateClient()    
    {
        Env.Load();
        var endpoint = Env.GetString(EnvironmentConstants.R2EndPoint, "http://127.0.0.1:9000");
        var accessKey = Env.GetString(EnvironmentConstants.R2AccessId, "minioadmin");
        var secretKey = Env.GetString(EnvironmentConstants.R2SecretKey, "minioadmin");

        return new AmazonS3Client(
            accessKey,
            secretKey,
            new AmazonS3Config
            {
                ServiceURL = endpoint,
                ForcePathStyle = true
            }
        );
    }
    
        public async Task<FileSystemDocumentDto> UploadAsync(IFormFile file, string userId, string? folder = null)
        {
        var client = CreateClient();
        var prefix = string.IsNullOrEmpty(folder) ? "uploads" : folder;
        var key = $"{prefix}/{Guid.NewGuid():N}_{file.FileName}";

        try
        {
            await client.PutBucketAsync(new PutBucketRequest
            {
                BucketName = BucketName,
                UseClientRegion = true
            });
        }
        catch { }

        using var stream = file.OpenReadStream();
        await client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = BucketName,
            Key = key,
            InputStream = stream,
            ContentType = file.ContentType,
            // DisablePayloadSigning = true
        });

        Env.Load();
        var endpoint = Env.GetString(EnvironmentConstants.R2EndPoint, "http://127.0.0.1:9000");
        var url = $"{endpoint}/{BucketName}/{key}";

        return new FileSystemDocumentDto
        {
            Id = key,
            Url = url
        };
    }

    public async Task DeleteAsync(string documentId, string userId)
    {
        var client = CreateClient();
        await client.DeleteObjectAsync(new DeleteObjectRequest
        {
            BucketName = BucketName,
            Key = documentId
        });
    }
}