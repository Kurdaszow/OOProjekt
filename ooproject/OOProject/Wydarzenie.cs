    abstract class Wydarzenie
    {
        protected DateTime dataPoczatek;
        protected DateTime dataKoniec;
        protected TimeSpan czasTrwania;
        protected bool potwierdzenie;
        protected string nazwa;
        public Wydarzenie(DateTime start, DateTime end, string nazwa)
        {
            if (end <= start)
            {
                throw new ArgumentException("Data zakończenia musi być późniejsza od daty rozpoczęcia.");
            }
            this.dataPoczatek = start;
            this.dataKoniec = end;
            this.czasTrwania = end - start;
            this.nazwa = nazwa;
        }
        public DateTime GetDataPoczatek(){return dataPoczatek;}
        public DateTime GetDataKoniec(){return dataKoniec;}
        public string GetNazwa(){return nazwa;}
        public TimeSpan GetCzasTrwania(){return czasTrwania;}
        public void UstawTermin(DateTime start, DateTime koniec)
        {
            if (koniec <= start)
            {
                throw new ArgumentException("Data zakończenia musi być późniejsza od daty rozpoczęcia.");
            }
            dataPoczatek = start;
            dataKoniec = koniec;
            czasTrwania = koniec - start;
        }
        public abstract string WyswietlBilet();
        public void ZmienCzasTrwania(TimeSpan nowyCzasTrwania)
        {
            if (nowyCzasTrwania <= TimeSpan.Zero)
            {
                throw new ArgumentException("Czas trwania musi być większy od zera.");
            }
            czasTrwania = nowyCzasTrwania;
            dataKoniec = dataPoczatek + nowyCzasTrwania;
        }
        public abstract void Potwierdz();
        public abstract bool CzyPotwierdzone();
    }

