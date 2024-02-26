using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SzamitastechnikaiBolt
{
    internal class Program
    {
        class Bolt
        {
            public string nev;
            public int darabszam;
            public int ar;
            public string kategoria;
            public string[] muszakiParameterek = new string[3];
        }
        static void Main(string[] args)
        {
            List<Bolt> adatok = new List<Bolt>();

            StreamReader sr = new StreamReader("adatok.txt");
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                string[] sorok = sr.ReadLine().Split(';');
                Bolt adat = new Bolt();
                adat.nev = sorok[0];
                adat.darabszam = Convert.ToInt32(sorok[1]);
                adat.ar = Convert.ToInt32(sorok[2]);
                adat.kategoria = sorok[3];
                string[] sor = sorok[4].Split('#');
                for (int i = 0; i < 2; i++)
                {
                    adat.muszakiParameterek[i] = sor[i];

                }
                adatok.Add(adat);
            }
            sr.Close();

            for (int i = 0; i < adatok.Count; i++)
            {
                Console.WriteLine("Név: {0}\t Darabszám: {1}\t Ár: {2}\t Kategória: {3}\t Paraméterek: {4}", adatok[i].nev, adatok[i].darabszam, adatok[i].ar, adatok[i].kategoria, adatok[i].muszakiParameterek[0]);
            }
        }
    }
}