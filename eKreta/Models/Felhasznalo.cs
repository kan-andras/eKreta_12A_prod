using SQLite;

namespace eKreta.Models
{
    public class Felhasznalo
    {
        public Felhasznalo()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FelhasznaloNev { get; set; }
        public string TeljesNev { get; set; }
        public string Jelszo {  get; set; }
        public int Szerepkor { get; set; }
        public string SzerepkorNev => Enum.GetName(typeof(Szerepkor), Szerepkor) ?? "Ismeretlen";

        public Felhasznalo(string felhasznaloNev, string teljesNev, int szerepkor)
        {
            FelhasznaloNev = felhasznaloNev;
            TeljesNev = teljesNev;
            Szerepkor = szerepkor;
        }

        public Felhasznalo(string felhasznaloNev, string teljesNev, string jelszo, int szerepkor)
        {
            FelhasznaloNev = felhasznaloNev;
            TeljesNev = teljesNev;
            Jelszo = jelszo;
            Szerepkor = szerepkor;
        }

    }
}
