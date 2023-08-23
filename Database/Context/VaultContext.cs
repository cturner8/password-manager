namespace Database.Context;

using Database.Models;
using Microsoft.EntityFrameworkCore;

public class VaultContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Vault> Vaults { get; set; }
    public DbSet<VaultLogin> VaultLogins { get; set; }
    public DbSet<VaultNote> VaultNotes { get; set; }


    public string DbPath { get; }

    public VaultContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "vault.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
