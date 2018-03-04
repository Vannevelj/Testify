using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Testify.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(int userId);
    }

    public class User
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
    }
}
