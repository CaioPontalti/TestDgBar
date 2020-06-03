using Clearsale.Domain;
using Clearsale.Domain.Commands.User;
using Clearsale.Domain.Interfaces;
using Clearsale.Domain.Models;
using Clearsale.Infra.Context;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clearsale.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {

            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var result = await sqlConn.QueryAsync<User>(
                        @"SELECT 
	                    Id,
	                    Email
                    FROM [user]");

                    return await Task.FromResult(result);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public async Task<User> GetUser(User user)
        {

            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var result = await sqlConn.QueryFirstOrDefaultAsync<User>(
                        @"SELECT 
	                        Id,
	                        Email
                        FROM [user]
                        WHERE Email = @email
                        AND Password = @password", new { @email = user.Email, @password = user.Password });

                    return await Task.FromResult(result);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task Save(User user)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    await sqlConn.ExecuteScalarAsync<User>(
                    @"INSERT INTO [user] VALUES (@Id, @email, @password)",
                        new { @Id = user.Id, @email = user.Email, @password = user.Password });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}
