using System.Collections.Generic;
using Convey.CQRS.Events;
using Fls.Supervision.Api.Commands;

namespace Fls.Supervision.Api.Database 
{
    interface IRepository
    {
        IEvent GetById(int id);
        List<IEvent> GetAll();
        int Add(IEvent obj);
        void DeleteById(int id);
        void DeleteAll();   
    }
}