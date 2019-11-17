using SignalRChat.Domain.Results;
using SignalRChat.Infra.Results;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SignalRChat.Domain.Features.Users
{
    public interface IUserRepository
    {
        Task<Result<bool, Error>> AddUser(User user);
        Task<Result<User, Error>> GetUser(Expression<Func<User, bool>> predicate);
    }
}
