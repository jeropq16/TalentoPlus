using _1_Application.DTOs;
using _1_Application.DTOs.AiRequestDto;
using _1_Application.Interfaces;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;

namespace _1_Application.Services;

public class AiService : IAiService
{
    private readonly IDashboardService _dashboardService;
    private readonly IConfiguration _config;

    public AiService(IDashboardService dashboardService, IConfiguration config)
    {
        _dashboardService = dashboardService;
        _config = config;
    }

    public async Task<AiAnswerResponse> AskAsync(string question)
    {
        // 1. Obtener datos reales
        var data = await _dashboardService.GetDashboardAsync();

        // 2. Construir contexto
        string context = $@"
                            Información de la empresa:
                            Total empleados: {data.TotalEmployees}
                            Activos: {data.ActiveEmployees}
                            Inactivos: {data.InactiveEmployees}
                            Vacaciones: {data.VacationEmployees}
                            Salario promedio: {data.AverageSalary}
                            Nómina total: {data.TotalPayroll}

Empleados por departamento:
{string.Join("\n", data.EmployeesPerDepartment.Select(d => $"{d.Department}: {d.Count}"))}
";

        // 3. Cliente OpenAI
        var client = new ChatClient(
            model: "gpt-4o-mini", 
            apiKey: _config["OpenAI:ApiKey"]
        );

        // 4. Prompt
        var messages = new List<ChatMessage>
        {
            ChatMessage.FromSystem("Eres un asistente experto en análisis de recursos humanos."),
            ChatMessage.FromSystem("Debes responder basado EXCLUSIVAMENTE en los datos enviados del dashboard."),
            ChatMessage.FromUser($"Datos:\n{context}"),
            ChatMessage.FromUser($"Pregunta: {question}")
        };

        var result = await client.CompleteChatAsync(messages);

        return new AiAnswerResponse
        {
            Answer = result.Value.FirstChoice.Message.Content[0].Text
        };
    }
}