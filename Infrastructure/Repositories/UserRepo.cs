using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class UserRepo : BaseRepo<UserEntity>
{
    private readonly DataContext _context;

    public UserRepo(DataContext context) : base(context)
    {
        _context = context;
    }
}
