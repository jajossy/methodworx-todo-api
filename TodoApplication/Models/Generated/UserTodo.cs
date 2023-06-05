using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TodoApplication.Models.Generated
{
    [Table("UserTodo")]
    public partial class UserTodo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid TodoId { get; set; }        
        [StringLength(100)]
        public string Description { get; set; } = null!;
        public bool? IsDeleted { get; set; }
        public bool? IsDone { get; set; }
        public Guid? UserId { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? EditedDate { get; set; }
        
        public DateTimeOffset? TodoDate { get; set; }

        [NotMapped]
        [ForeignKey("UserId")]
        [InverseProperty("UserTodos")]
        public virtual User? User { get; set; } = null!;
    }
}
