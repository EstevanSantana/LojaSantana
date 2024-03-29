﻿using LS.Domain.Core.Data;
using System;
using System.Threading.Tasks;
using LS.Infra.Data.Read.Interface;

namespace LS.Infra.Data.Read.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IReadContext _context;

        public UnitOfWork(IReadContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            var changeAmount = await _context.SaveChanges();

            return changeAmount > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
