namespace DevFreelaaLD.API.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreationDate = DateTime.Now;
            IsDeleted = false;
        }

        public int Id { get; private set; }

        public DateTime CreationDate { get; private set; }

        public bool IsDeleted { get; private set; }

        public void SetAsDeleted()
        {
            IsDeleted = true;
        }
    }
}
