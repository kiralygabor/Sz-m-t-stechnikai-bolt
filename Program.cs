﻿using System;
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
        static List<string> eszkozok = new List<string>();

        class Bolt
        {
            public string nev;
            public int darabszam;
            public int ar;
            public string kategoria;
            public int azonosito;
            public string[] muszakiParameterek = new string[3];

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

                    /*Console.Write("Azonosító: ");
                    ujAdat.azonosito = Convert.ToInt32(Console.ReadLine());*/

                    Console.WriteLine("Műszaki paraméterek: ");

                    Console.Write("1.: ");
                    ujAdat.muszakiParameterek[0] = Console.ReadLine();

                    Console.Write("2.: ");
                    ujAdat.muszakiParameterek[1] = Console.ReadLine();

                    Console.Write("3.: ");
                    ujAdat.muszakiParameterek[2] = Console.ReadLine();

                    adatok.Add(ujAdat);
                    eszkozok.Add(ujAdat.nev);
                    Console.WriteLine("A termek hozza lett adva az adatbazishoz!");
                    Console.WriteLine();

                    Console.WriteLine("Szeretne meg adatot hozzaadni? (Igen, Nem):");
                    valasz = Console.ReadLine();

                }

                if (valasz == "Nem") 
                {
                    Console.WriteLine("Kilepes az escape gombbal tortenik");
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

                string[] osszes_termek = new string[adatok.Count];

                for (int i = 0; i < adatok.Count; i++)
                {
                    osszes_termek[i] = adatok[i].nev;
                }

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
                    adatok.RemoveAt(kivalasztott);
                    Console.WriteLine("A torles sikeres volt!");
                    Console.WriteLine("Kilepes az escape gombbal tortenik");
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
                Console.Write("Név: ");
                string modositottNev = Console.ReadLine();
                adatok[kivalasztott].nev = modositottNev;

                Console.Write("Ár: ");
                int modositottAr = Convert.ToInt32(Console.ReadLine());
                adatok[kivalasztott].ar = modositottAr;

                Console.Write("Darabszám: ");
                int modositottDarabszam = Convert.ToInt32(Console.ReadLine());
                adatok[kivalasztott].darabszam = modositottDarabszam;

                Console.Write("Kategoria: "); ;
                string modositottKategoria = Console.ReadLine();
                adatok[kivalasztott].kategoria = modositottKategoria;

                /*Console.WriteLine("Azonosító: ");
                int modositottAzonosito = Convert.ToInt32(Console.ReadLine());
                adatok[kivalasztott].darabszam = modositottAzonosito;*/

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
            }

            public static void Vasarlas(List<Bolt> adatok)
            {
                Console.WriteLine("Név: {0} \t Ár: {1} \t Darabszám: {2} \t Paraméterek: {3} {4} {5}", adatok[kivalasztott].nev, adatok[kivalasztott].ar, adatok[kivalasztott].darabszam, adatok[kivalasztott].muszakiParameterek[0], adatok[kivalasztott].muszakiParameterek[1], adatok[kivalasztott].muszakiParameterek[2]);
                Console.WriteLine();
                Console.Write("Mennnyiség: ");
                int vasaroltdb = Convert.ToInt32(Console.ReadLine());

                int raktaron = adatok[kivalasztott].darabszam;

                if (raktaron > vasaroltdb)
                {
                    raktaron = raktaron - vasaroltdb;
                    Console.WriteLine("A vásárlás sikeres.");
                }
                else
                {
                    Console.WriteLine("A vásárlás sikertelen.");
                }
                adatok[kivalasztott].darabszam = raktaron;

            }

        }

        static void Belepes(List<Bolt> adatok)
        {
            kivalasztott = 0;

            string[] menupontok = { "Adatbázis Kezelése", "Terméklista" };


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
                    case ConsoleKey.DownArrow: if (kivalasztott < kategoriak.Length - 1) kivalasztott++; break;
                }
                #endregion

            } while (lenyomott.Key != ConsoleKey.Enter);


            #endregion
            Console.Clear();
            if (kivalasztott == 0)
            {
                AdatbazisKezeles(adatok);
            }
            else
            {
                FoMenu(adatok);
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
                #endregion

                #region Gomblenyomás

                lenyomott = Console.ReadKey();

                switch (lenyomott.Key)
                {
                    case ConsoleKey.UpArrow: if (kivalasztott > 0) kivalasztott--; break;
                    case ConsoleKey.DownArrow: if (kivalasztott < kategoriak.Length - 1) kivalasztott++; break;
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

                #region Gomblenyomás

                lenyomott = Console.ReadKey();

                switch (lenyomott.Key)
                {
                    case ConsoleKey.UpArrow: if (kivalasztott > 0) kivalasztott--; break;
                    case ConsoleKey.DownArrow: if (kivalasztott < kategoriak.Length - 1) kivalasztott++; break;
                }
                #endregion

            } while (lenyomott.Key != ConsoleKey.Enter && lenyomott.Key != ConsoleKey.Escape);


            #endregion
            Console.Clear();

            if (lenyomott.Key == ConsoleKey.Enter)
            {
                Menuopciok(adatok);
            }
            if (lenyomott.Key == ConsoleKey.Escape)
            {
                FoMenu(adatok);
            }


            
            
        }

        static void Menuopciok(List<Bolt> adatok)
        {
            kivalasztott = 0;

            string[] opciok = {"Vásárlás", "Termékleírás"};

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
                    if (i == kivalasztott)
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

                #region Gomblenyomás

                lenyomott = Console.ReadKey();

                switch (lenyomott.Key)
                {
                    case ConsoleKey.UpArrow: if (kivalasztott > 0) kivalasztott--; break;
                    case ConsoleKey.DownArrow: if (kivalasztott < kategoriak.Length - 1) kivalasztott++; break;
                }
                #endregion

            } while (lenyomott.Key != ConsoleKey.Enter && lenyomott.Key != ConsoleKey.Escape);


            #endregion
            Console.Clear();

            if (lenyomott.Key == ConsoleKey.Enter)
            {
                if (kivalasztott == 0)
                {
                    Bolt.Vasarlas(adatok);
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
                adat.azonosito = Convert.ToInt32(sorok[4]);
                string[] sor = sorok[5].Split('#');
                for (int i = 0; i < 2; i++)
                {
                    adat.muszakiParameterek[i] = sor[i];

                }
                adatok.Add(adat);
            }
            sr.Close();


            Belepes(adatok);

            
            /*Console.WriteLine(adatok[0].darabszam);
            Bolt.Vasarlas(0, adatok);
            Console.WriteLine(adatok[0].darabszam);
            

            
            Console.WriteLine("Név: {0} \t Ár: {1} \t Darabszám: {2} \t Paraméterek: {3} {4} {5}", adatok[0].nev, adatok[0].ar, adatok[0].darabszam, adatok[0].muszakiParameterek[0], adatok[0].muszakiParameterek[1], adatok[0].muszakiParameterek[2]);
            Bolt.Modositas(0, adatok);
            Console.WriteLine("Név: {0} \t Ár: {1} \t Darabszám: {2} \t Paraméterek: {3} {4} {5}", adatok[0].nev, adatok[0].ar, adatok[0].darabszam, adatok[0].muszakiParameterek[0], adatok[0].muszakiParameterek[1], adatok[0].muszakiParameterek[2]);
            

            
            Bolt.Torles(0, adatok);
            Bolt.Hozzaadas(adatok);

            for (int i = 0;i < adatok.Count; i++)
            {
                Console.WriteLine("Név: {0}  Ár: {1} Darabszám: {2} Paraméterek: {3} {4} {5}", adatok[i].nev, adatok[i].ar, adatok[i].darabszam, adatok[i].muszakiParameterek[0], adatok[i].muszakiParameterek[1], adatok[i].muszakiParameterek[2]);
            }*/

            for (int i = 0;i < adatok.Count; i++) 
            { 
            
            }
        }
    }
}