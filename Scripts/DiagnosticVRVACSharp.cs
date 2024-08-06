using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagnosticVRVACSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<mCharacter> characterList = new List<mCharacter>();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("===== Main Menu =====");
                Console.WriteLine("1. Create character");
                Console.WriteLine("2. Read character");
                Console.WriteLine("3. Update character");
                Console.WriteLine("4. Delete character");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateCharacter(characterList);
                        break;
                    case "2":
                        ReadCharacter(characterList);
                        break;
                    case "3":
                        UpdateCharacter(characterList);
                        break;
                    case "4":
                        DeleteCharacter(characterList);
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
                Console.WriteLine();
            }
        }

        static void CreateCharacter(List<mCharacter> characterList)
        {
            Console.Write("Enter character's name: ");
            string name = Console.ReadLine();
            Console.Write("Enter character's description: ");
            string description = Console.ReadLine();
            Console.Write("Enter character's type: ");
            string type = Console.ReadLine();

            characterList.Add(new mCharacter(name, description, type));
            Console.WriteLine("Character created successfully.");
        }

        static void ReadCharacter(List<mCharacter> characterList)
        {
            if (characterList.Count == 0)
            {
                Console.WriteLine("No characters in the list.");
                return;
            }

            Console.WriteLine("List of characters:");
            for (int i = 0; i < characterList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {characterList[i].Name}");
            }

            Console.Write("Select a character to view details: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= characterList.Count)
            {
                characterList[index - 1].Details();
                ActionSubMenu(characterList[index - 1]);
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }

        static void ActionSubMenu(mCharacter character)
        {
            bool backToMainMenu = false;

            while (!backToMainMenu)
            {
                Console.WriteLine("\n===== Action Submenu =====");
                Console.WriteLine("1. Walk");
                Console.WriteLine("2. Code");
                Console.WriteLine("3. Sleep");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        character.Walk();
                        break;
                    case "2":
                        character.Code();
                        break;
                    case "3":
                        character.Sleep();
                        break;
                    case "4":
                        backToMainMenu = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        static void UpdateCharacter(List<mCharacter> characterList)
        {
            if (characterList.Count == 0)
            {
                Console.WriteLine("No characters in the list.");
                return;
            }

            Console.WriteLine("List of characters:");
            for (int i = 0; i < characterList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {characterList[i].Name}");
            }

            Console.Write("Select a character to update: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= characterList.Count)
            {
                Console.Write("Enter new name for the character: ");
                string newName = Console.ReadLine();
                Console.Write("Enter new description for the character: ");
                string newDescription = Console.ReadLine();
                Console.Write("Enter new type for the character: ");
                string newType = Console.ReadLine();

                characterList[index - 1].Name = newName;
                characterList[index - 1].Description = newDescription;
                characterList[index - 1].Type = newType;

                Console.WriteLine("Character updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }

        static void DeleteCharacter(List<mCharacter> characterList)
        {
            if (characterList.Count == 0)
            {
                Console.WriteLine("No characters in the list.");
                return;
            }

            Console.WriteLine("List of characters:");
            for (int i = 0; i < characterList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {characterList[i].Name}");
            }

            Console.Write("Select a character to delete: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= characterList.Count)
            {
                characterList.RemoveAt(index - 1);
                Console.WriteLine("Character deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }
    }
    class mCharacter
    {
        public mCharacter(string name, string description, string type) { 
            this.Name = name;
            this.Description = description;
            this.Type = type;
        }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Type { set; get; }
        public void Code() {
            Console.WriteLine("Character is coding! Supposedly");
        }
        public void Walk()
        {
            Console.WriteLine("Character is running! Go Go lazy boy!!!");
        }
        public void Sleep()
        {
            Console.WriteLine("Character is sleeping! shshsh be quiet!!!");
        }
        public void Details()
        {
            Console.WriteLine("My DNI Info");
            Console.WriteLine($"Name: {this.Name}");
            Console.WriteLine($"Description: {this.Description}");
            Console.WriteLine($"Type: {this.Type}");
        }
    }
}
