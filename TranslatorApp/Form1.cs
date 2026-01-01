using System;
using System.Windows.Forms;

namespace TranslatorApp // Proje isminle aynı olmalı
{
    public partial class Form1 : Form
    {
        // 3. Aşamada yazdığımız Servis sınıfından bir örnek (nesne) oluşturuyoruz.
        // OOP Mantığı: İşi yapacak "işçiyi" işe alıyoruz.
        TranslationService _translationService;

        public Form1()
        {
            InitializeComponent();
            _translationService = new TranslationService();
        }

        // Form açıldığında çalışacak kodlar
        private void Form1_Load(object sender, EventArgs e)
        {
            // Dil seçeneklerini kutulara ekleyelim
            // ValueMember ve DisplayMember mantığı ile de yapılabilir ama basit tutalım:

            // Kaynak Dil (Hangi dilden?)
            cmbFrom.Items.Add("en"); // İngilizce
            cmbFrom.Items.Add("tr"); // Türkçe
            cmbFrom.Items.Add("it"); // İtalyanca
            cmbFrom.Items.Add("fr"); // Fransızca
            cmbFrom.SelectedIndex = 0; // İlkini seçili yap (en)

            // Hedef Dil (Hangi dile?)
            cmbTo.Items.Add("tr"); // Türkçe
            cmbTo.Items.Add("en"); // İngilizce
            cmbTo.Items.Add("it"); // İtalyanca
            cmbTo.Items.Add("fr"); // Fransızca
            cmbTo.SelectedIndex = 0; // İlkini seçili yap (tr)
        }

        // "Çevir" butonuna basınca çalışacak kodlar
        // DİKKAT: 'async' kelimesi servisin cevabını bekleyebilmek için gerekli.
        private async void btnTranslate_Click(object sender, EventArgs e)
        {
            // 1. Girdileri kontrol et
            string metin = txtSource.Text;
            string kaynakDil = cmbFrom.SelectedItem.ToString();
            string hedefDil = cmbTo.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(metin))
            {
                MessageBox.Show("Lütfen çevrilecek bir metin girin.");
                return;
            }

            // 2. Butonu geçici olarak kapat (üst üste basılmasın)
            btnTranslate.Text = "Çevriliyor...";
            btnTranslate.Enabled = false;

            try
            {
                // 3. Dil ikilisini oluştur (Örn: "en|tr")
                string dilIkilisi = $"{kaynakDil}|{hedefDil}";

                // 4. Servisi çağır ve sonucu bekle (İşçiye emri veriyoruz)
                string sonuc = await _translationService.TranslateText(metin, dilIkilisi);

                // 5. Sonucu ekrana yaz
                txtTarget.Text = sonuc;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                // 6. Butonu eski haline getir
                btnTranslate.Text = "Çevir";
                btnTranslate.Enabled = true;
            }
        }
    }
}