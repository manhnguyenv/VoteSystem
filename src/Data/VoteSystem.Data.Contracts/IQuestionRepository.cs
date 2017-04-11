﻿using System.Collections.Generic;

using VoteSystem.Data.Entities;

namespace VoteSystem.Data.Contracts
{
    public interface IQuestionRepository
    {
        void Add(Question question);

        void Update(Question question);

        void Delete(Question question);

        IEnumerable<Question> GetAllQuestionsWithAnswers();

        IEnumerable<Question> GetUsersAnswersByVoteSystemId(int voteSystemId);
    }
}
