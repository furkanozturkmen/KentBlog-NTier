namespace KentBlog.UI.Utilities
{
    public class Helpers
    {
        public static string UrlDostuYap(string metin)
        {
            if (string.IsNullOrEmpty(metin)) return "";

            metin = metin.Replace("ç", "c").Replace("ğ", "g").Replace("ı", "i").Replace("ö", "o").Replace("ş", "s").Replace("ü", "u").Replace("İ", "i");
            metin = metin.Replace("Ç", "C").Replace("Ğ", "G").Replace("İ", "I").Replace("Ö", "O").Replace("Ş", "S").Replace("Ü", "U");
            metin = metin.ToLower();
            metin = System.Text.RegularExpressions.Regex.Replace(metin, @"[^a-z0-9\s-]", "");
            metin = System.Text.RegularExpressions.Regex.Replace(metin, @"\s+", "-");
            metin = System.Text.RegularExpressions.Regex.Replace(metin, @"-+", "-");
            return metin;
        }
    }

}
