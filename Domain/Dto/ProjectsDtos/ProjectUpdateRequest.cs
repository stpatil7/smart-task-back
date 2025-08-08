namespace Domain.Dto.ProjectsDtos
{
    public class ProjectUpdateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
    }
}
