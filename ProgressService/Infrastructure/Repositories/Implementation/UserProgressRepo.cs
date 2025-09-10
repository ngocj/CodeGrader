using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Interface;

namespace Infrastructure.Repositories.Implementation
{
    public class UserProgressRepo: GenericRepo<UserProgress>, IUserProgressRepo
    {
        protected readonly ProgressContext _progressContext;

        public UserProgressRepo(ProgressContext context) : base(context)
        {
            _progressContext = context;
        }
    }
}
