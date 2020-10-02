using System;
using System.Collections.Generic;
using System.Threading;

namespace TjuvOchPolisMattias
{
    class Program
    {
        static char[,] gamePlan = new char[25, 100];
        static List<Person> personList = new List<Person>();
        static List<Thief> prisonList = new List<Thief>();
        static Random rnd = new Random();
        static string encounterMessage;
        static string noGoodsToStealMessage;
        static int nrOfRobbedCitizens;
        static int nrOfCommitedRobberies;
        static string ThievesBehindBarsMessage;
        static string ReleaseMessage;
        static int prisonerIDnr = 1;

        static void Main(string[] args)
        {
            GeneratePeopleType(15, "police");  //Change the int parameter value to alter the number of the specific
            GeneratePeopleType(20, "citizen");//type of instance that will be generated. 
            GeneratePeopleType(15, "thief"); //This will affect the crime/arrest ratio in the app.

            while (true)
            {
                GenerateMap();
                Print();
                UpdateCoordinates();
            }
        }

          //Updates the X and Y property-values of each instance(Police, Thief, Citizen). If the values reaches the limits of the
         //gamePlan array it will reverse the values until they reach the opposite ones,
        //giving the illusion of teleporting the playericon.
        static void UpdateCoordinates()
        {
            foreach (var directionNr in personList)
            {

                switch (directionNr.Direction)
                {
                    case 1: //Vertical upward
                        if (directionNr.MovementYAxis == 24)
                            directionNr.MovementYAxis = 0;
                        else
                            directionNr.MovementYAxis++;

                        break;

                    case 2: //Vertical downward
                        if (directionNr.MovementYAxis == 0)
                            directionNr.MovementYAxis = 24;
                        else
                            directionNr.MovementYAxis--;

                        break;

                    case 3: //Horizontal right
                        if (directionNr.MoveMentXAxis == 99)
                            directionNr.MoveMentXAxis = 0;
                        else
                            directionNr.MoveMentXAxis++;

                        break;

                    case 4: //Horizontal left
                        if (directionNr.MoveMentXAxis == 0)
                            directionNr.MoveMentXAxis = 99;
                        else
                            directionNr.MoveMentXAxis--;

                        break;

                    case 5: //up/right
                        if (directionNr.MovementYAxis == 0 || directionNr.MoveMentXAxis == 99)
                        {
                            while (true)
                            {
                                directionNr.MovementYAxis++;
                                directionNr.MoveMentXAxis--;
                                if (directionNr.MovementYAxis == 24 || directionNr.MoveMentXAxis == 0)
                                    break;
                            }
                        }
                        else
                        {
                            directionNr.MovementYAxis--;
                            directionNr.MoveMentXAxis++;
                        }
                        break;

                    case 6: //up/left
                        if (directionNr.MovementYAxis == 0 || directionNr.MoveMentXAxis == 0)
                        {
                            while (true)
                            {
                                directionNr.MovementYAxis++;
                                directionNr.MoveMentXAxis++;
                                if (directionNr.MovementYAxis == 24 || directionNr.MoveMentXAxis == 99)
                                    break;
                            }
                        }
                        else
                        {
                            directionNr.MovementYAxis--;
                            directionNr.MoveMentXAxis--;
                        }
                        break;

                    case 7: //Down/left
                        if (directionNr.MovementYAxis == 24 || directionNr.MoveMentXAxis == 0)
                        {
                            while (true)
                            {
                                directionNr.MovementYAxis--;
                                directionNr.MoveMentXAxis++;
                                if (directionNr.MovementYAxis == 0 || directionNr.MoveMentXAxis == 99)
                                    break;
                            }
                        }
                        else
                        {
                            directionNr.MovementYAxis++;
                            directionNr.MoveMentXAxis--;
                        }
                        break;

                    case 8: //Down/right
                        if (directionNr.MovementYAxis == 24 || directionNr.MoveMentXAxis == 99)
                        {
                            while (true)
                            {
                                directionNr.MovementYAxis--;
                                directionNr.MoveMentXAxis--;
                                if (directionNr.MovementYAxis == 0 || directionNr.MoveMentXAxis == 0)
                                    break;
                            }
                        }
                        else
                        {
                            directionNr.MovementYAxis++;
                            directionNr.MoveMentXAxis++;
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        //Prints the game and fields of type strings. Resets them all at the end of the method.
        static void Print()
        {
            PrisonTimer();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"[Rånade medborgare: {nrOfRobbedCitizens}." +
                              $" Totalt antal rån: {nrOfCommitedRobberies}.] MÖJLIGT STRAFF: 30 DAGARS FÄNGELSE\n" +
                              $"{ThievesBehindBarsMessage}");
           
            for (int y = 0; y < 25; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    switch (gamePlan[y, x])
                    {
                        case 'P':
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(gamePlan[y, x]);
                            break;

                        case 'T':
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(gamePlan[y, x]);
                            break;

                        case 'M':
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(gamePlan[y, x]);
                            break;

                        case 'X':
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(gamePlan[y, x]);
                            break;

                        case ' ':
                            Console.Write(gamePlan[y, x]);
                            break;

                        default:
                            break;
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
            if (!string.IsNullOrEmpty(encounterMessage) || !string.IsNullOrEmpty(noGoodsToStealMessage)|| !string.IsNullOrEmpty(ReleaseMessage))
            {
                Console.WriteLine($"{encounterMessage}\n{noGoodsToStealMessage}{ReleaseMessage}");
                Thread.Sleep(2000);
                Console.Clear();
            }
            else
                Thread.Sleep(500);

            ThievesBehindBarsMessage = null;
            encounterMessage = null;
            noGoodsToStealMessage = null;
            ReleaseMessage = null;
        }

        //Indexing of the gamePlan array with blank spaces and selected icons of players.
        static void GenerateMap()
        {
            for (int y = 0; y < 25; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    gamePlan[y, x] = ' ';
                    foreach (var propertyCoordinates in personList)
                    {
                        if (propertyCoordinates.MovementYAxis == y && propertyCoordinates.MoveMentXAxis == x)
                        {
                            gamePlan[y, x] = propertyCoordinates.PlayerIcon;
                        }
                    }
                }
            }
            CollideCheckAndItemTransaction();
        }

        //Creates the specified amount of instances of subclasses, alters the propertyvalues and then adds them to a list.
        static void GeneratePeopleType(int thisMany, string thisKind)
        {
            for (int i = 0; i < thisMany; i++)
            {
                int directionNumber = rnd.Next(1, 8 + 1);
                int xCoordinate = rnd.Next(0, 99 + 1);
                int yCoordinate = rnd.Next(0, 24 + 1);
                if (thisKind == "police")
                {
                    Police tempPerson = new Police(yCoordinate, xCoordinate, directionNumber, 'P')
                    {
                        Keys = 0,
                        Money = 0,
                        CellPhone = 0,
                        Watch = 0
                    };
                    personList.Add(tempPerson);
                }
                else if (thisKind == "thief")
                {
                    Thief tempPerson = new Thief(yCoordinate, xCoordinate, directionNumber, 'T', false, 0)
                    {
                        Keys = 0,
                        Money = 0,
                        CellPhone = 0,
                        Watch = 0
                    };
                    personList.Add(tempPerson);
                }
                else if (thisKind == "citizen")
                {
                    Citizen tempPerson = new Citizen(yCoordinate, xCoordinate, directionNumber, 'M')
                    {
                        Keys = 1,
                        Money = rnd.Next(1, 2000),
                        CellPhone = 1,
                        Watch = 1
                    };
                    personList.Add(tempPerson);
                }
            }
        }

         //Checks if several instances share the same values of the X and Y properties.
        //Depending on what types of instances that share the same values their inventory properties will be changed.
        static void CollideCheckAndItemTransaction()
        {
            for (int i = 0; i < personList.Count - 1; i++)
            {
                int tempY = personList[i].MovementYAxis;
                int tempX = personList[i].MoveMentXAxis;
                for (int j = i + 1; j < personList.Count; j++)
                {
                    if (tempY == personList[j].MovementYAxis && tempX == personList[j].MoveMentXAxis)
                    {
                        if (personList[i] is Police && personList[j] is Thief)
                        {
                            if (personList[j].Watch == 0 && personList[j].Keys == 0 && personList[j].Money == 0 && personList[j].CellPhone == 0)
                            {
                                personList[j].PrisonIdUpdate(prisonerIDnr);
                                encounterMessage += "*|Polis stötte på en tjuv! Inte gjort nåt, greps ändå..|*";
                            }
                            else
                            {
                                personList[j].PrisonIdUpdate(prisonerIDnr);
                                personList[i].CellPhone += personList[j].CellPhone;
                                personList[i].Keys += personList[j].Keys;
                                personList[i].Money += personList[j].Money;
                                personList[i].Watch += personList[j].Watch;
                                encounterMessage += $"*|Polis grep en tjuv! Allt stöldgods i beslag: " +
                                                    $"{(personList[j].CellPhone > 0 ? $"{personList[j].CellPhone} x mobiltelefon. " : "")}" +
                                                    $"{(personList[j].Keys > 0 ? $"{personList[j].Keys} x nyckel. " : "")}" +
                                                    $"{(personList[j].Money > 0 ? $"{personList[j].Money} SEK. " : "")}" +
                                                    $"{(personList[j].Watch > 0 ? $"{personList[j].Watch} x klocka. " : "")}|*";
                            }
                            
                            gamePlan[tempY, tempX] = 'X';
                            personList[j].PrisonCheck();
                            prisonList.Add((Thief)personList[j]);
                            personList.Remove(personList[j]);
                            prisonerIDnr++;
                        }
                        else if (personList[i] is Citizen && personList[j] is Thief)
                        {
                            int stolenProperty = rnd.Next(1, 4 + 1);
                            if (personList[i].Keys > 0 && personList[i].Money > 0 && personList[i].Watch > 0 && personList[i].CellPhone > 0)
                                nrOfRobbedCitizens++;

                            switch (stolenProperty)
                            {
                                case 1:
                                    if (personList[i].CellPhone == 0)
                                        noGoodsToStealMessage += "*|Rånförsök, tjuven kunde dock inte hitta telefonen. Någon hann sno den före!|*";
                                    else
                                    {
                                        encounterMessage += "*|Tjuv rånade en medborgare på dennes telefon!|*";
                                        personList[j].CellPhone += personList[i].CellPhone;
                                        personList[j].CellPhone = 0;
                                    }
                                    break;

                                case 2:
                                    if (personList[i].Money == 0)
                                        noGoodsToStealMessage += "*|Rånförsök, tjuven kunde dock inte hitta pengarna. Någon hann sno dem före!|*";
                                    else
                                    {
                                        encounterMessage += $"*|Tjuv rånade en medborgare på dennes pengar, totalt {personList[i].Money} kr!|*";
                                        personList[j].Money += personList[i].Money;
                                        personList[i].Money = 0;
                                    }
                                    break;

                                case 3:
                                    if (personList[i].Keys == 0)
                                        noGoodsToStealMessage += "*|Rånförsök, tjuven kunde dock inte hitta nycklarna. Någon hann sno dem före!|*";
                                    else
                                    {
                                        encounterMessage += "*|Tjuv rånade en medborgare på dennes nycklar!|*";
                                        personList[j].Keys += personList[i].Keys;
                                        personList[i].Keys = 0;
                                    }
                                    break;

                                case 4:
                                    if (personList[i].Watch == 0)
                                        noGoodsToStealMessage += "*|Rånförsök, tjuven kunde dock inte hitta klockan. Någon hann sno den före!|*";
                                    else
                                    {
                                        encounterMessage += "*|Tjuv rånade en medborgare på dennes klocka!|*";
                                        personList[j].Watch += personList[i].Watch;
                                        personList[i].Watch = 0;
                                    }
                                    break;

                                default:
                                    break;
                            }
                            gamePlan[tempY, tempX] = 'X';
                            nrOfCommitedRobberies++;
                        }
                        else
                            continue;
                    }
                }
            }
        }

         //Makes sure the instances of type Thief is added to a list representing a prison.
        //Also puts the instance back to the original list after a certain int-value has been reached.
        static void PrisonTimer()
        {
            for (int i = prisonList.Count-1; i >= 0; i--)
            {
                if (prisonList[i].ThiefImprisoned == true)
                {
                    prisonList[i].PrisonTimer++;

                    ThievesBehindBarsMessage += $"*|Fånge ID: {prisonList[i].PrisonerID}. Tid i finkan: {prisonList[i].PrisonTimer} dagar|*";
                    if (prisonList[i].PrisonTimer == 30)
                    {
                        prisonList[i].PrisonTimer = 0;
                        prisonList[i].ThiefImprisoned = false;
                        personList.Add(prisonList[i]);
                        prisonList.RemoveAt(i);
                        ReleaseMessage = "*|Fånge utsläppt! Har avtjänat sitt straff.. time for crime|*";
                    }
                }
            }
            if (prisonList.Count == 0)
                prisonerIDnr = 1;
        }
    }
}
