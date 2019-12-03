using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BooksBorrow.Database.Models
{
    public abstract class BaseModel
    {
        [Required]
        public virtual bool Is_Deleted { get; set; }

        [Required]
        public virtual DateTime Created_Time { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string Created_By { get; set; }

        [Required]
        public virtual DateTime? Updated_Time { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string Updated_By { get; set; }
    }
}
