﻿using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class CreateProjectInputModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int IdClient { get; set; }

        public int IdFreeLancer { get; set; }

        public decimal TotalCost { get; set; }

        public Project ToEntity()
            => new Project(Title, Description, IdClient, IdFreeLancer, TotalCost);
    }
}