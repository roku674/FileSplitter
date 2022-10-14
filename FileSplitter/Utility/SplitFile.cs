using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSplitter.Utility
{
    public class SplitFile
    {
        public static void Split(string inputFile, int chunkSize)
        {
            // Write a function that will split any file bigger than 10mb into 1mb chunks
            using (FileStream fileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            {
                //I'm sure there's some faster way to do it than a O(n)^2 approach but I haven't though of it...

                string pathNoExt = Path.GetFileNameWithoutExtension(inputFile);

                //Clean start every time pls
                if (Directory.Exists(pathNoExt))
                {
                    Directory.Delete(pathNoExt, true);
                }
                Directory.CreateDirectory(pathNoExt);

                int fileLength = (int)new FileInfo(inputFile).Length;
                byte[] chunk = new byte[chunkSize];
                try
                {
                    Stream inputStream = File.OpenRead(inputFile);
                    for (int i = 0;inputStream.Position < inputStream.Length;i++)
                    {
                        //open file stop at the OG file's end
                        try
                        {
                            Stream outputStream = File.Create(pathNoExt + "/" + pathNoExt + "-" + i + ".txt");
                            int remainder = chunkSize;
                            int bytesRead = inputStream.Read(chunk, 0, Math.Min(remainder, chunkSize)); //how much we've read so far
                            while (remainder > 0 && bytesRead > 0)
                            {
                                bytesRead = inputStream.Read(chunk, 0, Math.Min(remainder, chunkSize));

                                outputStream.Write(chunk, 0, bytesRead); //write it out to file
                                remainder -= bytesRead;
                            }
                            //cleanup
                            outputStream.Close();
                            outputStream.Dispose();
                        }
                        catch (Exception e)
                        {
                            //error logging
                            File.WriteAllText(pathNoExt + " / " + pathNoExt + " -ERROR " + i + ".txt", e.ToString());
                        }
                    }
                    inputStream.Close();
                    inputStream.Dispose();
                }
                catch (Exception e)
                {
                    File.WriteAllText(inputFile + " -ERROR.txt", e.ToString());
                }

                //any possible extra files need to be cleaned up
                try
                {
                    string[] files = Directory.GetFiles(pathNoExt + "/", "*.txt");

                    foreach (string file in files)
                    {
                        Stream inputStream2 = File.OpenRead(inputFile);

                        if (inputStream2.Length <= 0)
                        {
                            File.Delete(file);
                        }

                        inputStream2.Close();
                        inputStream2.Dispose();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                /*
                int counter;
                int sum = 0;
                while ((counter = fileStream.Read(chunk, sum, fileLength - sum)) > 0)
                {
                    sum += counter;
                    fileStream.Read(chunk, sum, chunkSize);
                    string section = Encoding.UTF8.GetString(chunk);
                    File.WriteAllText(pathNoExt + "/" + fileName + "-" + counter, section);
                }*/
            }
        }
    }
}