using System.ComponentModel.DataAnnotations;

namespace DatabaseConnection.Models {
    public class ConnectionWord {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Parent { get; set; }
    }
}
