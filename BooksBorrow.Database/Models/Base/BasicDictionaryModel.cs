using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BooksBorrow.Database.Models
{
    public abstract class BasicDictionaryModel : BaseModel, IDbModel
    {
        [Required]
        [MaxLength(100)]
        public virtual string Full_Name { get; set; }

        [Required]
        [MaxLength(100)]
        public virtual string Short_Name { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string Sort_Code { get; set; }
    }
}
