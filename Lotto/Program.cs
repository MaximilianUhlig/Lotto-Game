using System;
using System.Collections.Generic;

namespace Lotto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Initializes "numbers" to have no contents, "actualNumbers" to contain the contents from "ActualNumber" method, and running is set to true so the menu can display until exited.
            int[] numbers = null;
            int[] actualNumbers = ActualNumber();
            bool running = true;

            //Main while loop for the menu. Using switch cases to tell the program which methods to use for a specific option.
            while (running)
            {
                
                Console.WriteLine("=== Welcome to the Lotto ===");
                Console.WriteLine("1. Pick numbers");
                Console.WriteLine("2. Display numbers");
                Console.WriteLine("3. Sort numbers");
                Console.WriteLine("4. Compare numbers");
                Console.WriteLine("5. Frequency of numbers");
                Console.WriteLine("6. Exit");
                
                string input = Console.ReadLine();
                int option;
                Console.Clear();
                
                if (!int.TryParse(input, out option))
                {
                    Console.WriteLine("Not a valid option.");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                
                switch (option)
                {
                    case 1:
                        numbers = UserNumber();
                        break;

                    case 2:
                        if (numbers != null)
                        {
                            Console.WriteLine("Your numbers are: ");
                            DisplayNumbers(numbers);
                        }
                        else
                        {

                            Console.WriteLine("No numbers have been picked yet.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;

                    case 3:
                        if (numbers != null)
                        {
                            BubbleSort(numbers);
                        }
                        else
                        {
                            Console.WriteLine("No numbers have been picked yet.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;

                    case 4:
                        if (numbers != null)
                        {
                            CompareArrays(numbers, actualNumbers);
                        }
                        else
                        {
                            Console.WriteLine("No numbers have been picked yet.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;

                    case 5:
                        if (numbers != null)
                        {
                            NumberFrequency(numbers);
                        }
                        else
                        {
                            Console.WriteLine("No numbers have been picked yet.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;

                    case 6:
                        Console.WriteLine("Exiting...Goodbye!");
                        running = false;
                        break;
                }
            }
            Console.ReadLine();
        }

        //This method prompts the user to input their lotto numbers. Checking to see if the number is between 0 - 70.
        static int[] UserNumber()
        {
            int[] userNumbers = new int[6];
            bool running = true;
            
            for (int i = 0; i < 6; i++)
            { 
                Console.WriteLine("Please enter 6 different numbers (0 - 70)");
                Console.Write("Enter number {0}: ", i + 1);

                string input = Console.ReadLine();
                    
                if (!int.TryParse(input, out userNumbers[i]))
                {
                    Console.WriteLine("You didnt enter a number");
                    i--;
                    continue;
                }
                if (userNumbers[i] > 70)
                {
                    Console.WriteLine("Please enter a valid number (0 - 70)");
                    i--;
                    continue;
                }
                if (userNumbers[i] < 0)
                {
                    Console.WriteLine("Please enter a valid number (0 - 70)");
                    i--;
                    continue;
                }
                if (userNumbers.Take(i).Contains(userNumbers[i]))
                {
                    Console.WriteLine("This number has already been picked. Try again.");
                    userNumbers[i] = 0;
                    i--;
                    continue;
                }
                //userNumbers[i] = userNumbers[i];
            }
            Console.ReadKey();
            Console.Clear();
            return userNumbers;
        }

        //This method displays the users numbers before or after being sorted.
        static void DisplayNumbers(int[] displayNumbers)
        {
            foreach (int number in displayNumbers)
            {
                Console.WriteLine(number);
            }
            Console.ReadKey();
            Console.Clear();
        }

        //This method bubble sorts the users Lotto number in ascending order.
        static void BubbleSort(int[] bubbleSort)
        {
            int n = bubbleSort.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (bubbleSort[j] > bubbleSort[j + 1])
                    {
                        int temp = bubbleSort[j];
                        bubbleSort[j] = bubbleSort[j + 1];
                        bubbleSort[j + 1] = temp;
                    }
                }
            }
        }
        
        //Generates and fills the actualNumber array with random numbers (these are the actual lottery numbers).
        static int[] ActualNumber()
        {
            int min = 0;
            int max = 70;

            int[] actualNumber = new int[6];
            Random random = new Random();
            for (int i = 0; i < actualNumber.Length; i++)
            {
                actualNumber[i] = random.Next(min, max);
            }
            return actualNumber;
        }

        //Compares the UserNumber array and ActualNumber array to see if they match. It outputs a text based on the users score.
        static void CompareArrays(int[] array1, int[] array2)
        {
            List<int> arrayMatches = new List<int>();
            for (int i = 0; i < array1.Length; i++)
            {
                for (int j = 0; j < array2.Length; j++)
                {
                    if (array1[i] == array2[j] && !arrayMatches.Contains(array1[i]))
                    {
                        arrayMatches.Add(array1[i]);
                        break;
                    }
                }
            }

            if (arrayMatches.Count == 0)
            {
                Console.WriteLine("No matches");
                Console.ReadKey();
                Console.Clear();
            }
            else if (arrayMatches.Count == 6)
            {
                Console.WriteLine("Wow you won the lottery!");
            }
            else
            {
                Console.WriteLine($"You got {arrayMatches.Count} matches");
            }
        }

        //Checks for the frequency of numbers the user entered. It creates a list, and then loops through userNumber array, entering each number into the list.
        /*
         *A dictionary is then created. A foreach loop is used to look through all the numbers in the numberMatches list, saving it under number variable.
         *The if else statement is used to add the numbers to the dictionary. If a number appears multiple times it gets tallied up, if it only appears once its value is equal to 1.
         *Finally for each number in the numberFrequency dictionary, its key is printed (the number entered) and its value (the amount of times it appears.
         */
        static void NumberFrequency(int[] array1)
        {
            List<int> numberMatches = new List<int>();
            for (int i = 0; i < array1.Length; i++)
            {
                numberMatches.Add(array1[i]);
            }

            //Dictionary<key, value>
            Dictionary<int, int> numberFrequency = new Dictionary<int, int>();
            foreach (int number in numberMatches)
            {
                if (numberFrequency.ContainsKey(number))
                {
                    numberFrequency[number]++;
                }
                else
                {
                    numberFrequency[number] = 1;
                }
            }

            foreach (var number in numberFrequency)
            {
                Console.WriteLine($"{number.Key} appears {number.Value} times");
            }
            Console.ReadKey();
            Console.Clear();
        }
    }
}
