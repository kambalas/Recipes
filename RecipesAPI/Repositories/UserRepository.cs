using Microsoft.EntityFrameworkCore;
using PoS.Infrastructure.Repositories;
using RecipesAPI.Models;
using RecipesAPI.Repositories.Interfaces;
using System.Linq.Expressions;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace RecipesAPI.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public override async Task<User> InsertAsync(User user)
        {
            DbSet.Add(user);
            await Context.Instance.SaveChangesAsync();

            return user;
        }

        public override async Task<User> GetByIdAsync(object id)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity == null)
                throw new NotImplementedException();
            else return entity;
        }

    }
}