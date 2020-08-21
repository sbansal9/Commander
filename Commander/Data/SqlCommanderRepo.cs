using Commander.Models;
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
            //var commands = new List<Command>
            //{
            //    new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil Water", Platform = "Ketle & Pan" },
            //    new Command { Id = 2, HowTo = "Cut Bread", Line = "Get a knife", Platform = "Knife & chopping board" },
            //    new Command { Id = 3, HowTo = "Make cup of tea", Line = "Place teabag in cu[", Platform = "Ketle & Cup" }
            //};

            //return commands;
            return _context.Command.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Command.FirstOrDefault(p => p.Id == id);
        }
    }
}
