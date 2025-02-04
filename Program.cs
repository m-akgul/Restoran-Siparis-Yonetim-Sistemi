using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran_Siparis_Yonetim_Sistemi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*

            1- Masa Ac 
            Bugun aktif olan masalar secilir.
            Restoranda 3 tane disarida, 4 tane iceride masa var ise ve bugun yagmurlu ise disaridaki 3 masa kullanilamaz.

            2- Masa Islem
            Musteri geldiginde bos olan masalardan biri secilir.
            Sonra musterinin alacagı icecek vs girilir.

            3- Masa Hesap
            Masa numarasi ile musterinin ne aldigina bakilacak.
            Odeme alinip masa doludan bos hale getirilecek.

            4- Kasa Islemleri
            Her bir masanin kazandirdigi miktar ve toplam kazanc gösterilecek.

            */

            int islem; // Ana menu islem secimi
            int masaSayisi = 7; // Toplam masa sayisi
            string[] masaDurum = new string[masaSayisi]; // Masa durumlarini gösteren dizi dolu/bos
            bool masaAktif = false; // Masa Ac ile masa aktiflestirme yapilmis mi kontrolu
            int aktifBolge = 0; // Aktif bolge secimi
            // Siparisleri yazdirmak icin dizi
            string[] menu = new string[17] {"Mercimek Çorbası   120TL",
                                            "Ezogelin Çorbası   120TL",
                                            "Domates Çorbası    120TL",
                                            "Mantar Çorbası     130TL",
                                            "Yeşil Salata       180TL",
                                            "Sezar Salata       285TL",
                                            "Falafel Salata     245TL",
                                            "İzmir Köfte        120TL",
                                            "Hünkar Beğendi     408TL",
                                            "Et Kavurma         330TL",
                                            "Kazandibi          128TL",
                                            "Fırın Sütlaç       100TL",
                                            "Profiterol         130TL",
                                            "Künefe             250TL",
                                            "Ayran              45TL",
                                            "Şalgam             45TL",
                                            "Soda               30TL"};
            int[] menuFiyat = new int[17] { 120, 120, 120, 130, 180, 285, 245, 120, 408, 330, 128, 100, 130, 250, 45, 45, 30 }; // Urun fiyatlari
            string[][] masaSecilenUrunler = new string[masaSayisi][]; // Masanin siparisleri
            int[][] masaHesap = new int[masaSayisi][]; // Masanin hesabi
            int[] secilenUrunSayisi = new int[masaSayisi]; // Masanin sectigi urun sayisi
            int[] masaToplamHesap = new int[masaSayisi]; // Masanin toplam hesabi
            int toplamHesap = 0; // Restoranin toplam hesabi

        /* ----------------------------------- ANA MENU BASLANGIC ----------------------------------- */
        anaMenu:
            Console.BackgroundColor = ConsoleColor.Yellow; // Arka plani sari yap
            Console.Clear();

        anaMenuHataliGiris:
            Console.ForegroundColor = ConsoleColor.DarkGreen; // Ana Menu yesil renkte yazdir
            Console.WriteLine(
                "                    ANA MENÜ");
            Console.ForegroundColor = ConsoleColor.Black; // Secimleri siyah renkte yazdir
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(
                "Masa Aç          [1]\n" +
                "Masa İşlem       [2]\n" +
                "Masa Hesap       [3]\n" +
                "Kasa İşlemleri   [4]");
            Console.WriteLine("--------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.DarkBlue; // Cikis yap mavi renkte yazdir 
            Console.WriteLine("ÇIKIŞ YAP        [0]");
            Console.ForegroundColor = ConsoleColor.Black; // Cizgiyi siyah yazdır
            Console.WriteLine("--------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red; // Secimi ve gerekirse hatalari kirmizi renkte yazdir
            Console.Write("Seçiminiz: ");

            try
            {
                islem = int.Parse(Console.ReadKey().KeyChar.ToString());
                // Menu secenekleri disinda girilirse ana menuye don
                if (islem != 0 && islem != 1 && islem != 2 && islem != 3 && islem != 4)
                {
                    Console.Clear();
                    Console.WriteLine("Geçersiz Giriş!\n");
                    goto anaMenuHataliGiris;
                }
                // Acilis yapilmadan baska islem secilirse ana menuye don
                else if (islem != 0 && islem != 1 && !masaAktif)
                {
                    Console.Clear();
                    Console.WriteLine("Önce Masa Aç menüsünden bugün açık olacak masaları belirtmelisiniz!\n");
                    goto anaMenuHataliGiris;
                }
                // Acilis birden fazla kez aktiflestirilmeye calisilirsa ana menuye don
                else if (islem == 1 && masaAktif)
                {
                    Console.Clear();
                    Console.WriteLine("Bugün masa açma işlemi zaten yapıldı!\n");
                    goto anaMenuHataliGiris;
                }
            }
            catch (FormatException)
            {
                // Harf girilirse ana menuye don
                Console.Clear();
                Console.WriteLine("Menüde bulunan sayılardan (1, 2, 3, 4, 0) giriniz!\n");
                goto anaMenuHataliGiris;
            }
            /* ----------------------------------- ANA MENU BITIS ----------------------------------- */

            switch (islem)
            {
                case 0: // Cikis Yap
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Magenta; // Mesaji mor yazdir
                    Console.WriteLine("Çıkış yapmak için klavyede bir tuşa basınız. Ana Menüye dönmek için ESC tuşuna basınız...");
                    // ESC basinca ANA MENU yerine NA MENU yazıyordu.
                    // Basılan tusun ne oldugu yazildigi icin ve ESC bos bir tus oldugu icin sonraki WriteLine komutundaki ilk harf siliniyordu. intercept: true ile tusun yazılmasını engelledim.
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        goto anaMenu;
                    }
                    return;

                case 1: // Masa Ac
                    Console.BackgroundColor = ConsoleColor.Gray; // Arka plani gri yap
                    Console.Clear();
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                        Console.WriteLine("--------------------------------------------------");
                        Console.ForegroundColor = ConsoleColor.DarkMagenta; // Mor baslik
                        Console.WriteLine("                    MASA AÇ");
                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine(
                            "                   Bugün Açılacak Masalar\n" +
                            "İÇERİ: Masa No 1 - 4                    TERAS: Masa No 5 - 7\n" +
                            "İçeri için          [1]\n" +
                            "Teras için          [2]\n" +
                            "Bütün Masalar için  [3]");
                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                        Console.WriteLine("--------------------------------------------------");
                        Console.ForegroundColor = ConsoleColor.DarkBlue; // Mavi Ana Menu yazisi
                        Console.WriteLine("ANA MENÜ     [ESC]");
                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                        Console.WriteLine("--------------------------------------------------");
                        Console.ForegroundColor = ConsoleColor.DarkMagenta; // Mor giriş yazisi
                        Console.Write("Bugün açılacak masaları seçiniz: ");
                        try
                        {
                            ConsoleKeyInfo giris = Console.ReadKey(true);

                            // ESC basilirsa ana menuye git
                            if (giris.Key == ConsoleKey.Escape)
                            {
                                goto anaMenu;
                            }

                            aktifBolge = int.Parse(giris.KeyChar.ToString());
                            // Gerekli buyuklukte dizi olustur ve icine BOS yaz
                            switch (aktifBolge)
                            {
                                case 1:
                                    masaDurum = new string[4];
                                    masaSecilenUrunler = new string[4][];
                                    secilenUrunSayisi = new int[4];
                                    masaHesap = new int[4][];
                                    masaToplamHesap = new int[4];
                                    masaAktif = true;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        masaDurum[i] = "BOŞ";
                                        secilenUrunSayisi[i] = 0;
                                        masaToplamHesap[i] = 0;
                                    }
                                    break;
                                case 2:
                                    masaDurum = new string[3];
                                    masaSecilenUrunler = new string[3][];
                                    secilenUrunSayisi = new int[3];
                                    masaHesap = new int[3][];
                                    masaToplamHesap = new int[3];
                                    masaAktif = true;
                                    for (int i = 0; i < 3; i++)
                                    {
                                        masaDurum[i] = "BOŞ";
                                        secilenUrunSayisi[i] = 0;
                                        masaToplamHesap[i] = 0;
                                    }
                                    break;
                                case 3:
                                    masaDurum = new string[masaSayisi];
                                    masaSecilenUrunler = new string[masaSayisi][];
                                    secilenUrunSayisi = new int[masaSayisi];
                                    masaHesap = new int[masaSayisi][];
                                    masaToplamHesap = new int[masaSayisi];
                                    masaAktif = true;
                                    for (int i = 0; i < masaSayisi; i++)
                                    {
                                        masaDurum[i] = "BOŞ";
                                        secilenUrunSayisi[i] = 0;
                                        masaToplamHesap[i] = 0;
                                    }
                                    break;
                                default: // Menu secenekleri disinda girilirse yeniden sor
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Yanlış giriş! Lütfen 1, 2 veya 3 giriniz.\n");
                                    break;
                            }
                        }
                        catch (FormatException)
                        {
                            // Sayi disinda girilirse yeniden sor
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Yanlış giriş! Lütfen 1, 2 veya 3 giriniz.\n");
                        }
                    } while (aktifBolge != 1 && aktifBolge != 2 && aktifBolge != 3);
                    goto anaMenu;

                case 2: // Masa Islem
                    Console.BackgroundColor = ConsoleColor.Gray; // Arka plani gri yap
                    Console.Clear();
                masaIslem:
                    Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                    Console.WriteLine("--------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta; // Mor baslik
                    Console.WriteLine("                    MASA İŞLEM");
                    Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                    Console.WriteLine("--------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    if (aktifBolge == 1)
                    {
                        Console.WriteLine(
                            "İçeri Bölümdeki Masalar\n" +
                            $"Masa 1     [{masaDurum[0]}]\n" +
                            $"Masa 2     [{masaDurum[1]}]\n" +
                            $"Masa 3     [{masaDurum[2]}]\n" +
                            $"Masa 4     [{masaDurum[3]}]\n");
                    }
                    else if (aktifBolge == 2)
                    {
                        Console.WriteLine(
                            "Terastaki Masalar\n" +
                            $"Masa 5     [{masaDurum[0]}]\n" +
                            $"Masa 6     [{masaDurum[1]}]\n" +
                            $"Masa 7     [{masaDurum[2]}]");
                    }
                    else
                    {
                        Console.WriteLine(
                            "İçeri Bölümdeki Masalar\n" +
                            $"Masa 1     [{masaDurum[0]}]\n" +
                            $"Masa 2     [{masaDurum[1]}]\n" +
                            $"Masa 3     [{masaDurum[2]}]\n" +
                            $"Masa 4     [{masaDurum[3]}]\n" +
                            $"\nTerastaki Masalar\n" +
                            $"Masa 5     [{masaDurum[4]}]\n" +
                            $"Masa 6     [{masaDurum[5]}]\n" +
                            $"Masa 7     [{masaDurum[6]}]\n");
                    }
                    Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                    Console.WriteLine("--------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.DarkBlue; // Mavi Ana Menu yazisi
                    Console.WriteLine("ANA MENÜ     [ESC]");
                    Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                    Console.WriteLine("--------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta; // Mor giriş yazisi
                    Console.WriteLine("Masa seçiniz: ");
                    try
                    {
                        ConsoleKeyInfo giris = Console.ReadKey(true);

                        // ESC basilirsa ana menuye git
                        if (giris.Key == ConsoleKey.Escape)
                        {
                            goto anaMenu;
                        }

                        // Secilen masayi kontrol et
                        int masaSecim = int.Parse(giris.KeyChar.ToString());
                        switch (aktifBolge)
                        {
                            case 1: // Iceri
                                if (masaSecim > 4 || masaSecim == 0)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Lütfen masa numarasını doğru giriniz...");
                                    goto masaIslem;
                                }
                                if (masaDurum[masaSecim - 1] == "DOLU")
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Masa zaten dolu...");
                                    goto masaIslem;
                                }
                                // Siparis girisi icin max 50 siparis alan bir dizi
                                masaSecilenUrunler[masaSecim - 1] = new string[50];
                                masaHesap[masaSecim - 1] = new int[50];
                                break;
                            case 2: // Teras
                                if (masaSecim < 5 || masaSecim > 7 || masaSecim == 0)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Lütfen masa numarasını doğru giriniz...");
                                    goto masaIslem;
                                }
                                if (masaDurum[masaSecim - 5] == "DOLU")
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Masa zaten dolu...");
                                    goto masaIslem;
                                }
                                // Siparis girisi icin max 50 siparis alan bir dizi
                                masaSecilenUrunler[masaSecim - 5] = new string[50];
                                masaHesap[masaSecim - 5] = new int[50];
                                break;
                            case 3: // Hepsi
                                if (masaSecim > 7 || masaSecim == 0)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Lütfen masa numarasını doğru giriniz...");
                                    goto masaIslem;
                                }
                                if (masaDurum[masaSecim - 1] == "DOLU")
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Masa zaten dolu...");
                                    goto masaIslem;
                                }
                                // Siparis girisi icin max 50 siparis alan bir dizi
                                masaSecilenUrunler[masaSecim - 1] = new string[50];
                                masaHesap[masaSecim - 1] = new int[50];
                                break;
                        }
                        // Yemek Menusunu goster
                        Console.Clear();
                    yemekMenu:
                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                        Console.WriteLine("--------------------------------------------------");
                        Console.ForegroundColor = ConsoleColor.DarkMagenta; // Mor baslik
                        Console.WriteLine("                    MASA İŞLEM");
                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("                     M E N Ü");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Çorbalar                              Salatalar");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("[1]  Mercimek Çorbası   120TL         [5]  Yeşil Salata      180TL\n" +
                                          "[2]  Ezogelin Çorbası   120TL         [6]  Sezar Salata      285TL\n" +
                                          "[3]  Domates Çorbası    120TL         [7]  Falafel Salata    245TL\n" +
                                          "[4]  Mantar Çorbası     130TL");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Et Yemekleri                          Tatlılar");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("[8]  İzmir Köfte        120TL         [11] Kazandibi         128TL\n" +
                                          "[9]  Hünkar Beğendi     408TL         [12] Fırın Sütlaç      100TL\n" +
                                          "[10] Et Kavurma         330TL         [13] Profiterol        130TL\n" +
                                          "                                      [14] Künefe            250TL");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("İçecekler");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("[15] Ayran              45TL\n" +
                                          "[16] Şalgam             45TL\n" +
                                          "[17] Soda               30TL");
                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                        Console.WriteLine("--------------------------------------------------");
                        Console.ForegroundColor = ConsoleColor.DarkMagenta; // Mor baslik
                        Console.WriteLine($"               {masaSecim}.MASA SİPARİŞLERİ");
                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                        Console.WriteLine("--------------------------------------------------");

                        // Secilen Yemek Girisleri
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        if (aktifBolge == 2)
                        {
                            for (int i = 0; i < secilenUrunSayisi[masaSecim - 5]; i++)
                            {
                                Console.WriteLine(masaSecilenUrunler[masaSecim - 5][i]);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < secilenUrunSayisi[masaSecim - 1]; i++)
                            {
                                Console.WriteLine(masaSecilenUrunler[masaSecim - 1][i]);
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                        Console.WriteLine("--------------------------------------------------");
                        Console.ForegroundColor = ConsoleColor.DarkBlue; // Mavi Ana Menu yazisi
                        Console.WriteLine("ONAYLA       [Space]\n" +
                                          "ANA MENÜ     [ESC]\n" +
                                          "GERİ GİT     [Sol Ok]");
                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                        Console.WriteLine("--------------------------------------------------");
                        Console.ForegroundColor = ConsoleColor.DarkMagenta; // Mor giriş yazisi
                        Console.WriteLine("Ürün eklemek için numarasını giriniz: ");

                        string siparis = "";
                        while (true)
                        {
                            ConsoleKeyInfo siparisGiris = Console.ReadKey(true);

                            // ESC basilirsa eklenen urunleri sil ve ana menuye git
                            if (siparisGiris.Key == ConsoleKey.Escape)
                            {
                                if (aktifBolge == 2)
                                {
                                    if (secilenUrunSayisi[masaSecim - 5] > 0)
                                    {
                                        for (int i = 0; i < secilenUrunSayisi[masaSecim - 5]; i++)
                                        {
                                            masaSecilenUrunler[masaSecim - 5][i] = "";
                                            masaHesap[masaSecim - 5][i] = 0;
                                        }
                                        secilenUrunSayisi[masaSecim - 5] = 0;
                                        Console.Clear();
                                    }
                                }
                                else
                                {
                                    if (secilenUrunSayisi[masaSecim - 1] > 0)
                                    {
                                        for (int i = 0; i < secilenUrunSayisi[masaSecim - 1]; i++)
                                        {
                                            masaSecilenUrunler[masaSecim - 1][i] = "";
                                            masaHesap[masaSecim - 1][i] = 0;
                                        }
                                        secilenUrunSayisi[masaSecim - 1] = 0;
                                        Console.Clear();
                                    }
                                }
                                goto anaMenu;
                            }
                            // Space basinca siparis girislerini bitir ve masa durumunu dolu yap
                            else if (siparisGiris.Key == ConsoleKey.Spacebar)
                            {
                                if (aktifBolge == 2)
                                {
                                    if (secilenUrunSayisi[masaSecim - 5] == 0)
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Ürün Seçilmeden Onay işlemi yapılamaz!\n");
                                        goto yemekMenu;
                                    }
                                    masaDurum[masaSecim - 5] = "DOLU";
                                }
                                else
                                {
                                    if (secilenUrunSayisi[masaSecim - 1] == 0)
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Ürün Seçilmeden Onay işlemi yapılamaz!\n");
                                        goto yemekMenu;
                                    }
                                    masaDurum[masaSecim - 1] = "DOLU";
                                }
                                break;
                            }
                            // Sol ok basildiginda son girilen urunu sil
                            else if (siparisGiris.Key == ConsoleKey.LeftArrow)
                            {
                                if (aktifBolge == 2)
                                {
                                    if (secilenUrunSayisi[masaSecim - 5] > 0)
                                    {
                                        masaSecilenUrunler[masaSecim - 5][secilenUrunSayisi[masaSecim - 5] - 1] = "";
                                        masaHesap[masaSecim - 5][secilenUrunSayisi[masaSecim - 5] - 1] = 0;
                                        secilenUrunSayisi[masaSecim - 5]--;
                                        Console.Clear();
                                        goto yemekMenu;
                                    }
                                }
                                else
                                {
                                    if (secilenUrunSayisi[masaSecim - 1] > 0)
                                    {
                                        masaSecilenUrunler[masaSecim - 1][secilenUrunSayisi[masaSecim - 1] - 1] = "";
                                        masaHesap[masaSecim - 1][secilenUrunSayisi[masaSecim - 1] - 1] = 0;
                                        secilenUrunSayisi[masaSecim - 1]--;
                                        Console.Clear();
                                        goto yemekMenu;
                                    }
                                }
                            }
                            // ENTER basinca urun secimi yap
                            else if (siparisGiris.Key == ConsoleKey.Enter)
                            {
                                if (siparis != "")
                                {
                                    int secim = int.Parse(siparis);
                                    if (secim >= 1 && secim <= 17)
                                    {
                                        if (aktifBolge == 2)
                                        {
                                            masaSecilenUrunler[masaSecim - 5][secilenUrunSayisi[masaSecim - 5]] = menu[secim - 1];
                                            masaHesap[masaSecim - 5][secilenUrunSayisi[masaSecim - 5]] = menuFiyat[secim - 1];
                                            secilenUrunSayisi[masaSecim - 5]++;

                                        }
                                        else
                                        {
                                            masaSecilenUrunler[masaSecim - 1][secilenUrunSayisi[masaSecim - 1]] = menu[secim - 1];
                                            masaHesap[masaSecim - 1][secilenUrunSayisi[masaSecim - 1]] = menuFiyat[secim - 1];
                                            secilenUrunSayisi[masaSecim - 1]++;
                                        }
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Geçersiz seçim! Seçiminiz 1 ile 17 arasında olmalı.\n");
                                    }
                                    goto yemekMenu;
                                }
                            }
                            // Rakam mı kontrol et
                            else if (siparisGiris.KeyChar >= '0' && siparisGiris.KeyChar <= '9')
                            {
                                // Numara girislerini yazdır. Enter ile urun secimi yapilir
                                siparis += siparisGiris.KeyChar;
                                Console.Write(siparisGiris.KeyChar);
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        // Sayi disinda girilirse yeniden sor
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Yanlış giriş! Masa numarasını giriniz.\n");
                        goto masaIslem;
                    }
                    goto anaMenu;

                case 3: // Masa Hesap
                    Console.BackgroundColor = ConsoleColor.Gray; // Arka plani gri yap
                    Console.Clear();
                masaHesap:
                    Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                    Console.WriteLine("--------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta; // Mor baslik
                    Console.WriteLine("                    MASA HESAP");
                    Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                    Console.WriteLine("--------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    if (aktifBolge == 1)
                    {
                        Console.WriteLine(
                            "İçeri Bölümdeki Masalar\n" +
                            $"Masa 1     [{masaDurum[0]}]\n" +
                            $"Masa 2     [{masaDurum[1]}]\n" +
                            $"Masa 3     [{masaDurum[2]}]\n" +
                            $"Masa 4     [{masaDurum[3]}]\n");
                    }
                    else if (aktifBolge == 2)
                    {
                        Console.WriteLine(
                            "Terastaki Masalar\n" +
                            $"Masa 5     [{masaDurum[0]}]\n" +
                            $"Masa 6     [{masaDurum[1]}]\n" +
                            $"Masa 7     [{masaDurum[2]}]");
                    }
                    else
                    {
                        Console.WriteLine(
                            "İçeri Bölümdeki Masalar\n" +
                            $"Masa 1     [{masaDurum[0]}]\n" +
                            $"Masa 2     [{masaDurum[1]}]\n" +
                            $"Masa 3     [{masaDurum[2]}]\n" +
                            $"Masa 4     [{masaDurum[3]}]\n" +
                            $"\nTerastaki Masalar\n" +
                            $"Masa 5     [{masaDurum[4]}]\n" +
                            $"Masa 6     [{masaDurum[5]}]\n" +
                            $"Masa 7     [{masaDurum[6]}]\n");
                    }
                    Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                    Console.WriteLine("--------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.DarkBlue; // Mavi Ana Menu yazisi
                    Console.WriteLine("ANA MENÜ     [ESC]");
                    Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                    Console.WriteLine("--------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta; // Mor giriş yazisi
                    Console.WriteLine("Masa seçiniz: ");
                    try
                    {
                        ConsoleKeyInfo hesapGiris = Console.ReadKey(true);

                        // ESC basilirsa ana menuye git
                        if (hesapGiris.Key == ConsoleKey.Escape)
                        {
                            goto anaMenu;
                        }

                        int masaHesapSecim = int.Parse(hesapGiris.KeyChar.ToString());
                        switch (aktifBolge)
                        {
                            case 1: // Iceri
                                if (masaHesapSecim > 4 || masaHesapSecim == 0)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Lütfen masa numarasını doğru giriniz...\n");
                                    goto masaHesap;
                                }
                                if (masaDurum[masaHesapSecim - 1] == "DOLU")
                                {
                                    int toplam = 0;
                                    for (int i = 0; i < secilenUrunSayisi[masaHesapSecim - 1]; i++)
                                    {
                                        toplam += masaHesap[masaHesapSecim - 1][i];
                                    }
                                    while (true)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                                        Console.WriteLine("--------------------------------------------------");
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine($"{masaHesapSecim} numaralı masanın hesabı: {toplam}TL\n" +
                                                           "ONAYLA     [ENTER]\n" +
                                                           "VAZGEÇ     [ESC]");
                                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                                        Console.WriteLine("--------------------------------------------------");

                                        ConsoleKeyInfo onay = Console.ReadKey(true);
                                        if (onay.Key == ConsoleKey.Escape)
                                        {
                                            Console.Clear();
                                            goto masaHesap;
                                        }
                                        else if (onay.Key == ConsoleKey.Enter)
                                        {
                                            masaToplamHesap[masaHesapSecim - 1] += toplam;
                                            toplamHesap += toplam;
                                            masaDurum[masaHesapSecim - 1] = "BOŞ";

                                            if (secilenUrunSayisi[masaHesapSecim - 1] > 0)
                                            {
                                                for (int i = 0; i < secilenUrunSayisi[masaHesapSecim - 1]; i++)
                                                {
                                                    masaSecilenUrunler[masaHesapSecim - 1][i] = "";
                                                    masaHesap[masaHesapSecim - 1][i] = 0;
                                                }
                                                secilenUrunSayisi[masaHesapSecim - 1] = 0;
                                                Console.Clear();
                                            }

                                            goto anaMenu;
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Yanlış tuşa bastınız!\n");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Seçtiğiniz masa boş!\n");
                                    goto masaHesap;
                                }
                            case 2: // Teras
                                if (masaHesapSecim < 5 || masaHesapSecim > 7 || masaHesapSecim == 0)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Lütfen masa numarasını doğru giriniz...");
                                    goto masaHesap;
                                }
                                if (masaDurum[masaHesapSecim - 5] == "DOLU")
                                {
                                    int toplam = 0;

                                    for (int i = 0; i < secilenUrunSayisi[masaHesapSecim - 5]; i++)
                                    {
                                        toplam += masaHesap[masaHesapSecim - 5][i];
                                    }
                                    while (true)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                                        Console.WriteLine("--------------------------------------------------");
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine($"{masaHesapSecim} numaralı masanın hesabı: {toplam}TL\n" +
                                                           "ONAYLA     [ENTER]\n" +
                                                           "VAZGEÇ     [ESC]");
                                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                                        Console.WriteLine("--------------------------------------------------");

                                        ConsoleKeyInfo onay = Console.ReadKey(true);
                                        if (onay.Key == ConsoleKey.Escape)
                                        {
                                            Console.Clear();
                                            goto masaHesap;
                                        }
                                        else if (onay.Key == ConsoleKey.Enter)
                                        {
                                            masaToplamHesap[masaHesapSecim - 5] += toplam;
                                            toplamHesap += toplam;
                                            masaDurum[masaHesapSecim - 5] = "BOŞ";

                                            if (secilenUrunSayisi[masaHesapSecim - 5] > 0)
                                            {
                                                for (int i = 0; i < secilenUrunSayisi[masaHesapSecim - 5]; i++)
                                                {
                                                    masaSecilenUrunler[masaHesapSecim - 5][i] = "";
                                                    masaHesap[masaHesapSecim - 5][i] = 0;
                                                }
                                                secilenUrunSayisi[masaHesapSecim - 5] = 0;
                                                Console.Clear();
                                            }

                                            goto anaMenu;
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Yanlış tuşa bastınız!\n");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Seçtiğiniz masa boş!\n");
                                    goto masaHesap;
                                }
                            case 3: // Hepsi
                                if (masaHesapSecim > 7 || masaHesapSecim == 0)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Lütfen masa numarasını doğru giriniz...");
                                    goto masaHesap;
                                }
                                if (masaDurum[masaHesapSecim - 1] == "DOLU")
                                {
                                    int toplam = 0;

                                    for (int i = 0; i < secilenUrunSayisi[masaHesapSecim - 1]; i++)
                                    {
                                        toplam += masaHesap[masaHesapSecim - 1][i];
                                    }
                                    while (true)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                                        Console.WriteLine("--------------------------------------------------");
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine($"{masaHesapSecim} numaralı masanın hesabı: {toplam}TL\n" +
                                                           "ONAYLA     [ENTER]\n" +
                                                           "VAZGEÇ     [ESC]");
                                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                                        Console.WriteLine("--------------------------------------------------");

                                        ConsoleKeyInfo onay = Console.ReadKey(true);
                                        if (onay.Key == ConsoleKey.Escape)
                                        {
                                            Console.Clear();
                                            goto masaHesap;
                                        }
                                        else if (onay.Key == ConsoleKey.Enter)
                                        {
                                            masaToplamHesap[masaHesapSecim - 1] += toplam;
                                            toplamHesap += toplam;
                                            masaDurum[masaHesapSecim - 1] = "BOŞ";

                                            if (secilenUrunSayisi[masaHesapSecim - 1] > 0)
                                            {
                                                for (int i = 0; i < secilenUrunSayisi[masaHesapSecim - 1]; i++)
                                                {
                                                    masaSecilenUrunler[masaHesapSecim - 1][i] = "";
                                                    masaHesap[masaHesapSecim - 1][i] = 0;
                                                }
                                                secilenUrunSayisi[masaHesapSecim - 1] = 0;
                                                Console.Clear();
                                            }

                                            goto anaMenu;
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Yanlış tuşa bastınız!\n");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Seçtiğiniz masa boş!\n");
                                    goto masaHesap;
                                }
                        }
                        goto anaMenu;
                    }
                    catch (FormatException)
                    {
                        // Sayi disinda girilirse yeniden sor
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Yanlış giriş! Masa numarasını giriniz.\n");
                        goto masaHesap;
                    }

                case 4: // Kasa İslemleri
                    Console.BackgroundColor = ConsoleColor.Gray; // Arka plani gri yap
                    Console.Clear();
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                        Console.WriteLine("--------------------------------------------------");
                        Console.ForegroundColor = ConsoleColor.DarkMagenta; // Mor baslik
                        Console.WriteLine("                    KASA İŞLEMLERİ");
                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                        Console.WriteLine("--------------------------------------------------");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        if (aktifBolge == 1)
                        {
                            Console.WriteLine(
                                "İçeri Bölümdeki Masalar\n" +
                                $"Masa 1     [{masaToplamHesap[0]} TL]\n" +
                                $"Masa 2     [{masaToplamHesap[1]} TL]\n" +
                                $"Masa 3     [{masaToplamHesap[2]} TL]\n" +
                                $"Masa 4     [{masaToplamHesap[3]} TL]\n");
                        }
                        else if (aktifBolge == 2)
                        {
                            Console.WriteLine(
                                "Terastaki Masalar\n" +
                                $"Masa 5     [{masaToplamHesap[0]} TL]\n" +
                                $"Masa 6     [{masaToplamHesap[1]} TL]\n" +
                                $"Masa 7     [{masaToplamHesap[2]} TL]");
                        }
                        else
                        {
                            Console.WriteLine(
                                "İçeri Bölümdeki Masalar\n" +
                                $"Masa 1     [{masaToplamHesap[0]} TL]\n" +
                                $"Masa 2     [{masaToplamHesap[1]} TL]\n" +
                                $"Masa 3     [{masaToplamHesap[2]} TL]\n" +
                                $"Masa 4     [{masaToplamHesap[3]} TL]\n" +
                                $"\nTerastaki Masalar\n" +
                                $"Masa 5     [{masaToplamHesap[4]} TL]\n" +
                                $"Masa 6     [{masaToplamHesap[5]} TL]\n" +
                                $"Masa 7     [{masaToplamHesap[6]} TL]\n");
                        }
                        Console.ForegroundColor = ConsoleColor.DarkMagenta; // Mor toplam yazisi
                        Console.WriteLine($"TOPLAM KAZANÇ: {toplamHesap} TL");
                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("ANA MENÜ     [ESC]");
                        Console.ForegroundColor = ConsoleColor.Black; // Siyah cizgi
                        Console.WriteLine("--------------------------------------------------");
                        if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                        {
                            goto anaMenu;
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Yanlış tuşa bastınız!\n");
                        }
                    }
            }
        }
    }
}
