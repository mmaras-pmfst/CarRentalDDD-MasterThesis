﻿using Domain.CarBrand.Entities;
using Domain.CarCategory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;

internal class CarModelConfiguration : IEntityTypeConfiguration<CarModel>
{
    public void Configure(EntityTypeBuilder<CarModel> builder)
    {
        builder.ToTable(TableNames.CarModels, SchemaNames.Catalog);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CarModelName)
            .HasMaxLength(50)
            .IsRequired(true);

        builder.HasOne<CarCategory>() // CarModel belongs to one CarCategory
            .WithMany() //CarCategory belongs to many CarModels
            .HasForeignKey(x => x.CarCategoryId);


    }
}
