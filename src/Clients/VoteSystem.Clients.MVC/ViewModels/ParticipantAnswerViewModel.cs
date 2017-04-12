﻿using VoteSystem.Clients.MVC.Infrastructure.Mapping;
using VoteSystem.Clients.MVC.Infrastructure.Mapping.Contracts;
using VoteSystem.Data.Entities;

namespace VoteSystem.Clients.MVC.ViewModels
{
    public class ParticipantAnswerViewModel : IMapFrom<ParticipantAnswer>
    {
        public int Answer { get; set; }
    }
}