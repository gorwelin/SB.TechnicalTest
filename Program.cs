using System;

using SB.CoreTest;

/// <summary>
/// SchoolsBuddy Technical Test.
///
/// Your task is to find the highest floor of the building from which it is safe
/// to drop a marble without the marble breaking, and to do so using the fewest
/// number of marbles. You can break marbles in the process of finding the answer.
///
/// The method Building.DropMarble should be used to carry out a marble drop. It
/// returns a boolean indicating whether the marble dropped without breaking.
/// Use Building.NumberFloors for the total number of floors in the building.
///
/// A very basic solution has already been implemented but it is up to you to
/// find your own, more efficient solution.
///
/// Please use the function Attempt2 for your answer.
/// </summary>
namespace SB.TechnicalTest
{
    class Program
    {
        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine($"Attempt 1 Highest Safe Floor: {Attempt1()}");
            Console.WriteLine($"Attempt 1 Total Drops: {Building.TotalDrops}");

            Console.WriteLine();
            Building.Reset();

            Console.WriteLine($"Attempt 2 Highest Safe Floor: {Attempt2()}");
            Console.WriteLine($"Attempt 2 Total Drops: {Building.TotalDrops}");

            Console.WriteLine();
            Building.Reset();

            Console.WriteLine($"Attempt 3 Highest Safe Floor: {Attempt3()}");
            Console.WriteLine($"Attempt 3 Total Drops: {Building.TotalDrops}");


        }

        /// <summary>
        /// First attempt - start at first floor and work up one floor at a time
        /// until you reach a floor at which marble breaks.
        /// The highest safe floor is one below this.
        /// </summary>
        /// <returns>Highest safe floor.</returns>
        static int Attempt1()
        {
            var i = 0;
            while (++i <= Building.NumberFloors && Building.DropMarble(i));

            return i - 1;
        }

        /// <summary>
        /// 
        /// My approach is to use a jump search, using an interval of 12 to identify 
        /// the floor suitable in as minimal drops as possible
        /// 
        /// You could also implement a function to verify greatest jump number required
        /// to determine the most efficient number for producing the least jumps
        /// 
        /// Binary search would also be a suitable implementation for this, which would
        /// in theory reduce time complexity from O(√n) to O(log n), as shown in attempt 3 (sorry!)
        /// 
        /// </summary>
        /// <returns>Highest safe floor.</returns>
        static int Attempt2()
        {
            if(Building.NumberFloors == 0 || Building.NumberFloors == 1)
            {
                return 0;
            }

            int currentFloor = 0;
            double totalFloors = Building.NumberFloors;
            int jump = 12;

            while (currentFloor < totalFloors - 1) {
                var f = currentFloor + jump;
                
                if (f > totalFloors) f = currentFloor;

                if(!Building.DropMarble(f))
                {
                    totalFloors = f;
                    jump = 1;
                } else
                {
                    currentFloor = f;
                    if (jump > 1) jump--;
                }
            }


            return currentFloor;
        }

        static int Attempt3()
        {
            int currentFloor = 0;
            double totalFloors = Building.NumberFloors;
            int jump = Building.NumberFloors / 2;

            while (currentFloor < totalFloors - 1)
            {
                var f = currentFloor + jump;

                if (f > totalFloors) f = currentFloor;

                if (!Building.DropMarble(f))
                {
                    totalFloors = f;
                    jump /= 2;
                }
                else
                {
                    currentFloor = f;
                    if (jump > 1) jump--;
                }
            }


            return currentFloor;
        }



    }
}
