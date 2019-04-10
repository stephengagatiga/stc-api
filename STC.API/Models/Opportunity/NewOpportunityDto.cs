using STC.API.Entities.ComponentEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Opportunity
{
    public class NewOpportunityDto
    {
        [Required]
        public int AccountId { get; set; }

        public string BigDealCode { get; set; }

        [Required]
        public string Created { get; set; }

        [Required]
        public int CreatedById { get; set; }

        [Required]
        public string Modified { get; set; }

        [Required]
        public int ModifiedById { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public ICollection<NewComponentNoOppIdDto> Components { get; set; }

    }
}
