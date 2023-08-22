﻿namespace PasswordManager.Context;

using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;

public class VaultContext : DbContext
{
    public DbSet<User> Users { get; set; }

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
