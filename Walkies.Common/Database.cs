﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Walkies.Common
{
    using System.Data;
    using System.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using Dapper;
    using Npgsql;
    using MySql.Data.MySqlClient;

    public class Database
    {
        private IConfiguration _cfg;
        public Database(IConfiguration cfg)
        {
            _cfg = cfg;
        }

        public IDbConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(_cfg.GetConnectionString("DefaultDb"));
            conn.Open();
            return conn;
        }

        public async Task<IDbConnection> GetConnectionAsync()
        {
            SqlConnection conn = new SqlConnection(_cfg.GetConnectionString("DefaultDb"));
            await conn.OpenAsync();
            return conn;
        }

        public IEnumerable<dynamic> Query(string sql, object param = null)
        {
            using (var db = this.GetConnection())
            {
                return db.Query(sql, param: param);
            }
        }

        public async Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null)
        {
            using (var db = await this.GetConnectionAsync())
            {
                return await db.QueryAsync(sql, param: param);
            }
        }

        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using (var db = this.GetConnection())
            {
                return db.Query<T>(sql, param: param);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            using (var db = await this.GetConnectionAsync())
            {
                return await db.QueryAsync<T>(sql, param: param);
            }
        }

        public void Execute(string sql, object param = null)
        {
            using (var db = this.GetConnection())
            {
                db.Execute(sql, param: param);
            }
        }

        public async Task ExecuteAsync(string sql, object param = null)
        {
            using (var db = await this.GetConnectionAsync())
            {
                await db.ExecuteAsync(sql, param: param);
            }
        }
    }
}
