using SignalRChat.Infra.Results;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Domain.Features.Annotations
{
    public interface IAnnotationRepository
    {
        Task<Result<Annotation, Exception>> AddAnnotation(Annotation annotation);
        Result<IQueryable<Annotation>, Exception> GetAll();
    }
}
