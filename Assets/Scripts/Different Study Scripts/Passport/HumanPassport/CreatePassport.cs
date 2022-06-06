using System.Collections.Generic;
using Examples.HumanPassport.Human;
using Examples.HumanPassport.Utility;
using UnityEngine;

// namespace WorkerSpace
// {
//     public interface IWorker
//     {
//         void GetSalary(float money);
//         void Fire();
//         void GetOffer();
//         void GetTask(ITask task);
//     }
//
//     public class OfficeManager : IWorker
//     {
//         private bool _hasJob;
//
//         public void GetSalary(float money)
//         {
//             throw new NotImplementedException();
//         }
//
//         public void Fire()
//         {
//             _hasJob = false;
//         }
//
//         public void GetOffer()
//         {
//             _hasJob = true;
//         }
//
//         public void GetTask(ITask task)
//         {
//             throw new NotImplementedException();
//         }
//     }
//
//     public class TaxiDriver : IWorker
//     {
//         private bool _hasJob;
//
//         public void GetSalary(float money)
//         {
//             throw new NotImplementedException();
//         }
//
//         public void Fire()
//         {
//             _hasJob = false;
//         }
//
//         public void GetOffer()
//         {
//             _hasJob = true;
//         }
//
//         public void GetTask(ITask task)
//         {
//             throw new NotImplementedException();
//         }
//         
//     }
//
//     public class GoogleCompany
//     {
//         public void ManagerWorkers()
//         {
//             IWorker taxiDriver = new TaxiDriver();
//             IWorker officeManager = new OfficeManager();
//
//             if (taxiDriver is TaxiDriver driver)
//             {
//                 //driver.GetOrder(null);
//             }
//
//             Fire(taxiDriver);
//             Fire(officeManager);
//         }
//
//         private void Fire(IWorker worker)
//         {
//             worker.Fire();
//             if (worker is TaxiDriver taxiDriver)
//             {
//
//             }
//         }
//     }
//
//     public interface ITask
//     {
//
//     }
// }

    public class CreatePassport : MonoBehaviour
    {
        public void Awake()
        {
            var passportsToPrint = new List<Passport>();
            var nigger = new BlackHuman()
            {
                Name = "Abu", LastName = "Hufao", Sex = Sex.Male, BirtDate = new Date(1986, 12, 4)
            };
            var white = new WhiteHuman()
                {Name = "Liza", LastName = "Garsia", Sex = Sex.Female, BirtDate = new Date(1946, 6, 23)};
            var mulano = new MulanoHuman()
                {Name = "Dominik", LastName = "Piezo", Sex = Sex.Male, BirtDate = new Date(1968, 2, 2)};
            var white2 = new WhiteHuman() {Name = "asd", Sex = Sex.Female,};

            var niggerPassport = MakePassport(nigger);
            var whitePassport = MakePassport(white);
            var mulanoPassport = MakePassport(mulano);
            passportsToPrint.Add(niggerPassport);
            passportsToPrint.Add(whitePassport);
            passportsToPrint.Add(mulanoPassport);

            foreach (var passport in passportsToPrint)
            {
                Debug.Log(passport.PrintPassport());
            }

        }

        private HumanPassport MakePassport(Human human)
        {
            var skinColor = human.SkinColor();
            var passport = new HumanPassport(human.BirtDate, human.Name, human.LastName, human.Sex, skinColor);
            return passport;
        }
    }