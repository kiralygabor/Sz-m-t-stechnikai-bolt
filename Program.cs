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
        static string[] kategoriak_tomb = { "Okostelefon", "Egér", "Nyomtató", "Laptop", "Billentyűzet" };
        static List<string> kategoriak = new List<string>(kategoriak_tomb);
        static List<string> eszkozok = new List<string>();
        static int kivalasztottIndex = 0;
        static double penz = 0;
        static string jelszo = "admin";

        class Bolt
        {
            public string nev;
            public int darabszam;
            public int ar;
            public string kategoria;
            public char avulo;
            public string[] muszakiParameterek = new string[3];

            public static string[] TombFeltoltes(List<Bolt> adatok)
            {
                string[] osszes_termek = new string[adatok.Count];

                for (int i = 0; i < adatok.Count; i++)
                {
                    osszes_termek[i] = adatok[i].nev;
                }

                return osszes_termek;
            }

            public static void Hozzaadas(List<Bolt> adatok)
            {
                string valasz = "Igen";

                while (valasz == "Igen")

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

                    Console.WriteLine("Gyorsan Avuló: ");
                    ujAdat.avulo = Convert.ToChar(Console.ReadLine());

                    Console.WriteLine("Műszaki paraméterek: ");

                    Console.Write("1.: ");
                    ujAdat.muszakiParameterek[0] = Console.ReadLine();

                    Console.Write("2.: ");
                    ujAdat.muszakiParameterek[1] = Console.ReadLine();

                    Console.Write("3.: ");
                    ujAdat.muszakiParameterek[2] = Console.ReadLine();

                    adatok.Add(ujAdat);
                    eszkozok.Add(ujAdat.nev);
                    kategoriak.Add(ujAdat.kategoria);

                    Console.WriteLine("A termék hozzá lett adva az adatbázishoz!");
                    Console.WriteLine();

                    Console.WriteLine("Szeretne még adatot hozzáadni? (Igen, Nem):");
                    valasz = Console.ReadLine();

                }

                if (valasz == "Nem")
                {
                    Console.WriteLine("Visszalépés az escape gombbal történik");
                    ConsoleKeyInfo lenyomott;
                    lenyomott = Console.ReadKey();
                    if (lenyomott.Key == ConsoleKey.Escape)
                    {
                        AdatbazisKezeles(adatok);
                    }
                }


            }

            public static void Torles(List<Bolt> adatok)
            {
                kivalasztott = 0;

                string[] osszes_termek = TombFeltoltes(adatok);


                #region Menü
                ConsoleKeyInfo lenyomott;

                do
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Válasszon az alábbi termékek közül:\n");

                    #region Menü kiírása
                    for (int i = 0; i < osszes_termek.Length; i++)
                    {
                        if (i == kivalasztott)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.WriteLine("\t" + (i + 1) + ") " + osszes_termek[i]);
                    }
                    #endregion

                    #region Gomblenyomás

                    lenyomott = Console.ReadKey();

                    switch (lenyomott.Key)
                    {
                        case ConsoleKey.UpArrow: if (kivalasztott > 0) kivalasztott--; break;
                        case ConsoleKey.DownArrow: if (kivalasztott < osszes_termek.Length - 1) kivalasztott++; break;
                    }
                    #endregion

                } while (lenyomott.Key != ConsoleKey.Enter && lenyomott.Key != ConsoleKey.Escape);


                #endregion
                Console.Clear();
                if (lenyomott.Key == ConsoleKey.Enter)
                {
                    adatok.RemoveAt(kivalasztott);
                    Console.WriteLine("A törlés sikeres volt!");
                    Console.WriteLine("Visszalépés az escape gombbal történik");
                    lenyomott = Console.ReadKey();
                    if (lenyomott.Key == ConsoleKey.Escape)
                    {
                        AdatbazisKezeles(adatok);
                    }
                }
                else
                {
                    Belepes(adatok);
                }

            }

            public static void Modositas(List<Bolt> adatok)
            {
                kivalasztott = 0;

                string[] osszes_termek = TombFeltoltes(adatok);


                #region Menü
                ConsoleKeyInfo lenyomott;

                do
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Válasszon az alábbi lehetőségek közül:\n");

                    #region Menü kiírása
                    for (int i = 0; i < osszes_termek.Length; i++)
                    {
                        if (i == kivalasztott)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.WriteLine("\t" + (i + 1) + ") " + osszes_termek[i]);
                    }
                    #endregion

                    #region Gomblenyomás

                    lenyomott = Console.ReadKey();

                    switch (lenyomott.Key)
                    {
                        case ConsoleKey.UpArrow: if (kivalasztott > 0) kivalasztott--; break;
                        case ConsoleKey.DownArrow: if (kivalasztott < osszes_termek.Length - 1) kivalasztott++; break;
                    }
                    #endregion

                } while (lenyomott.Key != ConsoleKey.Enter && lenyomott.Key != ConsoleKey.Escape);


                #endregion
                Console.Clear();
                if (lenyomott.Key == ConsoleKey.Enter)
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

                    Console.Write("Kategória: "); ;
                    string modositottKategoria = Console.ReadLine();
                    adatok[kivalasztott].kategoria = modositottKategoria;

                    Console.WriteLine("Gyorsan Avuló: ");
                    char modositottAvulo = Convert.ToChar(Console.ReadLine());
                    adatok[kivalasztott].avulo = modositottAvulo;

                    Console.WriteLine("Műszaki paraméterek: ");

                    Console.Write("1.: ");
                    string modositottParameter = Console.ReadLine();
                    adatok[kivalasztott].muszakiParameterek[0] = modositottParameter;

                    Console.Write("2.: ");
                    modositottParameter = Console.ReadLine();
                    adatok[kivalasztott].muszakiParameterek[1] = modositottParameter;

                    Console.Write("3.: ");
                    modositottParameter = Console.ReadLine();
                    adatok[kivalasztott].muszakiParameterek[2] = modositottParameter;

                    Console.WriteLine("Sikeres módosítás!");
                    Console.WriteLine("Visszalépés az escape gombbal történik");
                    lenyomott = Console.ReadKey();

                    if (lenyomott.Key == ConsoleKey.Escape)
                    {
                        AdatbazisKezeles(adatok);
                    }
                }
                else
                {
                    Belepes(adatok);
                }

            }

            public static void Vasarlas(List<Bolt> adatok)
            {
                int kivalasztottIndex = IndexLekeres(kivalasztott, adatok);


                string fogyoban = "";
                double kedvezmeny;

                if (adatok[kivalasztott].darabszam < 10)
                {
                    fogyoban = "(A termék kifogyóban van!)";
                }
                else
                {
                    fogyoban = "";
                }

                if (adatok[kivalasztott].avulo == 'T')
                {
                    kedvezmeny = 0.8;
                }
                else
                {
                    kedvezmeny = 1;
                }

                if (kedvezmeny == 1)
                {
                    Console.WriteLine($"Pénz: {penz} ft");
                    Console.WriteLine();
                    Console.WriteLine("Név: {0} \nÁr: {1} Ft\nDarabszám: {2} {3}", adatok[kivalasztottIndex].nev, adatok[kivalasztottIndex].ar * kedvezmeny, adatok[kivalasztottIndex].darabszam, fogyoban);

                }
                else
                {
                    Console.WriteLine($"Pénz: {penz} ft");
                    Console.WriteLine();
                    Console.WriteLine("Név: {0} \nEredeti Ár: {1} Ft\nAkciós Ár: {2} Ft\nDarabszám: {3} {4}", adatok[kivalasztottIndex].nev, adatok[kivalasztottIndex].ar, adatok[kivalasztottIndex].ar * kedvezmeny, adatok[kivalasztottIndex].darabszam, fogyoban);
                }
                Console.WriteLine();
                Console.Write("Mennnyiség: ");
                int vasaroltdb = Convert.ToInt32(Console.ReadLine());

                int raktaron = adatok[kivalasztottIndex].darabszam;

                if (raktaron >= vasaroltdb && raktaron > 0 && vasaroltdb * adatok[kivalasztottIndex].ar <= penz)
                {
                    raktaron = raktaron - vasaroltdb;
                    Console.WriteLine();
                    Console.WriteLine("A vásárlás sikeres.");
                    if (adatok[kivalasztott].avulo == 'F')
                    {
                        penz -= vasaroltdb * adatok[kivalasztottIndex].ar;
                    }
                    else
                    {
                        penz -= vasaroltdb * (adatok[kivalasztottIndex].ar * kedvezmeny);
                    }
                    Console.WriteLine("Kilépés az escape gombbal történik.");
                }
                else if (raktaron < vasaroltdb)
                {
                    do
                    {
                        Console.WriteLine();
                        Console.WriteLine("Nincs elég a raktáron!");
                        Console.WriteLine();
                        Console.Write("Mennnyiség: ");
                        vasaroltdb = Convert.ToInt32(Console.ReadLine());

                    } while (raktaron < vasaroltdb);

                    Console.WriteLine("A vásárlás sikeres.");
                    if (adatok[kivalasztott].avulo == 'F')
                    {
                        penz -= vasaroltdb * adatok[kivalasztottIndex].ar;
                    }
                    else
                    {
                        penz -= vasaroltdb * (adatok[kivalasztottIndex].ar * kedvezmeny);
                    }
                    raktaron = raktaron - vasaroltdb;
                    Console.WriteLine("Kilépés az escape gombbal történik.");

                }
                else if (raktaron >= vasaroltdb && raktaron > 0 && vasaroltdb * adatok[kivalasztottIndex].ar > penz)
                {
                    do
                    {
                        Console.WriteLine();
                        Console.WriteLine("Nincs elegendő pénzed!");
                        Console.WriteLine();
                        Console.Write("Mennnyiség: ");
                        vasaroltdb = Convert.ToInt32(Console.ReadLine());

                    } while (raktaron >= vasaroltdb && raktaron > 0 && vasaroltdb * adatok[kivalasztottIndex].ar > penz);

                    Console.WriteLine("A vásárlás sikeres.");
                    if (adatok[kivalasztott].avulo == 'F')
                    {
                        penz -= vasaroltdb * adatok[kivalasztottIndex].ar;
                    }
                    else
                    {
                        penz -= vasaroltdb * (adatok[kivalasztottIndex].ar * kedvezmeny);
                    }
                    raktaron = raktaron - vasaroltdb;
                    Console.WriteLine("Kilépés az escape gombbal történik.");

                }
                adatok[kivalasztottIndex].darabszam = raktaron;

                ConsoleKeyInfo lenyomott;

                lenyomott = Console.ReadKey();

                if (lenyomott.Key == ConsoleKey.Escape)
                {
                    AlMenu(adatok);
                }

            }

        }


        static int IndexLekeres(int kivalasztott, List<Bolt> adatok)
        {
            int kivalasztottIndex = 0;
            for (int i = 0; i < adatok.Count; i++)
            {
                if (eszkozok[kivalasztott] == adatok[i].nev)
                {
                    kivalasztottIndex = i;
                    break;
                }
            }
            return kivalasztottIndex;
        }

        static void PenzFeltoltes()
        {

            Console.Write("Adja meg hogy mennyi pénzt szeretne feltölteni: ");
            penz += Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Sikeres pénz feltöltés!");
        }

        static void Belepes(List<Bolt> adatok)
        {
            kivalasztott = 0;

            string[] menupontok = { "Adatbázis Kezelése", "Terméklista", "Pénz feltöltése", "Programból való kilépés" };


            #region Menü
            ConsoleKeyInfo lenyomott;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Válasszon az alábbi lehetőségek közül:\n");

                #region Menü kiírása
                for (int i = 0; i < menupontok.Length; i++)
                {
                    if (i == kivalasztott)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("\t" + (i + 1) + ") " + menupontok[i]);
                }
                #endregion

                #region Gomblenyomás

                lenyomott = Console.ReadKey();

                switch (lenyomott.Key)
                {
                    case ConsoleKey.UpArrow: if (kivalasztott > 0) kivalasztott--; break;
                    case ConsoleKey.DownArrow: if (kivalasztott < menupontok.Length - 1) kivalasztott++; break;
                }
                #endregion

            } while (lenyomott.Key != ConsoleKey.Enter);


            #endregion
            Console.Clear();
            if (kivalasztott == 0)
            {
                Console.WriteLine("Adja meg a jelszót hogy hozzáférjen az adatbázishoz: ");
                string megadottjelszo = Console.ReadLine();
                if (megadottjelszo == jelszo)
                {
                    AdatbazisKezeles(adatok);
                }
                else
                {
                    Console.WriteLine("Jelszó helytelen!\nHozzáférés megtagadva!");
                    Console.WriteLine("Visszaléptetés a főmenübe!");
                    Thread.Sleep(2500);
                    Belepes(adatok);
                }
            }
            else if (kivalasztott == 1)
            {
                FoMenu(adatok);
            }
            else if (kivalasztott == 2)
            {
                PenzFeltoltes();
                Console.WriteLine("Visszalépés az escape gombbal történik.");
                lenyomott = Console.ReadKey();
                if (lenyomott.Key == ConsoleKey.Escape)
                {
                    Belepes(adatok);
                }
            }
            else
            {
                File.Delete("adatok.txt");
                StreamWriter sw = new StreamWriter("adatok.txt");
                sw.WriteLine("Név;Darabszám;Ár;Kategória;GyorsanAvuló;Műszakiparaméterek");
                for (int i = 0; i < adatok.Count; i++)
                {
                    sw.WriteLine(adatok[i].nev + ";" + adatok[i].darabszam + ";" + adatok[i].ar + ";" + adatok[i].kategoria + ";" + adatok[i].avulo + ";" + adatok[i].muszakiParameterek[0] + "#" + adatok[i].muszakiParameterek[1] + "#" + adatok[i].muszakiParameterek[2]);
                }
                sw.Close();
                Environment.Exit(0);
            }


        }

        static void AdatbazisKezeles(List<Bolt> adatok)
        {
            kivalasztott = 0;

            string[] adatbazis = { "Hozzáadás", "Törlés", "Módosítás" };


            #region Menü
            ConsoleKeyInfo lenyomott;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Válasszon az alábbi lehetőségek közül:\n");

                #region Menü kiírása
                for (int i = 0; i < adatbazis.Length; i++)
                {
                    if (i == kivalasztott)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("\t" + (i + 1) + ") " + adatbazis[i]);
                }

                Console.WriteLine();
                Console.WriteLine("Visszalépés az escape gombbal történik:");
                #endregion

                #region Gomblenyomás

                lenyomott = Console.ReadKey();

                switch (lenyomott.Key)
                {
                    case ConsoleKey.UpArrow: if (kivalasztott > 0) kivalasztott--; break;
                    case ConsoleKey.DownArrow: if (kivalasztott < adatbazis.Length - 1) kivalasztott++; break;
                }
                #endregion

            } while (lenyomott.Key != ConsoleKey.Enter && lenyomott.Key != ConsoleKey.Escape);


            #endregion
            Console.Clear();

            if (lenyomott.Key == ConsoleKey.Enter)
            {
                if (kivalasztott == 0)
                {
                    kivalasztott = -1;
                    Bolt.Hozzaadas(adatok);

                }
                else if (kivalasztott == 1)
                {
                    Bolt.Torles(adatok);
                }
                else
                {
                    Bolt.Modositas(adatok);
                }
            }
            if (lenyomott.Key == ConsoleKey.Escape)
            {
                Belepes(adatok);
            }



        }
        static void FoMenu(List<Bolt> adatok)
        {
            kivalasztott = 0;
            eszkozok.Clear();


            #region Menü
            ConsoleKeyInfo lenyomott;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Válasszon az alábbi kategóriák közül:\n");

                #region Menü kiírása
                for (int i = 0; i < kategoriak.Count; i++)
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

                Console.WriteLine();
                Console.WriteLine("Visszalépés az escape gombbal történik:");
                #endregion

                #region Gomblenyomás

                lenyomott = Console.ReadKey();

                switch (lenyomott.Key)
                {
                    case ConsoleKey.UpArrow: if (kivalasztott > 0) kivalasztott--; break;
                    case ConsoleKey.DownArrow: if (kivalasztott < kategoriak.Count - 1) kivalasztott++; break;
                }
                #endregion

            } while (lenyomott.Key != ConsoleKey.Enter && lenyomott.Key != ConsoleKey.Escape);


            #endregion
            Console.Clear();
            if (lenyomott.Key == ConsoleKey.Enter)
            {

                for (int i = 0; i < adatok.Count; i++)
                {
                    if (kategoriak[kivalasztott] == adatok[i].kategoria)
                    {
                        eszkozok.Add(adatok[i].nev);
                    }
                }

                AlMenu(adatok);
            }
            if (lenyomott.Key == ConsoleKey.Escape)
            {
                Belepes(adatok);
            }

        }

        static void AlMenu(List<Bolt> adatok)
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
                for (int i = 0; i < eszkozok.Count; i++)
                {
                    if (i == kivalasztott)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("\t" + (i + 1) + ") " + eszkozok[i]);
                }
                #endregion

                Console.WriteLine();
                Console.WriteLine("Visszalépés az escape gombbal történik:");

                #region Gomblenyomás

                lenyomott = Console.ReadKey();

                switch (lenyomott.Key)
                {
                    case ConsoleKey.UpArrow: if (kivalasztott > 0) kivalasztott--; break;
                    case ConsoleKey.DownArrow: if (kivalasztott < eszkozok.Count - 1) kivalasztott++; break;
                }
                #endregion

            } while (lenyomott.Key != ConsoleKey.Enter && lenyomott.Key != ConsoleKey.Escape);


            #endregion
            Console.Clear();

            if (lenyomott.Key == ConsoleKey.Enter)
            {
                kivalasztottIndex = IndexLekeres(kivalasztott, adatok);
                Menuopciok(adatok);
            }
            if (lenyomott.Key == ConsoleKey.Escape)
            {
                FoMenu(adatok);
            }
        }

        static void Menuopciok(List<Bolt> adatok)
        {
            int pozicio = 0;

            string[] opciok = { "Vásárlás", "Műszaki Paraméterek" };

            #region Menü
            ConsoleKeyInfo lenyomott;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Válasszon az alábbi lehetőségek közül:\n");

                #region Menü kiírása
                for (int i = 0; i < opciok.Length; i++)
                {
                    if (i == pozicio)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("\t" + (i + 1) + ") " + opciok[i]);
                }
                #endregion

                Console.WriteLine();
                Console.WriteLine("Visszalépés az escape gombbal történik:");

                #region Gomblenyomás

                lenyomott = Console.ReadKey();

                switch (lenyomott.Key)
                {
                    case ConsoleKey.UpArrow: if (pozicio > 0) pozicio--; break;
                    case ConsoleKey.DownArrow: if (pozicio < opciok.Length - 1) pozicio++; break;
                }
                #endregion

            } while (lenyomott.Key != ConsoleKey.Enter && lenyomott.Key != ConsoleKey.Escape);


            #endregion
            Console.Clear();

            if (lenyomott.Key == ConsoleKey.Enter)
            {
                if (pozicio == 0)
                {
                    Bolt.Vasarlas(adatok);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Műszaki Paraméterek: \n{adatok[kivalasztottIndex].muszakiParameterek[0]}\n{adatok[kivalasztottIndex].muszakiParameterek[1]}\n{adatok[kivalasztottIndex].muszakiParameterek[2]}");
                    Console.WriteLine();
                    Console.WriteLine("Visszalépés az escape gombbal történik.");
                    lenyomott = Console.ReadKey();
                    if (lenyomott.Key == ConsoleKey.Escape)
                    {
                        AlMenu(adatok);
                    }
                }
            }
            if (lenyomott.Key == ConsoleKey.Escape)
            {
                AlMenu(adatok);
            }

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
                adat.avulo = Convert.ToChar(sorok[4]);
                string[] sor = sorok[5].Split('#');
                for (int i = 0; i <= 2; i++)
                {
                    adat.muszakiParameterek[i] = sor[i];

                }
                adatok.Add(adat);
            }
            sr.Close();

            Belepes(adatok);


        }
    }
}