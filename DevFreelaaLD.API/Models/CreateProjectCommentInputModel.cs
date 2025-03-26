namespace DevFreelaaLD.API.Models
{
    public class CreateProjectCommentInputModel
    {
        public string IdProject { get; set; }

        public string Content { get; set; }

        public int IdUser { get; set; }
    }
}
