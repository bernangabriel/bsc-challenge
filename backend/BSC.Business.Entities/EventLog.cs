using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BSC.Business.Entities
{
    [DataContract]
    [Table("EventLog")]
    public class EventLog
    {
        #region Properties

        [Key] public int Id { get; set; }

        [DataMember] [ForeignKey("User")] public Guid UserId { get; set; }

        [DataMember] [ForeignKey("EventType")] public int EventTypeId { get; set; }

        [DataMember] public string Payload { get; set; }

        [DataMember]
        [Column(TypeName = "datetime2")]
        public DateTime EventDate { get; set; }

        [DataMember]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; }

        #endregion

        #region Relationships

        [DataMember] public virtual User User { get; set; }
        [DataMember] public virtual EventType EventType { get; set; }

        #endregion
    }
}