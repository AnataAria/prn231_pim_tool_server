using DataAccessLayer.BusinessObject;
using DataAccessLayer.Repository;
using Service.DTO;
using Service.DTO.Response;

namespace Service.Service;

public class GroupService(GroupRepository groupRepository, EmployeeRepository employeeRepository)
{
    private readonly GroupRepository _groupRepository = groupRepository;
    private readonly EmployeeRepository _employeeRepository = employeeRepository;

    public async Task<ResponseEntity<GroupResponseDto>> CreateGroupAsync(CreateGroupDto createGroupDto)
    {
        if (createGroupDto == null)
        {
            return ResponseEntity<GroupResponseDto>.BadRequest("Group data cannot be null.");
        }

        var group = new Group
        {
            LeaderId = createGroupDto.LeaderId,
            Version = 1
        };

        await _groupRepository.AddAsync(group);

        var groupResponseDto = new GroupResponseDto
        {
            Id = (int)group.Id,
            LeaderId = (int)group.LeaderId,
            Version = (int)group.Version,
        };

        return ResponseEntity<GroupResponseDto>.CreateSuccess(groupResponseDto);
    }

    public async Task<ResponseEntity<GroupResponseDto>> GetGroupByIdAsync(long groupId)
    {
        var group = await _groupRepository.GetByIdAsync(groupId);
        if (group == null)
        {
            return ResponseEntity<GroupResponseDto>.NotFound($"Group with ID {groupId} not found.");
        }

        var groupResponseDto = new GroupResponseDto
        {
            Id = (int)group.Id,
            LeaderId = (int)group.LeaderId,
            Version = (int)group.Version,
        };

        return ResponseEntity<GroupResponseDto>.CreateSuccess(groupResponseDto);
    }

    public async Task<ResponseEntity<GroupResponseDto>> UpdateGroupAsync(int groupId, CreateGroupDto updateGroupDto)
    {
        if (updateGroupDto == null)
        {
            return ResponseEntity<GroupResponseDto>.BadRequest("Updated group data cannot be null.");
        }

        var group = await _groupRepository.GetByIdAsync(groupId);
        if (group == null)
        {
            return ResponseEntity<GroupResponseDto>.NotFound($"Group with ID {groupId} not found.");
        }

        group.LeaderId = updateGroupDto.LeaderId;
        group.Version++;
        await _groupRepository.UpdateAsync(group);

        var groupResponseDto = new GroupResponseDto
        {
            Id = (int)group.Id,
            LeaderId = (int)group.LeaderId,
            Version = (int)group.Version,
        };

        return ResponseEntity<GroupResponseDto>.CreateSuccess(groupResponseDto);
    }
    public async Task<ResponseEntity<GroupResponseDto>> DeleteGroupAsync(int groupId)
    {
        var group = await _groupRepository.GetByIdAsync(groupId);
        if (group == null)
        {
            return ResponseEntity<GroupResponseDto>.NotFound($"Group with ID {groupId} not found.");
        }

        if (group.Project != null)
        {
            return ResponseEntity<GroupResponseDto>.BadRequest("Cannot delete a group that is linked to a project.");
        }

        await _groupRepository.DeleteGroupAsync(groupId);

        var groupResponseDto = new GroupResponseDto
        {
            Id = (int)group.Id,
            LeaderId = (int)group.LeaderId,
            Version = (int)group.Version,
        };

        return ResponseEntity<GroupResponseDto>.CreateSuccess(groupResponseDto);
    }


    public async Task<ResponseEntity<IEnumerable<GroupResponseDto>>> GetAllGroupsAsync()
    {
        var groups = await _groupRepository.GetAllGroupsAsync();

        var groupTasks = groups.Select(async g =>
        {
            var leader = await _employeeRepository.GetByIdAsync((int)g.LeaderId);
            var leaderName = leader.FirstName +" "+ leader.LastName;
            return new GroupResponseDto
            {
                Id = (int)g.Id,
                LeaderId = (int)g.LeaderId,
                LeaderName = leaderName,
                Version = (int)g.Version
            };
        });

        var groupDtos = await Task.WhenAll(groupTasks);

        return ResponseEntity<IEnumerable<GroupResponseDto>>.CreateSuccess(groupDtos);
    }


}