using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsultaMedica.Core.SeedWork
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();

    }
}
