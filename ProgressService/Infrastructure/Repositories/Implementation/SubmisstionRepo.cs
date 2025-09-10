using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementation
{
    public class SubmisstionRepo : GenericRepo<Submission>, ISubmissionRepo
    {

        protected readonly ProgressContext _progressContext;

        public SubmisstionRepo(ProgressContext context) : base(context)
        {
            _progressContext = context;
        }

        public async Task<List<Submission>> GetByProblemId(int problemId)
        {
           return await _progressContext.Submission.Where(s => s.ProblemId == problemId).ToListAsync();
        }

        public async Task<List<Submission>> GetByUserId(int userId)
        {
            return await _progressContext.Submission.Where(s => s.UserId == userId).ToListAsync();
        }   
    }
}
