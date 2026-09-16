namespace AutoHome.Domain.Entities;

public class Light
{
    public Guid Id { get; set; }
    public string DeviceId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public bool SupportsColour { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}