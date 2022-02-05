using APIBasicAuthentication.Entities;

namespace APIBasicAuthentication.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string name, string password);
        Task<IEnumerable<User>> GetUsers();
    }
    public class UserService : IUserService
    {
        private List<User> _users = new()
        {
            new User(){ Id = 1, FirstName="test", LastName="test", UserName="test", Passoword="test"}
        };

        public async Task<User> Authenticate(string name, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.UserName == name && x.Passoword == password));

            if (user is null) { return null; }
            
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await Task.Run(()=> _users);
        }
    }
}
