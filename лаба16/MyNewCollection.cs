using System;
using Hierarchy;

namespace лаба16
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args); //Делегат событий

    public class CollectionHandlerEventArgs : EventArgs //Класс событий
    {
        public string CollectionName { get; private set; } //Имя коллекции

        public string TypeOfChange { get; private set; } //Тип изменения

        public Student ChangedObj { get; private set; } //Изменённый объект

        public CollectionHandlerEventArgs(string name, string changes, Student _object) //Конструктор с параметрами
        {
            CollectionName = name;
            TypeOfChange = changes;
            ChangedObj = _object;
        }

        public override string ToString() //Вывод
        {
            return "В " + CollectionName + " отделении был "+ TypeOfChange + " студент " + ChangedObj.ToString();
        }
    }

    [Serializable]
    class MyNewCollection : MyQueue<Student>
    {
        public string CollectionName { get; private set; } //Имя коллекции

        public event CollectionHandler CollectionCountChanged; //Событие изменения кол-ва элементов
        public event CollectionHandler CollectionDepartmentChanged; //Событие перевода на другое отделение

        public MyNewCollection(string name) : base() //Конструктор с параметром
        {
            CollectionName = name;
        }

        public void Filling(int department) //Заполнение коллекции
        {
            var count = Capacity;
            for (int i = 0; i < count; i++)
            {
                switch (department)
                {
                    case 1:
                        Enqueue(FullTimeStudent.Create);
                        break;
                    case 2:
                        Enqueue(ExtramuralStudent.Create);
                        break;
                    case 3:
                        Enqueue(DistanceStudent.Create);
                        break;
                }
            }
        }

        public virtual void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args) //Обработчик события изменения кол-ва элементов
        {
            if (CollectionCountChanged != null)
                CollectionCountChanged(source, args);
        }

        public virtual void OnCollectionDepartmentChanged(object source, CollectionHandlerEventArgs args) //Обработчик события изменения ссылок на элементы
        {
            if (CollectionDepartmentChanged != null)
                CollectionDepartmentChanged(source, args);
        }

        public void AddDefault(int department) //Зачисление студента
        {
            int index = Count - 1;
            switch (department)
            {
                case 1:
                    if (Add(FullTimeStudent.Create, index))
                        OnCollectionCountChanged(this, new CollectionHandlerEventArgs(CollectionName, "Зачислен", base[index - 1].Data));
                    break;
                case 2:
                    if (Add(ExtramuralStudent.Create, index))
                        OnCollectionCountChanged(this, new CollectionHandlerEventArgs(CollectionName, "Зачислен", base[index - 1].Data));
                    break;
                case 3:
                    if (Add(DistanceStudent.Create, index))
                        OnCollectionCountChanged(this, new CollectionHandlerEventArgs(CollectionName, "Зачислен", base[index - 1].Data));
                    break;
            }
            Console.WriteLine("\nКоллекция после изменений:");
            Show();
        }

        public void AddDefault(Student person, int department)
        {
            int index = Count - 1;
            switch (department)
            {
                case 1:
                    var t = new FullTimeStudent(person);
                    if (Add(t, index))
                        OnCollectionDepartmentChanged(this, new CollectionHandlerEventArgs(CollectionName, "Зачислен", base[index].Data));
                    break;
                case 2:
                    var s = new ExtramuralStudent(person);
                    if (Add(s, index))
                        OnCollectionDepartmentChanged(this, new CollectionHandlerEventArgs(CollectionName, "Зачислен", base[index].Data));
                    break;
                case 3:
                    var m = new DistanceStudent(person);
                    if (Add(m, index))
                        OnCollectionDepartmentChanged(this, new CollectionHandlerEventArgs(CollectionName, "Зачислен", base[index].Data));
                    break;
            }
            Console.WriteLine("\nКоллекция после изменений:");
            Show();
        }

        public void Transport(int index) //Перевод студента
        {
            MyQueue<Student> clone = Clone();

            if (base.Remove(index))
                OnCollectionDepartmentChanged(this, new CollectionHandlerEventArgs(CollectionName, "Переведён на другой факультет", clone[index].Data));
            else
                Console.WriteLine("Индекс неверен!");
            if (Count == 0)
                Console.WriteLine("В отделении не осталось студентов");
            else
            {
                Console.WriteLine("\nКоллекция после изменений:");
                Show();
            }
        }

        public void RemoveDebt(int debt) //Отчисление студентов по долгам
        {
            MyQueue<Student> clone = Clone();

            for (int i = 0; i < Count; i++)
                if (clone[i].Data.GetDebts == debt)
                {
                    if(base.Remove(i))
                        OnCollectionCountChanged(this, new CollectionHandlerEventArgs(CollectionName, "Отчислен", clone[i].Data));
                }
            if (Count == 0)
                Console.WriteLine("В отделении не осталось студентов");
        }

        public new void Remove(int index) //Отчисление студента
        {
            MyQueue<Student> clone = Clone();

            if (base.Remove(index))
                OnCollectionCountChanged(this, new CollectionHandlerEventArgs(CollectionName, "Отчислен", clone[index - 1].Data));
            else
                Console.WriteLine("Индекс неверен!");
            if (Count == 0)
                Console.WriteLine("В отделении не осталось студентов");
            else
            {
                Console.WriteLine("\nКоллекция после изменений:");
                Show();
            }
        }

        public void Show() //Вывод коллекции
        {
            Console.WriteLine("\n" + CollectionName + " отделение\n");
            foreach (QueueElement<Student> person in this)
                person.Data.Show();
        }
    }
}
