using System;
class Aktywnosc : Wydarzenie
{
    private string lokalizacja;
    private string opis;
    private string kodBiletu;
public Aktywnosc(string lokalizacja, string opis, DateTime start, DateTime koniec, string nazwa) : base(start, koniec, nazwa)
{
    this.lokalizacja = lokalizacja;
    this.opis = opis;
    this.potwierdzenie = false;
    Random rnd = new Random();
    this.kodBiletu = rnd.Next(0, 1000000).ToString("000000");
}
    public override string ToString()
    {
        return $"Aktywność: {nazwa}  {dataPoczatek}  ->  {dataKoniec}";
    }
    public string GetLokalizacja() { return lokalizacja; }
    public string GetOpis() { return opis; }
    public override string WyswietlBilet()
    {
        return$"=== AKTYWNOSC ===\n" +$"Nazwa: {nazwa}\n" +$"Lokalizacja: {lokalizacja}\n" +$"Typ: {opis}\n" +$"Notatka: {kodBiletu}\n" +$"Od: {dataPoczatek}\n" +$"Do: {dataKoniec}\n"+$"Potwierdzone: {potwierdzenie}";
        
    }
    public void ZmienOpis(string opisZmiana)
    {
        Console.WriteLine($"{opis}");
        this.opis = opisZmiana;
        Console.WriteLine($"{opis}");
    }
    public void Wyswietl()
    {
        Console.WriteLine($"{lokalizacja} {opis} {kodBiletu} {potwierdzenie}");
    }
    public override void Potwierdz(){potwierdzenie = true;}
    public override bool CzyPotwierdzone(){return potwierdzenie;}
}