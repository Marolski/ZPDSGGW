using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Models;

namespace ZPDSGGW.Repository
{
    public interface IRepositoryMessage
    {
        IEnumerable<Message> GetMessagesByUserId(Guid id);
        void SaveMessage(Message message);
        Message GetMessageById(Guid id);
        bool SaveChanges();
    }
}
