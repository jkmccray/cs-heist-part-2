using System;

namespace HeistPartII
{
    public class LockSpecialist : IRobber
    {
        public string Name { get; set; }
        public int SkillLevel { get; set; }
        public int PercentageCut { get; set; }
        public string Specialty { get; } = "Lock Specialist";
        public int Index { get; set; }


        public void PerformSkill(Bank bank)
        {
            bank.VaultScore -= SkillLevel;
            Console.WriteLine($"{Name} is picking the vault lock! Decreased security by {SkillLevel}!");
            if (bank.AlarmScore <= 0)
            {
                Console.WriteLine($"{Name} has opened the vault!");
            }
        }
    }
}