using DevFreelaaLD.API.Entities;

namespace DevFreelaaLD.API.Models
{
    public class ProjectViewModel
    {
        public ProjectViewModel(int id, string title, string description, string clientName, int idClient, int idFreelancer, string freelancerName, decimal totalCost, List<ProjectComment> comments)
        {
            Id = id;
            Title = title;
            Description = description;
            ClientName = clientName;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            FreelancerName = freelancerName;
            TotalCost = totalCost;
            Comments = comments.Select(c => c.Content).ToList();
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public string ClientName { get; private set; }

        public int IdClient { get; private set; }

        public int IdFreelancer { get; private set; }

        public string FreelancerName { get; private set; }

        public decimal TotalCost { get; private set; }

        public List<string> Comments { get; private set; }

        public static ProjectViewModel FromEntity(Project entity)
            => new(entity.id, entity.Title, entity.Description, entity.Client.FullName, entity.IdClient,
                entity.IdFreelancer, entity.FreeLancer.FullName, entity.TotalCost, entity.Comments);
    }
}