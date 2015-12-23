//-----------------------------------------------------------------------
// <copyright file="MostLikeablePhotosForm.cs" company="A16_Ex02">
// Yafim Vodkov 308973882 Or Brand id 302521034
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using FacebookWrapper.ObjectModel;
using Utils;

namespace AppUI
{
    /// <summary>
    /// Get the N likeable pictures you have on facebook.
    /// </summary>
    public partial class MostLikeablePhotosForm : FbForm
    {
        /// <summary>
        /// List of the top N pictures
        /// </summary>
        private readonly List<Photo> m_AllPhotos;

        /// <summary>
        /// Instance of Util class
        /// </summary>
        private readonly Utils.Utils r_Util;

        /// <summary>
        /// Current image index
        /// </summary>
        private int m_IndexOfCurrentImage;

        /// <summary>
        /// Number of pictures 
        /// </summary>
        private int m_NumberOfPicturesToShow = 5;

        /// <summary>
        /// The current image to display
        /// </summary>
        private Photo m_CurrentImageDisplayed;

        /// <summary>
        /// List of most likeable photos
        /// </summary>
        private List<Photo> m_TopLikeablePhotos;

        /// <summary>
        /// Initializes a new instance of the MostLikeablePhotosForm class.
        /// </summary>
        public MostLikeablePhotosForm()
        {
            InitializeComponent();
            r_Util = Utils.Utils.Instance;

            pictureBoxCurrentPic.LoadCompleted += pictureBoxCurrentPic_LoadCompleted;

            m_AllPhotos = MainWindow.AllPhotos;
            m_IndexOfCurrentImage = 0;

            StartPosition = FormStartPosition.CenterScreen;

            getTopLikeablePhotos();
            loadImage(m_TopLikeablePhotos[0]);
        }

        /// <summary>
        /// Gets the top likable photos 
        /// </summary>
        private void getTopLikeablePhotos()
        {
            int width = 0;
            int height = 0;

            m_TopLikeablePhotos = new List<Photo>(m_NumberOfPicturesToShow);
            if (m_AllPhotos.Count < m_NumberOfPicturesToShow)
            {
                m_NumberOfPicturesToShow = m_AllPhotos.Count;
            }

            m_TopLikeablePhotos = r_Util.FindMostLikablePhotos(m_NumberOfPicturesToShow, m_AllPhotos);

            r_Util.SortPhotosByDescendingOrder(m_TopLikeablePhotos);
            r_Util.GetWidthAndHeight(ref width, ref height, m_TopLikeablePhotos);
            Size = new Size(width, height + ButtonMargin);
        }

        /// <summary>
        /// Close form
        /// </summary>
        /// <param name="i_Sender">Object sender</param>
        /// <param name="i_Event">The event</param>
        private void buttonExit_Click(object i_Sender, EventArgs i_Event)
        {
            this.Close();
        }

        /// <summary>
        /// Get the most likeable picture
        /// </summary>
        /// <param name="i_Sender">Object sender</param>
        /// <param name="i_Event">The event</param>
        private void buttonTopPicture_Click(object i_Sender, EventArgs i_Event)
        {
            m_IndexOfCurrentImage = 0;
            loadImage(m_TopLikeablePhotos[m_IndexOfCurrentImage]);
        }

        /// <summary>
        /// Next picture
        /// </summary>
        /// <param name="i_Sender">Object sender</param>
        /// <param name="i_Event">The event</param>
        private void buttonNext_Click(object i_Sender, EventArgs i_Event)
        {
            m_IndexOfCurrentImage = r_Util.SetNextImage(m_IndexOfCurrentImage, m_NumberOfPicturesToShow);
            loadImage(m_TopLikeablePhotos[m_IndexOfCurrentImage]);
        }

        /// <summary>
        /// Previous picture
        /// </summary>
        /// <param name="i_Sender">Object sender</param>
        /// <param name="i_Event">The event</param>
        private void buttonBack_Click(object i_Sender, EventArgs i_Event)
        {
            m_IndexOfCurrentImage = r_Util.SetPrevImage(m_IndexOfCurrentImage, m_NumberOfPicturesToShow);
            loadImage(m_TopLikeablePhotos[m_IndexOfCurrentImage]);
        }

        /// <summary>
        /// Set number of likes
        /// </summary>
        /// <param name="i_Photo">Current photo</param>
        private void setNumberOfLikes(Photo i_Photo)
        {
            labelNumberOfLikes.Text = string.Format("{0} Likes", i_Photo.LikedBy.Count);
        }

        /// <summary>
        /// Load image to display
        /// </summary>
        /// <param name="i_ImageToLoad">Image to load</param>
        private void loadImage(Photo i_ImageToLoad)
        {
            m_CurrentImageDisplayed = i_ImageToLoad;
            pictureBoxCurrentPic.LoadAsync(i_ImageToLoad.PictureNormalURL);
        }

        /// <summary>
        /// Set number of likes when load completed
        /// </summary>
        /// <param name="i_Sender">Object sender</param>
        /// <param name="i_Event">The event</param>
        public void pictureBoxCurrentPic_LoadCompleted(object i_Sender, AsyncCompletedEventArgs i_Event)
        {
            setNumberOfLikes(m_CurrentImageDisplayed);
        }
    }
}