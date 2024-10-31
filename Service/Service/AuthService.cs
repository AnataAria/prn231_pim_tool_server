using DataAccessLayer;
using DataAccessLayer.BusinessObject;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class AuthService (BaseRepository<User,  PIMDatabaseContext> userRepository)
    {
        private readonly BaseRepository<User, PIMDatabaseContext> userRepo = userRepository;
        
    }
}
