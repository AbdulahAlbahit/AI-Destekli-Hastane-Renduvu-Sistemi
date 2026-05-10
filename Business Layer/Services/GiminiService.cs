using Business_Layer.Dto;
using Business_Layer.Dto;
using Business_Layer.IServices;
using Data_Accese_Layer.Entities;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Business_Layer.Services
{
    public class GiminiService : IGeminiService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly string apikey;
        private readonly IClinicService _ClinicService;
        private readonly IDoctorServices _doctorService;
        private readonly IDepService _depService;

        public GiminiService(HttpClient client,IConfiguration configuration ,IClinicService ClinicService
            ,IDoctorServices doctorServices,IDepService depService)
        {
            _client=client;
            apikey = configuration["GeminiSettings:ApiKey"];
            _ClinicService=ClinicService;
            _doctorService=doctorServices;
            _depService=depService;
        }




        public async Task<Appointment> HandleAiRequest(string userText,int PatientId)
        {
            var aiResult = await GetAiSuggestionAsync(userText);

            
                // Burada senin AppointmentCreatedDto'nu dolduruyoruz
                var dep = _depService.GetDepByName(aiResult.ClinicName).Result;
                var clinic = _ClinicService.GetClinicByDepId(dep.DeptId).Result.FirstOrDefault();
                var doctor = _doctorService.GetDoctorbyClinicIdAsync(clinic.ClinicId).Result.FirstOrDefault();

            return new  Appointment
                {
                    ClinicId = clinic.ClinicId ,
                    DoctorId = doctor.DoctorId ,
                    TheDate = DateOnly.Parse(aiResult.Date),
                    TheTime = TimeOnly.Parse(aiResult.Time),
                    PatientId = PatientId,
                    TheStatus = "Beklemede"
                    


                
            };

          
        }





        public async Task<GeminiResultDto> GetAiSuggestionAsync(string userPrompt)
        {

             
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.1-flash-lite:generateContent?key={apikey}";

            string systemInstruction = $@"Sen profesyonel bir hastane asistanısın. 
                  BUGÜNÜN TARİHİ: {DateTime.Now:yyyy-MM-dd}
                  
                  GÖREVİN:
                  1. Kullanıcı şikayet ederse (Örn: 'Karnım ağrıyor'), ActionType: 'Analyze' yap ve uygun polikliniği seç.
                  2. Kullanıcı randevu isterse (Örn: 'Yarına randevu al'), ActionType: 'Book' yap ve tarih/saat ayıkla.
                  
                  YANIT FORMATI (Sadece JSON):
                  {{
                    ""ActionType"": ""Analyze"" veya ""Book"",
                    ""DepartmentName"": ""Poliklinik Adı"",
                    ""Date"": ""yyyy-MM-dd"" (Belirtilmemişse boş bırak),
                    ""Time"": ""HH:mm"" (Belirtilmemişse boş bırak),
                    ""BriefReason"": ""Kullanıcıya verilecek kısa cevap mesajı""
                  }}";
            var requestBody = new
            {
                contents = new[]
                {
                new { parts = new[] { new { text = $"{systemInstruction}\n\nKullanıcı Şikayeti: {userPrompt}" } } }
            }
            };

            var response = await _client.PostAsJsonAsync(url, requestBody);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GenminiResponse>();
                var jsonString = result?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;

                var analysis = JsonSerializer.Deserialize<GeminiResultDto>(jsonString);
                return analysis;
            }
           
            return null;

        }
        
    }
}
