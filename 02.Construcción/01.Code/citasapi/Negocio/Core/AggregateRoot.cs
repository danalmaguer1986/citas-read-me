using System;

namespace Negocio.Core
{
    public abstract class AggregateRoot : Entity, IAuditable
    {
        public virtual string AuditUser { get; set; }

        public virtual DateTime AuditDate { get; set; }
    }

    public interface IAuditable
    {
        DateTime AuditDate { get; set; }

        string AuditUser { get; set; }
    }


}
