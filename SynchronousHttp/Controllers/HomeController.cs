using SynchronousHttp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SynchronousHttp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            /* Check user is authenticated */
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            Account account = new Account("admin");
            return View();
        }

        /// <summary>
        /// Return JSON content
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string ShowJson ()
        {
            try
            {
                return Util.ReadJsonFile(Constants.ABSOLUTE_PATH
                            + Constants.APP_DATA_FOLDER + Constants.JSON_FILE_NAME);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Synchronous JSON content from HTTP Server
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string SynchronousJSON()
        {
            String result;
            List<JsonObject> lstJsonObject = new List<JsonObject>();
            try
            {
                /* Get URL_Root from config file */
                string urlRoot = Util.ReadProperties(Constants.ABSOLUTE_PATH
                    + Constants.CONFIG_FOLDER + Constants.CONFIG_FILE, Constants.URL_ROOT);

                /* Get List folders by urlRoot */
                List<string> lstFolder = Util.GetConentTHML(urlRoot, Constants.REGEX_FOLDER, false);

                int size = lstFolder.Count();
                if (size > 0)
                {
                    /* Order by ASC */
                    for (int i = (size - 1); i >= 0; i--)
                    {
                        JsonObject jsonObject = new JsonObject();

                        /* Get DateTime from folder name */
                        DateTime dateTime = Util.ConvertDate(Util.GetDateTime(lstFolder[i]));
                        if (dateTime > DateTime.Parse(Constants.DATE_TIME_RESET)) {
                            jsonObject.time = dateTime;
                            jsonObject.timeString = dateTime.ToString(Constants.DATE_FORMAT_JSON);

                            /* Get list files from folder name */
                            List<string> files = Util.GetConentTHML(urlRoot + Constants.FILE_PATH + lstFolder[i], Constants.REGEX_FILE_NAME, true);

                            /* Declare object saving all files */
                            List<Image> images = new List<Image>();
                            foreach (string file in files)
                            {
                                if (file.Trim().ToLower().EndsWith(Constants.IMAGE_TYPE_FILE))
                                {
                                    Image image = new Image();
                                    image.imageUrl = urlRoot + Constants.FILE_PATH + lstFolder[i] + Constants.FILE_PATH + file.Trim();
                                    image.thumbUrl = urlRoot + Constants.FILE_PATH + lstFolder[i] + Constants.FILE_PATH + file.Trim();
                                    images.Add(image);
                                }
                            }
                            jsonObject.images = images;
                        }
                        lstJsonObject.Add(jsonObject);
                    }

                    string json = Util.ConvertToJsonString(lstJsonObject);
                    if (json != null)
                    {
                        /* Writing json content to JSON file */
                        string fullPath = Util.GetFullPath(Constants.ABSOLUTE_PATH 
                            + Constants.APP_DATA_FOLDER + Constants.JSON_FILE_NAME);
                        if (fullPath != null)
                        {
                            System.IO.File.WriteAllText(fullPath, json);
                            result = Constants.CONVERT_JSON_SUCCESS;
                        }
                        else
                        {
                            result = Constants.SAVING_FILE_FAIL;
                        }
                    }
                    else
                    {
                        result = Constants.CONVERT_JSON_FAIL;
                    }
                }
                else
                {
                    result = Constants.HTTP_NO_CONTENT;
                }
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }

        [HttpGet]
        public string ShowImageByJSON()
        {
            try
            {
                return Util.ReadJsonFile(Constants.ABSOLUTE_PATH
                            + Constants.APP_DATA_FOLDER + Constants.JSON_FILE_NAME);
            }
            catch (Exception ex)
            {
                return "JSON file is not exist!";
            }
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}