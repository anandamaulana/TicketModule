using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KAppraisal.TicketModule.Clients;

public class FileSystemClient(HttpClient httpClient) : IFileSystemClient
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    private const string BucketName = "ticket-attachments"; //.

    public async Task<FileSystemDocumentDto> UploadAsync(IFormFile file, string userId)
    {
        using var content = new MultipartFormDataContent();
        using var stream = file.OpenReadStream();

        var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(
            file.ContentType ?? "application/octet-stream"
        );
        content.Add(fileContent, "file", file.FileName);

        // Set X-USER-ID header agar FileSystem tahu siapa yang upload
        httpClient.DefaultRequestHeaders.Remove("X-USER-ID");
        httpClient.DefaultRequestHeaders.Add("X-USER-ID", userId);

        var response = await httpClient.PostAsync($"/api/Files/{BucketName}", content);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<FileSystemDocumentDto>(json, _jsonOptions);

        return result ?? throw new Exception("FileSystem returned empty response.");
    }

    public async Task DeleteAsync(string documentId, string userId)
    {
        httpClient.DefaultRequestHeaders.Remove("X-USER-ID");
        httpClient.DefaultRequestHeaders.Add("X-USER-ID", userId);

        var response = await httpClient.DeleteAsync($"/api/Files/{documentId}");
        response.EnsureSuccessStatusCode();
    }
}
