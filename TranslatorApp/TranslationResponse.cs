using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorApp 
{
    // API'den gelen ana cevabı temsil eden sınıf
    public class TranslationResponse
    {
        public ResponseData responseData { get; set; }
        public int responseStatus { get; set; }
    }

    // Ana cevabın içindeki "responseData" kısmını temsil eden sınıf
    public class ResponseData
    {
        public string translatedText { get; set; }
        public double match { get; set; }
    }
}