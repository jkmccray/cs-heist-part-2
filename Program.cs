using System;
using System.Collections.Generic;
using System.Linq;

namespace HeistPartII
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            Hacker bud = new Hacker()
            {
                Name = "Bud",
                SkillLevel = 40,
                PercentageCut = 20,
                Index = i++
            };

            Muscle meathead = new Muscle()
            {
                Name = "MeatHead",
                SkillLevel = 57,
                PercentageCut = 15,
                Index = i++
            };

            LockSpecialist alan = new LockSpecialist()
            {
                Name = "Alan",
                SkillLevel = 72,
                PercentageCut = 24,
                Index = i++
            };

            Hacker burt = new Hacker()
            {
                Name = "Burt",
                SkillLevel = 64,
                PercentageCut = 70,
                Index = i++

            };

            Muscle beefbrain = new Muscle()
            {
                Name = "BeefBrain",
                SkillLevel = 19,
                PercentageCut = 10,
                Index = i++

            };

            LockSpecialist doug = new LockSpecialist()
            {
                Name = "Doug",
                SkillLevel = 32,
                PercentageCut = 26,
                Index = i++

            };

            List<IRobber> rolodex = new List<IRobber>(){
                bud, meathead, alan, burt, beefbrain, doug
            };

            Console.WriteLine($"Number of operatives in the rolodex: {rolodex.Count}");

            Console.Write("Enter name of a new possible crew member: ");
            string newRolodexEntryName = Console.ReadLine();

            while (newRolodexEntryName != "")
            {
                Console.WriteLine("Select a Specialty:");
                Console.WriteLine("1. Hacker (Disables alarms)");
                Console.WriteLine("2. Muscle (Disarms guards)");
                Console.WriteLine("3. Lock Specialist (Cracks vault)");
                int newRolodexEntrySpecialty = int.Parse(Console.ReadLine());

                Console.Write("Enter the crew member's skill level (between 1 and 100): ");
                int newRolodexEntrySkillLevel = int.Parse(Console.ReadLine());

                Console.Write("Enter the crew member's demand for percentage cut: ");
                int newRolodexEntryPercentageCut = int.Parse(Console.ReadLine());

                switch (newRolodexEntrySpecialty)
                {
                    case 1:
                        Hacker addedHacker = new Hacker()
                        {
                            Name = newRolodexEntryName,
                            SkillLevel = newRolodexEntrySkillLevel,
                            PercentageCut = newRolodexEntryPercentageCut,
                            Index = i++
                        };
                        rolodex.Add(addedHacker);
                        break;
                    case 2:
                        Muscle addedMuscle = new Muscle()
                        {
                            Name = newRolodexEntryName,
                            SkillLevel = newRolodexEntrySkillLevel,
                            PercentageCut = newRolodexEntryPercentageCut,
                            Index = i++
                        };
                        rolodex.Add(addedMuscle);
                        break;
                    case 3:
                        LockSpecialist addedLockSpecialist = new LockSpecialist()
                        {
                            Name = newRolodexEntryName,
                            SkillLevel = newRolodexEntrySkillLevel,
                            PercentageCut = newRolodexEntryPercentageCut,
                            Index = i++
                        };
                        rolodex.Add(addedLockSpecialist);
                        break;
                }

                Console.Write("Enter another potential crew member name: ");
                newRolodexEntryName = Console.ReadLine();
            }

            Random randomNumberGenerator = new Random();
            Bank bankTarget = new Bank()
            {
                AlarmScore = randomNumberGenerator.Next(0, 101),
                VaultScore = randomNumberGenerator.Next(0, 101),
                SecurityGuardScore = randomNumberGenerator.Next(0, 101),
                CashOnHand = randomNumberGenerator.Next(50_000, 1_000_000)
            };


            Dictionary<string, int> bankInfo = new Dictionary<string, int>(){
                    {"Alarm", bankTarget.AlarmScore},
                    {"Vault", bankTarget.VaultScore},
                    {"Guards", bankTarget.SecurityGuardScore}
            };

            int maxScore = bankInfo.Max(kvp => kvp.Value);
            string mostSecure = bankInfo.First(kvp => kvp.Value == maxScore).Key;

            int minScore = bankInfo.Min(kvp => kvp.Value);
            string leastSecure = bankInfo.First(kvp => kvp.Value == minScore).Key;

            Console.WriteLine("Recon Report:");
            Console.WriteLine($"Most secure: {mostSecure}");
            Console.WriteLine($"Least secure: {leastSecure}");

            foreach (IRobber robber in rolodex)
            {
                Console.WriteLine($"{robber.Index}. {robber.Name}");
                Console.WriteLine($"Specialty: {robber.Specialty}");
                Console.WriteLine($"Skill Level: {robber.SkillLevel}");
                Console.WriteLine($"Percentage Cut: {robber.PercentageCut}");
                Console.WriteLine("--------------------------------------------------");
            }

            List<IRobber> crew = new List<IRobber>();

            Console.Write("Select a robber to add to the crew: ");
            string crewMemberNumberSelected = Console.ReadLine();

            int percentageLeft = 100;

            while (crewMemberNumberSelected != "" && percentageLeft > 0)
            {
                IRobber crewMemberToBeAdded = rolodex.First(robber => robber.Index == int.Parse(crewMemberNumberSelected));
                percentageLeft -= crewMemberToBeAdded.PercentageCut;

                crew.Add(crewMemberToBeAdded);

                List<IRobber> filteredRolodex = rolodex
                .Where(robber => robber.PercentageCut <= percentageLeft)
                .Where(robber => !crew.Contains(robber))
                .ToList();

                foreach (IRobber robber in filteredRolodex)
                {
                    Console.WriteLine($"{robber.Index}. {robber.Name}");
                    Console.WriteLine($"Specialty: {robber.Specialty}");
                    Console.WriteLine($"Skill Level: {robber.SkillLevel}");
                    Console.WriteLine($"Percentage Cut: {robber.PercentageCut}");
                    Console.WriteLine("--------------------------------------------------");
                }

                if (filteredRolodex.Count > 0)
                {
                    Console.Write("Select another robber to add to the crew: ");
                    crewMemberNumberSelected = Console.ReadLine();
                }
            }

            foreach (IRobber robber in crew)
            {
                robber.PerformSkill(bankTarget);
            }

            if (bankTarget.IsSecure)
            {
                Console.WriteLine("The heist failed!");
            }
            else
            {
                Console.WriteLine("The heist succeeded!");
                int cashLeft = bankTarget.CashOnHand;
                foreach(IRobber robber in crew)
                {
                    int robbersTake = bankTarget.CashOnHand*robber.PercentageCut/100;
                    cashLeft -= robbersTake;
                    Console.WriteLine($"{robber.Name} got {robbersTake}");
                }
                Console.WriteLine($"You get {cashLeft}");
            }
        }
    }
}
