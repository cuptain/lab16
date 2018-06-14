using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchy
{
    [Serializable]
    public class Student
    {
        protected string name, surname; //Имя + Фамилия
        protected int debts, course; //Долги + курс

        //Получение имени
        public string GetName => name;

        //Получение фамилии
        public string GetSurname => surname;

        //Получение университета
        public int GetDebts => debts;

        //Получение курса
        public int GetCourse => course;

        //Конструктор без параметров
        public Student()
        {
            name = "";
            surname = "";
            course = 0;
            debts = 0;
        }

        //Конструктор с параметрами
        public Student(string Name, string Surname, int Course, int Debts)
        {
            name = Name;
            surname = Surname;
            course = Course;
            debts = Debts;
        }

        //Вывод
        public virtual void Show()
        {
            Console.WriteLine(surname + " " + name + ", " + course + " курс, количество задолженностей: " + debts);
        }

        //ToString
        public override string ToString()
        {
            return surname + " " + name + ", " + course + " курс, количество задолженностей:" + debts;
        }
    }
}
