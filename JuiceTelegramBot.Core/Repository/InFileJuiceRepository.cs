using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JuiceTelegramBot.Core.Repository
{
    public class InFileJuiceRepository : IJuiceRepository
    {
        private IList<string> juiceList = new List<string>();

        public InFileJuiceRepository()
        {

        }
        public void AddJuice(string name)
        {
            throw new NotImplementedException("Can't add item to file.");
        }

        public void DeleteJuice(string name)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetJuiceList()
        {
            try
            {
                using (FileStream file = new FileStream(@"Juice.txt", FileMode.Open, FileAccess.Read))
                {
                    juiceList.Clear();
                    StreamReader readFile = new StreamReader(file);
                    while (!readFile.EndOfStream)
                    {
                        juiceList.Add(readFile.ReadLine());

                    }
                    readFile.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return juiceList;
        }
    }
}
