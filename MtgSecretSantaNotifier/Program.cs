using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace MtgSecretSantaNotifier
{
    class Program
    {
        public static void Main(string[] args)
        {
            var FromEmailAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            var EmailSubjectLine = ConfigurationManager.AppSettings["EmailSubjectLine"];
            var filename = ConfigurationManager.AppSettings["InputFileName"];

            // Load all data into memory
            var people = loadFile(filename);


            // if we didn't run into any errors
            if (people != null && people.Count() > 0)
            {
                // send emails
                foreach(var person in people)
                {
                    var gifter = people.Where(x => x.Id == person.GifterId).FirstOrDefault();
                    if (gifter != null)
                    {
                        string body = buildMessageBody(person);
                        var success = sendEmail(FromEmailAddress, gifter.Email, EmailSubjectLine, body);
                        if (!success) LogToConsole("Failed to send email to " + gifter.Name + " ("+ gifter.RedditName + ") at " + gifter.Email + " for " + person.Name);
                    }
                }
            }
            // wait for confirmation before closing
            Console.WriteLine("Finished.  Press Enter to exit.");
            Console.ReadLine();
        }

        private static List<Person> loadFile(string filename)
        {
            int rows = 0;
            var people = new List<Person>();
            try
            {
                using(var csv = new CsvHelper.CsvReader(File.OpenText(filename)))
                {
                    csv.Configuration.HasHeaderRecord = true;

                    while(csv.Read())
                    {
                        rows++;
                        // skip header record
                        if (rows == 1) continue;
                        
                        var person = new Person();
                        foreach(var tuple in RowToken.AttributeList)
                        {
                            person[tuple.Item1] = csv.GetField(tuple.Item2);
                        }
                        people.Add(person);
                    }
                }

                return people;
            } catch(Exception e)
            {
                LogToConsole(e.ToString());
                return null;
            }
        }


        private static string buildMessageBody(Person person)
        {
            string AttributesToSendToGifter = ConfigurationManager.AppSettings["AttributesToSendToGifter"];
            char AttributesToSendDelimiter = ConfigurationManager.AppSettings["AttributesToSendDelimiter"][0]; ;
            var sb = new StringBuilder();
            sb.AppendLine(ConfigurationManager.AppSettings["EmailBodyIntroduction"]);
            sb.AppendLine();
            foreach(var attr in AttributesToSendToGifter.Split(AttributesToSendDelimiter))
            {
                var header = ConfigurationManager.AppSettings["AttributeHeader_" + attr];
                var line = header + ":\t" + person[attr];
                sb.AppendLine(line);
            }
            sb.AppendLine();
            sb.AppendLine(ConfigurationManager.AppSettings["EmailBodyConclusion"]);

            return sb.ToString();
        }



        private static bool sendEmail(string from, string to, string subject, string body)
        {
            try
            {
                var message = new MailMessage(from, to);
                message.Subject = subject;
                message.Body = body;
                var client = new SmtpClient();
                client.Send(message);
            } catch (Exception e)
            {
                return false;
            }

            return true;
        }

        private static void LogToConsole(string message)
        {
            Console.WriteLine(message);
            var dateString = DateTime.Now.Year.ToString("M2") + DateTime.Now.Month.ToString("M2") + DateTime.Now.Day.ToString("M2");
            var logFileName = ConfigurationManager.AppSettings["LogFileNamePrefix"] + "_" + dateString + ".txt"; 
            try
            {
                using (var log = File.AppendText(logFileName))
                {
                    log.WriteLine(message);
                }
            }
            catch (Exception e) { }
        }
    }
}
