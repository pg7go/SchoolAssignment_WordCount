using Microsoft.VisualStudio.TestTools.UnitTesting;
using wordCount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wordCount.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void Test()
        {
            string text;
            Assert.IsTrue(Program.OpenFile(@"C:\Users\13982\Desktop\测试\1984.txt", out text));
            Assert.AreEqual(591256, Program.CountChar(text));
        }

    }
}