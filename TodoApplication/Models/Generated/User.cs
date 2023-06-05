using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TodoApplication.Models.Generated
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            UserTodos = new HashSet<UserTodo>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Firstname { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Lastname { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Password { get; set; } = null!;
        [Required]
        public DateTimeOffset CreatedDate { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; } = null!;
        public DateTimeOffset? EditedDate { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserTodo> UserTodos { get; set; }
    }
}
