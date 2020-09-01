using Commander.Models;
using Commander.Models.DB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;

        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Command.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Command.FirstOrDefault(p => p.Id == id);
        }

        void ICommanderRepo.CreateCommand(Command cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException(nameof(cmd));

            _context.Command.Add(cmd);
        }

        void ICommanderRepo.UpdateCommand(Command cmd)
        {
            // Nothing
        }

        bool ICommanderRepo.SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        void ICommanderRepo.DeleteCommand(Command cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException(nameof(cmd));

            _context.Command.Remove(cmd);
        }






        /// <summary>  
        /// Get product by ID store procedure method.  
        /// </summary>  
        /// <param name="productId">Product ID value parameter</param>  
        /// <returns>Returns - List of product by ID</returns>  
        public async Task<List<SpGetProductByID>> GetProductByIDAsync(int productId)
        {
            // Initialization.  
            List<SpGetProductByID> lst = new List<SpGetProductByID>();

            try
            {
                // Settings.  
                SqlParameter usernameParam = new SqlParameter("@product_ID", productId.ToString() ?? (object)DBNull.Value);

                // Processing.  
                string sqlQuery = "EXEC [dbo].[GetProductByID] " +
                                    "@product_ID";

                lst = await _context.Query<SpGetProductByID>().FromSqlRaw(sqlQuery, usernameParam).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
            return lst;
        }

        /// <summary
        /// Get Products whose price is greater than equal to 1000 store procedure method.
        /// </summary>
        /// <returns>Returns - List of products whose price is greater than equal to 1000
        /// </returns>
        public async Task<List<SpGetProductByPriceGreaterThan1000>> GetProductByPriceGreaterThan1000Async()
        {
            // Initialization.  
            List<SpGetProductByPriceGreaterThan1000> lst = new List<SpGetProductByPriceGreaterThan1000>();

            try
            {
                // Processing.  
                string sqlQuery = "EXEC [dbo].[GetProductByPriceGreaterThan1000] ";

                lst = await _context.Query<SpGetProductByPriceGreaterThan1000>().FromSqlRaw(sqlQuery).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
            return lst;
        }
    }
}
