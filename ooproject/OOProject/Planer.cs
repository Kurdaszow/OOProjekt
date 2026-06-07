using System;
using System.Collections.Generic;
using System.IO;
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
            this.listaWydarzen = new List<Wydarzenie>();
            this.listaNoclegi = new List<Wydarzenie>();
        }
        public Planer(string sciezka)
        {
            listaWydarzen = new List<Wydarzenie>();
            listaNoclegi = new List<Wydarzenie>();
            if(!File.Exists(sciezka)) throw new FileNotFoundException();
            string[] linie = File.ReadAllLines(sciezka);
            foreach(string linia in linie)
            {
                string[] dane = linia.Split(';');
                switch(dane[0])
                {
                    case "AKTYWNOSC":
                        listaWydarzen.Add(new Aktywnosc(dane[1],dane[2],DateTime.Parse(dane[3]),DateTime.Parse(dane[4]),dane[5]));
                        break;
                    case "AUTOBUS":
                        listaWydarzen.Add(new Autobus(DateTime.Parse(dane[1]),DateTime.Parse(dane[2]),dane[3],dane[4],dane[5],dane[6],dane[7],int.Parse(dane[8])));
                        break;
                    case "SAMOLOT":
                        listaWydarzen.Add(new Samolot(DateTime.Parse(dane[1]),DateTime.Parse(dane[2]),dane[3],dane[4],dane[5],dane[6],dane[7],dane[8],DateTime.Parse(dane[9])));
                        break;
                    case "NOCLEG":
                        listaNoclegi.Add(new Nocleg(DateTime.Parse(dane[1]),DateTime.Parse(dane[2]),dane[3],dane[4],dane[5],dane[6]));
                        break;
                }
            }
            if(listaWydarzen.Count > 0)
            {
                dataPoczatek = listaWydarzen[0].GetDataPoczatek();
                AktualizujKoniec();
            }
        }

        public string WyswietlHarmonogram()
        {
            string message = "";
            for(int i = 0; i < listaWydarzen.Count; i++)
            {
                message += $"[{i}] {listaWydarzen[i]}\n";
            }
            return message;
        }
        public void DoPliku(string sciezka)
        {
            try
            {
                using(StreamWriter sw = new StreamWriter(sciezka))
                {
                    foreach(Wydarzenie w in listaWydarzen)
                    {
                        if(w is Aktywnosc a)
                        {
                            sw.WriteLine($"AKTYWNOSC;" +$"{a.GetLokalizacja()};" +$"{a.GetOpis()};" +$"{a.GetDataPoczatek()};" +$"{a.GetDataKoniec()};" +$"{a.GetNazwa()}");
                        }
                        else if(w is Autobus ab)
                        {
                            sw.WriteLine($"AUTOBUS;" +$"{ab.GetDataPoczatek()};" +$"{ab.GetDataKoniec()};" +$"{ab.GetNazwa()};" +$"{ab.GetNotatka()};" +$"{ab.GetMiejsce()};" +$"{ab.GetPrzystanekStartowy()};" +$"{ab.GetPrzystanekKoncowy()};" +$"{ab.GetLiczbaPrzystankow()}");
                        }
                        else if(w is Samolot s)
                        {
                            sw.WriteLine($"SAMOLOT;" +$"{s.GetDataPoczatek()};" +$"{s.GetDataKoniec()};" +$"{s.GetNazwa()};" +$"{s.GetNotatka()};" +$"{s.GetMiejsce()};" +$"{s.GetLotniskoWylotu()};" +$"{s.GetLotniskoPrzylotu()};" +$"{s.GetBramka()};" +$"{s.GetTerminOdprawy()}");
                        }
                    }
                    foreach(Wydarzenie n in listaNoclegi)
                    {
                        Nocleg nocleg = (Nocleg)n;
                        sw.WriteLine($"NOCLEG;" +$"{nocleg.GetDataPoczatek()};" +$"{nocleg.GetDataKoniec()};" +$"{nocleg.GetNazwa()};" +$"{nocleg.GetLokalizacja()};" +$"{nocleg.GetTyp()};" +$"{nocleg.GetNotatka()}");
                    }
                }
                Console.WriteLine("Zapisano.");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public string WyswietlNoclegi()
        {
            string wynik = "";
            for(int i = 0; i < listaNoclegi.Count; i++)
            {
                wynik += $"[{i}] {listaNoclegi[i]}\n";
            }
            return wynik;
}
        public Wydarzenie PobierzWydarzenie(int indeks)
        {
            if(indeks < 0 || indeks >= listaWydarzen.Count)
                return null;
            return listaWydarzen[indeks];
        }
        public Wydarzenie PobierzNocleg(int indeks)
        {
            if(indeks < 0 || indeks >= listaNoclegi.Count)
                return null;

            return listaNoclegi[indeks];
        }
        public void WstawWydarzenie(Wydarzenie wydarzenie)
        {
            try
            {
                if (wydarzenie.GetDataPoczatek() < dataPoczatek)
                {
                    Console.WriteLine("Wydarzenie nie może zaczynać się przed początkiem planu.");
                    return;
                }
                if (!SprawdzTermin(wydarzenie))
                {
                    Console.WriteLine("Termin koliduje z innym wydarzeniem.");
                    return;
                }
                int indeks = 0;
                while (indeks < listaWydarzen.Count &&
                    listaWydarzen[indeks].GetDataPoczatek() <
                    wydarzenie.GetDataPoczatek())
                {
                    indeks++;
                }

                listaWydarzen.Insert(indeks, wydarzenie);
                Console.WriteLine("Dodano wydarzenie.");
                AktualizujKoniec();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
        }
        public void WstawNocleg(Wydarzenie nocleg)
        {
            try
            {

                if (nocleg.GetDataPoczatek() < dataPoczatek)
                {
                    Console.WriteLine("Nocleg nie może zaczynać się przed początkiem planu.");
                    return;
                }
                if (!SprawdzTerminNoclegu(nocleg))
                {
                    Console.WriteLine("Termin noclegu koliduje z innym noclegiem.");
                    return;
                }
                int indeks = 0;
                while (indeks < listaNoclegi.Count &&
                    listaNoclegi[indeks].GetDataPoczatek() < nocleg.GetDataPoczatek())
                {
                    indeks++;
                }
                listaNoclegi.Insert(indeks, nocleg);
                Console.WriteLine("Dodano nocleg.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
        }
        private List<int> SprawdzTerminyNoclegow(TimeSpan time)
        {
            List<int> indeksy = new List<int>();
            for (int i = 0; i < listaNoclegi.Count - 1; i++)
            {
                TimeSpan czasPomiedzy = listaNoclegi[i + 1].GetDataPoczatek() - listaNoclegi[i].GetDataKoniec();
                if (czasPomiedzy >= time)
                {
                    indeksy.Add(i + 1);
                }
            }
            indeksy.Add(listaNoclegi.Count);
            return indeksy;
        }
        public void UsunWydarzenie(int index)
        {
            if (index < 0 || index >= listaWydarzen.Count)
            {
                Console.WriteLine("Niepoprawny indeks.");
                return;
            }

            listaWydarzen.RemoveAt(index);
            AktualizujKoniec();
            Console.WriteLine("Usunięto wydarzenie.");
        }

        public void UsunNocleg(int index)
        {
            if (index < 0 || index >= listaNoclegi.Count)
            {
                Console.WriteLine("Niepoprawny indeks.");
                return;
            }

            listaNoclegi.RemoveAt(index);
            Console.WriteLine("Usunięto nocleg.");
        }
        public void PrzestawNocleg(int index)
        {
            if (index < 0 || index >= listaNoclegi.Count)
            {
                Console.WriteLine("Niepoprawny indeks.");
                return;
            }
            Wydarzenie nocleg = listaNoclegi[index];
            try
            {
                listaNoclegi.RemoveAt(index);
                List<int> mozliweMiejsca = SprawdzTerminyNoclegow(nocleg.GetCzasTrwania());
                if (mozliweMiejsca.Count == 0)
                {
                    Console.WriteLine("Brak wolnych terminów.");
                    listaNoclegi.Insert(index, nocleg);
                    return;
                }
                Console.WriteLine("Możliwe miejsca:");
                foreach (int i in mozliweMiejsca)
                {
                    Console.WriteLine($"Index: {i}");
                }
                Console.Write("Wybierz indeks: ");
                if (!int.TryParse(Console.ReadLine(), out int nowyIndex))
                {
                    Console.WriteLine("Podano niepoprawną liczbę.");
                    listaNoclegi.Insert(index, nocleg);
                    return;
                }
                if (!mozliweMiejsca.Contains(nowyIndex))
                {
                    Console.WriteLine("Niepoprawny wybór.");
                    listaNoclegi.Insert(index, nocleg);
                    return;
                }
                DateTime nowyStart;
                if (nowyIndex == 0)
                {
                    nowyStart = dataPoczatek;
                }
                else
                {
                    nowyStart =listaNoclegi[nowyIndex - 1].GetDataKoniec();
                }
                DateTime nowyKoniec = nowyStart + nocleg.GetCzasTrwania();
                nocleg.UstawTermin(nowyStart,nowyKoniec);
                listaNoclegi.Insert(nowyIndex,nocleg);
                Console.WriteLine("Przestawiono nocleg.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                if (!listaNoclegi.Contains(nocleg))
                {
                    if (index <= listaNoclegi.Count) listaNoclegi.Insert(index, nocleg);
                    else listaNoclegi.Add(nocleg);
                }
            }
        }
        public void PrzestawWydarzenie(int index)
        {
            if (index < 0 || index >= listaWydarzen.Count)
            {
                Console.WriteLine("Niepoprawny indeks.");
                return;
            }
            Wydarzenie wydarzenie = listaWydarzen[index];
            try
            {
                listaWydarzen.RemoveAt(index);
                List<int> mozliweMiejsca =SprawdzTerminy(wydarzenie.GetCzasTrwania());
                if (mozliweMiejsca.Count == 0)
                {
                    Console.WriteLine("Brak wolnych terminów.");
                    listaWydarzen.Insert(index, wydarzenie);
                    AktualizujKoniec();
                    return;
                }
                Console.WriteLine("Możliwe miejsca:");
                foreach (int i in mozliweMiejsca)
                {
                    Console.WriteLine($"Index: {i}");
                }
                Console.Write("Wybierz indeks: ");
                if (!int.TryParse(Console.ReadLine(), out int nowyIndex))
                {
                    Console.WriteLine("Podano niepoprawną liczbę.");
                    listaWydarzen.Insert(index, wydarzenie);
                    AktualizujKoniec();
                    return;
                }
                if (!mozliweMiejsca.Contains(nowyIndex))
                {
                    Console.WriteLine("Niepoprawny wybór.");
                    listaWydarzen.Insert(index, wydarzenie);
                    AktualizujKoniec();
                    return;
                }
                DateTime nowyStart;
                if (nowyIndex == 0)
                {
                    nowyStart = dataPoczatek;
                }
                else
                {
                    nowyStart =listaWydarzen[nowyIndex - 1].GetDataKoniec();
                }
                DateTime nowyKoniec = nowyStart + wydarzenie.GetCzasTrwania();
                wydarzenie.UstawTermin(nowyStart,nowyKoniec);
                listaWydarzen.Insert(nowyIndex,wydarzenie);
                Console.WriteLine("Przestawiono wydarzenie.");
                AktualizujKoniec();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                if (!listaWydarzen.Contains(wydarzenie))
                {
                    if (index <= listaWydarzen.Count) listaWydarzen.Insert(index, wydarzenie);
                    else listaWydarzen.Add(wydarzenie);
                }

                AktualizujKoniec();
            }
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
        private bool SprawdzTerminNoclegu(
            Wydarzenie nowyNocleg)
        {
            foreach(Wydarzenie nocleg in listaNoclegi)
            {
                if(nowyNocleg.GetDataPoczatek() < nocleg.GetDataKoniec() && nowyNocleg.GetDataKoniec() > nocleg.GetDataPoczatek())
                {
                    return false;
                }
            }
            return true;
        }
        public bool ZmienCzasTrwaniaNoclegu(int index,TimeSpan nowyCzasTrwania)
        {
            if(index < 0 || index >= listaNoclegi.Count) return false;
            Wydarzenie nocleg = listaNoclegi[index];
            DateTime staryKoniec = nocleg.GetDataKoniec();
            DateTime nowyKoniec = nocleg.GetDataPoczatek() + nowyCzasTrwania;
            listaNoclegi.RemoveAt(index);
            nocleg.UstawTermin(nocleg.GetDataPoczatek(),nowyKoniec);
            bool poprawnyTermin =SprawdzTerminNoclegu(nocleg);
            if(poprawnyTermin)
            {
                nocleg.ZmienCzasTrwania(nowyCzasTrwania);
                listaNoclegi.Insert(index, nocleg);
                return true;
            }
            nocleg.UstawTermin(nocleg.GetDataPoczatek(),staryKoniec);
            listaNoclegi.Insert(index, nocleg);
            return false;
        }
        public bool ZmienCzasTrwaniaWydarzenia(int index,TimeSpan nowyCzasTrwania)
        {
            if(index < 0 || index >= listaWydarzen.Count) return false;
            Wydarzenie wydarzenie = listaWydarzen[index];
            DateTime staryKoniec = wydarzenie.GetDataKoniec();
            DateTime nowyKoniec = wydarzenie.GetDataPoczatek() + nowyCzasTrwania;
            listaWydarzen.RemoveAt(index);
            wydarzenie.UstawTermin(wydarzenie.GetDataPoczatek(),nowyKoniec);
            bool poprawnyTermin = SprawdzTermin(wydarzenie);
            if(poprawnyTermin)
            {
                wydarzenie.ZmienCzasTrwania(nowyCzasTrwania);
                listaWydarzen.Insert(index, wydarzenie);
                AktualizujKoniec();
                return true;
            }
            wydarzenie.UstawTermin(wydarzenie.GetDataPoczatek(),staryKoniec);
            listaWydarzen.Insert(index, wydarzenie);
            return false;
        }
        public void AktualizujKoniec()
        {
            if(listaWydarzen.Count > 0) dataKoniec = listaWydarzen[listaWydarzen.Count - 1].GetDataKoniec();
            else dataKoniec = dataPoczatek;
        }

    }