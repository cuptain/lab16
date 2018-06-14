using System;
using Hierarchy;

namespace лаба16
{
    [Serializable]
    class Journal //Журнал
    {
        private JournalEntry[] journal = new JournalEntry[0]; //Массив элементов журнала

        private string NameColl { get; set; } //Имя коллекции

        public Journal(string name) //Конструктор с параметром
        {
            NameColl = name;
        }

        public void Add(JournalEntry je) //Добавление записи в журнал
        {
            Array.Resize(ref journal, journal.Length + 1);
            journal[journal.Length - 1] = je;
        }

        public override string ToString() //Вывод
        {
            string str = NameColl + " отделение. Журнал:\n";
            int count = 0;
            foreach (var element in journal)
                str = String.Concat(str, ++count, ". ", element.ToString(), "\n");
            return str;
        }

        public void CollectionCountChanged(object sourse, CollectionHandlerEventArgs e) //Событие при изменении кол-ва элементов
        {
            JournalEntry je = new JournalEntry(e.CollectionName, e.TypeOfChange, e.ChangedObj);
            Add(je);
        }

        public void CollectionReferenceChanged(object sourse, CollectionHandlerEventArgs e) //Событие при изменении ссылок на элементы
        {
            JournalEntry je = new JournalEntry(e.CollectionName, e.TypeOfChange, e.ChangedObj);
            Add(je);
        }
    }

    [Serializable]
    class JournalEntry //Элемент журнала
    {
        private string CollectionName { get; set; } //Имя коллекции
        private string TypeOfChange { get; set; } //Тип изменения
        private Student ChangedObject { get; set; } //Изменённый объект

        public JournalEntry(string name, string changes, Student person) //Конструктор с параметрами
        {
            TypeOfChange = changes;
            ChangedObject = person;
            CollectionName = name;
        }

        public override string ToString() //Вывод
        {
            return CollectionName + " отделение. " + TypeOfChange + " студент " + ChangedObject.ToString();
        }
    }
}
