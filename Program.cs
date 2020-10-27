using System;
using System.Collections;
using System.Collections.Generic;

namespace Project_Euler
{
    class Program
    {
        static void Main()
        {
            // Main loop ==> Like a menu, you can come back to it or exit out.
            while (true){
                // Construct a list of problems
                List<Problem> Problems = new List<Problem>();

                // Add problem 1: "Multiples of 3 and 5"
                Problems.Add(
                    new Problem(1, "Multiples of 3 and 5",
                    "If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.\n" +
                    "Find the sum of all the multiples of 3 or 5 below 1000.\n",
                    964994, "5%")
                    );

                // Show the user a list of the problems
                Console.WriteLine("List of problems:");
                foreach (Problem P in Problems)
                {
                    Console.WriteLine(String.Format("[{0}]: {1}", P.ID, P.Title));
                }

                // Ask the user to input the ID of the desired problem
                Console.WriteLine("\nPlease input the ID of a problem to print it's details.");
                int ID = Convert.ToInt32(Console.ReadLine());

                // Control for input
                while (ID > Problems.Count){
                    Console.WriteLine("\nPlease input a valid ID. :)");
                    ID = Convert.ToInt32(Console.ReadLine());
                }
                
                // Print the details of the problem
                Problems[ID-1].PrintInfo();
                PrintSolution(ID);

                // Ask the user what to do next
                Console.WriteLine("\nPlease input '1' if you want to select another problem. Any other input will close this program.");
                string Input = Console.ReadLine();
                if (Input != "1")
                {
                    // Close the program
                    return;
                }
                Console.WriteLine("\n");
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
                Console.WriteLine("\n"+this.Title + ":\n" + this.Discription + "\nSolved by: " + this.Solved_by + "\nDifficulty: " + this.Difficulty);
            }
        }
    }
}