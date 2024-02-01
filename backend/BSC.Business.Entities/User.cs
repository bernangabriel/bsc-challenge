using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BSC.Business.Entities
{
    [DataContract]
    [Table("AppUser")]
    public class User
    {
        #region Properties

        [Key] public Guid UserId { get; set; }

        [DataMember] public string UserName { get; set; }

        [DataMember] public string Name { get; set; }

        [DataMember] public string LastName { get; set; }

        [DataMember] public string Email { get; set; }

        [DataMember] public string Password { get; set; }

        [DataMember]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; }

        [DataMember] public bool IsActiveFlag { get; set; }

        [DataMember] public bool IsDeleteFlag { get; set; }

        #endregion
    }
}