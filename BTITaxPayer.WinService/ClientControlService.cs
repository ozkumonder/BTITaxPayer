using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using BTITaxPayerService.Core;
using BTITaxPayerService.Job;

namespace BTITaxPayer.WinService
{
    public partial class ClientControlService : ServiceBase
    {
        public ClientControlService()
        {
            InitializeComponent();
        }

        private ConfigSettings settings;
        protected override void OnStart(string[] args)
        {
            try
            {
                LogHelper.Log("<!----------------------Starting Service------------------------------!>");
                var path = string.Concat(ConfigHelper.ReadPath);

                if (File.Exists(path))
                {
                   var config = ConfigHelper.DeserializeDatabaseConfiguration(path);
                    GlobalSettings.ServerName = config.ServerName;
                    GlobalSettings.DatabaseName = config.DatabaseName;
                    GlobalSettings.DbUserName = config.DbUserName;
                    GlobalSettings.DbPassword = config.DbPassword;

                    GlobalSettings.LogoUserName = config.LogoUserName;
                    GlobalSettings.LogoPassword = config.LogoPassword;
                    GlobalSettings.LogoFirmNumber = config.LogoFirmNumber;
                    GlobalSettings.LogoPeriodNumber = config.LogoPeriodNumber;
                    GlobalSettings.LogoPath = config.LogoPath;

                    GlobalSettings.RestClientId = config.RestClientId;
                    GlobalSettings.RestClientSecret = config.RestClientSecret;
                    GlobalSettings.RestServiceUrl = config.RestServiceUrl;
                    GlobalSettings.RestTimeOut = config.RestTimeOut;

                    GlobalSettings.ConnectUser = config.ConnectUser;
                    GlobalSettings.ConnectPass = config.ConnectPass;
                    GlobalSettings.ConnectWorkSpace = config.ConnectWorkSpace;

                    GlobalSettings.eLogoUserName = config.eLogoUserName;
                    GlobalSettings.eLogoPassword = config.eLogoPassword;

                    GlobalSettings.SchedulerType = config.SchedulerType;
                    GlobalSettings.TaskStartDate = config.TaskStartDate;
                    GlobalSettings.TaskRunTime = config.TaskRunTime;
                    GlobalSettings.TaskRepeatCount = config.TaskRepeatCount;
                    GlobalSettings.TaskRunAlways = config.TaskRunAlways;
                    GlobalSettings.TaskWeeklyParameter = config.TaskWeeklyParameter;
                    GlobalSettings.TaskMonthlyParameter = config.TaskMonthlyParameter;
                }
                else
                {
                    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), nameof(ClientControlService), MethodBase.GetCurrentMethod().Name,
                        "BTI_EMUKELLEF config dosyası bulunamadı! Lüften BTIConfigManager ile configürasyon dosyasını oluşturup uygulama dizinine atınız!"));
                }
                var job = new Job();
                job.Execute();
                if (!File.Exists(ConfigHelper.ReadPath))
                {
                    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), nameof(ClientControlService), MethodBase.GetCurrentMethod().Name,
                        "BTI_EMUKELLEF config dosyası bulunamadı! Lüften BTIConfigManager ile configürasyon dosyasını oluşturup uygulama dizinine atınız!"));
                }
                else
                {
                    const string sqlQuery = @"IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES T WHERE T.TABLE_NAME = 'BTI_EMUKELLEF')
                    BEGIN
                        CREATE TABLE BTI_EMUKELLEF
                        (
                            Identifier VARCHAR(250),
                            Title VARCHAR(MAX),
                            Type VARCHAR(250),
                            AccountType VARCHAR(250),
                            AliasInvoicePK VARCHAR(250),
                            AliasInvoiceGB VARCHAR(250),
                            AliasDespatchAdvicePK VARCHAR(250),
                            AliasDespatchAdviceGB VARCHAR(250),
                            FirstCreationTimeInvoicePK DATE,
                            FirstCreationTimeInvoiceGB DATE,
                            AliasCreationTimeInvoicePK DATE,
                            AliasDeletionTimeInvoicePK DATE,
                            AliasCreationTimeInvoiceGB DATE,
                            AliasDeletionTimeInvoiceGB DATE,
                            AliasCreationTimeDespatchAdvicePK DATE,
                            AliasCreationTimeDespatchAdviceGB DATE
                        )
                        IF OBJECT_ID('BTI_EMUKELLEF') IS NOT NULL SELECT 1 ISOK ELSE SELECT 0 ISOK
                    END 
                    ELSE
					SELECT 'BTI_EMUKELLEF Table already exists' RESULT";
                    var connectionString = ConfigHelper.ConnectionString();
                    try
                    {
                        using (var connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            using (var command = new SqlCommand(sqlQuery, connection))
                            {
                                var result = command.ExecuteScalar();

                                if (result.Equals(1))
                                {
                                    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " ", nameof(ClientControlService),
                                        " ", MethodBase.GetCurrentMethod().Name, " ",
                                        "BTI_EMUKELLEF tablosu oluşturuldu!"));

                                    LogHelper.Log("<!----------------------Service Running------------------------------!>");
                                }
                                else if (result.ToString().Contains("exists"))
                                {
                                    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " ", nameof(ClientControlService), " ", MethodBase.GetCurrentMethod().Name, " ",
                                        "BTI_EMUKELLEF mevcut olduğundan yeniden oluşturulmadı..."));
                                }
                                Scheduler.StartJob().GetAwaiter().GetResult();
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        string.Concat(LogHelper.LogType.Error.ToLogType(), " ", nameof(ClientControlService), " ", MethodBase.GetCurrentMethod().Name, " ",
                            "BTI_EMUKELLEF tablosu oluşturulamadı!", exception.Message);
                    }

                }

            }
            catch (Exception exception)
            {
                LogHelper.Log(string.Concat(LogHelper.LogType.Error.ToLogType(), " ", exception.Message));
            }
        }


        protected override void OnStop()
        {
        }
        public void OnDebug()
        {
            OnStart(null);
        }
    }
}
