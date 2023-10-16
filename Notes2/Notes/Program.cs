using System;

namespace notes
{
    class NotesApp
    {
        public static readonly Dictionary<DateTime, List<NoteData>> Notes = new Dictionary<DateTime, List<NoteData>>()
        {
            { DateTime.Now, new List<NoteData>()
                {
                    new NoteData("Репетитор", "\n-Сделать домашнее задание\n-Занятие в 19:00"),
                    new NoteData("Сделать практос по питону", "\n-Написать новеллу\n-Срок сдачи 25.10"),
                }
            },
            { DateTime.Now.AddDays(+1), new List<NoteData>()
                {
                    new NoteData("День рождение подруги", "\n-Туса в 20:00"),
                }
            },{ DateTime.Now.AddDays(+2), new List<NoteData>()
                {
                    new NoteData(" ", " "),
                    new NoteData(" ", " "),
                }
            },{ DateTime.Now.AddDays(+3), new List<NoteData>()
                {
                    new NoteData("Впасть в депрессию из-за количества практосов и начала осени ", "\n-Лежать в кровати и смотреть сериалы"),
                    new NoteData(" ", " "),
                }
            },{ DateTime.Now.AddDays(+4), new List<NoteData>()
                {
                    new NoteData("Пора отчислиться", "\n-Подать доки на отчисление"),
                    new NoteData("Отпраздновать отчисление", "\n-Позвать друзей\n-Купить торт\n-Снять квартиру в москоу сити "),
                }
            },{ DateTime.Now.AddDays(+5), new List<NoteData>()
                {
                    new NoteData(" ", " "),
                    new NoteData(" ", " "),
                }
            },{ DateTime.Now.AddDays(+6), new List<NoteData>()
                {
                    new NoteData(" ", " "),
                    new NoteData(" ", " "),
                }
            },{ DateTime.Now.AddDays(+8), new List<NoteData>()
                {
                    new NoteData(" ", " "),
                    new NoteData(" ", " "),
                }
        }, };

        public static void Main()
        {
            Current = Notes.Keys.ToList()[0];
            DrawPage("Main");

            Console.ReadLine();
        }

        public static DateTime Current = DateTime.Now;
        public static int SelectedRow = 0;

        public static void DrawPage(string pageName)
        {
            Console.Clear();

            switch (pageName)
            {
                case "Main":
                    Console.WriteLine();
                    if (!Notes.TryGetValue(Current, out var notes))
                    {
                        CloseApp();
                        break;
                    }

                    Console.WriteLine($"Выбранная дата - {ConvertTime(Current)}");
                    foreach (var note in notes)
                    {
                        int index = notes.IndexOf(note);
                        string text = "  ";
                        if (SelectedRow == index)
                            text = "-> ";

                        text += note.Name;
                        Console.WriteLine(text);
                    }

                    ConsoleKeyInfo key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (SelectedRow <= 0) break;
                            SelectedRow--;

                            DrawPage("Main");
                            break;
                        case ConsoleKey.DownArrow:
                            if (SelectedRow >= notes.Count - 1) break;
                            SelectedRow++;

                            DrawPage("Main");
                            break;
                        case ConsoleKey.LeftArrow:
                            var categories = Notes.Keys.ToList();
                            int indexCategory = categories.IndexOf(Current);
                            if (indexCategory <= 0) break;

                            Current = categories[indexCategory - 1];
                            DrawPage("Main");
                            break;
                        case ConsoleKey.RightArrow:
                            categories = Notes.Keys.ToList();
                            indexCategory = categories.IndexOf(Current);
                            if (indexCategory >= categories.Count - 1) break;

                            Current = categories[indexCategory + 1];
                            DrawPage("Main");
                            break;
                        case ConsoleKey.Enter:
                            var currentNote = notes.ElementAt(SelectedRow);
                            if (currentNote is null) break;

                            DrawPage("Note");
                            break;
                    }
                    break;
                case "Note":
                    if (!Notes.TryGetValue(Current, out notes))
                    {
                        CloseApp();
                        break;
                    }

                    var noteData = notes.ElementAt(SelectedRow);
                    if (noteData is null) break;

                    Console.WriteLine(noteData.Name);
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine($"Описание {noteData.Description}");
                    Console.WriteLine($"Дата: {ConvertTime(Current)}");

                    key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.Enter:
                            Console.Clear();
                            DrawPage("Main");
                            break;
                    }
                    break;
            }
        }

        public static void CloseApp() => Environment.Exit(0);
        public static string ConvertTime(DateTime time) => time.ToString("dd.MM.yyyy");

        public class NoteData
        {
            public string Name { get; set; }
            public string Description { get; set; }

            public NoteData(string name, string description)
            {
                Name = name;
                Description = description;
            }
        }
    }
}