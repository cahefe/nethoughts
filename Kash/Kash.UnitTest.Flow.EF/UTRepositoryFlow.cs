using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kash.UnitTest.Flow.EF
{
    [TestClass]
    public class UTRepositoryFlow : TestWithSqlite
    {
        public UTRepositoryFlow()
        {

        }
        [TestMethod]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            Assert.IsTrue(await DbContext.Database.CanConnectAsync());
        }
        // [TestMethod]
        // public void TestMethod1() { }
    }
}
