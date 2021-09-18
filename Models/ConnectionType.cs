using System.ComponentModel.DataAnnotations;

namespace DatabaseConnection.Models {
    public class ConnectionType {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Type { get; set; }
    }
}
