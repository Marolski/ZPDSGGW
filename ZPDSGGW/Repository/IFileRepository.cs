using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Models;

namespace ZPDSGGW.Repository
{
    public interface IFileRepository
    {
        Task AddAsync(File file);
        Task<File> GetFileByIdAsync(Guid id);
        void Update(File file);
    }
}
