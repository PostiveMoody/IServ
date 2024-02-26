using Community.OData.Linq;
using IServ.WebApplication.Dto;
using Microsoft.OData.Edm;

namespace IServ.WebApplication
{
    public class EdmModelFactory : IConventionModelFactory
    {
        private readonly object _lockObject = new object();
        private IEdmModel _model;

        public IEdmModel CreateOrGet()
        {
            if (_model != null)
                return _model;

            lock (_lockObject)
            {
                if (_model != null)
                    return _model;

                var builder = new ODataConventionModelBuilder();

                {
                    var entityType = builder.EntityType<UniversityDto>();
                    entityType.HasKey(x => x.UniversityId);
                    //entityType.Property(x => x.);
                    //entityType.Property(x => x.SubjectOfAppeal);
                    //entityType.Property(x => x.DeadlineForHiring);
                    //entityType.Property(x => x.Status);
                    //entityType.Property(x => x.Category);
                    //entityType.Property(x => x.CreationDate);

                    builder.EntitySet<UniversityDto>(nameof(UniversityDto));
                }

                _model = builder.GetEdmModel();
                return _model;
            }
        }
    }
}
