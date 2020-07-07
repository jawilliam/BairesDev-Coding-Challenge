using CommandLineApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCommandLineApp
{
    [TestClass]
    public class TestPeopleCommands
    {
        [TestMethod]
        public void PeopleListCommand_ThrowsException_WhenInFullPath_IsNull()
        {
            var args = new PeopleListCommandArgs { InFullPath = null };
            Assert.ThrowsException<ArgumentNullException>(() => new PeopleCommands().List(args));
        }

        [TestMethod]
        public void PeopleListCommand_ThrowsException_WhenInFullPath_IsNotValid()
        {
            var args = new PeopleListCommandArgs { InFullPath = "f" };
            Assert.ThrowsException<ApplicationException>(() => new PeopleCommands().List(args));

            Assert.AreEqual(Program.Main(new[] { "list", "--in=f" }), -1);
        }

        [TestMethod]
        public void PeopleListCommand_OK()
        {
            Program.Main(new[] { "list" });
        }

        [TestMethod]
        public void PeopleAnswerCommand_OK()
        {
            Program.Main(new[] { "answer" });
        }
    }
}
