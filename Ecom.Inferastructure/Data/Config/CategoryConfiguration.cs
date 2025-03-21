﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entitiy.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Inferastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property( x=>x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x=>x.ID).IsRequired();
            builder.HasData(new Category { ID = 1 ,Name="Test",Description="Test"});
        }
    }
}
