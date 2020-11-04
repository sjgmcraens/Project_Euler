﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;

namespace Project_Euler
{
    class Program
    {

        static void Main()
        {
            // Variabes we might need.
            string sectionBar = "\n========================================================================================================================\n";
            //bool firstMenuLoop = true;

            // Main loop ==> Like a menu, you can come back to it or exit out.
            while (true)
            {

                // Show the user a list of the problems
                Console.WriteLine(sectionBar);
                Console.WriteLine("List of problems: ("+Problems.P.Count+" total)");
                foreach (KeyValuePair<int, Problem> entry in Problems.P.OrderBy(key => key.Key))
                {
                    Console.WriteLine(String.Format("[{0}]: {1}", entry.Key, entry.Value.Title));
                }



                // Ask the user to input the ID of the desired problem; the program can be exited at this point.
                Console.WriteLine(sectionBar);
                Console.WriteLine("Please input the ID of a problem to print it's details.\n" +
                    "To exit the program, input 'exit'.");
                string Input = Console.ReadLine();
                if (Input == "exit") { return; }
                int ID = Convert.ToInt32(Input);

                // Some control for input
                while (!Problems.P.ContainsKey(ID))
                {
                    Console.WriteLine("Please input an existing ID.");
                    Input = Console.ReadLine();
                    if (Input == "exit") { return; }
                    ID = Convert.ToInt32(Input);
                }

                // Print the details of the selected problem
                Console.WriteLine(sectionBar);
                Problem p = Problems.P[ID];
                Console.WriteLine(p.Title + ":\n" + p.Discription + "\n\nSolved by: " +
                    p.Solved_by + "\nDifficulty: " + p.Difficulty + "%\n\nSolution: " + Problems.solutions(ID));

                // Ask the user what to do next
                Console.WriteLine(sectionBar);
                Console.WriteLine("Input 'exit' to exit the program; any other input will return you to the list of problems.");
                Input = Console.ReadLine();
                // If the input isn't '1', close the program
                if (Input == "exit")
                {
                    return;
                }

                //firstMenuLoop = false;
            }
        }

        static class Problems
        // This class contains the information pertaining to the all the problems.
        {
            // Dictinairy of problems
            public static readonly Dictionary<int, Problem> P;

            static Problems() // This is the constructor: below, we fill 'P' and add methods for returning the solution to the problems.
            {

                P = new Dictionary<int, Problem>();

                P.Add(1, new Problem("Multiples of 3 and 5",
                    "If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.\n" +
                    "Find the sum of all the multiples of 3 or 5 below 1000.",
                    964994, 5));

                P.Add(7, new Problem("10001st prime",
                    "By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.\n" +
                    "What is the 10 001st prime number?",
                    421486, 5));

                P.Add(31, new Problem("Coin sums",
                    "In the United Kingdom the currency is made up of pound (£) and pence (p). There are eight coins in general circulation:\n" +
                    "1p, 2p, 5p, 10p, 20p, 50p, £1 (100p), and £2 (200p).\n" +
                    "It is possible to make £2 in the following way:\n" +
                    "1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p\n" +
                    "How many different ways can £2 be made using any number of coins?",
                    84093, 5));

                P.Add(2, new Problem("Even Fibonacci numbers",
                    "Each new term in the Fibonacci sequence is generated by adding the previous two terms. By starting with 1 and 2, the first 10 terms will be:\n" +
                    "1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ...\n" +
                    "By considering the terms in the Fibonacci sequence whose values do not exceed four million, find the sum of the even-valued terms.",
                    768279, 5));

                P.Add(3, new Problem("Largest prime factor",
                    "The prime factors of 13195 are 5, 7, 13 and 29.\n" +
                    "What is the largest prime factor of the number 600851475143 ?",
                    550872, 5));

                P.Add(4, new Problem("Largest palindrome product",
                    "A palindromic number reads the same both ways. The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 × 99.\n" +
                    "Find the largest palindrome made from the product of two 3-digit numbers.",
                    486673, 5));
            }

            public static long solutions(int pID)
            {
                switch (pID)
                {
                    case 1: // Multiples of 3 and 5

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
                        return total;

                    case 7: // 10001st prime

                        return Prime.GetPrimeAtIndex(10001); // See also the static class 'Prime'

                    case 31: // Coin sums

                        int[] coins = { 1, 2, 5, 10, 20, 50, 100, 200 };
                        int target = 200;
                        int[] waysToMake = new int[target + 1]; // 0,1,2,3...200

                        waysToMake[0] = 1; // We can't make 0p using the coins, but this is done for handiness' sake.
                        // In the algorithm, this amounts to adding 1 possibility every time t-coin is 0p.

                        foreach (int coin in coins) // For every coin
                        {
                            for (int t = coin; t <= target; t++)
                            {
                                // Using only coins <= coin, how many ways to make target?
                                waysToMake[t] += waysToMake[t - coin];
                            }
                        }
                        return waysToMake[200];

                    case 2: // Even Fibbonachi numbers

                        List<int> Fib = new List<int>() { 1, 2 };
                        target = 4000000;
                        total = 2;
                        while (Fib[Fib.Count - 1] < target) // While < 4000000
                        {
                            int nextFib = Fib[Fib.Count - 1] + Fib[Fib.Count - 2];
                            Fib.Add(nextFib);
                            if (nextFib % 2 == 0) // If even
                            {
                                total += nextFib;
                            }
                        }
                        return total;

                    case 3: //Largest prime factor
                        long numToFactor = 600851475143;
                        List<long> factors = new List<long>();

                        while (numToFactor != 1) // As long as the remainder is not 1 (This would mean that it's last factor was found.)
                        {
                            // Loop through the primes:
                            for (int i=0; true; i++)
                            {
                                long P = Prime.GetPrimeAtIndex(i); // Get i'th prime.
                                // If wholly divisible:
                                if (numToFactor % P == 0)
                                {
                                    // Divide the number and add the factor to the list of factors.
                                    numToFactor /= P;
                                    factors.Add(P);
                                }
                            }
                        }
                        return factors.Max();

                    case 4: //Largest palindrome product

                        // I think it's best to check palindrome from largest to smallest.
                        // Since it must be a multiple of two three-digit numbers, 998001 is going to be the starting point.
                        // Instead of brute-forcing all multiplications (999*999, 998*999 ...), Lets go down from 998001 
                        // and keep checking palindromes untill we find one that can be made with two three-digit numbers. 

                        // Settings
                        int Multdigits = 3;
                        int Multamount = 2; // Doesn't work for inputs > 2 unfortunately.

                        // Some preparatory calculations.
                        int maxMult = Int32.Parse(String.Concat(Enumerable.Repeat("9", Multdigits))); // 999
                        int maxTotal = Convert.ToInt32(Math.Pow(maxMult, Multamount)); // 999*999 = 998001

                        int minMult = Convert.ToInt32(Math.Pow(10, Multdigits-1)); // 100
                        int minTotal = Convert.ToInt32(Math.Pow(minMult, Multamount)); // 100*100 = 100000

                        string maxTotal_str = Math.Pow(maxMult, Multamount).ToString(); // 998001

                        // Determine first palindrome
                        int palindrome = GetLargestPalinDromeBelow(maxTotal);

                        while (palindrome >= minTotal)
                        {
                            //Console.WriteLine(palindrome);

                            // Now that we have a palindrome, lets check whether it can be made using two three-digit numbers
                            for (int i = maxMult; i >= minMult; i--) // 999, 998, ... , 100
                            {
                                if (palindrome % i == 0) // If wholly divisible (to completely generalize: make this recursive)
                                {
                                    if ((palindrome / i).ToString().Length == Multdigits) // If the division creates a 3-digit number.
                                    {
                                        //Console.WriteLine(String.Format("Palindrome {0} can be made using {1} x {2}",palindrome,i,palindrome / i));
                                        return palindrome; // Return the succesfull palindrome.
                                    }
                                }
                            }
                            palindrome = GetLargestPalinDromeBelow(palindrome-1);
                            
                        }
                        // No solution was found.
                        return -1;

                    default:
                        Console.WriteLine(String.Format("the solution to problem {0} is not implemented yet.", pID));
                        return 0;
                }
            }
        }
        public class Problem
        {
            public int Difficulty, Solved_by;
            public string Title, Discription;

            public Problem(string Title_, string Discription_, int Solved_by_, int Difficulty_)
            {
                this.Title = Title_;
                this.Discription = Discription_;
                this.Difficulty = Difficulty_;
                this.Solved_by = Solved_by_;
            }
        }

        public static class Prime
            // This class contains a list of the known primes and related methods.
        {
            public static List<long> Primes;
            public static long largestCheckedNumber;

            // Constructor
            static Prime()
            {
                // Add the first prime
                Primes = new List<long>() { 2 };
                largestCheckedNumber = 2;
            }

            public static bool IsPrime(long n)
            // This function checks for primality. Since I use all primes below n to determine
            // wether n is prime, another function is refrenced and everything should word.
            {
                // If not all number up to p have been checked, calculate them
                if (largestCheckedNumber < n - 1)
                {
                    calcNewPrimes("upToNumber", n - 1);
                }
                // Update largestCheckedNumber
                if (n > largestCheckedNumber) {
                    largestCheckedNumber = n;
                }
                // Do prime Check
                foreach (int p in Primes) // For each prime that has been found so far
                {
                    if (n % p == 0) // If it devides evenly
                    {
                        return false;
                    }
                }
                return true;
            }

            public static void calcNewPrimes(string mode, long n)
            // This funcion calculates new primes.
            // It has a few modes and an input that specify's the amount of new primes
            // to be calculated according to the selected mode.
            {
                switch (mode) {

                    case "upToPrimeIndex": // doesn't count 0, so: n=1 ==> 2
                        for (long i = largestCheckedNumber + 1; Primes.Count <= n; i++)
                        {
                            if (IsPrime(i)) // If prime
                            {
                                Primes.Add(i); // Add i to Primes
                            }
                        }
                        break;

                    case "upToNumber": // 10 ==> 2,3,5,7 (includes 10)
                        for (long i = largestCheckedNumber + 1; i <= n; i++)
                        {
                            if (IsPrime(i)) // If prime
                            {
                                Primes.Add(i); // Add i to Primes
                            }
                        }
                        break;

                    case "amountOfNew":
                        int newPrimesFound = 0;
                        for (long i = largestCheckedNumber + 1; newPrimesFound < n; i++)
                        {
                            if (IsPrime(i))
                            {
                                Primes.Add(i); // Add i to P
                                newPrimesFound++;
                            }
                        }
                        break;

                    default:
                        Console.WriteLine(String.Format("Primes.calcNewPrimes Error: Unknown mode '{0}'. Valid modes are {1}.", mode, ""));
                        break;
                }
            }
            public static long GetPrimeAtIndex(int i)
            // This function returns the i'th prime. (starts at n=1 ==> 2)
            {
                // If the i'th prime is not known: calculate it.
                if (Primes.Count < i)
                {
                    calcNewPrimes("upToPrimeIndex", i);
                }
                return Primes[i - 1];
            }
        }
        public static string ReverseString(string s)
            // This function reverses a string
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static int GetLargestPalinDromeBelow(int n_int)
            // This function gives the largest palindrome below n (including n) Example: 99812
        {
            string n_str = n_int.ToString(); // Ex: "99812"
            bool lengthIsEven = n_str.Length % 2 == 0; // Ex: False
            // We determine the different parts of the number. Ex: 99, 8, 12
            string firstHalf_str = n_str.Substring(0, n_str.Length / 2); // This should floor the fraction. Ex: "99" 
            int firstHalf_int = Int32.Parse(firstHalf_str); // Ex: 99
            string secondHalf_str = n_str.Substring(n_str.Length-firstHalf_str.Length); // Ex: "12"
            int secondHalf_int = Int32.Parse(secondHalf_str); // Ex: 12

            //Console.WriteLine(firstHalf_str + " " + secondHalf_str);

            if (firstHalf_str == ReverseString(secondHalf_str))
            // If the second half reversed equals the first half, the number is a palindrome
            {
                //Console.WriteLine("Debug, 1");
                return n_int;
            }
            else if (secondHalf_int >= Int32.Parse(ReverseString(firstHalf_str)))
            // If the second half is >= than the reverse of the first half (Ex: 12>=99 ==> false), than finding the palingdrome is easy.
            // In the case of 32899 for example, the second half (99) can become the reverse of the first half (23).
            {
                //Console.WriteLine("Debug, 2");
                string newPalindrome = firstHalf_str;
                if (!lengthIsEven)
                {
                    newPalindrome += n_str.Length / 2; // Middle caracter is unchanged
                }
                return Int32.Parse(newPalindrome += ReverseString(firstHalf_str)); // Ex: 32899 ==> 32823
            }
            else
            // If the second half was smaller than the reverse of the first half, we take the first half including
            // the middle caracter, subtract 1, and fix the end to be the revers of the start.
            {
                //Console.WriteLine("Debug, 3");
                int firstHalfInclusive = Int32.Parse(n_str.Substring(0, n_str.Length / 2)); // Ex: 998
                int newFirstHalfInclusive = firstHalfInclusive-1; // Ex: 997
                //Console.WriteLine("NewFirstHalfIncl: " + newFirstHalfInclusive);
                string newFirstHalfInclusive_str = newFirstHalfInclusive.ToString(); // Ex: "997"
                int subtract = 0; // Case: even
                if (!lengthIsEven)
                {
                    subtract = 1;
                }
                string newFirstHalf_str = newFirstHalfInclusive_str.Substring(0, newFirstHalfInclusive_str.Length - subtract); // Ex: "99"
                string newSecondHalf_str = ReverseString(newFirstHalf_str);
                string newPalindrome = newFirstHalfInclusive_str + newSecondHalf_str; // Ex: 99799
                return Int32.Parse(newPalindrome);
            }
        }
    }
}