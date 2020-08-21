using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil Water", Platform = "Ketle & Pan" },
                new Command { Id = 2, HowTo = "Cut Bread", Line = "Get a knife", Platform = "Knife & chopping board" },
                new Command { Id = 3, HowTo = "Make cup of tea", Line = "Place teabag in cu[", Platform = "Ketle & Cup" }
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil Water", Platform = "Ketle & Pan" };
        }
    }
}
