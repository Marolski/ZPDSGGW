using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Database;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Commands
{
    public class ThesisTopicCommands : IRepositoryThesisTopic
    {
        private readonly ZPDSGGWContext _context;

        public ThesisTopicCommands(ZPDSGGWContext context)
        {
            _context = context;
        }

        public void CreateTopic(ThesisTopic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));
            _context.Add(topic);
        }

        public IEnumerable<User> GetPromoters()
        {
            var topics = _context.ThesisTopics.ToList();
            if (topics == null)
                throw new ArgumentNullException(nameof(topics));
            var promoterList = new List<User>();
            foreach (var topic in topics)
            {
                promoterList.Add(new User
                {
                    Name = topic.NamePromoter,
                    Surname = topic.SurnamePromoter,
                });
            }
            return promoterList;

        }

        public ThesisTopic GetTopicById(Guid id)=> _context.ThesisTopics.FirstOrDefault(x => x.Id == id);

        public IEnumerable<string> GetTopics()
        {
            var topics = _context.ThesisTopics.ToList();
            if (topics == null)
                throw new ArgumentNullException(nameof(topics));
            var topicList = new List<string>();
            foreach (var topic in topics)
            {
                topicList.Add(topic.Topic);
            }
            return topicList;
        }

        public IEnumerable<string> GetTopicsFromPromoter(string promoterName, string promoterSurname)
        {
            var topics = _context.ThesisTopics.Where(x => x.NamePromoter == promoterName && x.SurnamePromoter == promoterSurname).ToList();
            if (topics == null)
                throw new ArgumentNullException();
            var topicList = new List<string>();
            foreach (var topic in topics)
            {
                topicList.Add(topic.Topic);
            }
            return topicList;
        }

        public bool SaveChanges() => (_context.SaveChanges() >= 0);

        public void UpdateTopic(ThesisTopic topic)
        {
            //nothing
        }
    }
}
