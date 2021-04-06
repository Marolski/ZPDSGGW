using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Models;

namespace ZPDSGGW.Repository
{
    public interface IRepositoryThesisTopic
    {
        IEnumerable<ThesisTopic> GetTopics();
        IEnumerable<string> GetTopicsFromPromoter(Guid id);
        ThesisTopic GetTopicById(Guid id);
        void CreateTopic(ThesisTopic topic);
        bool SaveChanges();
        void UpdateTopic(ThesisTopic topic);
    }
}
