using auhnuh_server.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using auhnuh_server.Common.Constants;

namespace auhnuh_server.Infrastructure.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();

            builder.HasData(new Role
            {
                Id = 1,
                Name = SystemRole.Client.ToString(),
                NormalizedName = SystemRole.Client.ToString().ToUpper(),
            },
                new Role
                {
                    Id = 2,
                    Name = SystemRole.Admin.ToString(),
                    NormalizedName = SystemRole.Admin.ToString().ToUpper(),
                });
        }
    }
}
