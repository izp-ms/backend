using Application.Validators;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostcardDataRepository : Repository<PostcardData>, IPostcardDataRepository
{
    private readonly DataContext _dataContext;

    public PostcardDataRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<PostcardData>> GetAllPostcardsDataByUserId(int userId)
    {
        return await _dataContext.PostcardData
            .Include(x => x.Postcards)
            .Where(x => x.Postcards.Any(x => x.Users.Any(x => x.Id == userId)))
            .ToListAsync();
    }

    public async Task<IEnumerable<PostcardData>> GetPaginationByUserId(int pageNumber, int pageSize, int userId)
    {
        PaginationValidator.CheckPaginationValid(pageNumber, pageSize);

        return await _dataContext.PostcardData
            .Include(x => x.Postcards)
            .Where(x => x.Postcards.Any(x => x.Users.Any(x => x.Id == userId)))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
