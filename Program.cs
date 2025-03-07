﻿using System;
using ConsoleExtensions; // The Console class cannot be directly extended but you can do this (see ConsoleEx.cs)

namespace SearchArray
{
    class SearchArray
    {
        // Adjust these values as required
        static int size;
        static int rangeMin = -999; // minimum value for random fill (> int.MinValue)
        static int rangeMax = 999; // maximum value for random fill (<= int.MaxValue)
        static int randomSeed = 1234; // this ensures all runs get the same 'random' sequence for comparison purposes
        static bool showWorking = true; // set to trace execution of Binary Search

        // Declare an array to hold random inteher values between rangeMin and rangeMax (inclusive)
        static int[] array;

        static void Main(string[] args)
        {

            do
            {
                size = ConsoleEx.InputInt("Enter array size (1-1000):", 1, 1000, ConsoleColor.Cyan);
                array = new int[size];
                // fill the array with values between rangeMin and rangeMax
                FillArray(array, rangeMin, rangeMax);

                ConsoleEx.WriteLine("Unsorted array:");
                DisplayArray(array);

                // Only use Binary Search when ready
                bool tryBinary = ConsoleEx.ChoiceYN("Do Binary search (Y) or linear (N)?");

                // Array must be sorted for Binary Search to work
                if (tryBinary)
                {
                    SortArray(array);
                    ConsoleEx.WriteLine("Sorted Array:");
                    DisplayArray(array);
                }

                //  Search the array to see if a value is present or not 
                SearchForValues(tryBinary);

            } while (ConsoleEx.ChoiceYN());
            
        }


        private static int BinarySearch(int[] array, int find)
        {
            int elementsCompared = 0;
            if (showWorking) 
                ConsoleEx.WriteLine("\nBinary Search for " + find);
            //TODO Start
            // remove this line and code your solution between the commented algorithm lines

            int low = 0;
            int high = array.Length - 1;
            int midpoint;
            int foundAt = -1; // -1 indicates value not yet found

            if (showWorking) 
                ConsoleEx.WriteLine($" low   midpoint=value     high", ConsoleColor.Cyan);
            
            while ((foundAt == -1) && (low <= high))
            {
                midpoint = (low + high) / 2; // assumed integer overflow will not occur even

                if (showWorking)
                {
                    ConsoleEx.WriteLine($"[{low,3}] <-- [{midpoint,3}]={array[midpoint],-4} --> [{high,3}]", ConsoleColor.Cyan);
                    elementsCompared++;
                }

                if (array[midpoint] == find)
                    foundAt = midpoint;
                else if (array[midpoint] < find)
                    low = midpoint + 1;
                else
                    high = midpoint - 1;
            }

            //TODO End
            if (showWorking) 
                ConsoleEx.WriteLine(elementsCompared + " elements were compared", ConsoleColor.Cyan);
            return foundAt;
        }


        // <summary>This method searches an unsorted array of integers
        ///    for a given value</summary>
        /// <param name="array">the array to be searched</param>
        /// <param name="value">the value to be foundAt</param>
        /// <returns>An integer indicating
        /// either the zero-based index position of value if it is foundAt, 
        /// or -1 if it is not.</returns>
        /// 
        private static int LinearSearch(int[] array, int target)
        {
            int comparisons = 0; 
            int foundAt = -1; // target not yet found
            ConsoleEx.WriteLine("\nLinear Search for " + target);
            foreach (int element in array)
            {
                comparisons++;
                if (element == target)
                {
                    foundAt = comparisons - 1; 
                    break; // no need to look any further
                }
            }
            ConsoleEx.WriteLine(comparisons + " elements were compared");
            return foundAt;
        }

        private static void SearchForValues(bool tryBinary)
        {
            // Use rangeMin - 1 to indicate  no more inputs.
            int stopVal = rangeMin - 1;
            Console.WriteLine();
            ConsoleEx.WriteLine($"Enter values ({rangeMin} to {rangeMax}) to find, using {stopVal} to quit.");
            int target = ConsoleEx.InputInt($"Find value ({stopVal} to quit): ", rangeMin-1, rangeMax, ConsoleColor.Cyan);

            while (target != stopVal)
            {
                int foundAt;

                // Search for the value within the array
                if (tryBinary)
                {
                    foundAt = BinarySearch(array, target);
                }
                else
                {
                    foundAt = LinearSearch(array, target);
                }

                if (foundAt >= 0)
                {
                    ConsoleEx.WriteLine($"{target} was found at index {foundAt}");
                }
                else
                {
                    ConsoleEx.WriteLine($"{target} was not found");
                }

                target = ConsoleEx.InputInt($"Find value ({stopVal} to quit): ", rangeMin - 1, rangeMax, ConsoleColor.Cyan);

            }
        }

        private static void FillArray(int[] array, int fillMin, int fillMax)
        {
            // Fill whole array with random values between fillMin and fillMax
            {
                // use a constant seed for easier comparison
                Random randomiser = new Random(randomSeed);
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = randomiser.Next(fillMin, fillMax + 1);
                }
            }
        }
        private static void DisplayArray(int[] array)
        {
            int valuesPerLine = 10;
            ConsoleEx.WriteLine("The array elements are: ");

            // Low digits of index header
            ConsoleEx.Write("  +  ", ConsoleColor.DarkCyan);
            for (int col = 0; col < valuesPerLine; col++)
            {
                ConsoleEx.Write($"{col,5}", ConsoleColor.DarkCyan);
            }
            // 
            for (int i = 0; i < array.Length; i++)
            {
                // New rows begin with first index displayed in DarkCyan
                if (i % valuesPerLine == 0)
                {
                    ConsoleEx.WriteLine();
                    ConsoleEx.Write($"{i,5}", ConsoleColor.DarkCyan);
                }
                ConsoleEx.Write($"{array[i],5}");
            }

            ConsoleEx.WriteLine();
        }

        private static void SortArray(int[] array)
        {
            // Use built in array sort Method
            Array.Sort(array);
        }
    }
}
