using PersonServiceLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                PersonService PS = PersonService.GetInstance;
                List<Person> persons = new List<Person>();

                //Parse InputFile and Load repository
                #region Parse and Load
                if (File.Exists(args[0]))
                {

                    //Read Input File
                    StreamReader reader = null;
                    try
                    {
                        reader = new StreamReader(args[0]);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unable to read the file at {0}", args[0]);
                        Environment.Exit(1);
                    }

                    //Parse records and build Persons repository       
                    string PersonRecord = string.Empty;

                    while ((PersonRecord = reader.ReadLine()) != null)
                    {

                        try
                        {
                            PS.ParsePersonRecord(PersonRecord);
                        }
                        catch (FormatException)
                        {

                            Console.WriteLine("Unexpected record format at {0}", PersonRecord);
                            Console.ReadLine();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("File doesn't exist at '{0}'. Please provide valid file", args[0]);
                    Console.ReadLine();
                    Environment.Exit(1);
                }
                #endregion

                //Sorting records
                #region Sorting

                switch (args[1].Trim().ToLower())
                {
                    case "gender":
                        {
                            Console.WriteLine("Records sorted by Gender Ascending");
                            Console.WriteLine("==================================");
                            persons = PS.GetPersons_orderbyGender();
                            break;
                        }
                    case "dateofbirth":
                    case "dob":
                        {
                            Console.WriteLine("Records sorted by DateOfBirth Ascending");
                            Console.WriteLine("=======================================");
                            persons = PS.GetPersons_orderbyBirthDate();
                            break;
                        }
                    case "lastname":
                    case "name":
                        {
                            Console.WriteLine("Records sorted by LastName Descending");
                            Console.WriteLine("=====================================");
                            persons = PS.GetPersons_orderbyLastNameDescending();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid Sort order provided: '{0}'. Defaulting to sort by Last Name", args[1]);
                            Console.WriteLine("Records sorted by LastName Descending");
                            Console.WriteLine("====================================="); persons = PS.GetPersons_orderbyLastNameDescending();
                            break;
                        }
                }
                #endregion

                //Write output to screen
                Console.WriteLine(PS.CreateOutput(persons));
                Console.ReadLine();

            }
            else
            {
                Console.WriteLine("One or more command line arguments missing. Please try again. " +
                    "\nProvide input file path as well as output sort order as command line arguments");
                Console.ReadLine();
            }

            Environment.Exit(0);
        }
    }
}
