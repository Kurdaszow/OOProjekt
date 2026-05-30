    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Planer
    {
        private DateTime dataPoczatek;
        private DateTime dataKoniec;
        private List<Wydarzenie> listaWydarzen;
        private List<Wydarzenie> listaNoclegi;

        public Planer(DateTime start)
        {
            this.dataPoczatek = start;
            this.dataKoniec = start;

            this.listaWydarzen = new List<Wydarzenie>()
            {
                new ExampleWydarzenie(
                    new DateTime(2026, 5, 29, 14, 0, 0),
                    new DateTime(2026, 5, 29, 15, 0, 0),"wwww"
                ),

                new ExampleWydarzenie(
                    new DateTime(2026, 5, 29, 17, 0, 0),
                    new DateTime(2026, 5, 29, 18, 0, 0),"wjiajo"
                ),

                new ExampleWydarzenie(
                    new DateTime(2026, 5, 29, 20, 0, 0),
                    new DateTime(2026, 5, 29, 21, 0, 0),"wjoajfo"
                ),
                new ExampleWydarzenie(
                    new DateTime(2026, 5, 30, 20, 0, 0),
                    new DateTime(2026, 5, 30, 21, 0, 0),"wj9a9fw"
                ),
            };

            this.listaNoclegi = new List<Wydarzenie>();
        }

        public string WyswietlHarmonogram()
        {
            string message = "";
            foreach(Wydarzenie wydarzenie in listaWydarzen)
            {
                message += $"{wydarzenie.GetNazwa()}: "+$"{wydarzenie.GetDataPoczatek():dd-MM-yyyy HH:mm} -> " + $"{wydarzenie.GetDataKoniec():dd-MM-yyyy HH:mm}\n";
            }
            return message;
        }

        public void WstawWydarzenie()
        {
            // narazie testowe
            Wydarzenie noweWydarzenie = new ExampleWydarzenie(new DateTime(2026, 5, 29, 18, 30, 0),new DateTime(2026, 5, 29, 20, 30, 0),"woaj");

            if (this.listaWydarzen.Count == 0)
            {
                listaWydarzen.Add(noweWydarzenie);
                Console.WriteLine("Dodano pierwsze wydarzenie.");
                AktualizujKoniec();
                return;
            }


            if (SprawdzTermin(noweWydarzenie))
            {
                listaWydarzen.Add(noweWydarzenie);
                Console.WriteLine("Dodano wydarzenie.");
            }
            else
            {
                Console.WriteLine("Termin koliduje z innym wydarzeniem.");
            }
            AktualizujKoniec();
        }
        public void PrzestawWydarzenie(int index)
        {
            if (index < 0 || index >= listaWydarzen.Count)
            {
                Console.WriteLine("Niepoprawny indeks.");
                return;
            }
            Wydarzenie wydarzenie = listaWydarzen[index];
            listaWydarzen.RemoveAt(index);
            List<int> mozliweMiejsca = SprawdzTerminy(wydarzenie.GetCzasTrwania());
            if (mozliweMiejsca.Count == 0)
            {
                Console.WriteLine("Brak wolnych terminów.");
                listaWydarzen.Add(wydarzenie);
                AktualizujKoniec();
                return;
            }
            Console.WriteLine("Możliwe miejsca:");
            foreach (int i in mozliweMiejsca)
            {
                Console.WriteLine($"Index: {i}");
            }
            Console.Write("Wybierz indeks: ");
            int nowyIndex = int.Parse(Console.ReadLine());
            if (!mozliweMiejsca.Contains(nowyIndex))
            {
                Console.WriteLine("Niepoprawny wybór.");
                return;
            }
            DateTime nowyStart;

            if (nowyIndex == 0)
            {
                nowyStart = dataPoczatek;
            }
            else
            {
                nowyStart = listaWydarzen[nowyIndex - 1].GetDataKoniec();
            }
            DateTime nowyKoniec = nowyStart + wydarzenie.GetCzasTrwania();
            Wydarzenie noweWydarzenie =
                new ExampleWydarzenie(nowyStart, nowyKoniec,"jakistekse");
            listaWydarzen.Insert(nowyIndex, noweWydarzenie);
            Console.WriteLine("Przestawiono wydarzenie.");
            AktualizujKoniec();
        }
        public List<int> SprawdzTerminy(TimeSpan time)
        {
            List<int> indeksy = new List<int>();
            for (int i = 0; i < listaWydarzen.Count - 1; i++)
            {
                TimeSpan timebetween =listaWydarzen[i + 1].GetDataPoczatek() - listaWydarzen[i].GetDataKoniec();
                if (timebetween >= time)
                {
                    indeksy.Add(i + 1);
                }
            }
            indeksy.Add(listaWydarzen.Count);
            return indeksy;
        }
        public bool SprawdzTermin(Wydarzenie noweWydarzenie)
        {
            foreach (Wydarzenie wydarzenie in listaWydarzen)
            {
                bool kolizja= false;
                if(noweWydarzenie.GetDataPoczatek() < wydarzenie.GetDataKoniec() && noweWydarzenie.GetDataKoniec() > wydarzenie.GetDataPoczatek()){
                    kolizja = true;
                }
                if (kolizja)
                {
                    return false;
                }
            }
            return true;
        }
    public void AktualizujKoniec()
    {
        dataKoniec = listaWydarzen[listaWydarzen.Count()-1].GetDataKoniec();
    }

    }

    abstract class Wydarzenie
    {
        protected DateTime dataPoczatek;
        protected DateTime dataKoniec;
        protected TimeSpan czasTrwania;
        protected bool potwierdzenie;
        protected string nazwa;

        public DateTime GetDataPoczatek()
        {
            return dataPoczatek;
        }

        public DateTime GetDataKoniec()
        {
            return dataKoniec;
        }
        public string GetNazwa()
        {
            return nazwa;
        }
        public TimeSpan GetCzasTrwania()
        {
            return czasTrwania;
        }


        public abstract void ZmienDlugosc();
        public abstract void ZmienDate();
        public abstract void Potwierdz();
        public abstract bool CzyPotwierdzone();
    }

    class ExampleWydarzenie : Wydarzenie
    {

        public ExampleWydarzenie(DateTime start, DateTime end,string nazwa)
        {
            this.dataPoczatek = start;
            this.dataKoniec = end;
            this.czasTrwania = end - start;
            this.potwierdzenie = false;
            this.nazwa = nazwa;
        }


        public override void ZmienDlugosc() { }

        public override void ZmienDate() { }

        public override void Potwierdz()
        {
            potwierdzenie = true;
        }

        public override bool CzyPotwierdzone()
        {
            return potwierdzenie;
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Planer planer = new Planer(DateTime.Now);

            planer.WstawWydarzenie();
            Console.WriteLine(planer.WyswietlHarmonogram());
            planer.PrzestawWydarzenie(0);
            Console.WriteLine(planer.WyswietlHarmonogram());
        }
    }