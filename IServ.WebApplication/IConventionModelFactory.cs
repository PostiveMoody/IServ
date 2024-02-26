using Microsoft.OData.Edm;

namespace IServ.WebApplication
{
    public interface IConventionModelFactory
    {
        IEdmModel CreateOrGet();
    }
}
