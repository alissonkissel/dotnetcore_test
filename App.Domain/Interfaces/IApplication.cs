using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Interfaces
{
    public interface IApplication
    {
        Task<List<Entities.App>> GetAll();
        Task<Entities.App> GetID(int application);
        Task<bool> Post(Entities.App app);
        Task<bool> Path(int application, Entities.App app);
        Task<bool> Delete(int application);
        Task SavePatch();
        Task<bool> Put(int application, Entities.App app);
    }
}
