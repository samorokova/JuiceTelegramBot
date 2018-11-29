using JuiceTelegramBot.Core.DomainObject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JuiceTelegramBot.Core.Repository
{
    public class InFileJuiceRepository : IJuiceRepository
    {
        private IList<Juice> juiceList = new List<Juice>();

        public InFileJuiceRepository()
        {

        }
        public void AddJuice(string answer, bool isCustom, bool approved, DateTime juiceDateTime, string username)
        {
            try
            {
                File.AppendAllText(@"Juice.txt", $"{answer},{isCustom}, {approved}, {juiceDateTime}, {username}");

            }
            catch (Exception e)
            {

                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            
        }

        public void DeleteJuice(Juice juice)
        {
            throw new NotImplementedException();
        }

        public void DeleteJuice(string name)
        {
            throw new NotImplementedException();
        }

        public IList<Juice> GetJuiceList()
        {
            try
            {
                using (FileStream file = new FileStream(@"Juice.txt", FileMode.Open, FileAccess.Read))
                {
                    juiceList.Clear();
                    StreamReader readFile = new StreamReader(file);
                    while (!readFile.EndOfStream)
                    {
                        var line = readFile.ReadLine();
                        var lines = line.Split(',');
                        var juice = new Juice();
                        juice.Name = lines[0];
                        juice.IsCustom = Boolean.Parse(lines[1]);
                        juice.Approved = Boolean.Parse(lines[2]);
                        juice.JuiceDateTime = DateTime.Parse(lines[3]);
                        juice.UserName = lines[4];
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
