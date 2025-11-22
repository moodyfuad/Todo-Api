using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PersonTaskItemGroup : BaseEntity
    {
        public Person Person { get; set; }
        public Guid PersonId { get; set; }
        public TaskItemGroup TaskItemGroup { get; set; }
        public Guid TaskItemGroupId { get; set; }

        public UserRoles PersonRole { get; set; }
    }
}
