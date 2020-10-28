using System;
using System.Collections;
using System.Collections.Generic;

namespace Project_Euler
{
    class Program
    {
        
        static void Main()
        {
            // Variabes i might need.
            string sectionBar = "\n==================================================================================================================\n";

            // Main loop ==> Like a menu, you can come back to it or exit out.
            while (true){
                // Construct a list of problems
                List<Problem> Problems = new List<Problem>();

                // Add problems"
                Problems.Add(
                    new Problem(Problems.Count + 1, "Problem 1: Multiples of 3 and 5",
                    "If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.\n" +
                    "Find the sum of all the multiples of 3 or 5 below 1000.",
                    964994, "5%")
                    );

                Problems.Add(
                    new Problem(Problems.Count + 1, "Problem 7: 10001st prime",
                    "By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.\n" +
                    "What is the 10 001st prime number?",
                    421486, "5%")
                    );

                Problems.Add(
                    new Problem(Problems.Count + 1, "Problem 31: Coin sums",
                    "In the United Kingdom the currency is made up of pound (£) and pence (p). There are eight coins in general circulation:\n" +
                    "1p, 2p, 5p, 10p, 20p, 50p, £1 (100p), and £2 (200p).\n" +
                    "It is possible to make £2 in the following way:\n" +
                    "1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p\n" +
                    "How many different ways can £2 be made using any number of coins?",
                    84093, "5%")
                    );

                // Show the user a list of the problems
                Console.WriteLine(sectionBar);
                Console.WriteLine("List of problems:");
                foreach (Problem P in Problems)
                {
                    Console.WriteLine(String.Format("[{0}]: {1}", P.ID, P.Title));
                }

                // Ask the user to input the ID of the desired problem
                Console.WriteLine(sectionBar);
                Console.WriteLine("Please input the ID of a problem to print it's details.");
                int ID = Convert.ToInt32(Console.ReadLine());

                // Control for input
                while (ID > Problems.Count){
                    Console.WriteLine("Please input a valid ID. :)");
                    ID = Convert.ToInt32(Console.ReadLine());
                }

                // Print the details of the problem
                Console.WriteLine(sectionBar);
                Problems[ID-1].PrintInfo();
                PrintSolution(ID);

                // Ask the user what to do next
                Console.WriteLine(sectionBar);
                Console.WriteLine("Please input '1' if you want to select another problem. Any other input will close this program.");
                string Input = Console.ReadLine();
                // If the input isn't '1', close the program
                if (Input != "1")
                {
                    return;
                }
            }
        }
        
        static void PrintSolution(int ProblemID)
        // This function contains all the methods for solving the problems. Preferable, these methods would be part of the Problem class in some way, but I don't know how to do this.
        // I could have made this function return the solution value, but I dont know if this is allways an int.
        {
            switch (ProblemID)
            {
                case 1: // "Multiples of 3 and 5"
                    // For every natural number under 1000
                    int stopAt = 1000;
                    int total = 0;
                    for (int i = 0; i < stopAt; i++)
                    {
                        // Add it to the total if it is divisible by 3 or 5
                        if (i % 3 == 0 || i % 5 == 0)
                        {
                            total += i;
                        }
                    }
                    Console.WriteLine(String.Format("Solution: {0}", total));
                    break;

                case 2: // Determine the 10001st prime. (Turned out to be quite fast :D)

                    // the idea is that a prime number is not divisible by any prime number lower than itself.
                    // Therefore, I keep all discovered primes in a list for checking the next. (Runs in log(O) time? IDK)
                    List<int> P = new List<int>();
                    int numberToCheck = 1; // Prime numbers start at 2. (I wanted to do the +=1 at the start of the while loop)
                    int primeTarget = 10001;
                    while (P.Count < primeTarget) // 'primeNr' is checked this cycle
                    {
                        numberToCheck += 1;
                        bool isPrime = true; // True untill proven otherwise
                        foreach (int n in P) // For each prime that has been found so far
                        {
                            if (numberToCheck % n == 0) // If it devides evenly
                            {
                                isPrime = false;
                                break; // Break out op checking loop
                            }
                        }
                        if (isPrime)
                        {
                            P.Add(numberToCheck); // This list also keeps track of how many primes are found
                        }
                        
                    }
                    Console.WriteLine(String.Format("Solution: The {0}st prime is {1}", primeTarget, numberToCheck));
                    break;

                case 3: // I had to cheat a bit for this one. I observed a pattern in the tables used here: https://www.xarg.org/puzzle/project-euler/problem-31/
                    int[] coins = { 1, 2, 5, 10, 20, 50, 100, 200 };
                    int target = 200;
                    int[] waysToMake = new int[target+1]; // 0,1,2,3...200

                    waysToMake[0] = 1; // We can't make 0p using the coins, but this is done for handiness' sake.

                    foreach (int coin in coins) // For every coin
                    {
                        for (int t = coin; t<=target; t++)
                        {
                            // Using only coins <= coin, how many ways to make target?
                            waysToMake[t] += waysToMake[t-coin];
                            //Console.WriteLine(String.Format("Ways to make {0}p ==> {1}. (coins up to {2})", target, waysToMake[target], coin));
                        }
                    }
                    Console.WriteLine(String.Format("Solution: Amount of ways to make {0}p ==> {1}.", target, waysToMake[target]));
                    break;

                default: // Error (not found)
                    Console.WriteLine(String.Format("This problem (ID={0}) does not exist.", ProblemID));
                    break;
            }
        }

        public class Problem
        // This class contains the information pertaining to the problems.
        {
            public int ID, Solved_by;
            public string Title, Discription, Difficulty;
            public Problem(int id, string title, string discription, int solved_by, string difficulty)
            {
                this.ID = id;
                this.Title = title;
                this.Discription = discription;
                this.Solved_by = solved_by;
                this.Difficulty = difficulty;
            }
            public void PrintInfo()
            {
                Console.WriteLine(this.Title + ":\n" + this.Discription + "\nSolved by: " + this.Solved_by + "\nDifficulty: " + this.Difficulty);
            }
        }
    }
}