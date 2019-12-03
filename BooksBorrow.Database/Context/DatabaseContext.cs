using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksBorrow.Database.Context
{
    public class DatabaseContext: DbContext
    {
        private string _connectionString = string.Empty;

        public DatabaseContext(string connectionString = null)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            //optionsBuilder.UseMySql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }
        #region Entities

        #region Comment
        //public virtual DbSet<Document_Comment> Document_Comment { get; set; }
        //public virtual DbSet<Document_Comment_Reply> Document_Comment_Reply { get; set; }
        //public virtual DbSet<Document_Score> Document_Score { get; set; }
        #endregion

        #region Folder
        //public virtual DbSet<Document_Folder> Document_Folder { get; set; }
        #endregion

        #region Log
        //public virtual DbSet<Document_Log> Document_Log { get; set; }
        #endregion

        #region Message
        //public virtual DbSet<Document_Message> Document_Message { get; set; }
        #endregion

        #region Type
        //public virtual DbSet<Document_Type> Document_Type { get; set; }
        //public virtual DbSet<Document_Join_Document_Type> Document_Join_Document_Type { get; set; }
        #endregion

        #region User
        //public virtual DbSet<Document_User> Document_User { get; set; }
        //public virtual DbSet<Document_User_Group> Document_User_Group { get; set; }
        //public virtual DbSet<Document_User_Join_Document_User_Group> Document_User_Join_Document_User_Group { get; set; }
        //public virtual DbSet<Document_Visit_Right> Document_Visit_Right { get; set; }
        #endregion

        #region Role
        //public virtual DbSet<Document_User_Position> Document_User_Position { get; set; }
        //public virtual DbSet<Document_User_Role> Document_User_Role { get; set; }
        #endregion

        #endregion Entities
    }
}
