using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMakerFreeApi.Data.Models
{
    public class Token
    {
        public Token()
        {
        }
        
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string ClientId { get; set; }
        
        [Required]
        public string Value { get; set; }
        
        public int Type { get; set; }
        
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public DateTime CreatedDate { get; set; }
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}