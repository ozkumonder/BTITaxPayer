using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace BTITaxPayerService.Job
{
    public class Scheduler
    {
        public static async Task StartJob()
        {
            #region QuartzNet
            try
            {
                var factory = new StdSchedulerFactory();
                var scheduler = await factory.GetScheduler();

                // and start it off
                await scheduler.Start();

                // define the job and tie it to our HelloJob class
                var job = JobBuilder.Create<Job>().Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                
                var trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger")
                    .StartNow()
                    .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(01, 00))
                    .WithDescription("BTI Mükellef Servisi için oluşturulmuş zamanlayıcı.")
                    //.WithSimpleSchedule(x => x
                    //    .WithIntervalInHours(3)
                    //    //.WithRepeatCount(10)
                    //    .RepeatForever())
                    .Build();

                // Tell quartz to schedule the job using our trigger
                await scheduler.ScheduleJob(job, trigger);

                // some sleep to show what's happening
                //await Task.Delay(TimeSpan.FromSeconds(60));

                // and last shut down the scheduler when you are ready to close your program
                //await scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
            #endregion
        }
    }

}