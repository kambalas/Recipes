using Microsoft.Extensions.Logging;
using PoS.Infrastructure.Repositories;
using RecipesAPI.Models;
using RecipesAPI.Repositories.Interfaces;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecipesAPI.Repositories
{
    public class UserRepositoryDecorator : IUserRepository
    {
        private readonly IUserRepository _decoratedRepository;
        private readonly ILogger<UserRepositoryDecorator> _logger;

        public UserRepositoryDecorator(IUserRepository decoratedRepository, ILogger<UserRepositoryDecorator> logger)
        {
            _decoratedRepository = decoratedRepository;
            _logger = logger;
        }

        public int Count(Expression<Func<User, bool>>? filter = null)
        {
            _logger.LogInformation("Count method called");
            var result = _decoratedRepository.Count(filter);
            _logger.LogInformation($"Count result: {result}");
            return result;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            _logger.LogInformation($"DeleteAsync method called with id: {id}");
            var result = await _decoratedRepository.DeleteAsync(id);
            _logger.LogInformation($"DeleteAsync result: {result}");
            return result;
        }

        public async Task DeleteAsync(User entity)
        {
            _logger.LogInformation($"DeleteAsync method called with entity: {entity}");
            await _decoratedRepository.DeleteAsync(entity);
            _logger.LogInformation("DeleteAsync completed");
        }

        public async Task<bool> Exists(Expression<Func<User, bool>> filter)
        {
            _logger.LogInformation("Exists method called");
            var result = await _decoratedRepository.Exists(filter);
            _logger.LogInformation($"Exists result: {result}");
            return result;
        }

        public async Task<IEnumerable<User>> GetAsync(Expression<Func<User, bool>>? filter = null, Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null, int? itemsToSkip = null, int? itemsToTake = null)
        {
            _logger.LogInformation("GetAsync method called");
            var result = await _decoratedRepository.GetAsync(filter, orderBy, itemsToSkip, itemsToTake);
            _logger.LogInformation($"GetAsync result count: {result.Count()}");
            return result;
        }

        public async Task<User> GetByIdAsync(object id)
        {
            _logger.LogInformation($"GetByIdAsync method called with id: {id}");
            var result = await _decoratedRepository.GetByIdAsync(id);
            _logger.LogInformation($"GetByIdAsync result: {result}");
            return result;
        }

        public async Task<User?> GetFirstAsync(Expression<Func<User, bool>>? filter = null)
        {
            _logger.LogInformation("GetFirstAsync method called");
            var result = await _decoratedRepository.GetFirstAsync(filter);
            _logger.LogInformation($"GetFirstAsync result: {result}");
            return result;
        }

        public async Task<User> InsertAsync(User entity)
        {
            _logger.LogInformation($"InsertAsync method called with entity: {entity}");
            var result = await _decoratedRepository.InsertAsync(entity);
            _logger.LogInformation($"InsertAsync result: {result}");
            return result;
        }

        public async Task<User> UpdateAsync(User entity)
        {
            _logger.LogInformation($"UpdateAsync method called with entity: {entity}");
            var result = await _decoratedRepository.UpdateAsync(entity);
            _logger.LogInformation($"UpdateAsync result: {result}");
            return result;
        }
    }
}
