using System;

namespace Hierarchy
{
    internal static class StudentCreate
    {
        //Рандомное имя
        static string[] random_name = ("Василий Давид Иосиф Алексей Дмитрий Михаил Исаак Марсель Анатолий Александр Егор Валентин Никита Марк Игнат " +
                                       "Пётр Даниил Артём Андрей Кирилл Илья Валерий Станислав Георгий Григорий Сергей Максим Вадим").Split(' ');

        //Рандомная фамилия
        static string[] random_surname = ("Панин Василюк Нуреев Мочалкин Кричалкин Штоль Коваль Пульт Штора Соль Нечаев Нескаромный Котыченко Баженов Жданов " +
                                          "Боярский Лазарев Троекуров Ашанин Соколовский Шнуров Алексеев Толкин Субботин Баранов Костин Машин Петин Путин Колончаев Южанинов").Split(' ');

        //Рандомная специальность
        static string[] random_specialty = ("Геология История Менеджмент Юриспруденция Филология Экономика Физкультура Литература Программирование Физика Биология " +
                                            "Машиностроение Искусство Строительство Химия Механика Инженерия Информатика Лингвистика Востоковедение Агрономия").Split(' ');

        //Рандомное место работы
        static string[] random_work = ("Завод Булочная Аптека Супермаркет Гавань Казармы Стоянка Аппартаменты Школа Поликлиника Спортзал Бар Галлерея Ресторан").Split(' ');

        //Рандомное место жительства
        static string[] random_place = ("д. Пышма,г. Черносуск,д. Лентяево,г. Дурскенбург,д. Лубенцы,г. Червонь,д. Альпута,с. Платошино,д. Простоквашино,д. Коноха," +
                                        "г. Пусинск,д. Город,г. Деревня,с. Нижние пупки,д. Южаново,г. Поддубарск,д. Бараново,с. Гурово,д. Костино,г. Паскудинск").Split(',');

        //Переменная для рандома
        private static readonly Random Rnd = new Random();

        //Случайная генерация элемента
        public static Student CreateElement<T>()
        {
            if (typeof(T) == typeof(FullTimeStudent))
                return new FullTimeStudent(random_name[Rnd.Next(0, random_name.Length)], random_surname[Rnd.Next(0, random_surname.Length)], Rnd.Next(1, 6),
                    Rnd.Next(0, 4), random_specialty[Rnd.Next(0, random_specialty.Length)]);
                                             

            if (typeof(T) == typeof(ExtramuralStudent))
                return new ExtramuralStudent(random_name[Rnd.Next(0, random_name.Length)], random_surname[Rnd.Next(0, random_surname.Length)], Rnd.Next(1, 6), 
                    Rnd.Next(0, 4), random_work[Rnd.Next(0, random_work.Length)]);

            return new DistanceStudent(random_name[Rnd.Next(0, random_name.Length)], random_surname[Rnd.Next(0, random_surname.Length)], Rnd.Next(1, 6), 
                Rnd.Next(0, 4), random_place[Rnd.Next(0, random_place.Length)]);
        }
    }
}
