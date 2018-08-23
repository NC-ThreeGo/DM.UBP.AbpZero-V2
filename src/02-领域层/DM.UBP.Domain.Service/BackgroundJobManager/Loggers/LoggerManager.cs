//------------------------------------------------------------
// All Rights Reserved , Copyright (C)  
// 版本：1.0
/// <author>
///		<name></name>
///		<date>0001/1/1 0:00:00</date>
/// </author>
//------------------------------------------------------------

using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Domain.Uow;
using Abp.Domain.Services;
using Abp.Domain.Repositories;
using DM.UBP.Domain.Entity.BackgroundJobManager;
using System;

namespace DM.UBP.Domain.Service.BackgroundJobManager.Loggers
{
    /// <summary>
    /// 后台任务系统日志的Domain.Service
    /// <summary>
    public class LoggerManager : DomainService, ILoggerManager
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IRepository<Logger, long> _loggerRepository;

        public LoggerManager(
           IUnitOfWorkManager unitOfWorkManager,
           IRepository<Logger, long> loggerRepository
           )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _loggerRepository = loggerRepository;
        }

        public async Task<List<Logger>> GetAllLoggersAsync()
        {
            var loggers = _loggerRepository.GetAll().OrderBy(p => p.Id);
            return await loggers.ToListAsync();
        }

        public async Task<Logger> GetLoggerByIdAsync(long id)
        {
            return await _loggerRepository.GetAsync(id);
        }

        public async Task<bool> CreateLoggerAsync(Logger logger)
        {
            var entity = await _loggerRepository.InsertAsync(logger);
            return entity != null;
        }

        public async Task<long> CreateLoggerToGetIdAsync(Logger logger)
        {
            var entity = await _loggerRepository.InsertAsync(logger);
            if (entity != null)
                return entity.Id;
            return 0;
        }

        public async Task<bool> UpdateLoggerAsync(Logger logger)
        {
            try
            {
                var entity = await _loggerRepository.UpdateAsync(logger);
                return entity != null;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task DeleteLoggerAsync(Logger logger)
        {
            await _loggerRepository.DeleteAsync(logger);
        }

    }
}
