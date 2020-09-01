using Commander.Models;
using Commander.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public interface ICommanderRepo
    {
        bool SaveChanges();

        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);
        void CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
        void DeleteCommand(Command cmd);


        public Task<List<SpGetProductByID>> GetProductByIDAsync(int productId);
        public Task<List<SpGetProductByPriceGreaterThan1000>> GetProductByPriceGreaterThan1000Async();
    }
}
