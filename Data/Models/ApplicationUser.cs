using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TestMakerFreeApi.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        public string DisplayName { get; set; }

        public string Notes { get; set; }

        [Required] public int Type { get; set; }

        [Required] public int Flags { get; set; }

        [Required] public DateTime CreatedDate { get; set; }

        [Required] public DateTime LastModifiedDate { get; set; }

        public virtual List<Quiz> Quizzes { get; set; }

        public virtual List<Token> Tokens { get; set; }
    }
}