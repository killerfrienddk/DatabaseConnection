using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DatabaseConnection.Models {
    public class Sex {
        public int ID { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        [NotMapped]
        public Member Member { get; set; }
    }
}
