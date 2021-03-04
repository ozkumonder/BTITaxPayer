using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BTITaxPayerService.ConnectPostbox;
using BTITaxPayerService.Core;
using BTITaxPayerService.Model;
using BTITaxPayerService.ServiceManager;
using Dapper;
using Quartz;

namespace BTITaxPayerService.Job
{
    public class Job : IJob
    {
        private ConfigSettings Settings;
        public async Task Execute(IJobExecutionContext context)
        {
            Execute();
            //try
            //{
            //    var login = new LoginType
            //    {
            //        appStr = "TIGER",
            //        userName = ConfigHelper.DeserializeDatabaseConfiguration(ConfigHelper.ReadPath).eLogoUserName,//"0010797435",                
            //        passWord = ConfigHelper.DeserializeDatabaseConfiguration(ConfigHelper.ReadPath).eLogoPassword.DecryptIt(),//"0010797435",
            //        version = "2.1"
            //    };
            //    //var result = 0;
            //    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " <---Start Job Executing--->"));
            //    var pkList = ConnectService.GetPkList(login);
            //    var gbList = ConnectService.GetGbList(login);


            //    var mergeList = from pk in pkList.User
            //                    join gb in gbList.User
            //                        on pk.Identifier equals gb.Identifier
            //                    select new ClientDto()
            //                    {
            //                        Identifier = pk.Identifier,
            //                        Title = pk.Title,
            //                        Type = pk.Type,
            //                        FirstCreationTimeInvoicePK = pk.FirstCreationTime,
            //                        FirstCreationTimeInvoiceGB = gb.FirstCreationTime,
            //                        AccountType = pk.AccountType,
            //                        AliasInvoicePK = pk.Documents.Document.FirstOrDefault(x => x.Type == "Invoice")?.Alias.FirstOrDefault()?.Name,
            //                        AliasInvoiceGB = gb.Documents.Document.FirstOrDefault(x => x.Type == "Invoice")?.Alias.FirstOrDefault()?.Name,
            //                        AliasDespatchAdvicePK = pk.Documents.Document.FirstOrDefault(x => x.Type == "DespatchAdvice")?.Alias.FirstOrDefault()?.Name,
            //                        AliasDespatchAdviceGB = gb.Documents.Document.FirstOrDefault(x => x.Type == "DespatchAdvice")?.Alias.FirstOrDefault()?.Name,
            //                        AliasCreationTimeInvoicePK = pk.Documents.Document.FirstOrDefault()?.Alias.FirstOrDefault()?.CreationTime,
            //                        AliasCreationTimeInvoiceGB = gb.Documents.Document.FirstOrDefault()?.Alias.FirstOrDefault()?.CreationTime,
            //                        AliasCreationTimeDespatchAdvicePK = pk.Documents.Document.FirstOrDefault(x => x.Type == "DespatchAdvice")?.Alias.FirstOrDefault()?.CreationTime,
            //                        AliasCreationTimeDespatchAdviceGB = gb.Documents.Document.FirstOrDefault(x => x.Type == "DespatchAdvice")?.Alias.FirstOrDefault()?.CreationTime,
            //                        AliasDeletionTimeInvoicePK = pk.Documents.Document.FirstOrDefault(x => x.Type == "DespatchAdvice")?.Alias.FirstOrDefault()?.DeletionTime,
            //                        AliasDeletionTimeInvoiceGB = gb.Documents.Document.FirstOrDefault(x => x.Type == "DespatchAdvice")?.Alias.FirstOrDefault()?.DeletionTime
            //                    };
            //    var connectionString = ConfigHelper.ConnectionString();
            //    //DapperPlusManager.Entity<ClientDto>().Table("BTI_EMUKELLEF");

            //    using (var connection = new SqlConnection(connectionString))
            //    {
            //        LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), "Geçmiş bilgiler truncate ediliyor..."));
            //        await connection.ExecuteAsync("TRUNCATE TABLE BTI_EMUKELLEF");
            //        LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), "Geçmiş bilgiler truncate edildi..."));
            //        LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), "Mükellef bilgilieri insert ediliyor..."));
            //        //connection.BulkInsert(mergeList);
            //        LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), "Mükellef bilgilieri insert işlemi başarılı..."));

            //        var clientDtos = mergeList.ToList();

            //        connection.Open();
            //        SqlTransaction transaction = connection.BeginTransaction();

            //        using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
            //        {
            //            bulkCopy.BatchSize = 100;
            //            bulkCopy.DestinationTableName = "BTI_EMUKELLEF";
            //            try
            //            {
            //                bulkCopy.WriteToServer(clientDtos.AsDataTable());
            //            }
            //            catch (Exception)
            //            {
            //                transaction.Rollback();
            //                connection.Close();
            //            }
            //        }

            //        transaction.Commit();
            //    }
            //}
            //catch (System.Exception exception)
            //{
            //    LogHelper.Log(string.Concat(LogHelper.LogType.Error.ToLogType(), exception.Message));
            //}
            //finally
            //{
            //    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), string.Concat(" <---Finish Job Executing--->", Environment.NewLine)));
            //}
        }
        public void Execute()
        {
            try
            {
                var login = new LoginType
                {
                    appStr = "TIGER",
                    userName = ConfigHelper.DeserializeDatabaseConfiguration(ConfigHelper.ReadPath).eLogoUserName,//"0010797435",                
                    passWord = ConfigHelper.DeserializeDatabaseConfiguration(ConfigHelper.ReadPath).eLogoPassword.DecryptIt(),//"0010797435",
                    version = "2.1"
                };
                //var result = 0;
                LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " <---Start Job Executing--->"));
                var pkList = ConnectService.GetPkList(login);
                var gbList = ConnectService.GetGbList(login);


                var mergeList = from pk in pkList.User
                                join gb in gbList.User
                                    on pk.Identifier equals gb.Identifier
                                select new ClientDto()
                                {
                                    Identifier = pk.Identifier,
                                    Title = pk.Title,
                                    Type = pk.Type,
                                    FirstCreationTimeInvoicePK = pk.FirstCreationTime,
                                    FirstCreationTimeInvoiceGB = gb.FirstCreationTime,
                                    AccountType = pk.AccountType,
                                    AliasInvoicePK = pk.Documents.Document.FirstOrDefault(x => x.Type == "Invoice")?.Alias.FirstOrDefault()?.Name,
                                    AliasInvoiceGB = gb.Documents.Document.FirstOrDefault(x => x.Type == "Invoice")?.Alias.FirstOrDefault()?.Name,
                                    AliasDespatchAdvicePK = pk.Documents.Document.FirstOrDefault(x => x.Type == "DespatchAdvice")?.Alias.FirstOrDefault()?.Name,
                                    AliasDespatchAdviceGB = gb.Documents.Document.FirstOrDefault(x => x.Type == "DespatchAdvice")?.Alias.FirstOrDefault()?.Name,
                                    AliasCreationTimeInvoicePK = pk.Documents.Document.FirstOrDefault()?.Alias.FirstOrDefault()?.CreationTime,
                                    AliasCreationTimeInvoiceGB = gb.Documents.Document.FirstOrDefault()?.Alias.FirstOrDefault()?.CreationTime,
                                    AliasCreationTimeDespatchAdvicePK = pk.Documents.Document.FirstOrDefault(x => x.Type == "DespatchAdvice")?.Alias.FirstOrDefault()?.CreationTime,
                                    AliasCreationTimeDespatchAdviceGB = gb.Documents.Document.FirstOrDefault(x => x.Type == "DespatchAdvice")?.Alias.FirstOrDefault()?.CreationTime,
                                    AliasDeletionTimeInvoicePK = pk.Documents.Document.FirstOrDefault(x => x.Type == "DespatchAdvice")?.Alias.FirstOrDefault()?.DeletionTime,
                                    AliasDeletionTimeInvoiceGB = gb.Documents.Document.FirstOrDefault(x => x.Type == "DespatchAdvice")?.Alias.FirstOrDefault()?.DeletionTime
                                };
                var connectionString = ConfigHelper.ConnectionString();
                //DapperPlusManager.Entity<ClientDto>().Table("BTI_EMUKELLEF");

                using (var connection = new SqlConnection(connectionString))
                {
                    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), "Geçmiş bilgiler truncate ediliyor..."));
                    connection.ExecuteAsync("TRUNCATE TABLE BTI_EMUKELLEF");
                    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), "Geçmiş bilgiler truncate edildi..."));
                    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), "Mükellef bilgilieri insert ediliyor..."));

                    var clientDtos = mergeList.ToList();
                    var xx = clientDtos.AsDataTable();
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        bulkCopy.BatchSize = 100;
                        bulkCopy.DestinationTableName = "BTI_EMUKELLEF";
                        try
                        {
                            bulkCopy.WriteToServer(clientDtos.AsDataTable());
                        }
                        catch (Exception E)
                        {
                            transaction.Rollback();
                            connection.Close();
                        }
                    }

                    transaction.Commit();


                    //connection.BulkInsert(clientDtos);
                    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), "Mükellef bilgilieri insert işlemi başarılı..."));
                }
            }
            catch (System.Exception exception)
            {
                LogHelper.Log(string.Concat(LogHelper.LogType.Error.ToLogType(), exception.Message));
            }
            finally
            {
                LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), string.Concat(" <---Finish Job Executing--->", Environment.NewLine)));
            }
        }
    }
}