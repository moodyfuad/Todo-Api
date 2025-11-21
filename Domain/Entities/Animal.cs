using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{    public class Animal : BaseEntity
    {
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Person> Owners { get; set; } = [];
    }
}