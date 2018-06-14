using System;
using Hierarchy;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace лаба16
{
    class Program
    {
        //Меню
        public static int Menu(int k, string headLine, params string[] pechat)
        {
            Console.Clear();
            Console.WriteLine(headLine);
            int tek = 0, x = 2, y = 2, tekold = 0;
            Console.CursorVisible = false;
            var ok = false;
            for (var i = 0; i < pechat.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                if (i % (k + 1) == 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.Write(pechat[i]);
            }

    ;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(x, y + tekold);
                Console.Write(pechat[tekold] + " ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(x, y + tek);
                Console.Write(pechat[tek]);
                tekold = tek;
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        tek += k + 1;
                        break;
                    case ConsoleKey.UpArrow:
                        tek -= k + 1;
                        break;
                    case ConsoleKey.Enter:
                        ok = true;
                        break;
                }

                if (tek >= pechat.Length)
                    tek = 0;
                else if (tek < 0)
                    tek = pechat.Length - 1;
            } while (!ok);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            return tek + 1;
        }

        //Промежуточная функция
        public static void Continue()
        {
            Console.WriteLine("\nДля продолжения нажмите клавишу Enter...");
            Console.CursorVisible = false;
            Console.ReadLine();
        }

        static Random rnd = new Random();

        public static MyNewCollection colFullTime = new MyNewCollection("Дневное");
        public static Journal jFullTime = new Journal(colFullTime.CollectionName);
        public static MyNewCollection colExtramural = new MyNewCollection("Заочное");
        public static Journal jExtramural = new Journal(colExtramural.CollectionName);
        public static MyNewCollection colDistance = new MyNewCollection("Дистанционое");
        public static Journal jDistance = new Journal(colDistance.CollectionName);
        public static MyQueue<MyNewCollection> university;

        public static void Filling() //Заполнение всех коллекций
        {
            university = new MyQueue<MyNewCollection>(3);
            colFullTime.Filling(1);
            colExtramural.Filling(2);
            colDistance.Filling(3);
            university.Enqueue(colFullTime);
            university.Enqueue(colExtramural);
            university.Enqueue(colDistance);
        }

        public static void Transport()
        {
            Student s;
            int n = rnd.Next(1, 7);
            int i = 1;
            switch (n)
            {
                case 1:
                    s = colFullTime[i].Data;
                    colFullTime.Transport(i);
                    colExtramural.AddDefault(s, 2);
                    break;
                case 2:
                    s = colFullTime[i].Data;
                    colFullTime.Transport(i);
                    colDistance.AddDefault(s, 3);
                    break;
                case 3:
                    s = colExtramural[i].Data;
                    colExtramural.Transport(i);
                    colDistance.AddDefault(s, 3);
                    break;
                case 4:
                    s = colExtramural[i].Data;
                    colExtramural.Transport(i);
                    colFullTime.AddDefault(s, 1);
                    break;
                case 5:
                    s = colDistance[i].Data;
                    colDistance.Transport(i);
                    colExtramural.AddDefault(s, 2);
                    break;
                case 6:
                    s = colDistance[i].Data;
                    colDistance.Transport(i);
                    colFullTime.AddDefault(s, 1);
                    break;
            }
        }

        public static void DocumentSave() //сохранение документа
        {
            IFormatter formatter = new BinaryFormatter();
            using (FileStream s = File.Create("university.bin"))
                formatter.Serialize(s, university);
            Continue();
        }

        public static void DocumentOpen() //открытие документа
        {
            IFormatter formatter = new BinaryFormatter();
            using (FileStream s = File.OpenRead("university.bin"))
                university = (MyQueue<MyNewCollection>)formatter.Deserialize(s);
            colFullTime = university.Peek();
            university.Dequeue();
            colExtramural = university.Peek();
            university.Dequeue();
            colDistance = university.Peek();           
            Continue();
        }

        public static void Action(ref int k)
        {
            string[] menu = { "Отчислить всех студентов с тремя задолженностями", "Отчислить случайного студента",
                              "Зачислить случайного студента на случайное отделение", "Перевести случайного студента на случайное отделение", "Назад" };
            while (true)
            {
                var sw = Menu(0, "Выберите действие:", menu);
                switch (sw)
                {
                    case 1:
                        colFullTime.RemoveDebt(3);
                        colExtramural.RemoveDebt(3);
                        colDistance.RemoveDebt(3);
                        Console.WriteLine("Коллекции после действия:");
                        colFullTime.Show();
                        colExtramural.Show();
                        colDistance.Show();
                        Continue();
                        break;
                    case 2:
                        int q = rnd.Next(1,4);
                        switch (q)
                        {
                            case 1:
                                colFullTime.Remove(1);
                                break;
                            case 2:
                                colExtramural.Remove(1);
                                break;
                            case 3:
                                colDistance.Remove(1);
                                break;
                        }
                        if (colFullTime.Count == 0 || colExtramural.Count == 0 || colDistance.Count == 0)
                        {
                            k = 3;
                            Continue();
                            return;
                        }
                        else
                            Continue();
                        break;
                    case 3:
                        int m = rnd.Next(1, 4);
                        switch (m)
                        {
                            case 1:
                                colFullTime.AddDefault(1);
                                break;
                            case 2:
                                colExtramural.AddDefault(2);
                                break;
                            case 3:
                                colDistance.AddDefault(3);
                                break;
                        }
                        Continue();
                        break;
                    case 4:
                        Transport();
                        Continue();
                        break;
                    case 5:
                        return;
                }
            }

        }

        static void Main(string[] args)
        {
            string[] menu = { "Пересоздать коллекции", "Показать коллекции", "Показать журнал", "Действия над коллекциями","Загрузить коллекции","Сохранить коллекции", "Выход" };
            int k = 0;

            colFullTime.CollectionCountChanged += new CollectionHandler(jFullTime.CollectionCountChanged);
            colFullTime.CollectionDepartmentChanged += new CollectionHandler(jFullTime.CollectionReferenceChanged);
            colExtramural.CollectionCountChanged += new CollectionHandler(jExtramural.CollectionCountChanged);
            colExtramural.CollectionDepartmentChanged += new CollectionHandler(jExtramural.CollectionReferenceChanged);
            colDistance.CollectionCountChanged += new CollectionHandler(jDistance.CollectionCountChanged);
            colDistance.CollectionDepartmentChanged += new CollectionHandler(jDistance.CollectionReferenceChanged);

            Filling();

            while (true)
            {
                var sw = Menu(k, "Выберите действие:", menu);
                switch (sw)
                {
                    case 1:
                        Filling();
                        Continue();
                        k = 0;
                        break;
                    case 2:
                        colFullTime.Show();
                        colExtramural.Show();
                        colDistance.Show();
                        Continue();
                        break;
                    case 3:
                        Console.WriteLine(jFullTime.ToString());
                        Console.WriteLine(jExtramural.ToString());
                        Console.WriteLine(jDistance.ToString());
                        Continue();
                        break;
                    case 4:
                        Action(ref k);
                        break;
                    case 5:
                        DocumentOpen();
                        break;
                    case 6:
                        DocumentSave();
                        break;
                    case 7: return;
                }
            }
        }
    }
}
