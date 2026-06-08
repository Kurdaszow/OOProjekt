    using System;
    using System.Collections.Generic;
    using System.Linq;
    class Program
    {
        static void CzekajNaEnter(string komunikat = "Wciśnij enter aby kontynuować")
        {
            Console.WriteLine();
            Console.WriteLine(komunikat);
            Console.ReadLine();
            Console.Clear();
        }
        static string WczytajWymaganaWartosc(string komunikat)
        {
            while (true)
            {
                Console.Write(komunikat);
                string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) return input;
            Console.WriteLine("Wartość nie może być pusta. Spróbuj ponownie.");
            }
        }
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("===== MENU GŁÓWNE =====");
                Console.WriteLine("1 - Utwórz nowy planer");
                Console.WriteLine("2 - Wczytaj planer z pliku");
                Console.WriteLine("0 - Zakończ program");
                string wyborGlowny = Console.ReadLine();
                if (wyborGlowny == "0")
                {
                    break;
                }
                Planer planer = null;
                try
                {
                    switch (wyborGlowny)
                    {
                        case "1":
                            Console.Write("Data początku planu (yyyy-MM-dd HH:mm): ");
                            DateTime dataStart = DateTime.Parse(Console.ReadLine());
                            planer = new Planer(dataStart);
                            Console.WriteLine("Utworzono planer.");
                            CzekajNaEnter();
                            break;
                        case "2":
                            Console.Write("Ścieżka do pliku: ");
                            string sciezka = Console.ReadLine();
                            planer = new Planer(sciezka);
                            Console.WriteLine("Wczytano planer.");
                            CzekajNaEnter();
                            break;
                        default:
                            Console.WriteLine("Niepoprawna opcja.");
                            CzekajNaEnter();
                            continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}");
                    continue;
                }
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("===== PLANER =====");
                    Console.WriteLine("1 - Dodaj aktywność");
                    Console.WriteLine("2 - Dodaj przejazd");
                    Console.WriteLine("3 - Dodaj nocleg");
                    Console.WriteLine("4 - Wyświetl harmonogram");
                    Console.WriteLine("5 - Przestaw wydarzenie");
                    Console.WriteLine("6 - Zapisz do pliku");
                    Console.WriteLine("7 - Przestaw nocleg");
                    Console.WriteLine("8 - Wyświetl noclegi");
                    Console.WriteLine("0 - Zamknij planer");
                    string wybor = Console.ReadLine();
                    Console.Clear();
                    try
                    {
                        switch (wybor)
                        {
                            case "1":
                            {
                                string nazwa = WczytajWymaganaWartosc("Nazwa: ");
                                string lokalizacja = WczytajWymaganaWartosc("Lokalizacja: ");
                                Console.Write("Opis: ");
                                string opis = Console.ReadLine();
                                Console.Write("Data początku(yyyy-MM-dd HH:mm): ");
                                DateTime start = DateTime.Parse(Console.ReadLine());
                                Console.Write("Data końca(yyyy-MM-dd HH:mm): ");
                                DateTime koniec = DateTime.Parse(Console.ReadLine());
                                planer.WstawWydarzenie(new Aktywnosc(lokalizacja,opis,start,koniec,nazwa));
                                Console.WriteLine("Dodano aktywność.");
                                CzekajNaEnter();
                                break;
                            }
                            case "2":
                            {
                                Console.WriteLine();
                                Console.WriteLine("Rodzaj przejazdu:");
                                Console.WriteLine("1 - Autobus");
                                Console.WriteLine("2 - Samolot");
                                string typPrzejazdu = Console.ReadLine();
                                string nazwa = WczytajWymaganaWartosc("Nazwa: ");
                                Console.Write("Notatka: ");
                                string notatka = Console.ReadLine();
                                string miejsce = WczytajWymaganaWartosc("Miejsce: ");
                                Console.Write("Data początku(yyyy-MM-dd HH:mm): ");
                                DateTime start = DateTime.Parse(Console.ReadLine());
                                Console.Write("Data końca(yyyy-MM-dd HH:mm): ");
                                DateTime koniec = DateTime.Parse(Console.ReadLine());
                                switch (typPrzejazdu)
                                {
                                    case "1":
                                    {
                                        string startPrz = WczytajWymaganaWartosc("Przystanek startowy: ");
                                        string koniecPrz = WczytajWymaganaWartosc("Przystanek końcowy: ");
                                        Console.Write("Liczba przystanków: ");
                                        int liczbaPrzystankow = int.Parse(Console.ReadLine());
                                        planer.WstawWydarzenie(new Autobus(start,koniec,nazwa,notatka,miejsce,startPrz,koniecPrz,liczbaPrzystankow));
                                        Console.WriteLine("Dodano przejazd autobusem.");
                                        CzekajNaEnter();
                                        break;
                                    }
                                    case "2":
                                    {
                                        string lotniskoWylotu = WczytajWymaganaWartosc("Lotnisko wylotu: ");
                                        string lotniskoPrzylotu = WczytajWymaganaWartosc("Lotnisko przylotu: ");
                                        string bramka = WczytajWymaganaWartosc("Bramka: ");
                                        Console.Write("Termin odprawy: ");
                                        DateTime odprawa = DateTime.Parse(Console.ReadLine());
                                        planer.WstawWydarzenie(new Samolot(start,koniec,nazwa,notatka,miejsce,lotniskoWylotu,lotniskoPrzylotu,bramka,odprawa));
                                        Console.WriteLine("Dodano przelot.");
                                        CzekajNaEnter();
                                        break;
                                    }
                                    default:
                                    {
                                        Console.WriteLine("Niepoprawny typ przejazdu.");
                                        break;
                                    }
                                }
                                break;
                            }
                            case "3":
                            {
                                string nazwa = WczytajWymaganaWartosc("Nazwa: ");
                                string lokalizacja = WczytajWymaganaWartosc("Lokalizacja: ");
                                Console.Write("Typ: ");
                                string typ = Console.ReadLine();
                                Console.Write("Notatka: ");
                                string notatka = Console.ReadLine();
                                Console.Write("Data początku(yyyy-MM-dd HH:mm): ");
                                DateTime start = DateTime.Parse(Console.ReadLine());
                                Console.Write("Data końca(yyyy-MM-dd HH:mm): ");
                                DateTime koniec = DateTime.Parse(Console.ReadLine());
                                planer.WstawNocleg(new Nocleg(start,koniec,nazwa,lokalizacja,typ,notatka));
                                Console.WriteLine("Dodano nocleg.");
                                CzekajNaEnter();
                                break;
                            }
                           case "4":
                            {
                                Console.WriteLine(planer.WyswietlHarmonogram());
                                CzekajNaEnter("Wciśnij enter aby wybrać wydarzenie");
                                Console.WriteLine();
                                Console.Write("Podaj indeks wydarzenia (-1 aby wrócić): ");
                                int indeks = int.Parse(Console.ReadLine());
                                if(indeks != -1)
                                {
                                    Wydarzenie wydarzenie = planer.PobierzWydarzenie(indeks);
                                    if(wydarzenie != null)
                                    {
                                        while(true)
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("1 - Wyświetl bilet");
                                            Console.WriteLine("2 - Potwierdź");
                                            Console.WriteLine("3 - Zmień długość");
                                            if (wydarzenie is Przejazd)
                                            {
                                                Console.WriteLine("4 - Sprawdź opóźnienie");
                                            }
                                            Console.WriteLine("5 - Usuń wydarzenie");
                                            Console.WriteLine("0 - Powrót");
                                            string wyborWyd = Console.ReadLine();
                                            switch(wyborWyd)
                                            {
                                                case "1":
                                                    Console.WriteLine(wydarzenie.WyswietlBilet());
                                                    break;
                                                case "2":
                                                    wydarzenie.Potwierdz();
                                                    Console.WriteLine($"Potwierdzone: {wydarzenie.CzyPotwierdzone()}");
                                                    break;
                                                case "3":
                                                {
                                                    Console.Write("Nowy czas trwania (hh:mm): ");
                                                    string input = Console.ReadLine();
                                                    string[] parts = input.Split(':');
                                                    if (parts.Length != 2 ||
                                                        !int.TryParse(parts[0], out int godziny) ||
                                                        !int.TryParse(parts[1], out int minuty))
                                                    {
                                                        Console.WriteLine("Niepoprawny format. Użyj hh:mm");
                                                        break;
                                                    }
                                                    TimeSpan nowyCzas = new TimeSpan(godziny, minuty, 0);
                                                    bool sukces = planer.ZmienCzasTrwaniaWydarzenia(indeks, nowyCzas);
                                                    if (sukces)
                                                    {
                                                        Console.WriteLine("Zmieniono czas trwania wydarzenia.");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Nie można zmienić czasu trwania - kolizja.");
                                                    }
                                                    break;
                                                }
                                                  case "4":
                                                    if (wydarzenie is Przejazd p)
                                                    {
                                                        TimeSpan opoznienie = p.SprawdzOpoznienia();
                                                        Console.WriteLine($"Opóźnienie: {opoznienie.TotalMinutes} minut");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Ta opcja dostępna tylko dla przejazdów.");
                                                    }
                                                    break;
                                                    case "5":
                                                    {
                                                        planer.UsunWydarzenie(indeks);
                                                        Console.WriteLine("Usunięto wydarzenie.");
                                                        CzekajNaEnter();
                                                        goto KoniecMenuWydarzenia;
                                                    }

                                                case "0":
                                                    goto KoniecMenuWydarzenia;

                                                default:
                                                    Console.WriteLine("Niepoprawna opcja.");
                                                    break;
                                            }
                                        }
                                        KoniecMenuWydarzenia:;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Niepoprawny indeks.");
                                    }
                                }
                                break;
                            }
                            case "5":
                            {
                                Console.Write("Indeks wydarzenia: ");
                                int indeks = int.Parse(Console.ReadLine());
                                planer.PrzestawWydarzenie(indeks);
                                break;
                            }
                            case "6":
                            {
                                Console.Write("Ścieżka pliku: ");
                                string sciezka = Console.ReadLine();
                                planer.DoPliku(sciezka);
                                break;
                            }
                            case "7":
                            {
                                Console.Write("Indeks noclegu: ");
                                int indeks = int.Parse(Console.ReadLine());
                                planer.PrzestawNocleg(indeks);
                                break;
                            }
                            case "8":
                            {
                                Console.WriteLine(planer.WyswietlNoclegi());
                                Console.WriteLine();
                                Console.Write("Podaj indeks noclegu (-1 aby wrócić): ");
                                int indeks = int.Parse(Console.ReadLine());
                                if(indeks != -1)
                                {
                                    Wydarzenie nocleg = planer.PobierzNocleg(indeks);
                                    if(nocleg != null)
                                    {
                                        while(true)
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("1 - Wyświetl bilet");
                                            Console.WriteLine("2 - Potwierdź");
                                            Console.WriteLine("3 - Zmień długość");
                                            Console.WriteLine("4 - Zmień lokalizację");
                                            Console.WriteLine("5 - Usuń nocleg");
                                            Console.WriteLine("0 - Powrót");
                                            string wyborNocleg = Console.ReadLine();
                                            switch(wyborNocleg)
                                            {
                                                case "1":
                                                    Console.WriteLine(nocleg.WyswietlBilet());
                                                    break;
                                                case "2":
                                                    nocleg.Potwierdz();
                                                    Console.WriteLine($"Potwierdzone: {nocleg.CzyPotwierdzone()}");
                                                    break;
                                                case "3":
                                                {
                                                    Console.Write("Nowy czas trwania (hh:mm): ");
                                                    string input = Console.ReadLine();
                                                    string[] parts = input.Split(':');
                                                    if (parts.Length != 2 ||
                                                        !int.TryParse(parts[0], out int godziny) ||
                                                        !int.TryParse(parts[1], out int minuty))
                                                    {
                                                        Console.WriteLine("Niepoprawny format. Użyj hh:mm");
                                                        break;
                                                    }
                                                    TimeSpan nowyCzas = new TimeSpan(godziny, minuty, 0);
                                                    bool sukces = planer.ZmienCzasTrwaniaNoclegu(indeks, nowyCzas);
                                                    if (sukces)
                                                    {
                                                        Console.WriteLine("Zmieniono czas trwania noclegu.");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Nie można zmienić czasu trwania - kolizja.");
                                                    }
                                                    break;
                                                }
                                                case "4":
                                                {
                                                    string nowaLok = WczytajWymaganaWartosc("Nowa lokalizacja: ");
                                                    ((Nocleg)nocleg).ZmienLokalizacje(nowaLok);
                                                    break;
                                                }
                                                case "5":
                                                {
                                                    planer.UsunNocleg(indeks);
                                                   Console.WriteLine("Usunięto Nocleg.");
                                                    CzekajNaEnter();
                                                    goto KoniecMenuNoclegu;
                                                }
                                                case "0":
                                                    goto KoniecMenuNoclegu;
                                                default:
                                                    Console.WriteLine("Niepoprawna opcja.");
                                                    break;
                                            }
                                        }
                                        KoniecMenuNoclegu:;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Niepoprawny indeks.");
                                    }
                                }
                                break;
                            }
                            case "0":
                            {
                                Console.WriteLine("Planer został zamknięty.");
                                CzekajNaEnter();
                                planer = null;
                                goto ZamknijPlaner;
                            }
                            default:
                                Console.WriteLine("Niepoprawna opcja.");
                                break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Niepoprawny format danych.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Błąd: {ex.Message}");
                    }

                }
                ZamknijPlaner:;
            }
        }
    }