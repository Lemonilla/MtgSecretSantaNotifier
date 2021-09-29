using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace MtgSecretSantaNotifier
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var FromEmailAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            var EmailSubjectLine = ConfigurationManager.AppSettings["EmailSubjectLine"];
            var filename = ConfigurationManager.AppSettings["InputFileName"];
            var idAttrName = ConfigurationManager.AppSettings["AttributeNameForId"];
            var gifterIdAttrName = ConfigurationManager.AppSettings["AttributeNameForGifterId"];
            var emailAttrName = ConfigurationManager.AppSettings["AttributeNameForEmailAddress"];

            var service = XlsService.instance;
            // Load all data into memory
            loadFile(filename);


            // if we didn't run into any errors
            if (service.Users.Count() > 0)
            {
                // loop through the users
                // for each user send an email to their gifter based on their info
                foreach(var user in service.Users)
                {
                    var gifterId = user.GetAttributeValue(gifterIdAttrName);
                    var gifter = service.GetUserById(gifterId);
                        
                    if (gifter != null)
                    {
                        string body = buildMessageBody(user,gifter);
                        var success = sendEmail(FromEmailAddress, gifter.GetAttributeValue(emailAttrName), EmailSubjectLine, body);
                        if (!success) LogToConsole("Failed to send email to user with id " + gifter.GetAttributeValue(idAttrName) + " for user with id " + user.GetAttributeValue(idAttrName));
                    } 
                    else
                    {
                        // couldn't find gifter, so we shouldn't send an email for them
                        LogToConsole("User with id " + user.GetAttributeValue(idAttrName) + " did not match a gifter");
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
        private static void loadFile(string filename)
        {
            int rows = 0;
            var xlsService = XlsService.instance;
            try
            {
                using(var csv = new CsvHelper.CsvReader(File.OpenText(filename)))
                {
                    csv.Configuration.HasHeaderRecord = true;
                    csv.Configuration.Delimiter = "\t";

                    while(csv.Read())
                    {
                        rows++;
                        // Build our header list
                        if (rows == 1)
                        {
                            var columnIndex = 0;
                            string columnValue = null;
                            while (csv.TryGetField<string>(columnIndex, out columnValue))
                            {
                                xlsService.AddColumn(columnValue, columnIndex);
                                columnIndex++;
                            }
                            continue;
                        };

                        // read rows with headers we generated
                        var user = new User();
                        foreach(var column in xlsService.Columns)
                        {
                            user.Attributes.Add(new Attribute(column.Name, column.Index, csv.GetField(column.Index)));
                        }
                        xlsService.AddUser(user);
                    }
                }

            } catch(Exception e)
            {
                LogToConsole(e.ToString());
            }
        }

        /// <summary>
        /// Builds the body of the email to be sent to the Gifter
        /// </summary>
        /// <param name="giftee">The user who will receive the gift</param>
        /// <param name="gifter">The user who will be sending the gift</param>
        /// <returns></returns>
        private static string buildMessageBody(User giftee, User gifter)
        {
            string bodyFile = ConfigurationManager.AppSettings["EmailBodyTemplateFile"];

            if (!File.Exists(bodyFile))
            {
                throw new Exception("Email Body file not found");
            }

            string body = File.ReadAllText(bodyFile);
            foreach(var attr in XlsService.instance.Columns)
            {
                string gifteeSearchPattern = "${giftee_" + attr.Name + "}";
                body = body.Replace(gifteeSearchPattern, HttpUtility.HtmlEncode(giftee.GetAttributeValue(attr.Name)));
                string gifterSearchPattern = "${gifter_" + attr.Name + "}";
                body = body.Replace(gifterSearchPattern, HttpUtility.HtmlEncode(gifter.GetAttributeValue(attr.Name)));
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
                //message.BodyEncoding = Encoding.Unicode;
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
