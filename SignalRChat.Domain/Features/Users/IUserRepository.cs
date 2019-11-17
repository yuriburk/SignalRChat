using SignalRChat.Infra.Results;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SignalRChat.Domain.Features.Users
{
    public interface IUserRepository
    {
        Task<Result<User, Exception>> AddUser(User user);
        Task<Result<User, Exception>> GetUser(Expression<Func<User, bool>> predicate);
    }
}
