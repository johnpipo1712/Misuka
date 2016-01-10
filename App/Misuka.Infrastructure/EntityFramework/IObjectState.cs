
using System.ComponentModel.DataAnnotations.Schema;
namespace Misuka.Infrastructure.EntityFramework
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}