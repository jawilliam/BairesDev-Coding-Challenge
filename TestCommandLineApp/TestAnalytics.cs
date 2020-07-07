using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLineApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCommandLineApp
{
    [TestClass]
    public class TestAnalytics
    {
        [TestMethod]
        public void Analytics_Tokenize_OK()
        {
            var sink = new string[]
            {
                default, "", " ", 
                ", ", " ,", " , ", 
                " -", "- ", " - ",
                "& ", " &", " & ", 
                /*" and", "and ",*/ " and ",
                "a,b", "a ,b", "a, b",
                "a&b", "a &b", "a& b",
                "a-b", /*"a -b", "a- b", "a - b"*/
            };

            var analytics = new Analytics<string> { Sink = sink };
            var tokens = analytics.Tokenize(d => d).ToArray();

            Assert.IsTrue(tokens[0].Item1 == sink[0] && tokens[0].Item2.Length == 0);
            Assert.IsTrue(tokens[1].Item1 == sink[1] && tokens[1].Item2.Length == 0);
            Assert.IsTrue(tokens[2].Item1 == sink[2] && tokens[2].Item2.Length == 0);
            Assert.IsTrue(tokens[3].Item1 == sink[3] && tokens[3].Item2.Length == 0);
            Assert.IsTrue(tokens[4].Item1 == sink[4] && tokens[4].Item2.Length == 0);
            Assert.IsTrue(tokens[5].Item1 == sink[5] && tokens[5].Item2.Length == 0);
            Assert.IsTrue(tokens[6].Item1 == sink[6] && tokens[6].Item2.Length == 0);
            Assert.IsTrue(tokens[7].Item1 == sink[7] && tokens[7].Item2.Length == 0);
            Assert.IsTrue(tokens[8].Item1 == sink[8] && tokens[8].Item2.Length == 0);
            Assert.IsTrue(tokens[9].Item1 == sink[9] && tokens[9].Item2.Length == 0);
            Assert.IsTrue(tokens[10].Item1 == sink[10] && tokens[10].Item2.Length == 0);
            Assert.IsTrue(tokens[11].Item1 == sink[11] && tokens[11].Item2.Length == 0);

            Assert.IsTrue(tokens[12].Item1 == sink[12] && tokens[12].Item2.Length == 0);
            Assert.IsTrue(tokens[13].Item1 == sink[13] && 
                          tokens[13].Item2.Length == 2 &&
                          tokens[13].Item2[0] == "a" && tokens[13].Item2[1] == "b");
            Assert.IsTrue(tokens[14].Item1 == sink[14] &&
                          tokens[14].Item2.Length == 2 &&
                          tokens[14].Item2[0] == "a" && tokens[14].Item2[1] == "b");
            Assert.IsTrue(tokens[15].Item1 == sink[15] &&
                          tokens[15].Item2.Length == 2 &&
                          tokens[15].Item2[0] == "a" && tokens[15].Item2[1] == "b");
            Assert.IsTrue(tokens[16].Item1 == sink[16] &&
                          tokens[16].Item2.Length == 2 &&
                          tokens[16].Item2[0] == "a" && tokens[16].Item2[1] == "b");
            Assert.IsTrue(tokens[17].Item1 == sink[17] &&
                          tokens[17].Item2.Length == 2 &&
                          tokens[17].Item2[0] == "a" && tokens[17].Item2[1] == "b");
            Assert.IsTrue(tokens[18].Item1 == sink[18] &&
                          tokens[18].Item2.Length == 2 &&
                          tokens[18].Item2[0] == "a" && tokens[18].Item2[1] == "b");
            Assert.IsTrue(tokens[19].Item1 == sink[19] && 
                          tokens[19].Item2.Length == 2 &&
                          tokens[19].Item2[0] == "a" && tokens[19].Item2[1] == "b");
            //Assert.IsTrue(tokens[20].Item1 == sink[20] &&
            //              tokens[20].Item2.Length == 2 &&
            //              tokens[20].Item2[0] == "a" && tokens[20].Item2[1] == "b");
            //Assert.IsTrue(tokens[21].Item1 == sink[21] &&
            //              tokens[21].Item2.Length == 2 &&
            //              tokens[21].Item2[0] == "a" && tokens[21].Item2[1] == "b");
            //Assert.IsTrue(tokens[22].Item1 == sink[22] &&
            //              tokens[22].Item2.Length == 2 &&
            //              tokens[22].Item2[0] == "a" && tokens[22].Item2[1] == "b");

            Assert.AreEqual(tokens.Length, 20);
        }
    }
}
