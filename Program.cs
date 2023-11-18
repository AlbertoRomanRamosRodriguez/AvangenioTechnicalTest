using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvangenioTechnicalTest
{
    public class Program
    {

        public static IList<IList<int>> permute(List<int> nums)
        {
            var permutations = new List<IList<int>>();

            Permutation(nums, new List<int>(), permutations);

            return permutations;
        }

        public static void Permutation(List<int> choices, List<int> workingSet, List<IList<int>> permutations)
        {
            if (choices.Count == 0)
                permutations.Add(new List<int>(workingSet));

            for(int i = 0; i < choices.Count; i++)
            {
                var value = choices[i];
                workingSet.Add(value);
                choices.RemoveAt(i);

                // Recurse until there is no choice left
                Permutation(choices, workingSet, permutations);

                // Reorder the set
                choices.Insert(i, value);
                workingSet.Remove(value);
            }
        }

        private static int powerSum(int N=2, int X=100)
        {
            int combinations = 0;
            List<int> validSquaredNums = new List<int>();
            List<int> foundNumbers = new List<int>();

            for (int i=1; Math.Pow(i,N) < X; i++)
            {
                validSquaredNums.Add((int) Math.Pow(i, N));
            }
            
            var permutations = permute(validSquaredNums);
            List<int> candidateCombination, actualCombination = new List<int>(); // combination being analyzed, subsection that actually sums X
            int currentSum;

            for (int i = 0; i < permutations.Count; i++)
            {
                currentSum = 0;
                candidateCombination = (List<int>) permutations[i];
                actualCombination.Clear();

                foreach (int sqNum in candidateCombination)
                {
                    if (!(foundNumbers.Contains(sqNum)))
                    {
                        currentSum += sqNum;
                        actualCombination.Add(sqNum);

                        if (currentSum == X)
                        {
                            foundNumbers.AddRange(actualCombination);
                            combinations += 1;
                            break;
                        } else if(currentSum > X)
                        {
                            break;
                        }
                    }
                }

            }


            return combinations;
        }

        private static int Solution(int[] numbers)
        {
            // first bug was to assign 0 to the small variable assuming it may not be the smallest number and not even present in the numbers list
            // int small = 0
            int small = numbers[0]; //assigning the first value instead.

            //for (int i = 0; i < numbers.Length; i++)
            for (int i=0; i < numbers.Length; i++)
            {
                if (numbers[i] < small)
                {
                    small = numbers[i];
                }
            }

            return small;
        }

        static void Main (string[] args)
        {
            int[] numbers = {1,1,-2,2};

            int smallest = Solution(numbers);

            Console.WriteLine("##########\nExcercise 1:");
            Console.WriteLine($"The smallest number is {smallest}");
            Console.Write("##########\nExcercise 2:\nX = ");
            int X = int.Parse(Console.ReadLine());
            Console.Write("n = ");
            int N = int.Parse(Console.ReadLine());
            Console.WriteLine($"{powerSum(N, X)}");
            Console.ReadLine();
        }
    }
}
