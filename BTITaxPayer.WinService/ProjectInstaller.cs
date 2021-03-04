using System.ComponentModel;
using BTITaxPayerService.Core;

namespace BTITaxPayer.WinService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            LogHelper.Log("İnstaller burası");
        }
    }
}
