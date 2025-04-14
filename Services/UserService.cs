using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public  class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository= userRepository;
        }

        public async  Task<IList<User>> GetUsers()
        {
           return  (await _userRepository.GetAllAsync()).ToList();
        }

        public async Task<User> LoginUser(string userName)
        {
            User user = await _userRepository.GetUserByName(userName);
            return user;
        }


    }
}
