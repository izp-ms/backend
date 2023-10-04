namespace Application.Validators;

public class PaginationValidator
{
    private static readonly int[] AllowedPageSizes = { 10, 25, 50, 100 };

    public static void CheckPaginationValid(int pageNumber, int pageSize)
    {
        if (pageNumber < 0 || pageSize <= 0)
        {
            throw new ArgumentException("Invalid page number or page size.");
        }

        if (!AllowedPageSizes.Contains(pageSize))
        {
            throw new ArgumentException("Invalid page size. Allowed page sizes: 10, 25, 50, 100.");
        }
    }
}
