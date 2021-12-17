using App.Domain.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data
{
    public class Repository : IRepository
    {
        const string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=test;Integrated Security=SSPI;";

        public async Task<List<Domain.Entities.App>> GetAll()
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            var listApp = await connection.QueryAsync<Domain.Entities.App>(
                "SELECT *" +
                " FROM Application");

            return listApp.AsList();
        }
        public async Task<Domain.Entities.App> GetID(int id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync<Domain.Entities.App>(
                "SELECT * FROM Application " +
                "WHERE Application = @Application",
                new { @Application = id});
        }

        public async Task<bool> Post(Domain.Entities.App app)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            int rowsAffected = await connection.ExecuteAsync(@"
                INSERT INTO Application
                    (Url
                    ,PathLocal
                    ,DebuggingMode)
                VALUES
                    (@Url
                    ,@PathLocal
                    ,@DebuggingMode)", app);

            return rowsAffected > 0;
        }

        public async Task<bool> Path(int application, Domain.Entities.App app)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            SqlBuilder builder = new SqlBuilder();

            if (app.Application > 0)
                builder.Set($"Application = @{nameof(app.Application)}", new { app.Application });

            if (!string.IsNullOrWhiteSpace(app?.Url))
                builder.Set($"Url = @{nameof(app.Url)}", new { app.Url });

            if (!string.IsNullOrWhiteSpace(app?.PathLocal))
                builder.Set($"PathLocal = @{nameof(app.PathLocal)}", new { app.PathLocal });

            if (app?.DebuggingMode != null)
                builder.Set($"[DebuggingMode] = @{nameof(app.DebuggingMode)}", new { app.DebuggingMode });

            builder.Where($"[Application] = @Application", new { @Application = application });


            SqlBuilder.Template template = builder.AddTemplate(@"
                UPDATE Application
                SET 
                /**SETTS**/");

            var rowsAffected = await connection.ExecuteAsync(template.RawSql, template.Parameters);
            return rowsAffected > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            int rowsAffected = await connection.ExecuteAsync(@"
                DELETE Application 
                WHERE Application = @Application",
                new { @Application = id});

            return rowsAffected > 0;
        }

        public async Task<bool> Put(int application, Domain.Entities.App app)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            string updateQuery = @"
                UPDATE Application SET 
                    Url = @Url, 
                    PathLocal = @PathLocal, 
                    DebuggingMode = @DebuggingMode 
                WHERE
                    Application = @Application";

            var rowsAffected = await connection.ExecuteAsync(updateQuery, new
            {
                app.Url,
                app.PathLocal,
                app.DebuggingMode,
                application
            });

            return rowsAffected > 0;
        }
    }
}
