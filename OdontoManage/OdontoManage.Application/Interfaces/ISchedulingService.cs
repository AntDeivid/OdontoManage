using OdontoManage.Application.Models.DTOs;

namespace OdontoManage.Application.Interfaces;

public interface ISchedulingService
{
    SchedulingDto Create(SchedulingCreateDto schedulingCreateDto);
    
    List<SchedulingDto> GetByInterval(DateDto start, DateDto end);
    
    SchedulingDto GetById(Guid id);
    
    void Delete(Guid id);
}