﻿using Domain.Contracts;
using Domain.Entities;
using Infrastructure.DAO.ORM.Common.Base;
using System;
using System.Collections.Generic;

namespace Infrastructure.DAO.ORM.Repositories
{
    public class PresencaRepositoryEF : RepositoryBaseEF<Presenca>, IPresencaRepository
    {
    }
}