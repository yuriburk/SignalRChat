using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SignalRChat.Domain.Features.Users
{
    public interface IUserRepository
    {
        Task<Tuple<bool, List<string>>> AddUser(User user);
        Task<Tuple<User, List<string>>> GetUser(Expression<Func<User, bool>> predicate);
    }
}
