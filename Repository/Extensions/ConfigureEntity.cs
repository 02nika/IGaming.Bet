using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Extensions;

public static class ConfigureEntity
{
    public static void UserConfig(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<User>()
            .Property(p => p.Username)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<User>()
            .Property(p => p.PasswordHash)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Bets)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);
    }

    public static void BalanceConfig(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Balance>()
            .Property(p => p.Amount)
            .IsRequired()
            .HasColumnType("decimal(10, 2)");

        modelBuilder.Entity<Balance>()
            .HasOne(p => p.User)
            .WithOne(c => c.Balance)
            .HasForeignKey<User>(c => c.BalanceId);
    }
    
    public static void BetConfig(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bet>()
            .Property(p => p.BetAmount)
            .IsRequired()
            .HasColumnType("decimal(10, 2)");
        
        modelBuilder.Entity<Bet>()
            .Property(p => p.WinAmount)
            .IsRequired()
            .HasColumnType("decimal(10, 2)");

        modelBuilder.Entity<Bet>()
            .Property(p => p.Details)
            .IsRequired();
    }
}