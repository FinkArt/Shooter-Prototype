using System;

namespace Examples.HumanPassport.Utility
{
    public class Passport
    {
        protected Date _birthDate; //ссылаемся на класс дата? 
        protected Date _getDate;
        protected string _name;
        protected string _lastName;
        protected Sex _sex;

        public Passport(Date birthDate, string Name, string LastName, Sex sex)
        {
            _birthDate = birthDate;
            var now = DateTime.Now; //устанавливается дата на данный момент автоматически? 
            _name = Name;
            _lastName = LastName;
            _sex = sex;
            _getDate = new Date(now.Year, now.Month,
                now.Day); //передаем в класс Дата в метод 3 аргумента с текущими значениями даты? 
            var dateAsString = _getDate.ToString();
        }

        public virtual string PrintPassport()
        {
            return
                $"Name: {_name} {_lastName}, BirthDate: {_birthDate.ToString()}, Passport date: {_getDate.ToString()}, sex: {_sex.ToString()}";
        }
    }
}