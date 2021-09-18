using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace DatabaseConnection.Models {
    public class Member {
        public int UserID { get; set; }
        [NotMapped]
        public ulong DiscordID {
            get { return (ulong)DiscordDBID; }
            set { DiscordDBID = (long)value; }
        }

        [Column("discordID")]
        public long DiscordDBID { get; set; }
        public Sex Sex { get; set; }
        [ForeignKey("Sex")]
        public int SexID { get; set; }
        public string Storage { get; set; }

        [JsonIgnore]
        [NotMapped]
        public List<Families> CreatorOfFamilies { get; set; }
        [NotMapped]
        [JsonIgnore]
        public ICollection<MemberConnection> MemberConnections { get; set; }
        [NotMapped]
        [JsonIgnore]
        public ICollection<MemberConnection> MemberConnections2 { get; set; }
        [NotMapped]
        [JsonIgnore]
        public ICollection<FamilyConnection> FamilyConnections { get; set; }
    }
}