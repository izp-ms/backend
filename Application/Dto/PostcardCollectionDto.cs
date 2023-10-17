namespace Application.Dto;

public class PostcardCollectionDto
{
    public int UserId { get; set; }
    public IEnumerable<int> PostcardDataIds { get; set; } = new List<int>();
}
