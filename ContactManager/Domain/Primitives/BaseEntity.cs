using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Primitives;

public abstract class BaseEntity
{
    protected BaseEntity(int id) => Id = id;
    protected BaseEntity() { 
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; protected set; }
}
