
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var listAge = new List<int>();
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\murom\OneDrive\Рабочий стол\test\TestConsole\TestConsole\csvFiles\ListName.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;

                try
                { 
                    while (!parser.EndOfData)
                    {
                        string[] fileds = parser.ReadFields();

                        foreach (string file in fileds)
                        {
                            var surname = file.Split(' ')[0];
                            var name = file.Split(' ')[1];
                            var patronymic = file.Split(' ')[2].Replace(';', ' ').ToCharArray().Where(x => !char.IsDigit(x)).ToArray();
                            var dateBirth = file.Split(' ')[2].Replace(';', ' ').ToCharArray().Where(x => char.IsDigit(x)).ToArray();
                      
                            var date = DateTime.ParseExact(dateBirth, "yyyy", CultureInfo.CurrentCulture).Year;
                            var age = DateTime.Now.Year - date;
                            listAge.Add(age);

                            if (age < 60 && name[name.Length - 1] == 'а')
                            {
                                age = 60 - age;

                                if (age < 0)
                                {
                                    age = Math.Abs(age);
                                }
                            }
                            else
                            {
                                age = Math.Abs(65 - age);
                            }
                            Console.WriteLine("{0}.{1}.{2}   {3}", surname, name[0], patronymic[0], age);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                   
            }
            Console.WriteLine("_____________________________________________________");
            Console.WriteLine("Минимальный возраст {0}", listAge.Min());
            Console.WriteLine("Максимальный возраст {0}", listAge.Max());
            Console.WriteLine("Средний возраст {0}", Math.Round(listAge.Average()));
            Console.ReadKey();
        }

    }
}
