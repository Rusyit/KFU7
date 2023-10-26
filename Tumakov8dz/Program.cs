using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Tumakov8dz
{
    public enum AccType
    {
        Текущий,
        Сберегательный
    }

    class Bank3
    {

        private static int numOfBankAccs;
        private int accNum;
        private decimal accBalance;
        private AccType bankAccType;


        public int AccNum
        {
            get
            {
                return accNum;
            }
        }

        public decimal AccBalance
        {
            get
            {
                return accBalance;
            }
        }

        public AccType BankAccType
        {
            get
            {
                return bankAccType;
            }
        }

        public static int NumOfBankAccs { get => numOfBankAccs; set => numOfBankAccs = value; }

        private void ChangeNumOfBankAccs()
        {
            NumOfBankAccs++;
        }

        public bool MoreMoney(decimal moreMoney)
        {
            if ((accBalance - moreMoney > 0) && (moreMoney > 0))
            {
                accBalance -= moreMoney;
                return true;
            }

            return false;
        }

        public bool PutMoney(decimal putmoney)
        {
            if (putmoney > 0)
            {
                accBalance += putmoney;
                return true;
            }

            return false;
        }

        public bool TransMoney(Bank3 drawAccount, decimal drawAmount)
        {
            if ((drawAmount > 0) && (drawAccount.AccBalance - drawAmount > 0))
            {
                accBalance += drawAmount;
                drawAccount.accBalance -= drawAmount;
                return true;
            }

            return false;
        }

        public Bank3(AccType bankAccountType)
        {
            accNum = NumOfBankAccs;
            accBalance = 0;
            this.bankAccType = bankAccountType;
            ChangeNumOfBankAccs();
        }
    }

    class IFormattables : IFormattable
    {
        private int Val1 = 1;

        public override string ToString()
        {
            return ToString("G", NumberFormatInfo.CurrentInfo);
        }

        public string ToString(string format)
        {
            return ToString(format, NumberFormatInfo.CurrentInfo);
        }

        public string ToString(string format, IFormatProvider thing)
        {
            return Val1.ToString("G", thing);
        }
    }

    class Song
    {

        private string songName;
        private string songAuthor;
        private Song previousSong;

        public string SongName
        {
            get
            {
                return songName;
            }
        }

        public string SongAuthor
        {
            get
            {
                return songAuthor;
            }
        }

        public Song PreviousSong
        {
            get
            {
                return previousSong;
            }
        }

        public string Title
        {
            get
            {
                return songName + " " + songAuthor;
            }
        }

        public override bool Equals(object transmittedSong)
        {
            Song song = transmittedSong as Song;

            if ((song != null) && (song.SongName == songName) && (song.SongAuthor == songAuthor))
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Song(string songName, string songAuthor, Song previousSong)
        {
            this.songName = songName;
            this.songAuthor = songAuthor;
            this.previousSong = previousSong;
        }

        public Song(string songName, string songAuthor)
        {
            this.songName = songName;
            this.songAuthor = songAuthor;
            previousSong = null;
        }
    }
    class Program
    {
        static string ReversesString(string sString)
        {
            char[] stringChar = sString.ToCharArray();

            Array.Reverse(stringChar);

            return String.Concat(stringChar);
        }

        static bool ChecksObjectUsingIs(object checkObject)
        {
            if (checkObject is IFormattable)
            {
                return true;
            }

            return false;
        }

        static bool ChecksObjectUsingAs(object checkObject)
        {
            if (checkObject as IFormattable == null)
            {
                return false;
            }

            return true;
        }

        public static void SearchMail(ref string s)
        {
            string[] words = s.Split('@');
            s = words[1];
        }
        static void Main()
        {

            // УПРАЖНЕНИЕ 8.1.
            Console.WriteLine("УПРАЖНЕНИЕ 8.1.\n");

            try
            {
                Bank3 BankAcc1 = new Bank3(AccType.Сберегательный);
                Bank3 BankAcc2 = new Bank3(AccType.Текущий);
                bool putMoneyRes, transMoneyRes;

                putMoneyRes = BankAcc1.PutMoney(100000M);
                putMoneyRes = BankAcc2.PutMoney(10000M);

                if (putMoneyRes)
                {
                    Console.WriteLine($"{BankAcc1.BankAccType} №{BankAcc1.AccNum:D2}, баланс: {BankAcc1.AccBalance} рублей\t\t" +
                        $"{BankAcc2.BankAccType} №{BankAcc2.AccNum:D2}, баланс: {BankAcc2.AccBalance} рублей");

                    Console.WriteLine("\nВведите сумму которую хотите первести: ");
                    string c = Console.ReadLine();
                    int a;
                    int.TryParse(c, out a);
                    transMoneyRes = BankAcc1.TransMoney(BankAcc2, a);

                    if (transMoneyRes)
                    {
                        Console.WriteLine($"{BankAcc1.BankAccType} №{BankAcc1.AccNum:D2}, баланс: {BankAcc1.AccBalance} рублей\t\t" +
                            $"{BankAcc2.BankAccType} №{BankAcc2.AccNum:D2}, баланс: {BankAcc2.AccBalance} рублей");
                    }
                    else
                    {
                        Console.WriteLine("Вы неверно ввели сумму или на счете недостаточно средств");
                    }
                }
                else
                {
                    Console.WriteLine("Вы неверно ввели сумму или на счете недостаточно средств");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Вы ввели не число");
            }

            // Упражнение 8.2. 
            Console.WriteLine("\nУПРАЖНЕНИЕ 8.2.\n");

            string old, news;

            Console.Write("Введите строку: ");
            old = Console.ReadLine();

            news = ReversesString(old);
            Console.Write($"Из строки {old} получилась строка: {news}");

            // УПРАЖНЕНИЕ 8.3.
            Console.WriteLine("\nУПРАЖНЕНИЕ 8.3.\n");
            Console.WriteLine("Введите название файла: ");
            string fileName = Console.ReadLine();
            string path = Environment.CurrentDirectory + @"\" + fileName;
            if (File.Exists(path))
            {
                string fileText = File.ReadAllText(path).ToUpper();
                File.WriteAllText(Environment.CurrentDirectory + @"\dobavochnifail", fileText);
            }
            else
            {
                Console.WriteLine("Такого файла не существует");
            }

            // УПРАЖНЕНИЕ 8.4.
            Console.WriteLine("\n\nУПРАЖНЕНИЕ 8.4.\n");

            IFormattables firstObject = new IFormattables();
            Bank3 secondObject = new Bank3(AccType.Сберегательный);

            if (ChecksObjectUsingIs(firstObject))
            {
                Console.WriteLine("Объект реализует интерфейс System.IFormattable");
            }
            else
            {
                Console.WriteLine("Объект не реализует интерфейс System.IFormattable");
            }

            if (ChecksObjectUsingIs(secondObject))
            {
                Console.WriteLine("Объект реализует интерфейс System.IFormattable");
            }
            else
            {
                Console.WriteLine("Объект не реализует интерфейс System.IFormattable");
            }

            // ДОМАШНЕЕ ЗАДАНИЕ 8.2.
            Console.WriteLine("\nДОМАШНЕЕ ЗАДАНИЕ 8.2.\n");

            Song Song1 = new Song("Цунами", "Нюша");
            Song Song2 = new Song("Выше", "Нюша", Song1);
            Song Song3 = new Song("Целуй", "Нюша", Song2);
            Song Song4 = new Song("Наедине", "Нюша", Song3);

            List<Song> songList = new List<Song> { Song1, Song2, Song3, Song4 };

            foreach (Song song in songList)
            {
                Console.WriteLine(song.Title);
            }

            if (Song1.Equals(Song2))
            {
                Console.WriteLine("\nПесни одинаковы");
            }
            else
            {
                Console.WriteLine("\nПесни не одинаковы");
            }
        }
    }
}