﻿namespace VoteSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Mvc.Expressions;

    using VoteSystem.Data.Models;
    using VoteSystem.Services.Data.Contracts;
    using VoteSystem.Web.Infrastructure.Mapping;
    using VoteSystem.Web.ViewModels;

    public class UserController : AdministrationController
    {
        private IUserService users;
        private IParticipantService participants;

        public UserController(IUserService users, IParticipantService participants)
        {
            this.users = users;
            this.participants = participants;
        }
        
        public ActionResult Add(int rateSystemId)
        {
            var users = this.users
                .GetAllUnselectUsers(rateSystemId)
                .To<UserViewModel>()
                .ToList();

            var userSelectedVM = new UserSelectedViewModel()
            {
                Users = users,
                RateSystemId = rateSystemId
            };           

            return this.View(userSelectedVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(UserSelectedViewModel model)
        {
            var getSelectedUsers = model.GetSelectedUsers();

            foreach (var participant in getSelectedUsers)
            {
                var currentParticipant = new Participant()
                {
                    RateSystemId = model.RateSystemId,
                    UserId = participant.Id
                };

                this.participants.Add(currentParticipant);
            }

            this.participants.SaveChanges();

            return this.RedirectToAction<UserController>(c => c.Add(model.RateSystemId));
        }

        public ActionResult Remove(int rateSystemId)
        {
            var users = this.users
                .GetAllSelectUsers(rateSystemId)
                .To<UserViewModel>()
                .ToList();

            var userSelectedVM = new UserSelectedViewModel()
            {
                Users = users,
                RateSystemId = rateSystemId
            };

            return this.View(userSelectedVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(UserSelectedViewModel model)
        {
            var getSelectedUsers = model.GetSelectedUsers();

            foreach (var participant in getSelectedUsers)
            {
                var currentParticipant = this.participants.GetParticipantByRateSystemIdAndUserId(model.RateSystemId, participant.Id);

                if (currentParticipant == null)
                {
                    throw new ArgumentNullException("Participant can not be found!");
                }

                this.participants.Remove(currentParticipant);
            }

            this.participants.SaveChanges();

            return this.RedirectToAction<UserController>(c => c.Remove(model.RateSystemId));
        }

        public ActionResult Preview(int rateSystemId)
        {
            var users = this.users
                .GetAllSelectUsers(rateSystemId)
                .To<UserViewModel>()
                .ToList();

            var userSelectedVM = new UserSelectedViewModel()
            {
                Users = users,
                RateSystemId = rateSystemId
            };

            return this.View(userSelectedVM);
        }
    }
}