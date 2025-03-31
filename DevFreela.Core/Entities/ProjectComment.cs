namespace DevFreela.Core.Entities
{
    public class ProjectComment : BaseEntity
    {
        public ProjectComment(string content, int idProject, int idUser)
        {
            Content = content;
            IdProject = idProject;
            IdUser = idUser;
        }

        public string Content { get; set; }

        public int IdProject { get; set; }

        public Project Project { get; set; }

        public int IdUser { get; set; }

        public User User { get; set; }
    }
}