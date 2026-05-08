using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using fixmycity.data;
using fixmycity.models;
using fixmycity.Services;

namespace fixmycity.Repositories;

public class UserRepository(AppDbContext dbContext)
{
    public async Task<T?> GetByIdAsync<T>(
        string id,
        Expression<Func<User, T>> selector)
    {
        return await dbContext.Users
            .Where(u => u.Id == id)
            .Select(selector)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
    }
}