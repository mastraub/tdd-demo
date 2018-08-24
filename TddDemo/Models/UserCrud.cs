using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class UserCrud
    {
        private readonly MyDbContext _dbContext;

        public UserCrud(MyDbContext dbContext) => _dbContext = dbContext;

        public IEnumerable<User> GetAll() => _dbContext.Users.ToList();

        public User Create(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user; //
        }

        public User GetById(int id)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id == id);
        }
    }
}