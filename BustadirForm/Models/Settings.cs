using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BustadirForm.Models
{
    public class Settings
    {
        #region Singleton
        private static Settings _instance;
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Settings();
                }
                return _instance;
            }
        }
        private Settings()
        {
            AppName = "Bústaðir - Umsóknarblað";
            AppRegInfo = "© " + DateTime.Now.Year + " - " + AppName;
            DateTimeFormatReporting = "ddMMyyyy";
        }
        #endregion

        public string AppName { get; set; }

        public string AppRegInfo { get; set; }

        public string DateTimeFormatReporting { get; set; }
    }
}