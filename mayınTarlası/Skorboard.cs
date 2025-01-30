using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace minesweeper
{
    public class Skorboard
    {
        private List<Oyuncu> oyuncular;
        private const int MaksimumOyuncuSayisi = 10;
        private const string DosyaYolu = "skorlar.txt";

        public Skorboard()
        {
            oyuncular = new List<Oyuncu>();
            SkorlarıYukle();
        }

        public void SkorlariSifirla()
        {
            oyuncular.Clear();
            if (File.Exists(DosyaYolu))
            {
                File.Delete(DosyaYolu);
            }
        }

        public void YeniOyuncuEkle(string oyuncuAdi, int correctFlags, int gecenSure)
        {
            var mevcutOyuncu = oyuncular.FirstOrDefault(o => o.Adi == oyuncuAdi);

            if (mevcutOyuncu != null)
            {
                int yeniSkor = (correctFlags * 1000) / Math.Max(1, gecenSure);
                if (yeniSkor > mevcutOyuncu.Skor)
                {
                    mevcutOyuncu.Guncelle(correctFlags, gecenSure);
                }
            }
            else
            {
                Oyuncu yeniOyuncu = new Oyuncu(oyuncuAdi, correctFlags, gecenSure);
                oyuncular.Add(yeniOyuncu);
            }

            oyuncular = oyuncular.OrderByDescending(o => o.Skor).ToList();

            if (oyuncular.Count > MaksimumOyuncuSayisi)
            {
                oyuncular = oyuncular.Take(MaksimumOyuncuSayisi).ToList();
            }

            SkorlarıKaydet();
        }

        private void SkorlarıKaydet()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(DosyaYolu, false))
                {
                    foreach (var oyuncu in oyuncular)
                    {
                        writer.WriteLine($"{oyuncu.Adi},{oyuncu.CorrectFlags},{oyuncu.GecenSure}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Skor kaydedilirken hata oluştu: {ex.Message}");
            }
        }

        private void SkorlarıYukle()
        {
            if (File.Exists(DosyaYolu))
            {
                try
                {
                    oyuncular.Clear();

                    using (StreamReader reader = new StreamReader(DosyaYolu))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            if (parts.Length == 3)
                            {
                                string oyuncuAdi = parts[0];
                                if (int.TryParse(parts[1], out int correctFlags) &&
                                    int.TryParse(parts[2], out int gecenSure))
                                {
                                    Oyuncu oyuncu = new Oyuncu(oyuncuAdi, correctFlags, gecenSure);
                                    oyuncular.Add(oyuncu);
                                }
                            }
                        }
                    }

                    oyuncular = oyuncular.OrderByDescending(o => o.Skor).Take(MaksimumOyuncuSayisi).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Skorlar yüklenirken hata oluştu: {ex.Message}");
                    oyuncular.Clear();
                }
            }
        }

        public List<Oyuncu> GetEnIyiOyuncular()
        {
            return oyuncular.ToList();
        }
    }

    public class Oyuncu
    {
        public string Adi { get; set; }
        public int CorrectFlags { get; set; }
        public int GecenSure { get; set; }
        private int _skor;

        public int Skor
        {
            get
            {
                if (_skor == 0 && GecenSure > 0)
                {
                    _skor = (CorrectFlags * 1000) / GecenSure;
                }
                return _skor;
            }
        }

        public Oyuncu(string adi, int correctFlags, int gecenSure)
        {
            Adi = adi;
            CorrectFlags = correctFlags;
            GecenSure = Math.Max(1, gecenSure); // Sıfıra bölünmeyi önlemek için ekledim unutma
            _skor = (CorrectFlags * 1000) / GecenSure;
        }

        public void Guncelle(int correctFlags, int gecenSure)
        {
            CorrectFlags = correctFlags;
            GecenSure = Math.Max(1, gecenSure);
            _skor = (CorrectFlags * 1000) / GecenSure;
        }

        public override string ToString()
        {
            return $"{Adi} - {Skor} puan";
        }
    }
}


