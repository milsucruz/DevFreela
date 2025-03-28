﻿using DevFreelaaLD.API.Enums;

namespace DevFreelaaLD.API.Entities
{
    public class Project : BaseEntity
    {
        protected Project() { }

        public Project(string title, string description, int idClient, int idFreelancer, User freeLancer, decimal totalCost, ProjectStatusEnum status)
        {
            Title = title;
            Description = description;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            FreeLancer = freeLancer;
            TotalCost = totalCost;
            Status = ProjectStatusEnum.Created;
            Comments = [];
        }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public int IdClient { get; private set; }

        public User Client { get; private set; }

        public int IdFreelancer { get; private set; }

        public User FreeLancer { get; private set; }

        public decimal TotalCost { get; private set; }

        public DateTime StartedAt { get; private set; }

        public DateTime CompletedAt { get; private set; }

        public ProjectStatusEnum Status { get; private set; }

        public List<ProjectComment> Comments { get; private set; }

        public void Cancel()
        {
            if (Status == ProjectStatusEnum.InProgress || Status == ProjectStatusEnum.Suspended)
                Status = ProjectStatusEnum.Cancelled;
        }

        public void Start()
        {
            if (Status == ProjectStatusEnum.Created)
            {
                Status = ProjectStatusEnum.InProgress;
                StartedAt = DateTime.Now;
            }
        }

        public void Complete()
        {
            if (Status == ProjectStatusEnum.PaymentPending || Status == ProjectStatusEnum.InProgress)
            {
                Status = ProjectStatusEnum.Completed;
                CompletedAt = DateTime.Now;
            }
        }

        public void SetPaymentPading()
        {
            if (Status == ProjectStatusEnum.InProgress)
                Status = ProjectStatusEnum.PaymentPending;
        }

        public void Update(string title, string description, decimal totalCost)
        {
            Title = title;
            Description = description;
            TotalCost = totalCost;
        }
    }
}