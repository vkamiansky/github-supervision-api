using Fls.Supervision.Api.Commands;
using Convey.CQRS.Events;
using System.Collections.Generic;

namespace Fls.Supervision.Api.Database
{

    class GithubEventRepository : IRepository
    {
        public int Add(IEvent obj)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<IEvent> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IEvent GetById(int id)
        {
            throw new System.NotImplementedException();
        }
        /*
        bool InsertInDB(IEvent event)
        {
            
            return true;
        }

        IEvent GetFromDB(Dictionary<string,string> query)
        {
            
            return null;
        }*/
        
    }

}
