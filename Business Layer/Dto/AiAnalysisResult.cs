using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Dto
{
    public class AiAnalysisResult
    {
    
            public string PolyclinicName { get; set; } // Örn: "Kardiyoloji"
            public string Reason { get; set; }        // Örn: "Göğüs ağrısı şikayeti nedeniyle..."
            public string UrgencyLevel { get; set; }   // Örn: "Normal" veya "Acil"
        
    }
}
