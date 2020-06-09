using Assessment_Asp_Net.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment_Asp_Net.Data
{
    public class PessoaContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public PessoaContext(DbContextOptions<PessoaContext> options) : base(options)
        {

        }
    }
}
