using SignalRChat.Domain.Features.Annotations;
using SignalRChat.Infra.Contexts;
using SignalRChat.Infra.Results;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Infra.Features.Annotations
{
    public class AnnotationRepository : IAnnotationRepository
    {
        SignalRChatDbContext _context;

        public AnnotationRepository(SignalRChatDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Annotation, Exception>> AddAnnotation(Annotation annotation)
        {
            var newAnnotation = _context.Annotations.Add(annotation).Entity;
            await _context.SaveChangesAsync();

            return newAnnotation;
        }

        public Result<IQueryable<Annotation>, Exception> GetAll()
        {
            return _context.Annotations;
        }
    }
}