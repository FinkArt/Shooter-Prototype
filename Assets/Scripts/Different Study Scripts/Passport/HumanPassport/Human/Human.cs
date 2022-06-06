using Examples.HumanPassport.Utility;

namespace Examples.HumanPassport.Human
{
    public abstract class Human
    {
        public string Name;
        public string LastName;
        public Sex Sex;
        public Date BirtDate;

        public abstract string SkinColor();

        public virtual string Age(int i)
        {
            var age = i;
            return age.ToString();
        }
    }
}