using System.ServiceProcess;
using BTITaxPayer.WinService;

namespace BTITaxPayerService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ClientControlService()
            };
            ServiceBase.Run(ServicesToRun);

            //#if DEBUG

            //            var main = new ClientControlService();
            //            main.OnDebug();
            //            Thread.Sleep(Timeout.Infinite);
            //#else
            //            ServiceBase[] ServicesToRun;
            //            ServicesToRun = new ServiceBase[]
            //            {
            //                new ClientControlService()
            //            };
            //            ServiceBase.Run(ServicesToRun);
            //#endif

        }

    }
}
