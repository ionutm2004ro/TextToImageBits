using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace TextToImageBits
{
    class textTobinaryImage
    {
        public void Start()
        {
            
            
            Console.WriteLine("Input message:");
            Console.SetIn(new StreamReader(Console.OpenStandardInput(),
                               Console.InputEncoding,
                               false,
                               bufferSize: 9999999));

            string message = Console.ReadLine();

            int dimension = 180;

            dimension = fitToLength(message);
            //this part of the code is needed for a fixed dimension(when commenting the upper line)
            while (message.Length * 8 > (dimension * dimension))
            {// message bits > pixel count
                Console.WriteLine("The message is too long");
                Console.WriteLine("Input message:");
            }

            Bitmap bmp = new Bitmap(dimension, dimension);//width,height

            List<int> bitMsg = StringToBits(message);

            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    bmp.SetPixel(j, i, Color.Black);
                    if (bitMsg.Count() > i * dimension + j)
                    {
                        if (bitMsg[i * dimension + j] == 1)
                        {
                            bmp.SetPixel(j, i, Color.White);
                        }
                    }
                }
            }
            Console.WriteLine("Done");
            bmp.Save("bmpMessage.png");
            Console.ReadLine();
        }

        public List<int> StringToBits(string message)
        {
            Encoding enc = Encoding.ASCII;
            List<int> bitMsg = new List<int>();
            byte[] bytes = enc.GetBytes(message);
            foreach (byte b in bytes)
            {
                string bitString = Convert.ToString(b, 2);
                for (int i = 0; i < 8 - bitString.Count(); i++) {
                    bitMsg.Add(0);
                }
                foreach (var it in bitString)
                {
                    bitMsg.Add(Convert.ToInt32(it)-48);
                }
            }
            return bitMsg;
        }

        public int fitToLength(string message) {
            int dimension = 1;
            while (message.Length * 8 > (dimension * dimension)) {
                dimension++;
            }
            return dimension;
        }
    }
}
