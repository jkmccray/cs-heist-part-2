using System;

namespace HeistPartII
{
    public class Muscle : IRobber
    {
        public string Name { get; set; }
        public int SkillLevel { get; set; }
        public int PercentageCut { get; set; }
        public string Specialty { get; } = "Muscle";
        public int Index { get; set; }

        public void PerformSkill(Bank bank)
        {
            bank.SecurityGuardScore -= SkillLevel;
            Console.WriteLine($"{Name} is beating up the security guards! Decreased security by {SkillLevel}!");
            if (bank.AlarmScore <= 0)
            {
                Console.WriteLine($"{Name} has overpowered the security guards!");
            }
        }
    }
}