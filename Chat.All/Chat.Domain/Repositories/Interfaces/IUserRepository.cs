using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.EntityModel;

namespace Chat.Domain.Repositories.Interfaces
{
    public interface IUserRepository
    {
        int AddUser(User userModel);
    }
}
