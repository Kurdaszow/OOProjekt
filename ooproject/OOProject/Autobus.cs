using System;
class Autobus : Przejazd
{
    private string PrzystanekStartowy;
    private string PrzystanekKoncowy;
    private int liczbaPrzystankow;
    private bool czyPotwierdzone;

    public Autobus(DateTime start,DateTime end,string nazwa,string notatka,string miejsce,string przystanekS,string przystanekK,int lprzystankow): base(start, end, nazwa)
    {
        this.notatka = notatka;
        Random rnd = new Random();
        this.kodBiletu = rnd.Next(0, 1000000).ToString("000000");
        this.miejsce = miejsce;
        this.PrzystanekStartowy = przystanekS;
        this.PrzystanekKoncowy = przystanekK;
        this.liczbaPrzystankow = lprzystankow;
    }
    public string GetNotatka() { return notatka; }
    public string GetMiejsce() { return miejsce; }
    public string GetPrzystanekStartowy() { return PrzystanekStartowy; }
    public string GetPrzystanekKoncowy() { return PrzystanekKoncowy; }
    public int GetLiczbaPrzystankow() { return liczbaPrzystankow; }
    public override string ToString()
    {
        return $"Przejazd: Autobus {PrzystanekStartowy} {dataPoczatek}  ->  {PrzystanekKoncowy} {dataKoniec}";
    }
    public void Wyswietl()
    {
        Console.WriteLine(" ");
        Console.WriteLine($"{kodBiletu},{czyPotwierdzone},{PrzystanekKoncowy},{PrzystanekStartowy},{liczbaPrzystankow}");
    }
    //do dokonczenia
    public override string WyswietlBilet()
    {
        return
            $"=== BILET AUTOBUSOWY ===\n" +
            $"Nazwa: {nazwa}\n" +
            $"Kod biletu: {kodBiletu}\n" +
            $"Miejsce: {miejsce}\n" +
            $"Przystanek startowy: {PrzystanekStartowy}\n" +
            $"Przystanek końcowy: {PrzystanekKoncowy}\n" +
            $"Liczba przystanków: {liczbaPrzystankow}\n" +
            $"Start: {dataPoczatek}\n" +
            $"Koniec: {dataKoniec}\n" +
            $"Notatka: {notatka}";
    }
    public override TimeSpan SprawdzOpoznienia(){return new TimeSpan(0,0,0);}

    public override void Potwierdz(){potwierdzenie = true;}
    public override bool CzyPotwierdzone(){return potwierdzenie;}
}