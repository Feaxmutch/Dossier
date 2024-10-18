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
                        AddDossier(ref names, ref surnames, ref patronymics, ref posts);
                        break;

                    case MenuWriteDossers:
                        WriteAllDossiers(names, surnames, patronymics, posts);
                        break;

                    case MenuDeleteDosser:
                        DeleteDossier(ref names, ref surnames, ref patronymics, ref posts);
                        break;

                    case MenuFindDosserBySurname:
                        FindDossierBySurname(names, surnames, patronymics, posts);
                        break;

                    case CommandExit:
                        isWorking = false;
                        break;
                }
            }
        }

        static void AddDossier(ref string[] names, ref string[] surnames, ref string[] patronymics, ref string[] posts)
        {
            string[] userInputs = new string[4];

            Console.Clear();
            Console.CursorVisible = true;

            Console.Write("Введите имя: ");
            string newName = Console.ReadLine();

            Console.Write("Введите фамилию: ");
            string newSurname = Console.ReadLine();

            Console.Write("Введите отчество: ");
            string newPatronymic = Console.ReadLine();

            Console.Write("Введите Должность: ");
            string newPost = Console.ReadLine();

            Console.CursorVisible = false;

            names = AddElement(names, newName);
            surnames = AddElement(surnames, newSurname);
            patronymics = AddElement(patronymics, newPatronymic);
            posts = AddElement(posts, newPost);

            Console.WriteLine("Добавленно следющее досье:");
            WriteDossier(names, surnames, patronymics, posts, names.Length);

            Console.ReadKey(true);
        }

        static void WriteDossier(string[] names, string[] surnames, string[] patronymics, string[] posts, int dosserNumber)
        {
            string name = names[dosserNumber - 1] == string.Empty ? "Не_указано" : names[dosserNumber - 1];
            string surname = surnames[dosserNumber - 1] == string.Empty ? "Не_указано" : surnames[dosserNumber - 1];
            string patronymic = patronymics[dosserNumber - 1] == string.Empty ? "Не_указано" : patronymics[dosserNumber - 1];
            string post = posts[dosserNumber - 1] == string.Empty ? "Не_указано" : posts[dosserNumber - 1];

            Console.WriteLine($"{dosserNumber}. {surname} {name} {patronymic} - {post}");
        }

        static void DeleteDossier(ref string[] names, ref string[] surnames, ref string[] patronymics, ref string[] posts)
        {
            Console.Clear();
            WriteAllDossiers(names, surnames, patronymics, posts);
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
                    surnames = RemoveElement(surnames, dosserNumber - 1);
                    patronymics = RemoveElement(patronymics, dosserNumber - 1);
                    posts = RemoveElement(posts, dosserNumber - 1);
                }
            }
            else
            {
                Console.WriteLine($"\"{userInput}\" не является числом");
                Console.ReadKey();
            }
        }

        static void FindDossierBySurname(string[] names, string[] surnames, string[] patronymics, string[] posts)
        {
            string userInput = string.Empty;
            bool dossierIsFounded = false;

            Console.Clear();
            Console.CursorVisible = true;

            Console.Write("Введите начало фамилии, или полную фамилию: ");
            userInput = Console.ReadLine();

            for (int i = 0; i < surnames.Length; i++)
            {
                if (surnames[i].Contains(userInput.ToLower()))
                {
                    WriteDossier(names, surnames, patronymics, posts, i + 1);
                    dossierIsFounded = true;
                }
            }

            if (dossierIsFounded == false)
            {
                Console.Write("Ни одно досье не найдено.");
            }

            Console.ReadKey();
        }

        static void WriteAllDossiers(string[] names, string[] surnames, string[] patronymics, string[] posts)
        {
            Console.Clear();

            for (int i = 1; i <= names.Length; i++)
            {
                WriteDossier(names, surnames, patronymics, posts, i);
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
