using System;
class Nocleg: Wydarzenie{
    private string lokalizacja;
    private string typ;
    private string nazwa;
    private string notatka;
        public Nocleg(DateTime start,DateTime end,string nazwa,string lokalizacja,string typ,string notatka): base(start, end, nazwa)
        {
            this.lokalizacja = lokalizacja;
            this.typ = typ;
            this.nazwa = nazwa;
            this.notatka = notatka;
            this.potwierdzenie = false;
        }
        public string GetLokalizacja() { return lokalizacja; }
        public string GetTyp() { return typ; }
        public string GetNotatka() { return notatka; }
        public override string WyswietlBilet()
        {
            return$"=== NOCLEG ===\n" +$"Nazwa: {nazwa}\n" +$"Lokalizacja: {lokalizacja}\n" +$"Typ: {typ}\n" +$"Notatka: {notatka}\n" +$"Od: {dataPoczatek}\n" +$"Do: {dataKoniec}"+$"Potwierdzone: {potwierdzenie}";
        }
        public override string ToString()
        {
            return $"Nocleg: {typ} {nazwa} {lokalizacja} {dataPoczatek} -> {dataKoniec}";
        }
        public override void Potwierdz(){potwierdzenie = true;}
        public override bool CzyPotwierdzone(){return potwierdzenie;}
    public void ZmienLokalizacje(string nowaLokalizacja)
    {
        this.lokalizacja = nowaLokalizacja;
        Console.WriteLine($"Lokalizacja została pomyślnie zmieniona na: {this.lokalizacja}");
    }
}