using System;
using System.Collections;
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
        static string[] eszkozok = new string[5];
        static bool megnyomott = false;
        class Bolt
        {
            public string nev;
            public int darabszam;
            public int ar;
            public string kategoria;
            public string[] muszakiParameterek = new string[3];

            public static void Hozzaadas(List<Bolt> adatok)
            {

                Bolt ujAdat = new Bolt();

                Console.Write("Név: ");
                ujAdat.nev = Console.ReadLine();

                Console.Write("Darabszám: ");
                ujAdat.darabszam = Convert.ToInt32(Console.ReadLine());

                Console.Write("Ár: ");
                ujAdat.ar = Convert.ToInt32(Console.ReadLine());

                Console.Write("Kategória: ");
                ujAdat.kategoria = Console.ReadLine();

                Console.WriteLine("Műszaki paraméterek: ");

                Console.Write("1.: ");
                ujAdat.muszakiParameterek[0] = Console.ReadLine();

                Console.Write("2.: ");
                ujAdat.muszakiParameterek[1] = Console.ReadLine();

                Console.Write("3.: ");
                ujAdat.muszakiParameterek[2] = Console.ReadLine();

                adatok.Add(ujAdat);
            }

            public static void Torles(int kivalasztott, List<Bolt> adatok)
            {
                adatok.RemoveAt(kivalasztott);
            }

            public static void Modositas(int kivalasztott, List<Bolt> adatok)
            {
                Console.Write("Név: ");
                string modositottNev = Console.ReadLine();
                adatok[kivalasztott].nev = modositottNev;

                Console.Write("Ár: ");
                int modositottAr = Convert.ToInt32(Console.ReadLine());
                adatok[kivalasztott].ar = modositottAr;

                Console.Write("Darabszám: ");
                int modositottDarabszam = Convert.ToInt32(Console.ReadLine());
                adatok[kivalasztott].darabszam = modositottDarabszam;

                Console.WriteLine("Műszaki paraméterek: ");

                Console.Write("1.: ");
                string modositottParameter = Console.ReadLine();
                adatok[kivalasztott].muszakiParameterek[0] = modositottParameter;

                Console.Write("2.: ");
                modositottParameter = Console.ReadLine();
                adatok[kivalasztott].muszakiParameterek[1] = modositottParameter;

                Console.Write("2.: ");
                modositottParameter = Console.ReadLine();
                adatok[kivalasztott].muszakiParameterek[2] = modositottParameter;

                Console.WriteLine("Sikeres módosítás!");
            }

            public static void Vasarlas(int kivalasztott, List<Bolt> adatok)
            {
                Console.Write("Mennnyiség: ");
                int vasaroltdb = Convert.ToInt32(Console.ReadLine());

                int raktaron = adatok[kivalasztott].darabszam;

                if (raktaron > vasaroltdb)
                {
                    raktaron = raktaron - vasaroltdb;
                    Console.WriteLine("A vásárlás sikreres.");
                }
                else
                {
                    Console.WriteLine("A vásárlás sikertelen.");
                }
                adatok[kivalasztott].darabszam = raktaron;

            }



        }

        static void Menu(List<Bolt> adatok)
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
                    if (megnyomott == false) 
                    {
                        Console.WriteLine("\t" + (i + 1) + ") " + kategoriak[i]);
                    }
                    else if (megnyomott == true)
                    {
                        Console.WriteLine("\t" + (i + 1) + ") " + eszkozok[i]);
                    }
                   
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

            megnyomott = true;
            int index = 0;

            for (int i = 0; i < adatok.Count; i++)
            {
                if (kategoriak[kivalasztott] == adatok[i].kategoria)
                {
                    eszkozok[index] = adatok[i].nev;
                    index++;
                }

            }

            for (int i = 0; i < eszkozok.Length; i++)
            {
                Console.WriteLine(eszkozok[i]);
            }


            Menu(adatok);

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

            /*
            Console.WriteLine(adatok[0].darabszam);
            Bolt.Vasarlas(0, adatok);
            Console.WriteLine(adatok[0].darabszam);
            */

            /*
            Console.WriteLine("Név: {0} \t Ár: {1} \t Darabszám: {2} \t Paraméterek: {3} {4} {5}", adatok[0].nev, adatok[0].ar, adatok[0].darabszam, adatok[0].muszakiParameterek[0], adatok[0].muszakiParameterek[1], adatok[0].muszakiParameterek[2]);
            Bolt.Modositas(0, adatok);
            Console.WriteLine("Név: {0} \t Ár: {1} \t Darabszám: {2} \t Paraméterek: {3} {4} {5}", adatok[0].nev, adatok[0].ar, adatok[0].darabszam, adatok[0].muszakiParameterek[0], adatok[0].muszakiParameterek[1], adatok[0].muszakiParameterek[2]);
            */

            /*
            Bolt.Torles(0, adatok);
            Bolt.Hozzaadas(adatok);

            for (int i = 0;i < adatok.Count; i++)
            {
                Console.WriteLine("Név: {0}  Ár: {1} Darabszám: {2} Paraméterek: {3} {4} {5}", adatok[i].nev, adatok[i].ar, adatok[i].darabszam, adatok[i].muszakiParameterek[0], adatok[i].muszakiParameterek[1], adatok[i].muszakiParameterek[2]);
            }

            */

        }
    }
}