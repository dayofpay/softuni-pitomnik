using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using System.Threading;
using System.IO;
using System.Data;

namespace Pitomnik
{
    class Program
    {

        static void Main(string[] args)
        {
        Start:
            string authorone = "Владислав";
            string authortwo = "Ивелина";
            string project = "Питомник / Проект от " + authorone + " и " + authortwo;
            string selectoption = "Изберете опция:";
            string optionone = "Списък със всички служители";
            string optiontwo = "Списък със всички клиенти";
            string optionthree = "Списък на общ брой животни";
            string optionfour = "Списък със животни";
            string optionfive = "Списък със всички отпуски";
            string optionsix = "Медицински данни";
            string optionseven = "Подробна информация за всички отпуски";
            string optioneight = "Ръчно добавяне на служител";
            string optionnine = "Генериране на Settings файл";
            string connectionstring = "Server=.; Database = root; Integrated Security = true";
            Random random = new Random();
            Console.Title = project;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(selectoption);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[1] " + optionone);
            Console.WriteLine("[2] " + optiontwo);
            Console.WriteLine("[3] " + optionthree);
            Console.WriteLine("[4] " + optionfour);
            Console.WriteLine("[5] " + optionfive);
            Console.WriteLine("[6] " + optionsix);
            Console.WriteLine("[7] " + optionseven);
            Console.WriteLine("[8] " + optioneight);
            Console.WriteLine("[9] " + optionnine);
            Console.WriteLine("");
            Console.Write("[>] Избор: ");
            var option = Console.ReadLine();
            if (option == "9")
            {
            Settings:
                if (File.Exists("Settings.txt")) // Ако файлът вече съществува, програмата няма да направи нищо
                {
                    Console.WriteLine("Settings файла вече съществува...");
                    Thread.Sleep(1000);
                    Console.Clear();
                    goto Start;
                }
                else // Тази функция се задейства, когато програмата не открива Settings.txt файл във папката на програмата
                {
                    string fileName = "Settings.txt";
                    using (StreamWriter sw = File.CreateText(fileName))
                    {
                        sw.Write("autosave - enable");
                        Console.WriteLine("[>] Готово :");
                        Thread.Sleep(1000);
                        Console.Clear();
                        goto Start;
                    }
                }
            }
            // TODO: 
            // Да оправя грешките
            if (option == "7")
            {
                    Console.Title = project + " | " + optionseven;
                    SqlConnection dbCon = new SqlConnection(connectionstring);
                    dbCon.Open();
                    using (dbCon)
                    {
                        SqlCommand cmd2 = new SqlCommand("SELECT firstname,lastname,[from],[to] FROM otpuskidetails", dbCon); // Списък на отпуските с включени имена.
                        SqlDataReader reader = cmd2.ExecuteReader();
                    while (reader.Read())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("----------------------------");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Първо име: {0}", reader.GetString(0));
                            Console.WriteLine("Фамилия: {0}", reader.GetString(1));
                            Console.WriteLine("От: {0}", reader.GetString(2));
                            Console.WriteLine("До: {0}", reader.GetString(3));
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("----------------------------");
                        string autosave = System.IO.File.ReadAllText("Settings.txt");
                        if (autosave == "autosave - enable")
                        {
                            Directory.CreateDirectory("DetailedOtpuski");
                            string fileName = "DetailedOtpuski/OtpuskiDetails-" + reader.GetString(0) + random.Next(1, 500) + ".txt";
                            using (StreamWriter sw = File.CreateText(fileName))
                            {
                                sw.WriteLine("Първо име: {0}", reader.GetString(0));
                                sw.WriteLine("Фамилия: {0}", reader.GetString(1));
                                sw.WriteLine("От: {0}", reader.GetString(2));
                                sw.WriteLine("До: {0}", reader.GetString(3));
                                sw.WriteLine(project);
                            }
                        }
                    }
                    Console.WriteLine("Натиснете бутон, за да се върнете в главното меню");
                        var a = Console.ReadKey();
                        Console.Clear();
                        goto Start;
                    }
                }
                if (option == "6")
                {
                    Console.Title = project + " | " + optionsix;
                    SqlConnection dbCon = new SqlConnection(connectionstring);
                    dbCon.Open();
                    using (dbCon)
                    {
                        SqlCommand cmd2 = new SqlCommand("SELECT [НОМЕР НА КУЧЕ], [ИМУНИЗАЦИЯ], [ОБЕЗПАРАЗИТЯВАНЕ ВЪШНО], [ОБЕЗПАРАЗИТЯВАНЕ ВЪТРЕШНО] FROM medss", dbCon); // Допуснал съм правописна грешка, вместо "ВЪНШНО" съм написал "ВЪШНО"
                        SqlDataReader reader = cmd2.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("----------------------------");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Номер на куче: {0}", reader.GetString(0));
                            Console.WriteLine("Дата на имунизация: {0}", reader.GetString(1));
                            Console.WriteLine("Обезпаразитяване външно: {0}", reader.GetString(2));
                            Console.WriteLine("Обезпаразитяване вътрешно: {0}", reader.GetString(3));
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("----------------------------");
                        string autosave = System.IO.File.ReadAllText("Settings.txt");
                        if (autosave == "autosave - enable")
                        {
                            Directory.CreateDirectory("Pets");
                            string fileName = "Pets/PetDetails-" + reader.GetString(0) + random.Next(1, 500) + ".txt";
                            using (StreamWriter sw = File.CreateText(fileName))
                            {
                                sw.WriteLine("Номер на куче: {0}", reader.GetString(0));
                                sw.WriteLine("Дата на имунизация: {0}", reader.GetString(1));
                                sw.WriteLine("Обезпаразитяване външно: {0}", reader.GetString(2));
                                sw.WriteLine("Обезпаразитяване вътрешно: {0}", reader.GetString(3));
                                sw.WriteLine(project);
                            }
                        }
                    }
                        Console.WriteLine("Натиснете бутон, за да се върнете в главното меню");
                        var a = Console.ReadKey();
                        Console.Clear();
                        goto Start;
                    }
                }
                if (option == "5")
                {
                    Console.Title = project + " | " + optionfive;
                    SqlConnection dbCon = new SqlConnection(connectionstring);
                    dbCon.Open();
                    using (dbCon)
                    {
                        SqlCommand cmd2 = new SqlCommand("SELECT [Номер на служител],ОТ,ДО,[ОБЩО ДНИ],[ПОЛАГАЕМИ ДНИ] FROM otpuski2", dbCon);
                        SqlDataReader reader = cmd2.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("----------------------------");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Номер на служител: {0}", reader.GetString(0));
                            Console.WriteLine("ОТ: {0}", reader.GetString(1));
                            Console.WriteLine("ДО: {0}", reader.GetString(2));
                            Console.WriteLine("Общо дни: {0}", reader.GetString(3));
                            Console.WriteLine("Полагаеми дни: {0}", reader.GetString(4));
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("----------------------------");
                        string autosave = System.IO.File.ReadAllText("Settings.txt");
                        if (autosave == "autosave - enable")
                        {
                            Directory.CreateDirectory("Otpuski");
                            string fileName = "Otpuski/OtpuskiDetails-" + reader.GetString(0) + random.Next(1, 500) + ".txt";
                            using (StreamWriter sw = File.CreateText(fileName))
                            {
                                sw.WriteLine("Номер на служител: {0}", reader.GetString(0));
                                sw.WriteLine("ОТ: {0}", reader.GetString(1));
                                sw.WriteLine("ДО: {0}", reader.GetString(2));
                                sw.WriteLine("Общо дни: {0}", reader.GetString(3));
                                sw.WriteLine("Полагаеми дни: {0}", reader.GetString(4));
                                sw.WriteLine(project);
                            }
                        }
                    }
                        Console.WriteLine("Натиснете бутон, за да се върнете в главното меню");
                        var a = Console.ReadKey();
                        Console.Clear();
                        goto Start;
                    }
                }
                if (option == "4")
                {
                    Console.Title = project + " | " + optionfour;
                    SqlConnection dbCon = new SqlConnection(connectionstring);
                    dbCon.Open();
                    using (dbCon)
                    {
                        SqlCommand cmd2 = new SqlCommand("SELECT [Номер на куче],ИМЕ,ПОРОДА,ЦВЯТ,ВЪЗРАСТ,[ДАТА НА РАЖДАНЕ],[НОМЕР НА ИДЕНТИФИКАЦИОНЕН ЧИП],[ДНЕВНА ДАЖБА] FROM animals", dbCon);
                        SqlDataReader reader = cmd2.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("----------------------------");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Номер на куче: {0}", reader.GetString(0));
                            Console.WriteLine("Име на куче: {0}", reader.GetString(1));
                            Console.WriteLine("Порода: {0}", reader.GetString(2));
                            Console.WriteLine("Цвят: {0}", reader.GetString(3));
                            Console.WriteLine("Възраст: {0}", reader.GetString(4));
                            Console.WriteLine("Дата на раждане: {0}", reader.GetString(5));
                            Console.WriteLine("Номер на идентификациония чип: {0}", reader.GetString(6));
                            Console.WriteLine("ДНЕВНА ДАЖБА: {0}", reader.GetString(7));
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("----------------------------");
                        string autosave = System.IO.File.ReadAllText("Settings.txt");
                        if (autosave == "autosave - enable")
                        {
                            Directory.CreateDirectory("PetData");
                            string fileName = "PetData/PetData-" + reader.GetString(0) + random.Next(1, 500) + ".txt";
                            using (StreamWriter sw = File.CreateText(fileName))
                            {
                                sw.WriteLine("Номер на куче: {0}", reader.GetString(0));
                                sw.WriteLine("Име на куче: {0}", reader.GetString(1));
                                sw.WriteLine("Порода: {0}", reader.GetString(2));
                                sw.WriteLine("Цвят: {0}", reader.GetString(3));
                                sw.WriteLine("Възраст: {0}", reader.GetString(4));
                                sw.WriteLine("Дата на раждане: {0}", reader.GetString(5));
                                sw.WriteLine("Номер на идентификациония чип: {0}", reader.GetString(6));
                                sw.WriteLine("ДНЕВНА ДАЖБА: {0}", reader.GetString(7));
                                sw.WriteLine(project);
                            }
                        }
                }
                        Console.WriteLine("Натиснете бутон, за да се върнете в главното меню");
                        var a = Console.ReadKey();
                        Console.Clear();
                        goto Start;
                    }
                }
                if (option == "3")
                {
                    Console.Title = project + " | " + optionthree;
                    SqlConnection dbCon = new SqlConnection(connectionstring);
                    dbCon.Open();
                    using (dbCon)
                    {
                        SqlCommand komanda = new SqlCommand("SELECT COUNT(*) FROM animals", dbCon);
                        int obshtojivotni = (int)komanda.ExecuteScalar();
                        Console.WriteLine("Информация : " + obshtojivotni);
                        Console.WriteLine("Натиснете бутон, за да се върнете в главното меню");
                        var a = Console.ReadKey();
                        Console.Clear();
                        goto Start;
                    }
                }
                if (option == "2")
                {
                    Console.Title = project + " | " + optiontwo;
                    SqlConnection dbCon = new SqlConnection(connectionstring);
                    dbCon.Open();
                    using (dbCon)
                    {
                        SqlCommand komanda = new SqlCommand("SELECT COUNT(*) FROM employees", dbCon);
                        int obshtoklienti = (int)komanda.ExecuteScalar();
                        Console.WriteLine("Общо Клиенти : " + obshtoklienti);
                        Console.WriteLine("Натиснете бутон, за да се върнете в главното меню");
                        var a = Console.ReadKey();
                        Console.Clear();
                        goto Start;
                    }
                }
                if (option == "1")
                {
                    Console.Title = project + " | " + optionone;
                    SqlConnection dbCon = new SqlConnection(connectionstring);
                    dbCon.Open();
                    using (dbCon)
                    {
                        SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM personal", dbCon);
                        int employeesCount = (int)command.ExecuteScalar();
                        Console.WriteLine("Общо служители: {0} ", employeesCount);
                        SqlCommand cmd2 = new SqlCommand("SELECT id, СОБСТВЕНО,БАЩИНО,ФАМИЛИЯ,ПОЛ,ГРАД,УЛИЦА,ТЕЛЕФОН,МЕСТОРАБОТА,НАЗНАЧАВАНЕ,СТАЖ FROM personal", dbCon);
                        SqlDataReader reader = cmd2.ExecuteReader();
                        Console.WriteLine("Списък на служители ... :");
                        while (reader.Read())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("----------------------------");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("ID: {0}", reader.GetString(0));
                            Console.WriteLine("Собствено: {0}", reader.GetString(1));
                            Console.WriteLine("Бащино: {0}", reader.GetString(2));
                            Console.WriteLine("Фаимилия: {0}", reader.GetString(3));
                            Console.WriteLine("Пол: {0}", reader.GetString(4));
                            Console.WriteLine("Град: {0}", reader.GetString(5));
                            Console.WriteLine("Улица: {0}", reader.GetString(6));
                            Console.WriteLine("Телефон: {0}", reader.GetString(7));
                            Console.WriteLine("Месторабота: {0}", reader.GetString(8));
                            Console.WriteLine("Назначаване: {0}", reader.GetString(9));
                            Console.WriteLine("Стаж: {0}", reader.GetString(10));
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("----------------------------");
                        string autosave = System.IO.File.ReadAllText("Settings.txt");
                        if (autosave == "autosave - enable")
                        {
                            Directory.CreateDirectory("EmployeeData");
                            string fileName = "EmployeeData/Employee-" + reader.GetString(0) + random.Next(1, 500) + ".txt";
                            using (StreamWriter sw = File.CreateText(fileName))
                            {
                                sw.WriteLine("ID: {0}", reader.GetString(0));
                                sw.WriteLine("Собствено: {0}", reader.GetString(1));
                                sw.WriteLine("Бащино: {0}", reader.GetString(2));
                                sw.WriteLine("Фаимилия: {0}", reader.GetString(3));
                                sw.WriteLine("Пол: {0}", reader.GetString(4));
                                sw.WriteLine("Град: {0}", reader.GetString(5));
                                sw.WriteLine("Улица: {0}", reader.GetString(6));
                                sw.WriteLine("Телефон: {0}", reader.GetString(7));
                                sw.WriteLine("Месторабота: {0}", reader.GetString(8));
                                sw.WriteLine("Назначаване: {0}", reader.GetString(9));
                                sw.WriteLine("Стаж: {0}", reader.GetString(10));
                                sw.WriteLine(project);
                            }
                        }

                    }
                        Console.WriteLine("Натиснете бутон, за да се върнете в главното меню");
                        var a = Console.ReadKey();
                        Console.Clear();
                        goto Start;
                    }
                }
                else // Тази функция се задейства, когато програмата открие действие, което не е декларирано в програмата и не може да бъде изпълнено.
                {
                    MoreFunctions.Error();
                    goto Start;
                }
            }
        }
    }
