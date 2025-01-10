using Microsoft.EntityFrameworkCore;
using todolist.models;


namespace todolist.data

{
    public class todocontext : DbContext    
    {
        public todocontext(DbContextOptions<todocontext> options) : base(options) { }

        public DbSet<todo> TodoItems { get; set; }
    }
}
