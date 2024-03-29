﻿using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UserStatsRepository : Repository<UserStat>, IUserStatsRepository
{
    private readonly DataContext _dataContext;

    public UserStatsRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
}
