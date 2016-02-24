﻿namespace VoteSystem.Data.Models
{    
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;
    // TODO use AuditInfo later ...
    public class RateSystem /*: AuditInfo*/
    {
        public RateSystem()
        {
            this.Questions = new HashSet<Question>();
            this.Users = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        public string RateSystemName { get; set; }

        // TODO make sure that these properties are not null-able; and in the view model
        public DateTime? StarDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
