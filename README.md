Exception handling :هى الطريقة اللي بنخلى البرنامج يتعامل بيها مع المشاكل من خلال رسائل تظهر بدل ما البرنامج يقفل بشكل غير متوقع 
==============================================================================================
Task 1
(

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int>();

        try
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Write("Enter a number: ");
                int number = Convert.ToInt32(Console.ReadLine());

                for (int j = 0; j < numbers.Count; j++)
                {
                    if (numbers[j] == number)
                    {
                        throw new Exception("Duplicate number!");
                    }
                }

                numbers.Add(number);
            }

            Console.WriteLine("No duplicates found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
)
====================================================================================================

Task 2


using System;

class Program
{
    static void CheckVowels(string text)
    {
        bool found = false;

        for (int i = 0; i < text.Length; i++)
        {
            char letter = char.ToLower(text[i]);

            if (letter == 'a' ||
                letter == 'e' ||
                letter == 'i' ||
                letter == 'o' ||
                letter == 'u')
            {
                found = true;
            }
        }

        if (found == false)
        {
            throw new Exception("No vowels found!");
        }
    }

    static void Main()
    {
        try
        {
            Console.Write("Enter a string: ");
            string text = Console.ReadLine();

            CheckVowels(text);

            Console.WriteLine("Vowels found!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
