using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Database;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Commands
{
    public class MessageCommands : IRepositoryMessage
    {
        private readonly ZPDSGGWContext _context;
        public MessageCommands(ZPDSGGWContext context)
        {
            _context = context;
        }

        public Message GetMessageById(Guid id)
        {
            var message = _context.Message.FirstOrDefault(x => x.Id == id);
            if (message == null)
                throw new KeyNotFoundException();
            return message;
        }

        public IEnumerable<Message> GetMessagesByUserId(Guid id)
        {
            var messages = _context.Message.Where(x => x.SendTo == id);
            return messages;
        }

        public string GetPathFromMessage(Guid id)
        {
            var file = _context.Message.FirstOrDefault(x => x.SendTo == id);
            if (file == null)
                throw new KeyNotFoundException();
            return file.Path;
        }

        public bool SaveChanges() => (_context.SaveChanges() >= 0);

        public void SaveMessage(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            _context.Message.Add(message);
        }
    }
}
