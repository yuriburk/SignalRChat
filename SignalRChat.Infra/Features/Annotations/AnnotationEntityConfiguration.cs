using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalRChat.Domain.Features.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRChat.Infra.Features.Annotations
{
    public class AnnotationEntityConfiguration : IEntityTypeConfiguration<Annotation>
    {
        public void Configure(EntityTypeBuilder<Annotation> builder)
        {
            builder.ToTable("Annotation");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).HasMaxLength(255).IsRequired();
            builder.Property(m => m.Text).HasMaxLength(255).IsRequired();
        }
    }
}
