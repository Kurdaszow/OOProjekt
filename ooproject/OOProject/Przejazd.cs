using System;

abstract class Przejazd: Wydarzenie
{
    protected string notatka;
    protected string kodBiletu;
    protected string miejsce;
    protected Przejazd(DateTime start, DateTime end, string nazwa): base(start, end, nazwa){}
    public abstract TimeSpan SprawdzOpoznienia();
}
