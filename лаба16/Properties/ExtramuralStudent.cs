using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hierarchy
{
    [Serializable]
    class ExtramuralStudent : Student
    {
        protected string workplace; //Место работы

        //Получение Места работы
        public string GetWorkPlace
        {
            get { return workplace; }
            protected set { workplace = value; }
        }

        //Конструктор без параметров
        public ExtramuralStudent() : base()
        {
            workplace = "";
        }

        //Конструктор с параметрами
        public ExtramuralStudent(string name, string surname, int course, int debts, string Workplace) : base(name, surname, course, debts)
        {
            workplace = Workplace;
        }
         
        //Контруктор от студента
        public ExtramuralStudent(Student s) : base (s.GetName, s.GetSurname, s.GetCourse, s.GetDebts)
        {
            workplace = "Центр помощи";
        }

        //База
        public new Student BaseStudent => new Student(name, surname, course, debts);

        //Создание заочного студента
        public new static Student Create => StudentCreate.CreateElement<ExtramuralStudent>();

        //Вывод
        public override void Show()
        {
            Console.WriteLine(surname + " " + name + ", " + course + " курс, количество задолженностей: " + debts + "\nМесто работы: " + workplace + "\n");
        }

        //ToString
        public override string ToString()
        {
            return BaseStudent.ToString();
        }
    }
}
