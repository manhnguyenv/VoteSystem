﻿namespace VoteSystem.Web.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using VoteSystem.Data.Models;
    using VoteSystem.Web.Infrastructure.Mapping;

    public class QuestionAnswerViewModel : IMapFrom<QuestionAnswer>, IMapTo<QuestionAnswer>
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Answer")]
        public string QuestionAnswerName { get; set; }

        public int QuestionId { get; set; }

        public IList<UserAnswerViewModel> UserAnswers { get; set; }
    }
}