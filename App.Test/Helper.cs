using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using App.Domain.Entities;

namespace App.Test
{
    internal class Helper
    {
        const string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=test;Integrated Security=SSPI;";

        internal static void ClearTable()
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            connection.Execute("DELETE FROM Application");
        }

        internal static void InitTable()
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            var listApps = SetListApps();

            foreach (var app in listApps)
            {
                connection.Execute("" +
                    "INSERT INTO Application " +
                        "(Url, " +
                        "PathLocal, " +
                        "DebuggingMode) " +
                    "VALUES " +
                        "(@Url, " +
                        "@PathLocal, " +
                        "@DebuggingMode)", app);
            }
        }

        private static List<Domain.Entities.App> SetListApps()
        {
            var listApp = new List<Domain.Entities.App>();
            listApp.Add(new Domain.Entities.App
            {
                Url = @"www.google.com",
                PathLocal = @"c:\programas\google",
                DebuggingMode = false
            });

            listApp.Add(new Domain.Entities.App
            {
                Url = @"www.facebook.com",
                PathLocal = @"c:\programas\facebook",
                DebuggingMode = true
            });

            listApp.Add(new Domain.Entities.App
            {
                Url = @"www.instagram.com",
                PathLocal = @"c:\programas\instagram",
                DebuggingMode = true
            });

            listApp.Add(new Domain.Entities.App
            {
                Url = @"www.twitch.tv",
                PathLocal = @"c:\programas\twitch",
                DebuggingMode = false
            });

            return listApp;
        }
    }
}
