using App.Domain.Interfaces;
using System;
using System.Collections.Generic;
using App.Domain.Entities;
using System.Threading.Tasks;

namespace App.Application
{
    public class Application : IApplication
    {
        private readonly IRepository _repository;

        public Application(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Domain.Entities.App>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Domain.Entities.App> GetID(int application)
        {
            return await _repository.GetID(application);
        }

        public async Task<bool> Post(Domain.Entities.App app)
        {
            Validate(app);
            return await _repository.Post(app);
        }

        public async Task<bool> Path(int application, Domain.Entities.App app)
        {
            Validate(app);
            return await _repository.Path(application, app);
        }

        public async Task<bool> Delete(int application)
        {
            return await _repository.Delete(application);
        }

        public async Task<bool> Put(int application, Domain.Entities.App app)
        {
            Validate(app);
            return await _repository.Put(application, app);
        }

        private void Validate(Domain.Entities.App app)
        {
            if (app == null)
                throw new ArgumentException("Entitie app is null");
            if (string.IsNullOrEmpty(app.Url))
                throw new ArgumentException("App.Url is null");
            if (string.IsNullOrEmpty(app.PathLocal))
                throw new ArgumentException("App.PathLocal is null");
            if (app.DebuggingMode == null)
                throw new ArgumentException("App.DebuggingMode is null");
        }

        public Task SavePatch()
        {
            throw new NotImplementedException();
        }
    }
}
