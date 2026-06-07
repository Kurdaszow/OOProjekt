using System;
class Samolot : Przejazd
{
    private string lotniskoWylotu;
    private string lotniskoPrzylotu;
    private string Bramka;
    private DateTime terminOdprawy;
    public Samolot(DateTime start,DateTime end,string nazwa,string notatka,string miejsce,string lotniskoW,string lotniskoP,string bramka,DateTime terminOdp): base(start, end, nazwa)
    {
            if (terminOdp >= start)
            {
                throw new ArgumentException(
                    "Termin odprawy musi być wcześniejszy niż data wylotu.");
            }

        this.notatka = notatka;
        Random rnd = new Random();
        this.kodBiletu = rnd.Next(0, 1000000).ToString("000000");
        this.miejsce = miejsce;
        this.lotniskoWylotu = lotniskoW;
        this.lotniskoPrzylotu = lotniskoP;
        this.Bramka = bramka;
    }
    public string GetLotniskoWylotu() { return lotniskoWylotu; }
    public string GetLotniskoPrzylotu() { return lotniskoPrzylotu; }
    public string GetBramka() { return Bramka; }
    public DateTime GetTerminOdprawy() { return terminOdprawy; }
    public string GetNotatka() { return notatka; }
    public string GetMiejsce() { return miejsce; }
    public override string ToString()
    {
        return $"Przejazd: Samolot  {lotniskoWylotu} {dataPoczatek}  ->  {lotniskoPrzylotu} {dataKoniec}";
    }

//TU Zrobic te metody
    public override string WyswietlBilet()
    {
        return
            $"=== BILET LOTNICZY ===\n" +
            $"Nazwa: {nazwa}\n" +
            $"Kod biletu: {kodBiletu}\n" +
            $"Miejsce: {miejsce}\n" +
            $"Lotnisko wylotu: {lotniskoWylotu}\n" +
            $"Lotnisko przylotu: {lotniskoPrzylotu}\n" +
            $"Bramka: {Bramka}\n" +
            $"Odprawa: {terminOdprawy}\n" +
            $"Start: {dataPoczatek}\n" +
            $"Koniec: {dataKoniec}\n" +
            $"Notatka: {notatka}";
    }
    public override TimeSpan SprawdzOpoznienia(){return new TimeSpan(0,0,0);}
    public override void Potwierdz()
    {
        if (DateTime.Now >= terminOdprawy)
        {
            potwierdzenie = true;
            Console.WriteLine("Lot został potwierdzony.");
        }
        else
        {
            Console.WriteLine(
                $"Nie można jeszcze potwierdzić lotu. " +
                $"Odprawa rozpoczyna się: {terminOdprawy}");
        }
    }
    public override bool CzyPotwierdzone(){return potwierdzenie;}
}