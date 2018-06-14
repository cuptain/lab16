using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchy
{
    [Serializable]
    class FullTimeStudent : Student
    {
        protected string specialty; //учебная программа

        //Получение Места работы
        public string GetSpecialty
        {
            get { return specialty; }
            protected set { specialty = value; }
        }

        //Конструктор без параметров
        public FullTimeStudent() : base()
        {
            specialty = "";
        }

        //Конструктор с параметрами
        public FullTimeStudent(string name, string surname, int course, int debts, string Specialty) : base(name, surname, course, debts)
        {
            specialty = Specialty;
        }

        //Контруктор от студента
        public FullTimeStudent(Student s) : base (s.GetName, s.GetSurname, s.GetCourse, s.GetDebts)
        {
            specialty = "помощь студентам";
        }

        //База
        public new Student BaseStudent => new Student(name, surname, course, debts);

        //Создание дневного студента
        public new static Student Create => StudentCreate.CreateElement<FullTimeStudent>();

        //Вывод
        public override void Show()
        {
            Console.WriteLine(surname + " " + name + ", " + course + " курс, количество задолженностей: " + debts + "\nУчебная программа: " + specialty + "\n");
        }

        //ToString
        public override string ToString()
        {
            return BaseStudent.ToString();
        }
    }
}
