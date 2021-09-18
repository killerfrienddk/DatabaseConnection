using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace DatabaseConnection.Models {
    public class Families {
        public int ID { get; set; }
        public string Name { get; set; }
        public Member Creator { get; set; }
        [ForeignKey("Creator")]
        public int CreatorID { get; set; }

        [NotMapped]
        public ulong GuildID {
            get { return (ulong)GuildDBID; }
            set { GuildDBID = (long)value; }
        }

        [Column("guildID")]
        public long GuildDBID { get; set; }
        public string Storage { get; set; }

        [NotMapped]
        [JsonIgnore]
        public ICollection<FamilyConnection> FamilyConnections { get; set; }

        public string NameWithFamilyAtTheEnd() {
            if (Name.Contains("family")) return Name;
            else return Name + " family";
        }
    }
}