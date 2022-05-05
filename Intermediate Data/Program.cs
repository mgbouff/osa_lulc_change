using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combiner
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Environment.CurrentDirectory + "/combinedSet.csv";
            string set1path = Environment.CurrentDirectory + "/set1.csv";
            string set2path = Environment.CurrentDirectory + "/set2.csv";
            string overwrite = "y";

            if (File.Exists(path))
            {
                Console.WriteLine("File already exists! Overwrite it? y/n");
                overwrite = Console.ReadLine();
            }

            if (overwrite == "y")
            {
                // If the files that need to be combined exist and the result doesn't/can be overwriten
                if (File.Exists(set1path) && File.Exists(set2path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        StreamReader sr1 = new StreamReader(set1path);
                        StreamReader sr2 = new StreamReader(set2path);

                        // Read the first line of each file
                        string sr1Buffer = sr1.ReadLine();
                        string sr2Buffer = sr2.ReadLine();

                        // Track the line number
                        long lineNumber = 0;

                        while ((sr1Buffer != null) || (sr2Buffer != null))
                        {
                            string[] rowBuffer = new string[3];

                            if (sr1Buffer != null)
                            {
                                string[] sr1Columns = sr1Buffer.Split(',');

                                rowBuffer[0] = sr1Columns[0];
                                rowBuffer[1] = sr1Columns[1];
                            }
                            else
                            {
                                rowBuffer[0] = "n/a";
                                rowBuffer[1] = "n/a";
                            }

                            if (sr2Buffer != null)
                            {
                                string[] sr2Columns = sr2Buffer.Split(',');

                                if((sr1Buffer != null) && (rowBuffer[1] != sr2Columns[0]))
                                {
                                    Console.WriteLine("Warning!" + " overlapping cells did not match on line: " + lineNumber);
                                    Console.WriteLine("set1 value: " + rowBuffer[1]);
                                    Console.WriteLine("set2 value: " + sr2Columns[0]);
                                }

                                rowBuffer[1] = sr2Columns[0];
                                rowBuffer[2] = sr2Columns[1];
                            }
                            else
                            {
                                rowBuffer[2] = "n/a";
                            }

                            sw.WriteLine(rowBuffer[0] + "," + rowBuffer[1] + "," + rowBuffer[2]);
                            
                            // Increment read line
                            sr1Buffer = sr1.ReadLine();
                            sr2Buffer = sr2.ReadLine();

                            // Increment lineNumber
                            lineNumber++;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("one or both files cold not be found");
                }
            }
            else
            {
                Console.WriteLine("Files was not overwritten");
            }

            Console.WriteLine("press enter to exit");
            Console.ReadLine();
        }
    }
}
