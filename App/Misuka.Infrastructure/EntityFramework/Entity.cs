using System.ComponentModel.DataAnnotations.Schema;

namespace Misuka.Infrastructure.EntityFramework
{
    public abstract class Entity : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}