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
            public int arDollarban;
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
                adat.arDollarban = Convert.ToInt32(sorok[2]);
                adat.kategoria = sorok[3];
                string[] sor = sorok[4].Split('#');
                for (int i = 0; i < 3; i++)
                {
                    adat.muszakiParameterek[i] = sor[i];

                }
                adatok.Add(adat);
            }
            sr.Close();

            Console.WriteLine(adatok.Count);
        }
    }
}
