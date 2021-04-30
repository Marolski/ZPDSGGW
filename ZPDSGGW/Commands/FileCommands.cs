using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Database;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Commands
{
    public class FileCommands : IRepositoryFile
    {
        private readonly ZPDSGGWContext _context;

        public FileCommands(ZPDSGGWContext context)
        {
            _context = context;
        }
        public string GetPathById(Guid id)
        {
            var file = _context.File.FirstOrDefault(x => x.UserId == id);
            if (file == null)
                throw new KeyNotFoundException();
            return file.Path;
            
        }

        public void UpdateProposal(File file)
        {
            //nothing
        }
        public void SavePath(File file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));
            _context.Add(file);
        }
        public bool SaveChanges() => (_context.SaveChanges() >= 0);

        public File GetFileById(Guid id) => _context.File.FirstOrDefault(x => x.UserId == id);
    }
}
