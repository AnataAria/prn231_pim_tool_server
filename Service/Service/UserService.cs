using DataAccessLayer;
using DataAccessLayer.BusinessObject;
using DataAccessLayer.Repository;

namespace Service.Service;

public class UserService(BaseRepository<User, PIMDatabaseContext> userRepository) {
    private readonly BaseRepository<User, PIMDatabaseContext> _repository = userRepository;
}