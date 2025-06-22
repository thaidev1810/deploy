using BloodDonation.Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;

namespace BloodDonation.Application.Users.CreateHealthForm
{
    public class CreateHealthFormCommand : ICommand<CreateHealthFormResponse>
    {
        //public Guid UserId { get; set; }

        public List<HealthAnswerCommand> Answers { get; set; } = new();
    }

    public class HealthAnswerCommand
    {
        public Guid QuestionId { get; set; }
        public string Answer { get; set; } = null!;
    }
}