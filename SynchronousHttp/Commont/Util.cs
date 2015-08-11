using SynchronousHttp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace SynchronousHttp.Controllers
{
    public static class Util
    {
        /// <summary>
        /// Reading JSON file from file path
        /// </summary>
        /// <param name="path">JSON file path</param>
        /// <returns>JSON</returns>
        public static string ReadJsonFile (string path)
        {
            string content;
            if (path != null)
            {
                string fullPath = HttpContext.Current.Server.MapPath(path);
                System.IO.StreamReader file = new System.IO.StreamReader(fullPath);
                content = file.ReadToEnd().Trim();
                return content;
            }
            else
            {
                return null;
            }
        }

        public static string GetFullPath(string absolutePath)
        {
            if (absolutePath != null)
            {
                try
                {
                    return HttpContext.Current.Server.MapPath(absolutePath);
                }
                catch (Exception ex)
                {

                }
            }
            return null;
        }

        /// <summary>
        /// ReadProperties used to read text file
        /// </summary>
        /// <param name="path">absolute path</param>
        /// <param name="property">name of property</param>
        /// <returns></returns>
        public static string ReadProperties (string path, string property)
        {
            string line;
            if (path != null)
            {
                string fullPath = HttpContext.Current.Server.MapPath(path);
                System.IO.StreamReader file = new System.IO.StreamReader(fullPath);
                while ((line = file.ReadLine().Trim()) != null)
                {
                    if (!line.StartsWith(Constants.SHARP_CHARACTER) && line.ToLower().Contains(property))
                    {
                        return line.Replace(Constants.WHILE_SPACE_SIGN, Constants.EMPTY_CHARACTER)
                            .Replace(property, Constants.EMPTY_CHARACTER)
                            .Replace(Constants.EQUAL_SIGN, Constants.EMPTY_CHARACTER);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// GetContentHTML return List folders name
        /// </summary>
        /// <param name="URL">url root</param>
        /// <param name="regexPal">Regular Expressions</param>
        /// <param name="isFile">file or folder?</param>
        /// <returns></returns>
        public static List<string> GetConentTHML(string URL, string regexPal, bool isFile)
        {
            List<string> lstContents = new List<string>();
            if (URL != null && regexPal != null)
            {
                try
                {
                    if (URL.ToLower().StartsWith(Constants.SSL_PROTOCOL))
                    {
                        /* Bypass SSL if URL_FOLDER is HTTPS protocol */
                        ServicePointManager.ServerCertificateValidationCallback +=
                            (sender, cert, chain, sslPolicyErrors) => true;
                    }
                    WebClient client = new WebClient();
                    string html = client.DownloadString(URL);

                    Regex regex = new Regex(regexPal);
                    Match match = regex.Match(html);
                    while (match.Success)
                    {
                        string value = match.Value;
                        if (isFile)
                        {
                            /* Replace all unused characters by regular expressions */
                            Regex rgx = new Regex(Constants.START_A_TAGS);
                            value = rgx.Replace(value, Constants.EMPTY_CHARACTER);

                            value = value.Replace(Constants.QUOTATION_CHARACTER, Constants.EMPTY_CHARACTER)
                                .Replace(Constants.END_A_TAGS, Constants.EMPTY_CHARACTER);
                        }
                        if (!lstContents.Contains(value))
                        {
                            lstContents.Add(value);
                        }
                        match = match.NextMatch();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            return lstContents;
        }

        /// <summary>
        /// DownloadFileFromURL downloaded one file and saved follow fileName.
        /// </summary>
        /// <param name="URL">Links download file</param>
        /// <param name="fileName">Name of file</param>
        public static bool DownloadFileFromURL(string URL, string fileName)
        {
            bool result = true;
            WebClient client = new WebClient();
            try
            {
                client.DownloadFile(URL, fileName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// ConvertDate will converts string to DateTime
        /// </summary>
        /// <param name="content">type is string</param>
        /// <param name="date">Regex_date</param>
        /// <param name="time">Regex_time</param>
        /// <returns>DateTime</returns>
        public static DateTime ConvertDate(string dateTime)
        {
            if (dateTime != null)
            {
                try
                {
                    return DateTime.ParseExact(dateTime, Constants.DATE_FORMAT, null);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

            }
            return DateTime.Parse(Constants.DATE_TIME_RESET);
        }

        /// <summary>
        /// ConvertToJson convert this object to string then return
        /// </summary>
        /// <param name="JsonObject">this object will be convert to string</param>
        /// <returns>Json type</returns>
        public static string ConvertToJsonString(List<SynchronousHttp.Models.JsonObject> JsonObject)
        {
            try
            {
                return new JavaScriptSerializer().Serialize(JsonObject);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// getDateTime return date string has formatDate is yyyyMMddHHmmss
        /// </summary>
        /// <param name="content">value input is yyyyMMdd_HHmmss</param>
        /// <returns>date string</returns>
        public static string GetDateTime(string content)
        {
            if (content != null)
            {
                Regex regex = new Regex(Constants.REGEX_DATE_TIME);
                Match match = regex.Match(content);
                if (match.Success)
                {
                    return match.Value.Replace(Constants.UNDER_LINE, Constants.EMPTY_CHARACTER);
                }
            }
            return null;
        }
    }
}