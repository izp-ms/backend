namespace Application.Validators;

public class PaginationValidator
{
    public static void CheckPaginationValid(int pageNumber, int pageSize)
    {
        if (pageNumber < 0 || pageSize <= 0 || pageSize > 100)
        {
            throw new ArgumentException("Invalid page number or page size.");
        }
    }
}
