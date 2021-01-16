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
        public File GetPathById(Guid id) => _context.File.FirstOrDefault(x => x.Id == id);

        public void UpdateProposal(File file)
        {
            //nothing
        }
        public void SavePath(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(File.Path));
            var fileModel = new File
            {
                Id = Guid.NewGuid(),
                Path = path,
            };
            _context.Add(fileModel);
        }
        public bool SaveChanges() => (_context.SaveChanges() >= 0);
    }
}
