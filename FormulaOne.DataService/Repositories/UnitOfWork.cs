﻿using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.DataService.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public readonly AppDbContext _context;
        public IDriverRepository Drivers {  get; }

        public IAchievementRepository Achievements {  get; }

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("Logs");
            Drivers = new DriverRepository(context,logger);
            Achievements = new AchievementRepository(context,logger);
        }

        public async Task<bool> SaveAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
