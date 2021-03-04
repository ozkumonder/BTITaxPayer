using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Xml;
using BTITaxPayerService.ConnectPostbox;
using BTITaxPayerService.Core;
using BTITaxPayerService.Model;
using BTITaxPayerService.Utilities;

namespace BTITaxPayerService.ServiceManager
{
    public class ConnectService
    {
        public static UserList GetPkList(LoginType login)
        {
            LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), "<---------------------------- E-LOGO PKLIST Operation ---------------------------->"));
            var ssId = string.Empty;
            var userListPk = new UserList();
            try
            {
                string destinationPath = string.Empty;
                LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " E-Logo Postbox Servise bağlanılıyor..."));
                var connectPostbox = new PostBoxServiceClient("PostBoxServiceEndpoint");
                if (connectPostbox.Login(login, out ssId))
                {
                    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " E-Logo Postbox Servise bağlantı başarılı..."));
                    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " userPkList.zip dosyası indiriliyor..."));
                    var userList = connectPostbox.getUserListNew(login, UserListType.PKLIST);
                    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " userPkList.zip dosyası indirme başarılı..."));
                    File.WriteAllBytes(string.Concat(Path.GetTempPath(), "userPkList", ".zip"), userList.binaryData.Value);
                    var userPath = string.Concat(Path.GetTempPath(), "userPkList", ".zip");
                    string userExtractPath = @"C:\Temp";
                    if (!File.Exists(userExtractPath))
                    {
                        Directory.CreateDirectory(userExtractPath);
                    }
                    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " Zip dosyası çıkartılıyor..."));
                    using (ZipArchive archive = ZipFile.OpenRead(userPath))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            destinationPath = Path.GetFullPath(Path.Combine(userExtractPath, entry.FullName));

                            // Ordinal match is safest, case-sensitive volumes can be mounted within volumes that
                            // are case-insensitive.
                            if (destinationPath.StartsWith(userExtractPath, StringComparison.Ordinal))
                                if (File.Exists(destinationPath))
                                {
                                    File.Delete(destinationPath);
                                }
                            entry.ExtractToFile(destinationPath);
                            LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), $" Zip dosyası çıkartıldı ve {destinationPath}'e yazıldı..."));
                        }
                    }

                    var xmlDataDocument = new XmlDataDocument();

                    var fileStream = new FileStream(string.Concat(destinationPath), FileMode.Open, FileAccess.Read);
                    xmlDataDocument.Load(fileStream);

                    userListPk = SerializerXml.Deserialize<UserList>(xmlDataDocument.InnerXml);
                    userListPk.ListType = "PK";
                }
            }
            catch (Exception e)
            {
                LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " ", nameof(ConnectService), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }
            return userListPk;
        }
        public static UserList GetGbList(LoginType login)
        {
            LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), "<---------------------------- E-LOGO GBLIST Operation ---------------------------->"));
            var ssId = string.Empty;
            var userListGb = new UserList();
            try
            {
                string destinationPath = string.Empty;
                LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " E-Logo Postbox Servise bağlanılıyor..."));
                var connectPostbox = new PostBoxServiceClient("PostBoxServiceEndpoint");
                if (connectPostbox.Login(login, out ssId))
                {
                    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " E-Logo Postbox Servise bağlantı başarılı..."));
                    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " userPkList.zip dosyası indiriliyor..."));
                    var userList = connectPostbox.getUserListNew(login, UserListType.GBLIST);
                    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " userPkList.zip dosyası indirme başarılı..."));
                    File.WriteAllBytes(string.Concat(Path.GetTempPath(), "userGbList", ".zip"), userList.binaryData.Value);
                    var userPath = string.Concat(Path.GetTempPath(), "userGBList", ".zip");
                    string userExtractPath = @"C:\Temp";
                    if (!File.Exists(userExtractPath))
                    {
                        Directory.CreateDirectory(userExtractPath);
                    }
                    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " Zip dosyası çıkartılıyor..."));
                    using (ZipArchive archive = ZipFile.OpenRead(userPath))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            destinationPath = Path.GetFullPath(Path.Combine(userExtractPath, entry.FullName));

                            // Ordinal match is safest, case-sensitive volumes can be mounted within volumes that
                            // are case-insensitive.
                            if (destinationPath.StartsWith(userExtractPath, StringComparison.Ordinal))
                                if (File.Exists(destinationPath))
                                {
                                    File.Delete(destinationPath);
                                }
                            entry.ExtractToFile(destinationPath);
                            LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), $" Zip dosyası çıkartıldı ve {destinationPath}'e yazıldı..."));
                        }
                    }
                    var xmlDataDocument = new XmlDataDocument();
                    var fileStream = new FileStream(string.Concat(destinationPath), FileMode.Open, FileAccess.Read);
                    xmlDataDocument.Load(fileStream);

                    userListGb = SerializerXml.Deserialize<UserList>(xmlDataDocument.InnerXml);
                    userListGb.ListType = "GB";
                }
            }
            catch (Exception e)
            {
                LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " ", nameof(ConnectService), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }


            return userListGb;
        }
    }
}