using GeneticAlgorithmLib.source.mockImplementations;
using GeneticAlgorithmLib.source.statistics;
using Xunit;

namespace GeneticAlgorithmLib.test.statisticsTests
{
    public class ResultTests
    {
        [Fact]
        public static void FitnessIsStoredCorrectly()
        {
            var member = new RandomNumberMember();
            var fitness = 10;

            var result = new MemberRecord<double>(member, fitness);

            Assert.Equal(fitness, result.GetFitness());
        }

        [Fact]
        public static void AddDuplicateWorks()
        {
            var member = new RandomNumberMember();
            var fitness = 10;

            var result = new MemberRecord<double>(member, fitness);

            Assert.False(result.IsDuplicate);
            Assert.Equal(1, result.NumberOfDuplicates);

            result.AddDuplicate();

            Assert.True(result.IsDuplicate);
            Assert.Equal(2, result.NumberOfDuplicates);

            result.AddDuplicate();

            Assert.True(result.IsDuplicate);
            Assert.Equal(3, result.NumberOfDuplicates);
        }

        [Fact]
        public static void RepresentsMember()
        {
            var member = new RandomNumberMember();
            var fitness = 10;

            var result = new MemberRecord<double>(member, fitness);
            Assert.Equal(member.GetId(), result.GetMemberId());
        }
    }
}