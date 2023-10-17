using Application.Validators;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostcardRepository : Repository<Postcard>, IPostcardRepository
{
    private readonly DataContext _dataContext;

    public PostcardRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<Postcard>> GetAllPostcardsByUserId(FiltersPostcard filters)
    {
        IQueryable<Postcard> query = _dataContext.Postcards
        .Include(x => x.PostcardData)
        .Where(x => x.Users.Any(user => user.Id == filters.UserId));

        query = ApplyFilters(query, filters);
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Postcard>> GetPaginationByUserId(Pagination pagination, FiltersPostcard filters)
    {
        PaginationValidator.CheckPaginationValid(pagination.PageNumber, pagination.PageSize);

        IQueryable<Postcard> query = _dataContext.Postcards
            .Include(x => x.PostcardData)
            .Where(x => x.Users.Any(x => x.Id == filters.UserId));

        query = ApplyFilters(query, filters);
        return await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }

    private static IQueryable<Postcard> ApplyFilters(IQueryable<Postcard> query, FiltersPostcard filters)
    {
        if (!string.IsNullOrEmpty(filters.Search))
        {
            query = query
                .Where(x => x.Content.ToLower().Contains(filters.Search.ToLower())
                        || x.Title.ToLower().Contains(filters.Search.ToLower())
                );
        }

        if (!string.IsNullOrEmpty(filters.Type))
        {
            query = query.Where(x => x.Type.ToLower() == filters.Type.ToLower());
        }

        if (filters.IsSent.HasValue)
        {
            query = query.Where(x => x.IsSent == filters.IsSent);
        }

        if (filters.DateFrom.HasValue)
        {
            query = query.Where(x => x.CreatedAt >= filters.DateFrom);
        }

        if (filters.DateTo.HasValue)
        {
            query = query.Where(x => x.CreatedAt <= filters.DateTo);
        }

        if (!string.IsNullOrEmpty(filters.OrderBy))
        {
            query = OrderBy(query, filters.OrderBy);
        }

        return query;
    }

    private static IQueryable<Postcard> OrderBy(IQueryable<Postcard> query, string orderBy)
    {
        string direction = orderBy.StartsWith("-") ? "desc" : "asc";
        string property = orderBy.Replace("-", "");

        query = property switch
        {
            "title" => direction == "asc" ? query.OrderBy(x => x.Title) : query.OrderByDescending(x => x.Title),
            "content" => direction == "asc" ? query.OrderBy(x => x.Content) : query.OrderByDescending(x => x.Content),
            "type" => direction == "asc" ? query.OrderBy(x => x.Type) : query.OrderByDescending(x => x.Type),
            "isSent" => direction == "asc" ? query.OrderBy(x => x.IsSent) : query.OrderByDescending(x => x.IsSent),
            "createdAt" => direction == "asc" ? query.OrderBy(x => x.CreatedAt) : query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Id),
        };

        return query;
    }
}
