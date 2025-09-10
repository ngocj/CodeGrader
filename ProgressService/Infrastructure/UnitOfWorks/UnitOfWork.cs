using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ResultPattern;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Implementation;
using Infrastructure.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ProgressContext _progressContext;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(ProgressContext progressContext)
        {
            _progressContext = progressContext;
        }

        public IProblemStatsRepo _problemStatsRepo;
        public IProblemStatsRepo ProblemStats => _problemStatsRepo ??= new ProblemStatsRepo(_progressContext);

        public ISubmissionRepo _submissionRepo;

        public ISubmissionRepo Submission => _submissionRepo ??= new SubmisstionRepo(_progressContext);

        public IUserProgressRepo _userProgressRepo;
        public IUserProgressRepo UserProgress => _userProgressRepo ??= new UserProgressRepo(_progressContext);


        public async Task<Result> BeginTransaction()
        {
            try
            {
                _transaction = await _progressContext.Database.BeginTransactionAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }

        }

        public async Task<Result> CommitTransaction()
        {
            try
            {
                _transaction = await _progressContext.Database.BeginTransactionAsync();
                return Result.Success();
            }
            catch(Exception ex) 
            {
                return Result.Failure(ex.Message, null);
            }
        }

        public void Dispose()
        {
            _progressContext.Dispose();
            _transaction?.Dispose();
        }

        public async Task<Result> RollBackTransaction()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result<int>> SaveChangesAsync()
        {
            try
            {
                var count = await _progressContext.SaveChangesAsync();
                return Result<int>.Success("Success", count);

            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                        return Result<int>.Failure("Duplicate primary key");
                    default:
                        return Result<int>.Failure(ex.Message);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (ex.Message.Contains("actually affected 0"))
                {
                    return Result<int>.Success("Success", 0);
                }
                return Result<int>.Failure(ex.Message);
            }
            catch(Exception ex)
            {
                return Result<int>.Failure(ex.Message);
            }
        }
    }
}
