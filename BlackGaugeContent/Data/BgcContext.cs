﻿using Microsoft.EntityFrameworkCore;

namespace Bgc.Models
{
	public partial class BgcContext : DbContext
	{
		//public virtual DbSet<Blacklist>   Blacklists   { get; set; }
		//public virtual DbSet<Exclusive>   Exclusive    { get; set; }
		public virtual DbSet<Gender>      Genders      { get; set; }
		//public virtual DbSet<MemeComment> MemeComments { get; set; }
		public virtual DbSet<Meme>        Memes        { get; set; }
		public virtual DbSet<MemeRating>  MemeRatings  { get; set; }
		public virtual DbSet<AspUser>     Users        { get; set; }

		public BgcContext(DbContextOptions<BgcContext> options) : base(options) {}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var isSqlServer = Database.IsSqlServer();
			/*
			modelBuilder.Entity<Blacklist>(entity =>
			{
				if(isSqlServer)
					entity.ToTable("Blacklists", "Community");

				entity.Property(e => e.Id)
					.HasColumnName("ID")
					.ValueGeneratedOnAdd();
					//.ValueGeneratedNever();

				entity.Property(e => e.BlacklistedId).HasColumnName("BlacklistedID");

				entity.Property(e => e.Since).HasColumnType("datetime");

				entity.Property(e => e.UserId).HasColumnName("UserID");

				entity.HasOne(d => d.Blacklisted)
					.WithMany(p => p.Blacklisted)
					.HasForeignKey(d => d.BlacklistedId)
					.OnDelete(DeleteBehavior.Cascade)
					.HasConstraintName("FK_Blacklists_BlockedUsers");

				entity.HasOne(d => d.User)
					.WithMany(p => p.BlacklistsUser)
					.HasForeignKey(d => d.UserId)
					.OnDelete(DeleteBehavior.Cascade)
					.HasConstraintName("FK_Blacklists_Users");
			});

			modelBuilder.Entity<Exclusive>(entity =>
			{
				if(isSqlServer)
					entity.ToTable("Exclusive", "BGC");

				entity.Property(e => e.Id)
					.HasColumnName("ID")
					.ValueGeneratedOnAdd();
					//.ValueGeneratedNever();

				entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

				entity.Property(e => e.Title)
					.IsRequired()
					.HasColumnType("nchar(128)");

				entity.HasOne(d => d.Owner)
					.WithMany(p => p.Exclusive)
					.HasForeignKey(d => d.OwnerId)
					.HasConstraintName("FK_Exclusive_Users");
			});*/
			
			modelBuilder.Entity<Gender>(entity =>
			{
				if(isSqlServer)
					entity.ToTable("Genders", "Community");

				var pbuilder =
					entity.Property(e => e.Id)
						.HasColumnName("ID")
						.HasColumnType("tinyint");
				if(isSqlServer)
					pbuilder.ValueGeneratedOnAdd();
				else
					pbuilder.ValueGeneratedNever();

				entity.Property(e => e.Description)
					.IsRequired()
					.HasColumnType("nchar(256)");

				entity.Property(e => e.GenderName)
					.IsRequired()
					.HasColumnName("Gender")
					.HasColumnType("nchar(128)");
			});
			/*
			modelBuilder.Entity<MemeComment>(entity =>
			{
				if(isSqlServer)
					entity.ToTable("MemeComments", "Community");

				entity.Property(e => e.Id)
					.HasColumnName("ID")
					.ValueGeneratedOnAdd();
					//.ValueGeneratedNever();

				entity.Property(e => e.Comment)
					.IsRequired()
					.HasColumnType("nchar(256)");

				entity.Property(e => e.MemeId).HasColumnName("MemeID");

				entity.Property(e => e.UserId).HasColumnName("UserID");

				entity.HasOne(d => d.Meme)
					.WithMany(p => p.MemeComments)
					.HasForeignKey(d => d.MemeId)
					.OnDelete(DeleteBehavior.Cascade)
					.HasConstraintName("FK_MemeComments_Memes");

				entity.HasOne(d => d.User)
					.WithMany(p => p.MemeComments)
					.HasForeignKey(d => d.UserId)
					.OnDelete(DeleteBehavior.Cascade)
					.HasConstraintName("FK_MemeComments_Users");
			});*/

			modelBuilder.Entity<Meme>(entity =>
			{
				if(isSqlServer)
					entity.ToTable("Memes", "BGC");

				entity.Property(e => e.Id)
					.HasColumnName("ID")
					.IsRequired()
					.ValueGeneratedOnAdd();
					//.ValueGeneratedNever();

				entity.Property(e => e.AddTime).HasColumnType("datetime");

				entity.Property(e => e.Title)
					.HasMaxLength(256)
					.IsUnicode(false);

				entity.Property(e => e.Base64)
					.IsRequired()
					.HasMaxLength(int.MaxValue)
					.IsUnicode(false);
			});

			modelBuilder.Entity<MemeRating>(entity =>
			{
				if(isSqlServer)
					entity.ToTable("MemeRatings", "BGC");

				entity.Property(e => e.Id)
					.HasColumnName("Id")
					.ValueGeneratedOnAdd();
					//.ValueGeneratedNever();

				entity.Property(e => e.Rating)
					.IsRequired()
					.HasColumnType("tinyint");

				entity.Property(e => e.MemeId).HasColumnName("MemeId");

				entity.Property(e => e.UserId).HasColumnName("UserId");

				entity.HasOne(d => d.Meme)
					.WithMany(p => p.MemeRatings)
					.HasForeignKey(d => d.MemeId)
					.OnDelete(DeleteBehavior.Cascade)
					.HasConstraintName("FK_MemeRatings_Memes");

				entity.HasOne(d => d.User)
					.WithMany(p => p.MemeRatings)
					.HasForeignKey(d => d.UserId)
					.OnDelete(DeleteBehavior.Cascade)
					.HasConstraintName("FK_MemeRatings_Users");
			});
			
			modelBuilder.Entity<AspUser>(entity =>
			{
				if(isSqlServer)
					entity.ToTable("Users", "dbo");

				entity.Property(e => e.Id)
					.ValueGeneratedOnAdd();
					//.ValueGeneratedNever();

				entity.Property(e => e.Respek)
					.HasColumnName("Respek").IsRequired();

				entity.Property(e => e.UserName)
					.HasColumnType("nvarchar(128)")
					.HasColumnName("Name").IsRequired();

				entity.Property(e => e.Motto)
					.HasColumnType("nvarchar(256)");

				entity.Property(e => e.Email)
					.HasColumnType("nvarchar(256)").IsRequired();

				entity.Property(e => e.DogeCoins).IsRequired();

				entity.HasOne(d => d.Gender)
					.WithMany(p => p.Users)
					.HasForeignKey(d => d.GenderId)
					.OnDelete(DeleteBehavior.Cascade)
					.HasConstraintName("FK_Users_Genders");

			});
		}
	}
}
