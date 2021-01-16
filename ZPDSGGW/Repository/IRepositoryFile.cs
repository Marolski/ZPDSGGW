using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Models;

namespace ZPDSGGW.Repository
{
    public interface IRepositoryFile
    {
        File GetPathById(Guid id);
        void SavePath(string path);
        bool SaveChanges();
        void UpdateProposal(File file);
    }
}
