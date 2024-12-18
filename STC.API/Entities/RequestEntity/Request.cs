using STC.API.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.RequestEntity
{
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public RequestType RequestType { get; set; }
        [Required]
        public RequestSubject RequestSubject { get; set; }
        [Required]
        public RequestAction RequestAction { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public int ImplementedBy { get; set; }
        [Required]
        public DateTime ImplementedOn { get; set; }
    }
}
