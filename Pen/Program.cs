using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PenTest
{
    public class Pen
    {

        // how much ink in the pen
        private int inkContainerValue = 1000;

        // the size of the letters, which are written by pen
        private double sizeLetter = 1.0;

        // color of pen
        private String color = "BLUE";

        public Pen (int inkContainerValue)
        {
            this.inkContainerValue = inkContainerValue;
        }

        public Pen (int inkContainerValue, double sizeLetter)

        {
            this.inkContainerValue = inkContainerValue;
            this.sizeLetter = sizeLetter;
        }

        public Pen(int inkContainerValue, double sizeLetter, String color)
        {
            this.inkContainerValue = inkContainerValue;
            this.sizeLetter = sizeLetter;
            this.color = color;
        }

        public String write(String word)
        {
            if (!isWork())
            {
                return "";
            }
            double sizeOfWord = word.Length * sizeLetter;
            if (sizeOfWord <= inkContainerValue)
            {
                inkContainerValue = Convert.ToInt32(inkContainerValue - sizeOfWord);
                return word;
                String partOfWord = word.Substring(0, inkContainerValue);
                }
            else
                {
                String partOfWord = word.Substring(0, inkContainerValue);
                inkContainerValue = 0;
                return partOfWord;
                }
            }

        // ERROR!!! Bug
        public String getColor()
        {
            return "BLUE";
        }

        public Boolean isWork()
        {
            return inkContainerValue > 0;
        }
        public void doSomethingElse()
        {
            StreamWriter fileOutput = new StreamWriter("1.txt", false);
            fileOutput.WriteLine(color);
            Console.WriteLine(color);
            fileOutput.Close();

        }
    }

}
