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

        bool ICommanderRepo.SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}
