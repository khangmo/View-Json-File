using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SynchronousHttp.Controllers
{
    public static class Constants
    {
        /// <summary>
        /// name of config file
        /// </summary>
        public static string CONFIG_FILE = "conf.properties";
        
        /// <summary>
        /// special characters
        /// </summary>
        public static string WHILE_SPACE_SIGN = @"\s";
        public static string EQUAL_SIGN = "=";
        public static string ABSOLUTE_PATH = "~";
        public static string FILE_PATH =  "/";
        public static string SHARP_CHARACTER = "#";
        public static string QUOTATION_CHARACTER = "\"";
        public static string EMPTY_CHARACTER = "";
        public static string UNDER_LINE = "_";

        /// <summary>
        /// parameters for Quartz Scheduler
        /// </summary>
        public static string CONFIG_HOUR_INTERVAL = "interval";
        public static string CONFIG_START_HOUR = "start_hour";
        public static string CONFIG_START_MINUS = "start_minus";
        public static string CONFIG_FOLDER = "/Config/";

        /// <summary>
        /// Name of JSON file after synchoronouns
        /// </summary>
        public static string JSON_FILE_NAME = "json.json";
        public static string IMAGE_TYPE_FILE = ".jpg";

        public static string APP_DATA_FOLDER = "/App_Data/";

        /// <summary>
        /// Regular Expressions to parser for video folder
        /// </summary>
        public static string REGEX_FOLDER = @"video_\d{8}_\d{6}_\w{8}";
        public static string REGEX_FILE_NAME = "<a {1,10}href {0,10}=\"[a-zA-Z0-9._]+\">";
        public static string REGEX_DATE_TIME = @"\d{8}_\d{6}";
        public static string START_A_TAGS = @"<(\s){0,10}a(\s){0,10}href(\s){0,10}=";
        public static string END_A_TAGS = ">";

        /// <summary>
        /// This mask to parser date time string.
        /// </summary>
        public static string DATE_TIME_RESET = "01/01/1970 1:01:01 AM";
        public static string DATE_FORMAT = "yyyyMMddHHmmss";
        public static string DATE_FORMAT_JSON = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Protocal using in this application
        /// </summary>
        public static string SSL_PROTOCOL = "https";
        public static string HTTP_PROTOCOL = "http";
        public static string URL_ROOT = "url_root";

        /// <summary>
        /// Message 
        /// </summary>
        public static string CONVERT_JSON_SUCCESS = "Convert JSON success. Click button \"Check JSON\" for more detail.";
        public static string HTTP_NO_CONTENT = "Haven't data from HTTP";
        public static string CONVERT_JSON_FAIL = "Can not convert to JSON";
        public static string SAVING_FILE_FAIL = "Saving file fail!";
    }
}