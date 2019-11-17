using AutoMapper;
using SignalRChat.Applications.Features.Annotations.Handlers;
using SignalRChat.Domain.Features.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRChat.Applications.Features.Annotations
{
    public class AnnotationMappingProfile : Profile
    {
        public AnnotationMappingProfile()
        {
            CreateMap<AnnotationsCreate.Command, Annotation>();
        }
    }
}
