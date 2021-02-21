using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreLearning.DBLibrary.Common
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Created = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id {get; set;}

        public DateTime Created {get; set;}
    }
}