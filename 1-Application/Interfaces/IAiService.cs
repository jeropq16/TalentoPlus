using _1_Application.DTOs.AiRequestDto;

namespace _1_Application.Interfaces;

public interface IAiService
{
    Task<AiAnswerResponse> AskAsync(string question);
}