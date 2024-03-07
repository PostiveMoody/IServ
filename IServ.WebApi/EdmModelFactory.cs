using Community.OData.Linq;
using Microsoft.OData.Edm;

namespace IServ.WebApi
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

                //{
                //    var entityType = builder.EntityType<UniversityDto>();
                //    entityType.HasKey(x => x.UniversityId);
                //    entityType.Property(x => x.Country);
                //    entityType.Property(x => x.Name);
                //    entityType.Property(x => x.CreationDate);
                //    entityType.Property(x => x.AlphaTwoCode);

                //    builder.EntitySet<UniversityDto>(nameof(UniversityDto));
                //}

                _model = builder.GetEdmModel();
                return _model;
            }
        }
    }
}
