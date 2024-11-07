namespace Service.DTO;

public class CreateGroupDto
    {
        public int LeaderId { get; set; }
        public int ProjectId { get; set; }
    }

public class GroupResponseDto
    {
        public int Id { get; set; }
        public int LeaderId { get; set; }
        public string? LeaderName { get; set; }
        public int Version { get; set; }
    }