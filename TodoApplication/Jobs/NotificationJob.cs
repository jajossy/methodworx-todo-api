using Quartz;
using System.Net;
using System.Net.Mail;
using TodoApplication.Models.Generated;
using TodoApplication.Repository;

namespace TodoApplication.Jobs
{
    [DisallowConcurrentExecution]
    public class NotificationJob : IJob
    {
        private readonly ILogger<NotificationJob> _logger;
       
        public NotificationJob(ILogger<NotificationJob> logger)
        {
            _logger = logger;            
        }
        public Task Execute(IJobExecutionContext context)
        {
            

                //var task = todo.Select(todo => SendEmail(todo));
                //var result = await Task.WhenAll(task);
                //var gg = result.ToList();           

            return  Task.CompletedTask;
        }

        //private void SendEmail(UserTodo userTodo)
        //{
        //    var userInfo = _repository.Query<User>().FirstOrDefault(x => x.UserId == userTodo.UserId);
        //    using (var message = new MailMessage("jajossy2@gmail.com", userInfo.Email))
        //    {
        //        message.Subject = "You have pending Todo Task";
        //        message.Body = "Your Task " + userTodo.Description + "is pending today";
        //        using (SmtpClient client = new SmtpClient
        //        {
        //            EnableSsl = true,
        //            Host = "smtp.gmail.com",
        //            Port = 587,
        //            Credentials = new NetworkCredential("jajossy2@gmail.com", "jossyPWD1981!")
        //        })
        //        {
        //            client.Send(message);
        //        }
        //    }
        //}
    }
}
