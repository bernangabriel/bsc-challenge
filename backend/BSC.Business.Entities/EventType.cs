using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BSC.Business.Entities
{
    [DataContract]
    [Table("EventType")]
    public class EventType
    {
        #region Properties

        [Key] public int Id { get; set; }

        [DataMember] public string Name { get; set; }

        [DataMember]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; }

        [DataMember] public bool IsActiveFlag { get; set; }

        #endregion
    }
}