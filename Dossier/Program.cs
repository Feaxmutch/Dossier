namespace Dossier
{
    internal class Program
    {
        static void Main()
        {
            const string CommandAddDossier = "1";
            const string CommandWriteDossers = "2";
            const string CommandDeleteDosser = "3";
            const string CommandFindDosserBySurname = "4";
            const string CommandExit = "5";

            string[] names = Array.Empty<string>();
            string[] posts = Array.Empty<string>();
            string userInput = string.Empty;
            bool isWorking = true;

            while (isWorking)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.WriteLine($"\n{CommandAddDossier}) добавить досье" +
                                  $"\n{CommandWriteDossers}) вывести все досье" +
                                  $"\n{CommandDeleteDosser}) удалить досье" +
                                  $"\n{CommandFindDosserBySurname}) поиск по фамилии" +
                                  $"\n{CommandExit}) выход");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddDossier:
                        AddDossier(ref names, ref posts);
                        break;

                    case CommandWriteDossers:
                        WriteAllDossiers(names, posts);
                        break;

                    case CommandDeleteDosser:
                        DeleteDossier(ref names, ref posts);
                        break;

                    case CommandFindDosserBySurname:
                        FindDossierBySurname(names, posts);
                        break;

                    case CommandExit:
                        isWorking = false;
                        break;
                }
            }
        }

        static void AddDossier(ref string[] names, ref string[] posts)
        {
            string[] userInputs = new string[4];

            Console.Clear();
            Console.CursorVisible = true;

            Console.Write("Введите имя: ");
            userInputs[0] = Console.ReadLine();

            Console.Write("Введите фамилию: ");
            userInputs[1] = Console.ReadLine();

            Console.Write("Введите отчество: ");
            userInputs[2] = Console.ReadLine();

            Console.Write("Введите Должность: ");
            userInputs[3] = Console.ReadLine();

            for (int i = 0; i < userInputs.Length; i++)
            {
                if (userInputs[i] == string.Empty)
                {
                    userInputs[i] = "Не_указано";
                }
            }

            Console.CursorVisible = false;

            names = AddElement(names, $"{userInputs[1]} {userInputs[0]} {userInputs[2]}");
            posts = AddElement(posts, $"{userInputs[3]}");

            Console.WriteLine("Добавленно следющее досье:");
            WriteDossier(names, posts, names.Length);

            Console.ReadKey(true);
        }

        static void WriteDossier(string[] names, string[] posts, int dosserNumber)
        {
            int dosserIndex = dosserNumber - 1;
            string name = names[dosserIndex];
            string post = posts[dosserIndex];

            Console.WriteLine($"{dosserNumber}. {name} - {post}");
        }

        static void DeleteDossier(ref string[] names, ref string[] posts)
        {
            Console.Clear();
            WriteAllDossiers(names, posts);
            Console.Write("\nВведите номер досье, которое хотите удалить ");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int dosserNumber))
            {
                if (dosserNumber <= 0 || dosserNumber > names.Length)
                {
                    Console.WriteLine("Некоректный номер досье");
                    Console.ReadKey();
                }
                else
                {
                    names = RemoveElement(names, dosserNumber - 1);
                    posts = RemoveElement(posts, dosserNumber - 1);
                }
            }
            else
            {
                Console.WriteLine($"\"{userInput}\" не является числом");
                Console.ReadKey();
            }
        }

        static void FindDossierBySurname(string[] names, string[] posts)
        {
            string userInput = string.Empty;
            bool dossierIsFounded = false;
            string[] surnames = new string[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                surnames[i] = names[i].Split(' ')[0];
            }

            Console.Clear();
            Console.CursorVisible = true;

            Console.Write("Введите начало фамилии, или полную фамилию: ");
            userInput = Console.ReadLine();

            for (int i = 0; i < surnames.Length; i++)
            {
                if (surnames[i].ToLower().Contains(userInput.ToLower()))
                {
                    WriteDossier(names, posts, i + 1);
                    dossierIsFounded = true;
                }
            }

            if (dossierIsFounded == false)
            {
                Console.Write("Ни одно досье не найдено.");
            }

            Console.ReadKey();
        }

        static void WriteAllDossiers(string[] names, string[] posts)
        {
            Console.Clear();

            for (int i = 1; i <= names.Length; i++)
            {
                WriteDossier(names, posts, i);
            }

            Console.ReadKey(true);
        }

        static string[] AddElement(string[] array, string newElement)
        {
            string[] modifiedArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                modifiedArray[i] = array[i];
            }

            modifiedArray[array.Length] = newElement;
            return modifiedArray;
        }

        static string[] RemoveElement(string[] array, int elementIndex)
        {
            string[] modifiedArray = new string[array.Length - 1];

            for (int i = elementIndex; i < modifiedArray.Length; i++)
            {
                modifiedArray[i] = array[i + 1];
            }

            for (int i = 0; i < elementIndex; i++)
            {
                modifiedArray[i] = array[i];
            }

            return modifiedArray;
        }
    }
}
