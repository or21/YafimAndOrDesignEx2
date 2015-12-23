//-----------------------------------------------------------------------
// <copyright file="Utils.cs" company="A16_Ex02">
// Yafim Vodkov 308973882 Or Brand id 302521034
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using FacebookWrapper.ObjectModel;
using Newtonsoft.Json.Linq;

namespace Utils
{
    /// <summary>
    /// Singeltone class holds the necessarily logic to use AppUI class..
    /// </summary>
    public class Utils
    {
        #region Initialization
        /// <summary>
        /// Prevent race condition
        /// </summary>
        private static readonly object sr_LockInstance = new object();

        /// <summary>
        /// Instance of the Utils class.
        /// </summary>
        private static Utils s_Instance;

        /// <summary>
        /// Parsed json
        /// </summary>
        private JObject m_ParsedJson;

        /// <summary>
        /// Prevents a default instance of the Utils class from being created.
        /// </summary>
        private Utils()
        {
        }

        /// <summary>
        /// Gets instance of Utils class
        /// </summary>
        public static Utils Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    lock (sr_LockInstance)
                    {
                        if (s_Instance == null)
                        {
                            s_Instance = new Utils();
                        }
                    }
                }

                return s_Instance;
            }
        }
        #endregion

        #region WhoWasBornOnMyBirthdayForm logic

        /// <summary>
        /// Return list of people who was born on specific date.
        /// </summary>
        /// <param name="i_PathToJsonFile">Path to local json file</param>
        /// <param name="i_BirthdayDate">The date</param>
        /// <returns>List of people who was born on specific date</returns>
        public List<string> GetListOfPeopleFromJSON(string i_PathToJsonFile, string i_BirthdayDate)
        {
            List<string> i_ListOfPeopleWhoWasBornOnMyBirthday;
            string parsedBirthdayDate = parseBirthdayDate(i_BirthdayDate); 
            m_ParsedJson = getParsedJSONFile(i_PathToJsonFile);
            parseBirthdayJson(m_ParsedJson, out i_ListOfPeopleWhoWasBornOnMyBirthday, parsedBirthdayDate);

            return i_ListOfPeopleWhoWasBornOnMyBirthday;
        }

        /// <summary>
        /// Get parsed json file
        /// </summary>
        /// <param name="i_PathToJsonFile">Path to local json file</param>
        /// <returns>Parsed json file</returns>
        private JObject getParsedJSONFile(string i_PathToJsonFile)
        {
            string json = getLocalJsonFile(i_PathToJsonFile);
            m_ParsedJson = parseJson(json);

            return m_ParsedJson;
        }

        /// <summary>
        /// Parse given date to MM-DD format
        /// </summary>
        /// <param name="i_BirthdayToParse">Birthday date mm/dd/yyyy </param>
        /// <returns>parsed birthday as string</returns>
        private string parseBirthdayDate(string i_BirthdayToParse)
        {
            bool isValidDate = validateStringFormat(i_BirthdayToParse);
            string strToReturn;

            if (isValidDate)
            {
                string formattedBirhdayDate = string.Format(
                    "{0}{1}-{2}{3}",
                    i_BirthdayToParse[0],
                    i_BirthdayToParse[1],
                    i_BirthdayToParse[3],
                    i_BirthdayToParse[4]);
                strToReturn = formattedBirhdayDate;
            }
            else
            {
                throw new FormatException();
            }

            return strToReturn;
        }

        /// <summary>
        /// return true if i_StringToCheck format is dd/mm/yyyy, Otherwise false..
        /// </summary>
        /// <param name="i_StringToCheck">String to check</param>
        /// <returns>True if valid string</returns>
        private bool validateStringFormat(string i_StringToCheck)
        {
            bool isValid;
            try
            {
                DateTime parsedTime = DateTime.ParseExact(i_StringToCheck, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                isValid = true;
            }
            catch (FormatException fe)
            {
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Insert json-celeb data to collection
        /// </summary>
        /// <param name="i_Json">json object</param>
        /// <param name="o_ListOfPeopleWhoWasBornOnMyBirthday">The collection</param>
        /// <param name="i_Key">Key word</param>
        private void parseBirthdayJson(JObject i_Json, out List<string> o_ListOfPeopleWhoWasBornOnMyBirthday, string i_Key)
        {
            o_ListOfPeopleWhoWasBornOnMyBirthday = new List<string>();

            foreach (JToken name in i_Json[i_Key])
            {
                o_ListOfPeopleWhoWasBornOnMyBirthday.Add(name.ToString());
            }
        }

        /// <summary>
        /// Format selected name ('firstName lastName') to "firstName_lastName" 
        /// </summary>
        /// <param name="i_StrToForamt">String to format</param>
        /// <returns>formatted string</returns>
        private string setCurrentNameInFormat(string i_StrToForamt)
        {
            return i_StrToForamt.Replace(" ", "_");
        }

        /// <summary>
        /// Returns the image path of the celeb
        /// </summary>
        /// <param name="i_CelebFullName">Celeb full name ('firstName secondName')</param>
        /// <returns>Image path from wikipedia</returns>
        public string GetCelebPictureUrl(string i_CelebFullName)
        {
            string formattedName = setCurrentNameInFormat(i_CelebFullName);

            string jsonWikiRequest = buildJsonWikiRequest(formattedName);

            m_ParsedJson = getJsonFromUrl(jsonWikiRequest);

            string image = getJsonWikiImageQuery(m_ParsedJson);

            return image;
        }

        /// <summary>
        /// Get wikipedia information about the celeb
        /// </summary>
        /// <param name="i_CelebFullName">Celeb full name</param>
        /// <returns>Information as string</returns>
        public string GetCelebInfo(string i_CelebFullName )
        {
            return getWikiJsonInfo(m_ParsedJson);
        }

        /// <summary>
        /// Build json request to wikipedia server using its API.
        /// properties: images|intro content - based on given name
        /// </summary>        
        /// <param name="i_FullName">Full name as parameter</param>
        /// <returns>formatted http request</returns>
        private string buildJsonWikiRequest(string i_FullName)
        {
            return string.Format("https://en.wikipedia.org/w/api.php?action=query&titles={0}&prop=pageimages|extracts&exintro=&explaintext=&format=json&pithumbsize=300", i_FullName);
        }

        /// <summary>
        /// Get information from wiki json file
        /// </summary>
        /// <param name="i_Json">Json file</param>
        /// <returns>wiki information as string</returns>
        private string getWikiJsonInfo(JObject i_Json)
        {
            string wikiInfo;

            try
            {
                wikiInfo = GetJsonWikiInfoQuery(i_Json);
            }
            catch (Exception e)
            {
                wikiInfo = "Nothing was found... Please let us know";
            }

            return wikiInfo;
        }

        /// <summary>
        /// Get information from wiki-json 
        /// </summary>
        /// <param name="i_Json">Json file</param>
        /// <returns>Wiki information</returns>
        public string GetJsonWikiInfoQuery(JObject i_Json)
        {
            return i_Json["query"]["pages"].First.First["extract"].ToString();
        }

        /// <summary>
        /// Get image from wiki-json
        /// </summary>
        /// <param name="i_Json">Json file</param>
        /// <returns>Image path</returns>
        private string getJsonWikiImageQuery(JObject i_Json)
        {
            return i_Json["query"]["pages"].First.First["thumbnail"]["source"].ToString();
        }

        /// <summary>
        /// Send request to the wiki server, download json and parse it.
        /// </summary>
        /// <param name="i_JsonWikiUrl">Wiki path</param>
        /// <returns>json object</returns>
        private JObject getJsonFromUrl(string i_JsonWikiUrl)
        {
            using (WebDownload wc = new WebDownload())
            {
                string json = wc.DownloadString(i_JsonWikiUrl);
                return parseJson(json);
            }
        }

        /// <summary>
        /// Parse JSON file
        /// </summary>
        /// <param name="i_JsonToParse">json file to parse</param>
        /// <returns>parsed json file as JObject</returns>
        private JObject parseJson(string i_JsonToParse)
        {
            return JObject.Parse(i_JsonToParse);
        }

        /// <summary>
        /// Get local JSON file
        /// </summary>
        /// <param name="i_PathToJsonFile">Path to json file</param>
        /// <returns>json file as string</returns>
        private string getLocalJsonFile(string i_PathToJsonFile)
        {
            using (StreamReader reader = new StreamReader(i_PathToJsonFile))
            {
                return reader.ReadToEnd();
            }
        }
        #endregion

        #region MostLikeablePicturesForm logic

        /// <summary>
        /// Set next image
        /// </summary>
        /// <param name="i_IndexOfCurrentImage">Index of current image</param>
        /// <param name="i_NumberOfPictures">Number of pictures</param>
        /// <returns>next image index</returns>
        public int SetNextImage(int i_IndexOfCurrentImage, int i_NumberOfPictures)
        {
            return (i_IndexOfCurrentImage + 1 < i_NumberOfPictures) ? i_IndexOfCurrentImage + 1 : 0;
        }

        /// <summary>
        /// Set previous image
        /// </summary>
        /// <param name="i_IndexOfCurrentImage">Index of current image</param>
        /// <param name="i_NumberOfPictures">Number of pictures</param>
        /// <returns>index of previous image</returns>
        public int SetPrevImage(int i_IndexOfCurrentImage, int i_NumberOfPictures)
        {
            return (i_IndexOfCurrentImage - 1 >= 0) ? i_IndexOfCurrentImage - 1 : i_NumberOfPictures - 1;
        }

        #endregion

        #region MainForm

        /// <summary>
        /// Sort list of photos by number of likes 
        /// </summary>
        /// <param name="io_ListOfPhotos">List of photos</param>
        public void SortPhotosByDescendingOrder(List<Photo> io_ListOfPhotos)
        {
            io_ListOfPhotos.Sort((i_NumberOfLikesPhotoOne, i_NumberOfLikesPhotoTwo) =>
                i_NumberOfLikesPhotoOne.LikedBy.Count().CompareTo(i_NumberOfLikesPhotoTwo.LikedBy.Count()));
            io_ListOfPhotos.Reverse();
        }

        /// <summary>
        /// find most likeable photos
        /// </summary>
        /// <param name="i_NumberOfPhotosToShow">number of photos to show</param>
        /// <param name="i_ListOfPhotos">List of photos</param>
        /// <returns>Listed Number of photos to show</returns>
        public List<Photo> FindMostLikablePhotos(int i_NumberOfPhotosToShow, List<Photo> i_ListOfPhotos)
        {
            List<Photo> topLikeablePhotos = new List<Photo>(i_NumberOfPhotosToShow);

            Photo minPhoto = new Photo();

            foreach (Photo photo in i_ListOfPhotos)
            {
                if (topLikeablePhotos.Count != topLikeablePhotos.Capacity)
                {
                    topLikeablePhotos.Add(photo);
                    minPhoto = findMinInTopLikable(topLikeablePhotos);
                }
                else
                {
                    if (photo.LikedBy.Count >= minPhoto.LikedBy.Count)
                    {
                        addPhotoToList(photo, ref minPhoto, topLikeablePhotos);
                    }
                }
            }

            return topLikeablePhotos;
        }

        /// <summary>
        /// Add photos to list
        /// </summary>
        /// <param name="i_Photo">Photo to add</param>
        /// <param name="io_MinPhoto">Photo with minimum likes</param>
        /// <param name="i_MostLikeablePhotos">Most liked photos</param>
        private void addPhotoToList(Photo i_Photo, ref Photo io_MinPhoto, List<Photo> i_MostLikeablePhotos)
        {
            i_MostLikeablePhotos.Remove(io_MinPhoto);
            i_MostLikeablePhotos.Add(i_Photo);
            io_MinPhoto = findMinInTopLikable(i_MostLikeablePhotos);
        }

        /// <summary>
        /// Find the picture with the minimum likes
        /// </summary>
        /// <param name="i_MostLikeablePhotos">Most likeable picture</param>
        /// <returns>The photo with the minimum likes</returns>
        private Photo findMinInTopLikable(List<Photo> i_MostLikeablePhotos)
        {
            Photo minPhoto = i_MostLikeablePhotos[0];

            foreach (Photo photo in i_MostLikeablePhotos)
            {
                if (photo.LikedBy.Count <= minPhoto.LikedBy.Count)
                {
                    minPhoto = photo;
                }
            }

            return minPhoto;
        }

        /// <summary>
        /// Get photo dimensions
        /// </summary>
        /// <param name="i_Width">Picture with</param>
        /// <param name="i_Height">Picture height</param>
        /// <param name="i_MostLikeablePhotos">Most likeable photos</param>
        public void GetWidthAndHeight(ref int i_Width, ref int i_Height, List<Photo> i_MostLikeablePhotos)
        {
            foreach (Photo photo in i_MostLikeablePhotos)
            {
                if (photo.Width > i_Width)
                {
                    i_Width = (int)photo.Width;
                }

                if (photo.Height > i_Height)
                {
                    i_Height = (int)photo.Height;
                }
            }
        }

        #endregion
    }
}
