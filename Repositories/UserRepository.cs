using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using fixmycity.data;
using fixmycity.models;
using fixmycity.Services;

namespace fixmycity.Repositories;

public class UserRepository(AppDbContext db)
{
    public async Task<T?> GetByIdAsync<T>(
        string id,
        Expression<Func<User, T>> selector)
    {
        return await db.Users
            .AsNoTracking()
            .Where(u => u.Id == id)
            .Select(selector)
            .FirstOrDefaultAsync();
    }

    public async Task RegisterUserAsync(User user)
    {
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
    }
}