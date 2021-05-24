using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Models;

namespace ZPDSGGW.Repository
{
    public interface IRepositoryFile
    {
        string GetPathById(Guid id);
        void SavePath(File file);
        bool SaveChanges();
        void UpdateProposal(File file);
        IEnumerable<File> GetFilesById(Guid id);
        File GetFileById(Guid id);
        void DeleteFile(Guid id);
    }
}
