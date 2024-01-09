using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FineMusicAPI.Models;

public partial class DB : DbContext
{
    public DB()
    {
    }

    public DB(DbContextOptions<DB> options)
        : base(options)
    {
    }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<FollowedList> FollowedLists { get; set; }

    public virtual DbSet<Music> Musics { get; set; }

    public virtual DbSet<MusicList> MusicLists { get; set; }

    public virtual DbSet<MusicOfList> MusicOfLists { get; set; }

    public virtual DbSet<Singer> Singers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Chinese_PRC_CI_AS");

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.ToTable("Collection");

            entity.Property(e => e.Cover)
                .HasMaxLength(3000)
                .IsUnicode(false);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.Singer).WithMany(p => p.Collections)
                .HasForeignKey(d => d.SingerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Collection_Singer");
        });

        modelBuilder.Entity<FollowedList>(entity =>
        {
            entity.ToTable("FollowedList");

            entity.Property(e => e.CreateTime).HasColumnType("datetime");

            entity.HasOne(d => d.MusicList).WithMany(p => p.FollowedLists)
                .HasForeignKey(d => d.MusicListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FollowedList_MusicOfList");

            entity.HasOne(d => d.User).WithMany(p => p.FollowedLists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FollowedList_User");
        });

        modelBuilder.Entity<Music>(entity =>
        {
            entity.ToTable("Music");

            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(3000)
                .IsUnicode(false);
            entity.Property(e => e.FileSrc)
                .HasMaxLength(3000)
                .IsUnicode(false);
            entity.Property(e => e.LrcFile)
                .HasMaxLength(3000)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.Collection).WithMany(p => p.Musics)
                .HasForeignKey(d => d.CollectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Music_Collection");
        });

        modelBuilder.Entity<MusicList>(entity =>
        {
            entity.ToTable("MusicList");

            entity.Property(e => e.Cover)
                .HasMaxLength(3000)
                .IsUnicode(false);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(3000)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.MusicLists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MusicList_User");
        });

        modelBuilder.Entity<MusicOfList>(entity =>
        {
            entity.ToTable("MusicOfList");

            entity.Property(e => e.AddedTime).HasColumnType("datetime");

            entity.HasOne(d => d.Music).WithMany(p => p.MusicOfLists)
                .HasForeignKey(d => d.MusicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MusicOfList_Music");

            entity.HasOne(d => d.MusicList).WithMany(p => p.MusicOfLists)
                .HasForeignKey(d => d.MusicListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MusicOfList_MusicList");
        });

        modelBuilder.Entity<Singer>(entity =>
        {
            entity.ToTable("Singer");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Photo)
                .HasMaxLength(3000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nicename)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Photo)
                .HasMaxLength(3000)
                .IsUnicode(false);
            entity.Property(e => e.Slogan)
                .HasMaxLength(300)
                .IsUnicode(false);
        });
    }
}