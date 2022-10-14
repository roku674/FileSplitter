using FileSplitter.Utility;

namespace FileSplitter
{
    internal class Program
    {
        private const int mb1 = 1000000;

        private static void Main(string[] args)
        {
            // filfilesToSplit to split needs to be populated with at least 1 text file that is 10mb
            // or bigger

            string[] filesToSplit = Helper.GetFilesOver10mb();

            if (filesToSplit.Length > 0)
            {
                foreach (string file in filesToSplit)
                {
                    // Add the code in SplitFile.cs that split the current file into 1mb chunks
                    SplitFile.Split(file, mb1);
                    //*****************************************************************************************************************************************
                    // Feel free to email me if you have questions.  This isn't meant to be anything fancy.  I just want to see a 10mb or bigger text file
                    // split into 1mb slices.  The contents of the file are irrelevant, as long as it is text.  Please don't use a log file snippet or something
                    // that might contain sensitive information.

                    // Exmaple:  Your original file name is 10mb.txt
                    // Your output could be 10mb-1.txt, 10mb-2.txt...etc...that's not the requirement, just and example.
                    //*****************************************************************************************************************************************
                }
            }
        }
    }
}