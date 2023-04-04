using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using System.Web;

namespace TEST_GMAIL
{
    public partial class Form1 : Form
    {

       
        public Form1()
        {
            InitializeComponent();
        }
        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show(str.Distinct().ToArray().Length.ToString());
            Application.Exit();


            StreamReader reader = new StreamReader("email.txt", Encoding.UTF8);
            int count = 0;

            while ((line = reader.ReadLine()) != null)
            {
                count++;
                
                string to = str[i].Trim();
                if (!IsValid(to))
                {
                    continue;
                }
                string body = System.IO.File.ReadAllText(@"MailBody.txt");
           

                string from = "YourEmail@gmail.com";
                string pass = "YouPass";
                string subject = "EmailSubject";
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress(from);
                    mail.To.Add(to);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
					
                    //System.Net.Mail.Attachment attachment;
                    //attachment = new System.Net.Mail.Attachment(filename);
                    //mail.Attachments.Add(attachment);

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(from, pass);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);
					
                    //attachment.Dispose();
					
                    using (StreamWriter writetext = new StreamWriter("sent.txt", true))
                    {
                        writetext.WriteLine(to);
                    }

                    Console.WriteLine(count + " " + to);
                }
                catch (Exception ex)
                {
                    using (StreamWriter writetext = new StreamWriter("nosent.txt", true))
                    {
                        writetext.WriteLine(to + ";" + ex.Message + "\n");
                    }
                    MessageBox.Show(ex.Message);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
