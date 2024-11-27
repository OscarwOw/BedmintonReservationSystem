﻿using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        List<UserEntity> GetUserEntities();
        UserEntity? GetUserEntityById(int id);
        UserEntity? GetUserEntityByName(string name);
    }
}
