using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Interfaces
{
    public interface IRepository
    {
        Task<List<Domain.Entities.App>> GetAll();
        Task<Domain.Entities.App> GetID(int id);
        Task<bool> Post(Entities.App entidade);
        Task<bool> Path(int id, Entities.App app);
        Task<bool> Delete(int id);
        Task<bool> Put(int application, Entities.App app);
    }
}
