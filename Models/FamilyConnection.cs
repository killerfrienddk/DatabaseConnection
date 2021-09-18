using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnection.Models {
    public class FamilyConnection {
        public Member User { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public Families Family { get; set; }
        [ForeignKey("Family")]
        public int FamilyID { get; set; }
        public bool Head { get; set; }
    }
}
