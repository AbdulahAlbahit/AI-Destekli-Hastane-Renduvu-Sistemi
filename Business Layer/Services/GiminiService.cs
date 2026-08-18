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




        public async Task<Appointment> HandleAiRequest(GeminiResultDto aiResult, int PatientId)
        {
            var dep = await _depService.GetDepByName(aiResult.ClinicName);
            if (dep == null) 
                throw new Exception($"'{aiResult.ClinicName}' adında bir bölüm bulunamadı. Lütfen tam bölüm adını (örn: Dahiliye) yazın.");

            var clinics = await _ClinicService.GetClinicByDepId(dep.DeptId);
            var clinic = clinics?.FirstOrDefault();
            if (clinic == null) 
                throw new Exception("Bu bölümde uygun klinik bulunamadı.");

            var doctors = await _doctorService.GetDoctorbyClinicIdAsync(clinic.ClinicId);
            var doctor = doctors?.FirstOrDefault();
            if (doctor == null) 
                throw new Exception("Bu klinikte uygun doktor bulunamadı.");

            DateOnly theDate;
            if (!DateOnly.TryParse(aiResult.Date, out theDate))
                throw new Exception("Geçerli bir tarih anlaşılamadı. Lütfen tarihi 'Yarın', '20 Eylül' gibi belirterek tekrar deneyin.");

            TimeOnly theTime;
            if (!TimeOnly.TryParse(aiResult.Time, out theTime))
                throw new Exception("Geçerli bir saat anlaşılamadı. Lütfen 'saat 14:30' şeklinde belirterek tekrar deneyin.");

            return new Appointment
            {
                ClinicId = clinic.ClinicId,
                DoctorId = doctor.DoctorId,
                TheDate = theDate,
                TheTime = theTime,
                PatientId = PatientId,
                TheStatus = "Beklemede"
            };
        }





        public async Task<GeminiResultDto> GetAiSuggestionAsync(string userPrompt)
        {

             
            // 'gemini-flash-latest' yüksek yoğunluktan (503) hata verdiği için alternatif olarak 'gemini-3.5-flash' kullanıyoruz.
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.5-flash:generateContent?key={apikey}";

            string systemInstruction = $@"Sen profesyonel bir hastane asistanısın. 
                  BUGÜNÜN TARİHİ: {DateTime.Now:yyyy-MM-dd}
                  
                  GÖREVİN:
                  1. Kullanıcı şikayet ederse (Örn: 'Karnım ağrıyor'), ActionType: 'Analyze' yap ve uygun polikliniği seç.
                  2. Kullanıcı randevu isterse (Örn: 'Yarına randevu al'), ActionType: 'Book' yap ve tarih/saat ayıkla.
                  
                  YANIT FORMATI (Sadece saf JSON, markdown block kullanma):
                  {{
                    ""ActionType"": ""Analyze"" veya ""Book"",
                    ""ClinicName"": ""Poliklinik Adı"",
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

                if (!string.IsNullOrWhiteSpace(jsonString))
                {
                    // AI markdown bloğu dönerse temizleyelim
                    jsonString = jsonString.Replace("```json", "").Replace("```", "").Trim();
                    
                    try 
                    {
                        var analysis = JsonSerializer.Deserialize<GeminiResultDto>(jsonString);
                        if (analysis != null) 
                            return analysis;
                    }
                    catch (Exception ex)
                    {
                        return new GeminiResultDto 
                        { 
                            ActionType = "Analyze", 
                            BriefReason = "JSON Parse Hatası: " + ex.Message + " | Gelen Metin: " + jsonString
                        };
                    }
                }
                else
                {
                    return new GeminiResultDto { ActionType = "Analyze", BriefReason = "Google API'den boş yanıt geldi." };
                }
            }
            else
            {
                 var err = await response.Content.ReadAsStringAsync();
                 return new GeminiResultDto { ActionType = "Analyze", BriefReason = "HTTP Hatası: " + response.StatusCode + " | " + err };
            }
           
            // Herhangi bir hata veya null dönme durumunda boş referans hatasını önlemek için varsayılan model dönelim
            return new GeminiResultDto 
            { 
                ActionType = "Analyze", 
                BriefReason = "Yapay zeka asistanından geçerli bir yanıt alınamadı. Lütfen şikayetinizi farklı bir şekilde tekrar ifade edin." 
            };

        }
        
    }
}
