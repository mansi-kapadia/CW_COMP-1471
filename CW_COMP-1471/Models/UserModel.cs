﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW_COMP_1471.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    [Table("users")]
    public class User
    {
        [Key]
        [Column("userid")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;

        [Column("firstname")]
        public string FirstName { get; set; }
        [Column("lastname")]
        public string LastName { get; set; }
        [Required]
        [Column("username")]
        public string UserName { get; set; }
        [Required]
        [Column("password")]
        public string Password { get; set; }
        [Required]
        [Column("roleid")]
        public int RoleId { get; set; }
        [Required]
        [Column("email")]
        public string Email { get; set; }
        [Column("phoneno")]
        public string PhoneNumber { get; set; }
        [Column("address")]
        public string Address { get; set; }

        [NotMapped]
        public SelectList? Roles { get; set; }

        //[NotMapped]
        //public Role? Role { get; set; } = new Role();
    }
}
