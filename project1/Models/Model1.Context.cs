﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace project1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class project1Entities : DbContext
    {
        public project1Entities()
            : base("name=project1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<basic> basics { get; set; }
        public virtual DbSet<department> departments { get; set; }
        public virtual DbSet<education> educations { get; set; }
        public virtual DbSet<forgot> forgots { get; set; }
        public virtual DbSet<login> logins { get; set; }
        public virtual DbSet<signup> signups { get; set; }
        public virtual DbSet<skill> skills { get; set; }
        public virtual DbSet<work> works { get; set; }
        public virtual DbSet<leave> leaves { get; set; }
        public virtual DbSet<raise> raises { get; set; }
    }
}
