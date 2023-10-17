using Application.Validators;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostcardDataRepository : Repository<PostcardData>, IPostcardDataRepository
{
    private readonly DataContext _dataContext;

    public PostcardDataRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<PostcardData>> GetAllPostcardsData(FiltersPostcardData filters)
    {
        IQueryable<PostcardData> query = _dataContext.PostcardData
            .Include(x => x.Postcards);

        query = ApplyFilters(query, filters);
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<PostcardData>> GetPagination(Pagination pagination, FiltersPostcardData filters)
    {
        PaginationValidator.CheckPaginationValid(pagination.PageNumber, pagination.PageSize);

        IQueryable<PostcardData> query = _dataContext.PostcardData
            .Include(x => x.Postcards);

        query = ApplyFilters(query, filters);
        return await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }

    private static IQueryable<PostcardData> ApplyFilters(IQueryable<PostcardData> query, FiltersPostcardData filters)
    {
        if (filters.UserId.HasValue)
        {
            query = query.Where(x => x.Postcards.Any(x => x.Users.Any(x => x.Id == filters.UserId)));
        }

        if (!string.IsNullOrEmpty(filters.Search))
        {
            query = query.Where(x => x.Title.ToLower().Contains(filters.Search.ToLower()));
        }

        if (!string.IsNullOrEmpty(filters.City))
        {
            query = query.Where(x => x.City.ToLower() == filters.City.ToLower());
        }

        if (!string.IsNullOrEmpty(filters.Country))
        {
            query = query.Where(x => x.Country.ToLower() == filters.Country.ToLower());
        }

        if (!string.IsNullOrEmpty(filters.Longitude))
        {
            query = query.Where(x => x.Longitude.ToLower() == filters.Longitude.ToLower());
        }

        if (!string.IsNullOrEmpty(filters.Latitude))
        {
            query = query.Where(x => x.Latitude.ToLower() == filters.Latitude.ToLower());
        }

        if (filters.CollectRangeInMeters.HasValue)
        {
            query = query.Where(x => x.CollectRangeInMeters == filters.CollectRangeInMeters);
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

    private static IQueryable<PostcardData> OrderBy(IQueryable<PostcardData> query, string orderBy)
    {
        string direction = orderBy.StartsWith("-") ? "desc" : "asc";
        string property = orderBy.Replace("-", "");

        query = property switch
        {
            "title" => direction == "asc" ? query.OrderBy(x => x.Title) : query.OrderByDescending(x => x.Title),
            "country" => direction == "asc" ? query.OrderBy(x => x.Country) : query.OrderByDescending(x => x.Country),
            "city" => direction == "asc" ? query.OrderBy(x => x.City) : query.OrderByDescending(x => x.City),
            "longitude" => direction == "asc" ? query.OrderBy(x => x.Longitude) : query.OrderByDescending(x => x.Longitude),
            "latitude" => direction == "asc" ? query.OrderBy(x => x.Latitude) : query.OrderByDescending(x => x.Latitude),
            "collectRangeInMeters" => direction == "asc" ? query.OrderBy(x => x.CollectRangeInMeters) : query.OrderByDescending(x => x.CollectRangeInMeters),
            "createdAt" => direction == "asc" ? query.OrderBy(x => x.CreatedAt) : query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Id),
        };

        return query;
    }
}
