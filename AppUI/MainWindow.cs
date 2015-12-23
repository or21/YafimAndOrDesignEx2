//-----------------------------------------------------------------------
// <copyright file="MainWindow.cs" company="A16_Ex02">
// Yafim Vodkov 308973882 Or Brand id 302521034
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using Utils;

namespace AppUI
{
    /// <summary>
    /// UI of the application
    /// </summary>
    public partial class MainWindow : FbForm
    {
        /// <summary>
        /// Post message
        /// </summary>
        private const string k_StartPost = "What's on your mind...";

        /// <summary>
        /// Events message
        /// </summary>
        private const string k_NoEventYet = "No Events yet";

        /// <summary>
        /// No likes message
        /// </summary>
        private const string k_NoLikes = "You don't like any page";

        /// <summary>
        /// No checkIn message
        /// </summary>
        private const string k_NoCheckIns = "You didn't do any check in";

        /// <summary>
        /// No post to retrieve message
        /// </summary>
        private const string k_NoPostsToRetrieve = "No Posts to retrieve :(";

        /// <summary>
        /// Wait message
        /// </summary>
        private const string k_WaitMessage = "This may take few seconds... Please click OK and Go get yourself a cup of coffee";

        /// <summary>
        /// LoggedIn user
        /// </summary>
        private readonly User r_LoggedInUser;

        /// <summary>
        /// Instance of Util class
        /// </summary>
        private readonly Utils.Utils r_Util;

        /// <summary>
        /// Number of pictures to show
        /// </summary>
        private int m_NumberOfPicturesToShow = 5;

        /// <summary>
        /// List of facebook photos
        /// </summary>
        private List<Photo> m_ListOfPhotos;

        /// <summary>
        /// List of top likeable photos
        /// </summary>
        private List<Photo> m_MostLikeablePhotos;

        /// <summary>
        /// List of threads
        /// </summary>
        private readonly List<Thread> r_Threads = new List<Thread>();

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        /// <param name="i_UserData">The user facebook data</param>
        public MainWindow(LoginResult i_UserData)
        {
            InitializeComponent();
            r_LoggedInUser = i_UserData.LoggedInUser;
            FacebookService.s_CollectionLimit = 1000;
            r_Util = Utils.Utils.Instance;
        }

        /// <summary>
        /// Loads the user info on different thread
        /// </summary>
        /// <param name="i_Event"></param>
        protected override void OnLoad(EventArgs i_Event) 
        {
            base.OnLoad(i_Event);
            new Thread(fetchUserInfo).Start();
        }

        /// <summary>
        /// Clear textBox when clicked
        /// </summary>
        /// <param name="i_Sender">Object sender</param>
        /// <param name="i_Event">The event</param>
        private void textBoxPost_Click(object i_Sender, EventArgs i_Event)
        {
            textBoxPost.Clear();
            textBoxPost.ForeColor = Color.Black;
        }

        /// <summary>
        /// Fetch events and show them in relevant textbox
        /// </summary>
        private void fetchEvents()
        {
            listBoxEvents.Invoke(new Action(() => listBoxEvents.HorizontalScrollbar = true));
            listBoxEvents.Invoke(new Action(() => listBoxEvents.DisplayMember = "Name"));
            foreach (Event fbEvent in r_LoggedInUser.Events)
            {
                listBoxEvents.Invoke(new Action(() => listBoxEvents.Items.Add(fbEvent)));
            }

            if (r_LoggedInUser.Events.Count == 0)
            {
                listBoxEvents.Invoke(new Action(() => listBoxEvents.BackColor = Color.Gray));
                listBoxEvents.Invoke(new Action(() => listBoxEvents.Items.Add(k_NoEventYet)));
            }
        }

        /// <summary>
        /// Fetch user information and show them in relevant textbox
        /// </summary>
        private void fetchUserInfo()
        {
            Thread threadPhotos = new Thread(fetchPhotos);
            r_Threads.Add(threadPhotos);
            threadPhotos.Start();

            textBoxPost.Invoke(new Action(() => textBoxPost.Text = k_StartPost));
            new Thread(fetchEvents).Start();
            new Thread(fetchUserData).Start();
            new Thread(fetchNewsFeed).Start();
            new Thread(fetchPages).Start();
            new Thread(fetchCheckIn).Start();
        }

        /// <summary>
        /// Fetch pages and show them in relevant textbox
        /// </summary>
        private void fetchPages()
        {
            listBoxPages.Invoke(new Action(() => listBoxPages.HorizontalScrollbar = true));
            listBoxPages.Invoke(new Action(() => listBoxPages.DisplayMember = "Name"));
            foreach (Page fbPage in r_LoggedInUser.LikedPages)
            {
                listBoxPages.Invoke(new Action(() => listBoxPages.Items.Add(fbPage)));
            }

            if (r_LoggedInUser.LikedPages.Count == 0)
            {
                listBoxPages.Invoke(new Action(() => listBoxPages.BackColor = Color.Gray));
                listBoxPages.Invoke(new Action(() => listBoxPages.Items.Add(k_NoLikes)));
            }
        }

        /// <summary>
        /// Fetch checkIns and show them in relevant textbox
        /// </summary>
        private void fetchCheckIn()
        {
            listBoxCheckIn.Invoke(new Action(() => listBoxCheckIn.HorizontalScrollbar = true));
            listBoxCheckIn.Invoke(new Action(() => listBoxCheckIn.DisplayMember = "Message"));
            foreach (Checkin fbCheckin in r_LoggedInUser.Checkins)
            {
                if (fbCheckin.Message != null)
                {
                    listBoxCheckIn.Invoke(new Action(() => listBoxCheckIn.Items.Add(fbCheckin)));
                }
            }

            if (r_LoggedInUser.Checkins.Count == 0)
            {
                listBoxCheckIn.Invoke(new Action(() => listBoxCheckIn.BackColor = Color.Gray));
                listBoxCheckIn.Invoke(new Action(() => listBoxCheckIn.Items.Add(k_NoCheckIns)));
            }
        }

        /// <summary>
        /// Fetch user photos
        /// </summary>
        private void fetchPhotos()
        {
            m_ListOfPhotos = new List<Photo>();
            m_MostLikeablePhotos = new List<Photo>(m_NumberOfPicturesToShow);
            foreach (Album album in r_LoggedInUser.Albums)
            {
                foreach (Photo photo in album.Photos)
                {
                    m_ListOfPhotos.Add(photo);
                }
            }

            if (m_ListOfPhotos.Count == 0)
            {
                buttonGetMostPhotos.Enabled = false;
            }
            else
            {
                if (m_ListOfPhotos.Count < m_NumberOfPicturesToShow)
                {
                    m_NumberOfPicturesToShow = m_ListOfPhotos.Count;
                }

                m_MostLikeablePhotos = r_Util.FindMostLikablePhotos(m_NumberOfPicturesToShow, m_ListOfPhotos);
            }
        }

        /// <summary>
        /// Fetch User posts and show them in relevant textbox
        /// </summary>
        private void fetchNewsFeed()
        {
            listBoxFeed.Invoke(new Action(() => listBoxFeed.HorizontalScrollbar = true));
            foreach (Post post in r_LoggedInUser.NewsFeed)
            {
                if (post.Message != null)
                {
                    listBoxFeed.Invoke(new Action(() => listBoxFeed.Items.Add(post.UpdateTime + ": " + post.Message)));
                }
                else if (post.Caption != null)
                {
                    listBoxFeed.Invoke(new Action(() => listBoxFeed.Items.Add(post.UpdateTime + ": " + post.Caption)));
                }
                else
                {
                    listBoxFeed.Invoke(new Action(() => listBoxFeed.Items.Add(string.Format(post.UpdateTime + ": " + "[{0}]", post.Type))));
                }
            }

            if (r_LoggedInUser.NewsFeed.Count == 0)
            {
                listBoxFeed.Invoke(new Action(() => listBoxFeed.BackColor = Color.Gray));
                listBoxFeed.Invoke(new Action(() => listBoxFeed.Items.Add(k_NoPostsToRetrieve)));
            }
        }

        /// <summary>
        /// Fetch user data and show it in relevant textbox
        /// </summary>
        private void fetchUserData()
        {
            listBoxProfie.Invoke(new Action(() => listBoxProfie.HorizontalScrollbar = true));
            pictureBoxProfile.LoadAsync(r_LoggedInUser.PictureNormalURL);
            if (r_LoggedInUser.Birthday != null)
            {
                listBoxProfie.Invoke(new Action(() => listBoxProfie.Items.Add("Birthday: " + r_LoggedInUser.Birthday)));
            }

            if (r_LoggedInUser.Gender != null)
            {
                listBoxProfie.Invoke(new Action(() => listBoxProfie.Items.Add("Gender: " + r_LoggedInUser.Gender)));
            }

            if (r_LoggedInUser.Hometown != null)
            {
                listBoxProfie.Invoke(new Action(() => listBoxProfie.Items.Add("Hometown: " + r_LoggedInUser.Hometown.Name)));
            }

            if (r_LoggedInUser.Email != null)
            {
                listBoxProfie.Invoke(new Action(() => listBoxProfie.Items.Add("Email: " + r_LoggedInUser.Email)));
            }
        }

        /// <summary>
        /// Post status in facebook wall
        /// </summary>
        /// <param name="i_Sender">Object sender</param>
        /// <param name="i_Event">The event</param>
        private void buttonPost_Click(object i_Sender, EventArgs i_Event)
        {
            Status postedStatus = r_LoggedInUser.PostStatus(textBoxPost.Text);
            MessageBox.Show(string.Format(@"Status: {0} Posted", postedStatus.Message));
        }

        /// <summary>
        /// Logout and exit application
        /// </summary>
        /// <param name="i_Sender">Object sender</param>
        /// <param name="i_Event">The event</param>
        private void buttonLogout_Click(object i_Sender, EventArgs i_Event)
        {
            Application.Exit();
        }

        /// <summary>
        /// Show 5 most likeable pictures 
        /// </summary>
        /// <param name="i_Sender">Object sender</param>
        /// <param name="i_Event">The event</param>
        private void buttonTopLikeablePhotos_Click(object i_Sender, EventArgs i_Event)
        {
            MessageBox.Show(k_WaitMessage);
            int width = 0;
            int height = 0;

            foreach (Thread thread in r_Threads)
            {
                thread.Join();
            }

            r_Util.SortPhotosByDescendingOrder(m_MostLikeablePhotos);
            r_Util.GetWidthAndHeight(ref width, ref height, m_MostLikeablePhotos);
            createMostLikeablePictureForm(width, height);
        }

        /// <summary>
        /// Creates a new most likeable pictures form
        /// </summary>
        /// <param name="i_Width">Picture Width</param>
        /// <param name="i_Height">Picture Height</param>
        private void createMostLikeablePictureForm(int i_Width, int i_Height)
        {
            MostLikeablePhotosForm likeablePhotosForm = new MostLikeablePhotosForm(m_MostLikeablePhotos, m_NumberOfPicturesToShow)
            {
                Size = new Size(i_Width, i_Height + ButtonMargin),
                StartPosition = FormStartPosition.CenterScreen
            };
            likeablePhotosForm.ShowDialog();
        }

        /// <summary>
        /// Open new WhoWasBornOnMyBirthdayForm instance.
        /// </summary>
        /// <param name="i_Sender">Object sender</param>
        /// <param name="i_Event">The event</param>
        private void buttonGetCelebsBD_Click(object i_Sender, EventArgs i_Event)
        {
            WhoWasBornOnMyBirthdayForm whoWasBornOnMyBirthdayForm = new WhoWasBornOnMyBirthdayForm(r_LoggedInUser.Birthday);
            whoWasBornOnMyBirthdayForm.ShowDialog();
        }
    }
}
