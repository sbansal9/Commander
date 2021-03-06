﻿using Commander.Models;
using Commander.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
        {

        }

        public DbSet<Command> Command { get; set; }
        public DbSet<SpGetProductByPriceGreaterThan1000> Products { get; set; }
        public DbSet<SpGetProductByID> Product { get; set; }
    }
}
