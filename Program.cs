using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace SzamitastechnikaiBolt
{
    internal class Program
    {
        static int kivalasztott;
        static string[] kategoriak = { "Okostelefon", "Egér", "Nyomtató", "Laptop", "Billentyűzet" };
        class Bolt
        {
            public string nev;
            public int darabszam;
            public int ar;
            public string kategoria;
            public string[] muszakiParameterek = new string[3];

            static void Hozzaadas()
            {
                return;
            }

            static void Torles()
            {
                return;
            }
            static void Modositas()
            {
                return;
            }
            static void Vasarlas()
            {
                return;
            }

           

        }

        static void Menu(List<Bolt> adatok, string[,] p_matrix)
        {
            kivalasztott = 0;

            #region Menü
            ConsoleKeyInfo lenyomott;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Válasszon az alábbi lehetőségek közül:\n");

                #region Menü kiírása
                for (int i = 0; i < kategoriak.Length; i++)
                {
                    if (i == kivalasztott)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("\t" + (i + 1) + ") " + kategoriak[i]);
                }
                #endregion

                #region Gomblenyomás

                lenyomott = Console.ReadKey();

                switch (lenyomott.Key)
                {
                    case ConsoleKey.UpArrow: if (kivalasztott > 0) kivalasztott--; break;
                    case ConsoleKey.DownArrow: if (kivalasztott < kategoriak.Length - 1) kivalasztott++; break;
                }
                #endregion

            } while (lenyomott.Key != ConsoleKey.Enter);


            #endregion
            Console.Clear();

            List<Bolt> lista = new List<Bolt>();
            Bolt termek = new Bolt();


            for (int i = 0; i < adatok.Count; i++)
            {
                if (kategoriak[kivalasztott] == adatok[i].kategoria)
                {
                    termek.nev = adatok[i].nev;
                    termek.darabszam = adatok[i].darabszam;
                    termek.ar = adatok[i].ar;
                    termek.kategoria = adatok[i].kategoria;
                    termek.muszakiParameterek = adatok[i].muszakiParameterek;

                    lista.Add(termek);
    
                }

            }

            string[,] matrix = new string[5, 5];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                matrix[i, 0] = lista[i].nev;
                matrix[i, 1] = lista[i].darabszam.ToString();
                matrix[i, 2] = lista[i].ar.ToString();
                matrix[i, 3] = lista[i].kategoria;
                matrix[i, 4] = lista[i].muszakiParameterek[0];

            }


            Menu(string[,] matrix);

            Thread.Sleep(5000);

           
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


            Menu(adatok);
            Console.WriteLine(kivalasztott);




        }
    }
}