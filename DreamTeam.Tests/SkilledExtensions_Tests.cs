using System;
using DreamTeam.Models.Abstract;
using Kalavarda.Primitives;
using Moq;
using NUnit.Framework;

namespace DreamTeam.Tests
{
    public class SkilledExtensions_Tests
    {
        static TestDataItem[] TestData =
        {
            new TestDataItem
            {
                Skills = new ISkill[]
                {
                    new TestSkill { MaxDistance = 100 },
                    new TestSkill { MaxDistance = 10 },
                    new TestSkill { MaxDistance = 1 }
                },
                Result = 100
            },
            new TestDataItem
            {
                Skills = new ISkill[]
                {
                    new TestSkill { MaxDistance = 100, Cooldown = TimeSpan.FromSeconds(1.5) },
                    new TestSkill { MaxDistance = 10 },
                    new TestSkill { MaxDistance = 1, Cooldown = TimeSpan.FromSeconds(0.5) }
                },
                Result = 10
            },
            new TestDataItem
            {
                Skills = new ISkill[]
                {
                    new TestSkill { MaxDistance = 100, Cooldown = TimeSpan.FromSeconds(0.5) },
                    new TestSkill { MaxDistance = 10, Cooldown = TimeSpan.FromSeconds(1.5) },
                    new TestSkill { MaxDistance = 1, Cooldown = TimeSpan.FromSeconds(2.5) }
                },
                Result = 100
            },
        };

        [TestCaseSource(nameof(TestData))]
        public void GetMaxAttackDistance_Test(TestDataItem item)
        {
            var skilled = new Mock<ISkilled>();
            skilled
                .Setup(s => s.Skills)
                .Returns(item.Skills);
            var distance = skilled.Object.GetMaxSkillDistance();
            Assert.AreEqual(item.Result, distance, 0.01);
        }

        public class TestDataItem
        {
            public ISkill[] Skills { get; set; }

            public float Result { get; set; }
        }

        private class TestSkill: ISkill
        {
            public string Name => string.Empty;

            public float MaxDistance { get; set; }
            
            public ITimeLimiter TimeLimiter { get; } = new TimeLimiter(TimeSpan.FromSeconds(1));

            public TimeSpan Cooldown { get; set; }
        }
    }
}
