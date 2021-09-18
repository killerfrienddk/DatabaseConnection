using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnection.Models {
    public class MemberConnection {
        public Member User { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public ConnectionType ConnectionType { get; set; }
        [ForeignKey("ConnectionType")]
        public int ConnectionTypeID { get; set; }
        public Member User2 { get; set; }
        [ForeignKey("User2")]
        public int User2ID { get; set; }
        public ConnectionType ConnectionType2 { get; set; }
        [ForeignKey("ConnectionType2")]
        public int ConnectionType2ID { get; set; }

        [NotMapped]
        public string DiscordID { get; set; }
        [NotMapped]
        public int FamilyID { get; set; }
    }
}
