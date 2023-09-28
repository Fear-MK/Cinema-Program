using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cinema
{
    using System;
    using System.Data;

    class Cinema
    {
        static int[] ticketsSold = new int[5];
        static bool[][] seats = new bool[5][];
        static int rows = 5;
        static int columns = 9;

        static void Main()
        {
            InitializeSeats(); //This is just a cleaner function that creates the seating 2D arrays

            while (true)
            {
                Console.Clear(); //Makes the screen blank if you go back to the original screen by iniating a "continue;" at some point in the code
                DisplayMenu();

                Console.Write("Enter the number of the film you wish to see: ");
                int filmNumber;

                while (true) //This makes it so you cant continue until you enter a valid number, same for all upcoming while loops.
                {
                    if (!int.TryParse(Console.ReadLine(), out filmNumber) || filmNumber < 1 || filmNumber > 5) //tryparse is a function I found that shaves alot of unnecessary lines of codee
                    {
                        Console.WriteLine("No such number, please enter again (between 1 and 5)");
                        continue;
                    }
                    break;
                }


                Console.Write("Enter your age: ");
                int age;
                while (true)
                {
                    if (!int.TryParse(Console.ReadLine(), out age))
                    {
                        Console.WriteLine("Invalid age input, please retry: ");
                        Thread.Sleep(2000);
                        continue;
                    }
                    break;
                }


                if (age < GetFilmAgeRating(filmNumber)) //Learnt switch/case for a simple function here.
                {
                    Console.WriteLine("Access denied – You are too young. Exiting.");
                    Thread.Sleep(2000);
                    continue;
                }

                Console.Write("Enter the date you want to watch the film (DD/MM/YYYY): ");
                DateTime chosenDate;
                while (true)
                {
                    if (!DateTime.TryParse(Console.ReadLine(), out chosenDate) || chosenDate < DateTime.Today || chosenDate > DateTime.Today.AddDays(6)) //checks if the date is a valid date, and if it is less than a week ahead of the current date
                    {
                        Console.WriteLine("Invalid date, please retry. (DD/MM/YYYY)");
                        Thread.Sleep(2000);
                        continue;
                    }
                    break;
                }

                BookTicket(filmNumber, chosenDate); //adds ticket to the array and the date
            }
        }

        static void InitializeSeats()
        {
            for (int i = 0; i < 5; i++)
            {
                seats[i] = new bool[rows * columns];
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("Welcome to our Multiplex");
            Console.WriteLine("We are presently showing:");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{i + 1}. {GetFilmName(i + 1)} ({GetFilmAgeRating(i + 1)}) Tickets Sold: {ticketsSold[i]}");
            }
        }

        static void BookTicket(int filmNumber, DateTime chosenDate)
        {
            Console.WriteLine($"-------------------\nAquinas Multiplex\nFilm: {GetFilmName(filmNumber)}\nDate: {chosenDate:dd/MM/yyyy}\nEnjoy the film\n-------------------");
            ticketsSold[filmNumber - 1]++;
            seats[filmNumber - 1] = SelectSeats(filmNumber);
        }

        static bool[] SelectSeats(int filmNumber)
        {
            while (true) 
            {
                Console.Clear();

                Console.WriteLine($"Available seats for {GetFilmName(filmNumber)}:");
                DisplaySeats(seats[filmNumber - 1]);

                Console.Write("Please enter the row where you want to sit: ");
                int selectedRow;
                if (!int.TryParse(Console.ReadLine(), out selectedRow) || selectedRow < 1 || selectedRow > rows)
                {
                    Console.WriteLine("Invalid row input");
                    Thread.Sleep(1000);
                    continue;
                }

                Console.Write("Please enter the column where you want to sit: ");
                int selectedColumn;
                if (!int.TryParse(Console.ReadLine(), out selectedColumn) || selectedColumn < 1 || selectedColumn > columns)
                {
                    Console.WriteLine("Invalid column input");
                    Thread.Sleep(1000);
                    continue;
                }

                if (seats[filmNumber - 1][(selectedRow - 1) * columns + selectedColumn - 1])
                {
                    Console.WriteLine("Sorry, this seat is occupied");
                    Thread.Sleep(1000);
                    continue;
                }

                seats[filmNumber - 1][(selectedRow - 1) * columns + selectedColumn - 1] = true; //simple bodmas there
                return seats[filmNumber - 1]; //returns the array
            }

        }

        static void DisplaySeats(bool[] seats) //this just displays the seats from the 2d array
        {
            Console.Clear();
            Console.WriteLine("  123456789");
            for (int i = 0; i < rows; i++)
            {
                Console.Write($"{i + 1} ");
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(seats[i * columns + j] ? 'X' : 'O');
                }
                Console.WriteLine();
            }
        }

        static string GetFilmName(int filmNumber) //simple stuff, didnt want to make a text file as too much hassle, this is much more readable.
        {
            switch (filmNumber)
            {
                case 1: return "Rush";
                case 2: return "How I Live Now";
                case 3: return "Thor: The Dark World";
                case 4: return "Filth";
                case 5: return "Planes";
                default: return "Unknown Film";
            }
        }

        static int GetFilmAgeRating(int filmNumber) //same as above but with the age ratings, could pretty easily make a txt file for both these but wasnt asked for
        {
            switch (filmNumber)
            {
                case 1: return 15;
                case 2: return 15;
                case 3: return 12;
                case 4: return 18;
                case 5: return 0;
                default: return 0;
            }
        }
    }

}
