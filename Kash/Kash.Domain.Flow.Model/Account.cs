using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kash.Domain.Flow.Model
{
    public class Account : EntryInfo
    {
        [Key]
        public long ID { get; set; }
        public long OwnerUserID { get; set; }
        [Column("AccountType")]
        public EnumAccountType Type { get; set; } = EnumAccountType.Undifined;
        [Column("AccountStatus")]
        public EnumAccountStatus Status { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public virtual Account OwnerUser { get; set; }
    }
}
