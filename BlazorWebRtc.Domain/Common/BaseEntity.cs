namespace BlazorWebRtc.Domain.Common;

public abstract class BaseEntity
{
    public BaseEntity()
    {
        CreateDate = DateTime.Now;
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }    
}
