using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DTO;

namespace DataAccesLayer
{
    public class FileLog : IFileLog
    {
        public FileLog()
        {
            resetFile();
        }

        public void Log(List<Event> newEvents)
        {
            foreach (var e in newEvents)
            {
                string log = e.Time.ToString() + "  |\tType: " + e.Type + "  |\t Category: " + e.Category +
                             "\t|  Involved tracks:";

                foreach (var involved in e.Involved)
                {
                    log += " " + involved;
                }

                using (StreamWriter file =
                    new StreamWriter("log.txt", true))
                {
                    file.WriteLine(log);
                    file.Close();
                }
            }
        }

        private void resetFile()
        {
            string file = "log.txt";

            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }
    }
}
