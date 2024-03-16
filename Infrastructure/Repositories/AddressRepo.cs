using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class AddressRepo : BaseRepo<AddressEntity>
{
    private readonly DataContext _context;

    public AddressRepo(DataContext context) : base(context)
    {
        _context = context;
    }
}
