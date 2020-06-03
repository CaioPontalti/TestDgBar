using Clearsale.Domain;
using Clearsale.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Clearsale.Infra.Context
{
    public class DataContext : IDisposable
    {
        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public SqlConnection Connection { get; set; }

        public SqlConnection OpenConn()
        {
            Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            Connection.Open();

            return Connection;
        }


        public void Dispose()
        {
            if (Connection != null)
                if (Connection.State != ConnectionState.Closed)
                    Connection.Close();
        }
    }
}
