using Examples.HumanPassport.Utility;

namespace Examples.HumanPassport.Human
{
    public class HumanPassport : Passport
    {
        protected string _skinColor;

        public HumanPassport(Date birthDate, string Name, string LastName, Sex sex, string skinColor) : base(birthDate,
            Name, LastName, sex)
        {
            _skinColor = skinColor;
        }

        public override string PrintPassport()
        {
            return base.PrintPassport() + $" skin color: {_skinColor}";
        }
    }
}