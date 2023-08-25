using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class AddressRepository : Repository<Address>, IAddressRepository
{
    private readonly DataContext _dataContext;

    public AddressRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
}
