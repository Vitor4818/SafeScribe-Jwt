using SafeScribe.Data;
using SafeScribe.Model;

namespace SafeScribe.Business
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            return await _repository.GetUserAsync(username, password);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _repository.GetAllUsersAsync();
        }

        // Novo método para cadastrar usuário
        public async Task<User> RegisterUserAsync(string username, string password)
        {
            var existingUser = await _repository.GetByUsernameAsync(username);
            if (existingUser != null)
                throw new Exception("Usuário já existe");

            var user = new User
            {
                Username = username,
                Password = password // ⚠️ Em produção, sempre hash a senha!
            };

            await _repository.AddUserAsync(user);
            return user;
        }
    }
}
