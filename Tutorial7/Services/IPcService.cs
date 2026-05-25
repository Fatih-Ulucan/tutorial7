using Tutorial7.DTOs;

namespace Tutorial7.Services
{
    public interface IPcService
    {
        Task<IEnumerable<PCResponseDto>> GetAllPcsAsync();
        Task<PCDetailResponseDto?> GetPcWithComponentsAsync(int id);
        Task<PCResponseDto> CreatePcAsync(PCRequestDto dto);
        Task<bool> UpdatePcAsync(int id, PCRequestDto dto);
        Task<bool> DeletePcAsync(int id);
    }
}