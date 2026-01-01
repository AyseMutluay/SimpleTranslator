using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json; // Az önce yüklediğimiz kütüphane

namespace TranslatorApp // DİKKAT: Burası senin proje isminle aynı olmalı!
{
    public class TranslationService
    {
        // İnternete çıkış kapımız (HttpClient)
        private readonly HttpClient _httpClient;

        public TranslationService()
        {
            _httpClient = new HttpClient();
        }

        // Çeviri işlemini yapacak asıl metot
        public async Task<string> TranslateText(string input, string langPair)
        {
            // 1. URL Hazırlama
            // MyMemory API formatı: .../get?q=METİN&langpair=en|tr
            string url = $"https://api.mymemory.translated.net/get?q={Uri.EscapeDataString(input)}&langpair={langPair}";

            try
            {
                // 2. API'ye istek gönder (Cevap gelene kadar bekle - await)
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                // Hata var mı kontrol et (Örn: İnternet yoksa)
                response.EnsureSuccessStatusCode();

                // 3. Gelen cevabı yazı olarak oku
                string jsonResult = await response.Content.ReadAsStringAsync();

                // 4. JSON yazısını C# nesnesine (TranslationResponse) çevir
                var resultObject = JsonConvert.DeserializeObject<TranslationResponse>(jsonResult);

                // 5. Sadece çevrilmiş metni al ve geri gönder
                return resultObject.responseData.translatedText;
            }
            catch (Exception ex)
            {
                // Bir hata olursa (internet yoksa vs.) hatayı geri döndür
                return "Hata: " + ex.Message;
            }
        }
    }
}