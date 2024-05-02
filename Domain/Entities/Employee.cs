using Domain.Common;

namespace Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public virtual Payment Payment { get; set; }

    }
}
