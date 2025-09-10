using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Repositories.Implementation;

namespace Infrastructure.Repositories.Interface
{
    public interface ISubmissionRepo : IGenericRepo<Submission>
    {
        Task<List<Submission>> GetByUserId(int userId);
        Task<List<Submission>> GetByProblemId(int problemId);
    }
}
