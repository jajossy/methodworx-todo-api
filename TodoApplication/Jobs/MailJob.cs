using Quartz;
using System.Net.Mail;
using System.Net;
using System.Runtime.CompilerServices;

namespace TodoApplication.Jobs
{
    public class MailJob : IJob
    {
        //public void Execute(IJobExecutionContext context)
        //{
        //    using (var message = new MailMessage("testuser@gmail.com", "testdestinationmail@gmail.com"))
        //    {
        //        message.Subject = "Message Subject test";
        //        message.Body = "Message body test at " + DateTime.Now;
        //        using (SmtpClient client = new SmtpClient
        //        {
        //            EnableSsl = true,
        //            Host = "smtp.gmail.com",
        //            Port = 587,
        //            Credentials = new NetworkCredential("testuser@gmail.com", "123546")
        //        })
        //        {
        //            client.Send(message);
        //        }
        //    }
        //}
        Task IJob.Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
