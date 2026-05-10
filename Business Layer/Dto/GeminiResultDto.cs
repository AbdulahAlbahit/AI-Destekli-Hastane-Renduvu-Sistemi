using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Dto
{
    public class GeminiResultDto
    {
       
            public string ActionType { get; set; } // "Analyze" veya "Book"
            public string ClinicName { get; set; } // Veritabanında arama yapmak için kullanacağız
            public string Date { get; set; }       // "yyyy-MM-dd" formatında isteyeceğiz
            public string Time { get; set; }       // "HH:mm" formatında isteyeceğiz
            public string BriefReason { get; set; }
      
    }
}
