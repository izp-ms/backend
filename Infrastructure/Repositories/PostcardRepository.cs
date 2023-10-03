using Application.Validators;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostcardRepository : Repository<Postcard>, IPostcardRepository
{
    private readonly DataContext _dataContext;

    public PostcardRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<Postcard>> GetAllPostcardsByUserId(int userId)
    {
        return await _dataContext.Postcards
            .Include(x => x.PostcardData)
            .Where(x => x.Users.Any(x => x.Id == userId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Postcard>> GetPaginationByUserId(int pageNumber, int pageSize, int userId)
    {
        PaginationValidator.CheckPaginationValid(pageNumber, pageSize);

        return await _dataContext.Postcards
            .Include(x => x.PostcardData)
            .Where(x => x.Users.Any(x => x.Id == userId))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
