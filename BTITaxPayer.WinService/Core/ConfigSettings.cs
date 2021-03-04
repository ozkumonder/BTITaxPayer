﻿using System;
using DevExpress.XtraScheduler;

namespace BTITaxPayerService.Core
{
    public class ConfigSettings
    {
        #region SQL Info
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string DbUserName { get; set; }
        public string DbPassword { get; set; }

        #endregion

        #region Logo Info
        public string LogoUserName { get; set; }
        public string LogoPassword { get; set; }
        public string LogoFirmNumber { get; set; }
        public string LogoPeriodNumber { get; set; }
        public string LogoPath { get; set; }
        #endregion

        #region Rest Info
        public string RestClientId { get; set; }
        public string RestClientSecret { get; set; }
        public string RestServiceUrl { get; set; }
        public float RestTimeOut { get; set; }

        #endregion

        #region Connect Info
        public string ConnectUser { get; set; }
        public string ConnectPass { get; set; }
        public short ConnectWorkSpace { get; set; }
        #endregion

        #region E-Logo Info
        public string eLogoUserName { get; set; }
        public string eLogoPassword { get; set; }

        #endregion

        #region Ftp Info
        public string FtpHost { get; set; }
        public string FtpUser { get; set; }
        public string FtpPassword { get; set; }
        public string FilePath { get; set; }
        public string FilePrefix { get; set; }
        #endregion

        #region Temp Info
        public string FolderPath { get; set; }

        #endregion

        #region Scheduler Info

        public int SchedulerType { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskRunTime { get; set; }
        public int TaskRepeatCount { get; set; }
        public bool TaskRunAlways { get; set; }
        //public DateTime TaskDailyParameter { get; set; }
        public WeekDays TaskWeeklyParameter { get; set; }
        public int TaskMonthlyParameter { get; set; }
        #endregion


    }

    public class GlobalSettings
    {
        #region SQL Info
        public static string ServerName { get; set; }
        public static string DatabaseName { get; set; }
        public static string DbUserName { get; set; }
        public static string DbPassword { get; set; }

        #endregion

        #region Logo Info
        public static string LogoUserName { get; set; }
        public static string LogoPassword { get; set; }
        public static string LogoFirmNumber { get; set; }
        public static string LogoPeriodNumber { get; set; }
        public static string LogoPath { get; set; }
        #endregion

        #region Rest Info
        public static string RestClientId { get; set; }
        public static string RestClientSecret { get; set; }
        public static string RestServiceUrl { get; set; }
        public static float RestTimeOut { get; set; }

        #endregion

        #region Connect Info
        public static string ConnectUser { get; set; }
        public static string ConnectPass { get; set; }
        public static short ConnectWorkSpace { get; set; }
        #endregion

        #region E-Logo Info
        public static string eLogoUserName { get; set; }
        public static string eLogoPassword { get; set; }

        #endregion

        #region Ftp Info
        public static string FtpHost { get; set; }
        public static string FtpUser { get; set; }
        public static string FtpPassword { get; set; }
        public static string FilePath { get; set; }
        public static string FilePrefix { get; set; }
        #endregion

        #region Temp Info
        public static string FolderPath { get; set; }

        #endregion

        #region Scheduler Info
        public static int SchedulerType { get; set; }
        public static DateTime TaskStartDate { get; set; }
        public static DateTime TaskRunTime { get; set; }
        public static int TaskRepeatCount { get; set; }
        public static bool TaskRunAlways { get; set; }
        //public DateTime TaskDailyParameter { get; set; }
        public static WeekDays TaskWeeklyParameter { get; set; }
        public static int TaskMonthlyParameter { get; set; }
        #endregion




    }

}
