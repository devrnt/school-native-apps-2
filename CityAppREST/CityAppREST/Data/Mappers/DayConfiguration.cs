using System;
using CityAppREST.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityAppREST.Data.Mappers
{
    public class DayConfiguration : IEntityTypeConfiguration<Day>
    {

        public void Configure(EntityTypeBuilder<Day> builder)
        {
            builder.HasKey(d => d.DayOfWeek);
        }
    }
}
