namespace Dossier
{
    internal class Program
    {
        static void Main()
        {
            const string MenuAddDossier = "1";
            const string MenuWriteDossers = "2";
            const string MenuDeleteDosser = "3";
            const string MenuFindDosserBySurname = "4";
            const string CommandExit = "5";

            string[] names = Array.Empty<string>();
            string[] surnames = Array.Empty<string>();
            string[] patronymics = Array.Empty<string>();
            string[] posts = Array.Empty<string>();
            string userInput = string.Empty;
            bool isWorking = true;

            while (isWorking)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.WriteLine($"\n{MenuAddDossier}) добавить досье" +
                                  $"\n{MenuWriteDossers}) вывести все досье" +
                                  $"\n{MenuDeleteDosser}) удалить досье" +
                                  $"\n{MenuFindDosserBySurname}) поиск по фамилии" +
                                  $"\n{CommandExit}) выход");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case MenuAddDossier:
                        AddDossier(ref names, ref posts);
                        break;

                    case MenuWriteDossers:
                        WriteAllDossiers(names, posts);
                        break;

                    case MenuDeleteDosser:
                        DeleteDossier(ref names, ref posts);
                        break;

                    case MenuFindDosserBySurname:
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
            string name = names[dosserNumber - 1];
            string post = posts[dosserNumber - 1];

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
            string[] newArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }

            newArray[array.Length] = newElement;
            return newArray;
        }

        static string[] RemoveElement(string[] array, int elementIndex)
        {
            string[] newArray = new string[array.Length - 1];

            array[elementIndex] = null;

            for (int i = elementIndex; i < newArray.Length; i++)
            {
                array[i] = array[i + 1];
            }

            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = array[i];
            }

            return newArray;
        }
    }
}
