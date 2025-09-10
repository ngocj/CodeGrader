using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ResultPattern;
using Infrastructure.Repositories.Interface;

namespace Infrastructure.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        Task<Result<int>> SaveChangesAsync();
        Task<Result> BeginTransaction();
        Task<Result> CommitTransaction();
        Task<Result> RollBackTransaction();

        IProblemStatsRepo ProblemStats { get; }
        ISubmissionRepo Submission { get; }
        IUserProgressRepo UserProgress { get; }
    }
}
