using Connection.Commons;
using Connection.Dispatchers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Connection.DataBase
{
    public class ClientDataBase : DbContext
    {
        #region Public Variables

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Sprites> Sprites { get; set; }
        public DbSet<Other> Other { get; set; }
        public DbSet<Home> Home { get; set; }
        public DbSet<AbilityElement> AbilityElement { get; set; }
        public DbSet<TypeClass> TypeClass { get; set; }
        public DbSet<StatElement> StatElement { get; set; }
        public DbSet<StatStat> StatStat { get; set; }
        public DbSet<TypeElement> TypeElement { get; set; }
        public DbSet<Types> Types { get; set; }
        public DbSet<PokemonElement> PokemonElement { get; set; }
        public DbSet<PokemonPokemon> PokemonPokemon { get; set; }
        
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite($"DataSource={GlobalParameters.DataBasePath}");

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Pokemon>()
        //        .HasOne(p => p.Sprites);

        //    modelBuilder.Entity<Sprites>()
        //        .HasOne(p => p.Other);

        //    modelBuilder.Entity<Other>()
        //        .HasOne(p => p.Home);
        //}
    }

}
