using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class AuthRepo : BaseRepo<AuthEntity>
{
    private readonly DataContext _context;

    public AuthRepo(DataContext context) : base(context)
    {
        _context = context;
    }
}
