using System;
using System.Collections.Generic;

namespace SAP1Prototype
{
    class Program
    {

        static void Main(string[] args)
        {
            Program p = new Program();
            List<byte> memory = new List<byte>();
            p.InitializeRam(memory);
            p.CompileAndRun(memory);
            Console.WriteLine("\n\nPRESS ANY KEY TO EXIT.....");
            Console.ReadLine();
        }

        public void InitializeRam(List<byte> memory)
        {
            for (int i = 0; i < 16; i++)
            {
                int result = 0;
                Console.WriteLine("Enter value for address {0}", Convert.ToString(i, 2).PadLeft(4, '0'));
                string input = Console.ReadLine();
                for (int j = 0; j < input.Length; j++)
                {
                    if (Int32.Parse(input[j].ToString()) == 1)
                    {
                        result += (int)Math.Pow(2, input.Length - 1 - j);
                    }
                }
                byte newData = Convert.ToByte(result);
                memory.Add(newData);
            }
            Console.WriteLine("\nData loaded into RAM!\n");
        }

        private void CompileAndRun(List<byte> memory)
        {
            byte accumulator = 0;
            byte b_register = 0;

            for (int i = 0; i < 16; i++)
            {
                if (memory[i] < 16)
                {
                    accumulator = memory[(int)(memory[i])];
                }
                else if (memory[i] >= 16 && memory[i] < 32)
                {
                    int addressGet = memory[i] - 16;
                    b_register = memory[addressGet];
                    accumulator = (byte)(accumulator + b_register);
                }
                else if (memory[i] >= 32 && memory[i] < 48)
                {
                    int addressGet = memory[i] - 32;
                    b_register = memory[addressGet];
                    accumulator = (byte)(accumulator - b_register);
                }
                else if (memory[i] >= 224 && memory[i] < 240)
                {
                    Console.WriteLine("Output: {0}", Convert.ToString(accumulator, 2).PadLeft(8, '0'));
                }
                else if (memory[i] >= 240)
                {
                    Console.WriteLine("\nProgram exited successfully!");
                    break;
                }

                if (i == 15)
                {
                    Console.WriteLine("\nProgram did not exit successfully");
                }
            }
        }
    }
}
