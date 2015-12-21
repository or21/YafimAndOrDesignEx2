//-----------------------------------------------------------------------
// <copyright file="MostLikeablePhotosForm.cs" company="A16_Ex02">
// Yafim Vodkov 308973882 Or Brand id 302521034
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        /// Number of pictures 
        /// </summary>
        private readonly int m_NumberOfPicturesToShow;

        /// <summary>
        /// List of the top N pictures
        /// </summary>
        private readonly List<Photo> m_TopLikeablePhotos;

        /// <summary>
        /// Instance of Util class
        /// </summary>
        private readonly Utils.Utils r_Util;

        /// <summary>
        /// Current image index
        /// </summary>
        private int m_IndexOfCurrentImage;

        /// <summary>
        /// The current image to display
        /// </summary>
        private Photo m_CurrentImageDisplayed;

        /// <summary>
        /// Initializes a new instance of the MostLikeablePhotosForm class.
        /// </summary>
        /// <param name="i_TopLikeablePhotos">Top likeable pictures</param>
        /// <param name="i_NumberOfPicturesToShow">Number of pictures to show</param>
        public MostLikeablePhotosForm(List<Photo> i_TopLikeablePhotos, int i_NumberOfPicturesToShow)
        {
            InitializeComponent();

            pictureBoxCurrentPic.LoadCompleted += pictureBoxCurrentPic_LoadCompleted;

            m_TopLikeablePhotos = i_TopLikeablePhotos;
            m_IndexOfCurrentImage = 0;

            m_NumberOfPicturesToShow = i_NumberOfPicturesToShow;

            r_Util = Utils.Utils.Instance;
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