namespace DevFreelaaLD.API.Models
{
    public class CreateProjectInputModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int IdClient { get; set; }

        public int IdFreeLancerId { get; set; }

        public decimal TotalCost { get; set; }
    }
}
