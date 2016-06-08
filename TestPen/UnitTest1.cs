using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PenTest;

namespace TestPen
{
    [TestClass]
    public class UnitTest1
    {
        private String  colorTrue = "BLUE";

        private String colorFalse = "RED";

        private static string GetRandomString(int stringLength)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;

            for (int i = 0; i < stringLength; i++)
            {
                //generate digit for unicode range
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                //Convert digit to char
                builder.Append(ch);
            }
            return builder.ToString();
        }

        private static int GetIntRandomInRange (int RightRange, int LeftRange)
        {
            Random random = new Random();
            return random.Next(RightRange, LeftRange);
        }

        private static double GetDoubleRandomInRange(double RightRange, double LeftRange)
        {
            Random random = new Random();
            return random.NextDouble() * (LeftRange-RightRange) + RightRange; 
        }


        [TestMethod]
        public void Test01_Pen_Write_InkOnly_ShouldWriteWholeWord ()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 15);
            int wordlength = GetIntRandomInRange(1, inkContainerValue - 1);

            // get randow string
            string wordRand = GetRandomString(wordlength);

            var Pen_InkOnly = new Pen(inkContainerValue);
            string actual = Pen_InkOnly.write (wordRand);

            Assert.AreEqual (wordRand, actual, string.Concat("word ", wordRand, " is written successfully"));
        }

        [TestMethod]
        public void Test02_Pen_Write_InkOnly_ShouldWriteZeroWord ()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 15);

            // get randow string
            string wordRand = "";

            var Pen_InkOnly = new Pen(inkContainerValue);
            string actual = Pen_InkOnly.write(wordRand);

            Assert.AreEqual(wordRand, actual, "Zero word is written successfully");
        }

        [TestMethod]
        public void Test03_Pen_Write_InkOnly_ShouldWritePartOfWord()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 15);
            int wordlength = GetIntRandomInRange(inkContainerValue+1, inkContainerValue*2);

            // get randow string
            string wordRand = GetRandomString(wordlength);

            var Pen_InkOnly = new Pen(inkContainerValue);
            string actual = Pen_InkOnly.write(wordRand);
            string expected = wordRand.Substring(1, inkContainerValue);

            Assert.AreNotEqual(expected, actual, string.Concat("word ", expected, " is written successfully at attempt to write ", wordRand));
        }

        [TestMethod]
        public void Test04_Pen_Write_InkOnly_PenNotWork()
        {
            // arrange
            int inkContainerValue = 0;

            // get randow string
            string wordRand = "s";

            var Pen_InkOnly = new Pen(inkContainerValue);
            string actual = Pen_InkOnly.write(wordRand);

            Assert.AreEqual("", actual, "Broken pen is processed successfully");
        }

        [TestMethod]
        public void Test05_Pen_Write_InkSizeOnly_ShouldWriteWholeWord()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 20);
            double sizeOfLetters = GetDoubleRandomInRange(0.1, 3);
            int wordlength = GetIntRandomInRange(1, Convert.ToInt32(Math.Ceiling(inkContainerValue / sizeOfLetters - 1)));

            // get randow string
            string wordRand = GetRandomString(wordlength);

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters);
            string actual = Pen_InkOnly.write(wordRand);

            Assert.AreEqual(wordRand, actual, string.Concat("word ", wordRand, " is written in size ", sizeOfLetters , " successfully at inkContainerValue =", inkContainerValue));
        }

        [TestMethod]
        public void Test06_Pen_Write_InkSizeOnly_ShouldWritePartWord_bigWord()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 20);
            double sizeOfLetters = GetDoubleRandomInRange(1, 1.001);
            int wordlength = GetIntRandomInRange(inkContainerValue + 1, inkContainerValue * 2);
 
            // get randow string
            string wordRand = GetRandomString(wordlength);
            string expected = wordRand.Substring(0, Convert.ToInt32(Math.Ceiling(inkContainerValue * sizeOfLetters) - 1));

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters);
            string actual = Pen_InkOnly.write(wordRand);
            
            Assert.AreEqual(expected, actual, string.Concat("word ", expected, " is written successfully at attempt to write" , wordRand, " in size ", sizeOfLetters, " at inkContainerValue =", inkContainerValue));
        }

        [TestMethod]
        public void Test07_Pen_Write_InkSizeOnly_ShouldWritePartWord_bigSize()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 20);
            double sizeOfLetters = GetDoubleRandomInRange(5, 6);
            int wordlength = GetIntRandomInRange(inkContainerValue-1, 19);

            // get randow string
            string wordRand = GetRandomString(wordlength);

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters);
            string actual = Pen_InkOnly.write(wordRand);
            string expected = wordRand.Substring(0, Convert.ToInt32(Math.Ceiling(inkContainerValue * sizeOfLetters) - 1));

            Assert.AreEqual(expected, actual, string.Concat("word ", expected, " is written successfully at attempt to write", wordRand, " in size ", sizeOfLetters, " at inkContainerValue =", inkContainerValue));
        }

        [TestMethod]
        public void Test08_Pen_Write_InkSizeOnly_ShouldWriteZeroWord()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 15);
            double sizeOfLetters = GetDoubleRandomInRange(0.1, 3);

            // get randow string
            string wordRand = "";

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters);
            string actual = Pen_InkOnly.write(wordRand);

            Assert.AreEqual(wordRand, actual, "Zero word is written successfully");
        }

        [TestMethod]
        public void Test09_Pen_Write_InkSizeOnly_ShouldWriteZeroLength()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 15);
            double sizeOfLetters = 0;
            int wordlength = GetIntRandomInRange(1, 3);

            // get randow string
            string wordRand = GetRandomString(wordlength);

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters);
            string actual = Pen_InkOnly.write(wordRand);

            Assert.AreEqual(wordRand, actual, string.Concat("word ", wordRand, " is written with zero size successfully at inkContainerValue =", inkContainerValue));
        }

        [TestMethod]
        public void Test10_Pen_Write_InkSizeOnly_PenNotWork()
        {
            // arrange
            int inkContainerValue = 0;
            double sizeOfLetters = GetDoubleRandomInRange(0.1, 3);

            // get randow string
            string wordRand = "s";

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters);
            string actual = Pen_InkOnly.write(wordRand);

            Assert.AreEqual("", actual, "Broken pen is processed successfully");
        }

        [TestMethod]
        public void Test11_Pen_Write_InkSizeColorOnly_ShouldWriteWholeWord()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 20);
            double sizeOfLetters = GetDoubleRandomInRange(0.1, 3);
            int wordlength = GetIntRandomInRange(1, Convert.ToInt32(Math.Ceiling(inkContainerValue / sizeOfLetters - 1)));
            String color = colorTrue;

            // get randow string
            string wordRand = GetRandomString(wordlength);

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters, color);
            string actual = Pen_InkOnly.write(wordRand);

            Assert.AreEqual(wordRand, actual, string.Concat("word ", wordRand, " is written in size ", sizeOfLetters, " successfully at inkContainerValue =", inkContainerValue));
        }

        [TestMethod]
        public void Test12_Pen_Write_InkSizeColorOnly_ShouldNoWriteWholeWord_wrongColor()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 20);
            double sizeOfLetters = GetDoubleRandomInRange(0.1, 3);
            int wordlength = GetIntRandomInRange(1, Convert.ToInt32(Math.Ceiling(inkContainerValue / sizeOfLetters - 1)));
            String color = colorFalse;

            // get randow string
            string wordRand = GetRandomString(wordlength);

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters, color);
            string actual = Pen_InkOnly.write(wordRand);

            Assert.AreEqual(colorTrue, actual, "Wrong Color is processed successfully");
        }


        [TestMethod]
        public void Test13_Pen_Write_InkSizeColorOnly_ShouldWritePartWord_bigWord()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 20);
            double sizeOfLetters = GetDoubleRandomInRange(1, 1.001);
            int wordlength = GetIntRandomInRange(inkContainerValue + 1, inkContainerValue * 2);
            String color = colorTrue;

            // get randow string
            string wordRand = GetRandomString(wordlength);
            string expected = wordRand.Substring(0, Convert.ToInt32(Math.Ceiling(inkContainerValue * sizeOfLetters) - 1));

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters, color);
            string actual = Pen_InkOnly.write(wordRand);

            Assert.AreEqual(expected, actual, string.Concat("word ", expected, " is written successfully at attempt to write", wordRand, " in size ", sizeOfLetters, " at inkContainerValue =", inkContainerValue));
        }

        [TestMethod]
        public void Test14_Pen_Write_InkSizeColorOnly_ShouldNoWritePartWord_bigWord_wrongColor()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 20);
            double sizeOfLetters = GetDoubleRandomInRange(0.999, 1.001);
            int wordlength = GetIntRandomInRange(inkContainerValue + 1, inkContainerValue * 2);
            String color = colorFalse;

            // get randow string
            string wordRand = GetRandomString(wordlength);
            string expected = wordRand.Substring(1, Convert.ToInt32(Math.Ceiling(inkContainerValue * sizeOfLetters)));

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters, color);
            string actual = Pen_InkOnly.write(wordRand);

            Assert.AreEqual(colorTrue, actual, "Wrong Color is processed successfully");
        }

        [TestMethod]
        public void Test15_Pen_Write_InkSizeColorOnly_ShouldWritePartWord_bigSize()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 20);
            double sizeOfLetters = GetDoubleRandomInRange(5, 6);
            int wordlength = GetIntRandomInRange(inkContainerValue - 1, 19);
            String color = colorTrue;

            // get randow string
            string wordRand = GetRandomString(wordlength);

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters, color);
            string actual = Pen_InkOnly.write(wordRand);
            string expected = wordRand.Substring(0, Convert.ToInt32(Math.Ceiling(inkContainerValue * sizeOfLetters) - 1));

            Assert.AreNotEqual(expected, actual, string.Concat("word ", expected, " is written successfully at attempt to write", wordRand, " in size ", sizeOfLetters, " at inkContainerValue =", inkContainerValue));
        }

        [TestMethod]
        public void Test16_Pen_Write_InkSizeColorOnly_ShouldNoWritePartWord_bigSize_WrongColor()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 20);
            double sizeOfLetters = GetDoubleRandomInRange(5, 6);
            int wordlength = GetIntRandomInRange(inkContainerValue - 1, 19);
            String color = colorFalse; 

            // get randow string
            string wordRand = GetRandomString(wordlength);

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters, color);
            string actual = Pen_InkOnly.write(wordRand);
            string expected = wordRand.Substring(0, Convert.ToInt32(Math.Ceiling(inkContainerValue * sizeOfLetters) - 1));

            Assert.AreEqual(colorTrue, actual, "Wrong Color is processed successfully");
        }

        [TestMethod]
        public void Test17_Pen_Write_InkSizeColorOnly_ShouldWriteZeroWord_bothColor()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 15);
            double sizeOfLetters = GetDoubleRandomInRange(0.1, 3);
            String color = colorTrue;


            // get randow string
            string wordRand = "";

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters, color);
            string actual = Pen_InkOnly.write(wordRand);

            Assert.AreEqual(wordRand, actual, "Zero word is written successfully");

            color = colorFalse;
            var Pen_InkOnly1 = new Pen(inkContainerValue, sizeOfLetters, color);
            actual = Pen_InkOnly.write(wordRand);

            Assert.AreEqual(wordRand, actual, "Zero word is written successfully");
        }

        [TestMethod]
        public void Test18_Pen_Write_InkSizeColorOnly_ShouldWriteZeroLength_bothColor()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 15);
            double sizeOfLetters = 0;
            int wordlength = GetIntRandomInRange(1, 3);
            String color = colorTrue;

            // get randow string
            string wordRand = GetRandomString(wordlength);

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters, color);
            string actual = Pen_InkOnly.write(wordRand);

            Assert.AreEqual(wordRand, actual, string.Concat("word ", wordRand, " is written with zero size successfully at inkContainerValue =", inkContainerValue));

            color = colorFalse;
            var Pen_InkOnly1 = new Pen(inkContainerValue, sizeOfLetters, color);
            actual = Pen_InkOnly.write(wordRand);

            Assert.AreEqual(wordRand, actual, string.Concat("word ", wordRand, " is written with zero size successfully at inkContainerValue =", inkContainerValue));
        }

        [TestMethod]
        public void Test19_Pen_Write_InkSizeColorOnly_PenNotWork()
        {
            // arrange
            int inkContainerValue = 0;
            double sizeOfLetters = GetDoubleRandomInRange(0.1, 3);
            String color = colorTrue;


            // get randow string
            string wordRand = "s";

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters, color);
            string actual = Pen_InkOnly.write(wordRand);

            Assert.AreEqual("", actual, "Broken pen is processed successfully");

            color = colorFalse;
            var Pen_InkOnly1 = new Pen(inkContainerValue, sizeOfLetters, color);
            actual = Pen_InkOnly.write(wordRand);
            Assert.AreEqual("", actual, "Broken pen is processed successfully");

        }

        [TestMethod]
        public void Test20_Pen_Write_doSomethingElse_correctColor()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 20);
            double sizeOfLetters = GetDoubleRandomInRange(0.1, 3);
            int wordlength = GetIntRandomInRange(1, Convert.ToInt32(Math.Ceiling(inkContainerValue / sizeOfLetters - 1)));
            String color = colorTrue;

            // get randow string
            string wordRand = GetRandomString(wordlength);

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters, color);
            Pen_InkOnly.doSomethingElse();

            StreamReader fileInput = new StreamReader("1.txt");

            string actual = fileInput.ReadLine();

            fileInput.Close();

            Assert.AreEqual(colorTrue, actual, "color is correct");
        }

        [TestMethod]
        public void Test21_Pen_Write_doSomethingElse_wrongColor()
        {
            // arrange
            int inkContainerValue = GetIntRandomInRange(5, 20);
            double sizeOfLetters = GetDoubleRandomInRange(0.1, 3);
            int wordlength = GetIntRandomInRange(1, Convert.ToInt32(Math.Ceiling(inkContainerValue / sizeOfLetters - 1)));
            String color = colorFalse;

            // get randow string
            string wordRand = GetRandomString(wordlength);

            var Pen_InkOnly = new Pen(inkContainerValue, sizeOfLetters, color);
            Pen_InkOnly.doSomethingElse();

            StreamReader fileInput = new StreamReader("1.txt");

            string actual = fileInput.ReadLine();

            fileInput.Close();

            Assert.AreNotEqual(colorTrue, actual, "color is correct");
        }
    }
}
