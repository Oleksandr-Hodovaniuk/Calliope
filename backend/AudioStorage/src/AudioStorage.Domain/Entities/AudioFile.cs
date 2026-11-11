namespace AudioStorage.Domain.Entities;

public class AudioFile : BaseEntity
{
    public string FileName => $"{Id}.mp3";
    public long Size { get; set; }
    public TimeSpan Duration { get; set; }
    public string StoragePath { get; set; } = default!;
    public DateTimeOffset UploadedAt { get; set; } = DateTimeOffset.UtcNow;
}
