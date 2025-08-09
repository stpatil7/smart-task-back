namespace Domain.Dto.ProjectCreateDtos
{
    public class ProjectCreateRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CreatedById { get; set; }
    }
}
