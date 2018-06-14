using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchy
{
    [Serializable]
    class DistanceStudent : Student
    {
        protected string location;

        //Получение места жительства
        public string GetLocation
        {
            get { return location; }
            protected set { location = value; }
        }

        //Конструктор без параметров
        public DistanceStudent() : base()
        {
            location = "";
        }

        //База
        public new Student BaseStudent => new Student(name, surname, course, debts);

        //Конструктор с параметрами
        public DistanceStudent(string name, string surname, int course, int debts, string Location) : base(name, surname, course, debts)
        {
            location = Location;
        }

        //Контруктор от студента
        public DistanceStudent(Student s) : base (s.GetName, s.GetSurname, s.GetCourse, s.GetDebts)
        {
            location = "Г. Помогайск";
        }

        //Создание дистанционного студента
        public new static Student Create => StudentCreate.CreateElement<DistanceStudent>();

        //Вывод
        public override void Show()
        {
            Console.WriteLine(surname + " " + name + ", " + course + " курс, количество задолженностей: " + debts + "\nМесто жительства: " + location + "\n");
        }

        //ToString
        public override string ToString()
        {
            return BaseStudent.ToString();
        }
    }
}
