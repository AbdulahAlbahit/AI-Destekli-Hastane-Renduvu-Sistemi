using Business_Layer.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Data_Accese_Layer.Dto;
using Data_Accese_Layer.Entities;
namespace Business_Layer.IServices
{
    public interface IGeminiService
    {
        Task<GeminiResultDto> GetAiSuggestionAsync(string userPrompt);
        Task<Appointment> HandleAiRequest(string userText, int PatientId);

    }
}
