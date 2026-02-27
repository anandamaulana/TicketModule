namespace KAppraisal.TicketModule.Clients;

public class FileSystemDocumentDto
{
    public string Id { get; set; } = string.Empty;
    public string OriginalFilename { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string? ContentType { get; set; }
}

public interface IFileSystemClient
{
    Task<FileSystemDocumentDto> UploadAsync(IFormFile file, string userId, string? folder = null);
    Task DeleteAsync(string documentId, string userId);
}
