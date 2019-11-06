using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace MtgSecretSantaNotifier
{
    public class Program
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

        /// <summary>
        /// Loads a list of people into memory.  This file has strict formatting requirements defined in the README.
        /// </summary>
        /// <param name="filename">Path to file</param>
        /// <returns></returns>
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
                            person[tuple.Name] = csv.GetField(tuple.Row);
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

        /// <summary>
        /// Builds the body of the email to be sent to the Gifter
        /// </summary>
        /// <param name="giftee">The person who will receive the gift</param>
        /// <returns></returns>
        private static string buildMessageBody(Person giftee)
        {
            string body = ConfigurationManager.AppSettings["EmailBodyTemplate"];
            foreach(var attr in RowToken.AttributeList)
            {
                string searchPattern = "${" + attr.Name + "}";
                body = body.Replace(searchPattern, (string)giftee[attr.Name]);
            }
            return body;
        }

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="from">Email Address to list as the From Address</param>
        /// <param name="to">Email Address to send the email to</param>
        /// <param name="subject">String to put in the subject line</param>
        /// <param name="body">Body text of the email</param>
        /// <returns></returns>
        private static bool sendEmail(string from, string to, string subject, string body)
        {
            try
            {
                var message = new MailMessage(from, to);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                var client = new SmtpClient();
                client.Send(message);
            } catch (Exception e)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Logs a message to both the console and the error log file
        /// </summary>
        /// <param name="message">Message to log</param>
        private static void LogToConsole(string message)
        {
            Console.WriteLine(message);
            var dateString = DateTime.Now.Year.ToString("N2") + DateTime.Now.Month.ToString("N2") + DateTime.Now.Day.ToString("N2");
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
