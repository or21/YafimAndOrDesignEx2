//-----------------------------------------------------------------------
// <copyright file="MyPost.cs" company="A16_Ex02">
// Yafim Vodkov 308973882 Or Brand id 302521034
// </copyright>
//-----------------------------------------------------------------------

using System;
using FacebookWrapper.ObjectModel;

namespace Utils
{
    /// <summary>
    /// Wrapper for Post object
    /// </summary>
    public class MyPost : Post
    {
        /// <summary>
        /// Original facebook post
        /// </summary>
        private readonly Post m_OriginalPost;

        /// <summary>
        /// Display message
        /// </summary>
        private string m_DisplayMessage;

        /// <summary>
        /// Initializes a new instance of the MyPost class.
        /// </summary>
        /// <param name="i_StatusWithoutPostObj">Status without post</param>
        public MyPost(string i_StatusWithoutPostObj)
        {
            DisplayMessage = i_StatusWithoutPostObj;
        }

        /// <summary>
        /// Initializes a new instance of the MyPost class.
        /// </summary>
        /// <param name="i_Post">Post object</param>
        public MyPost(Post i_Post)
        {
            m_OriginalPost = i_Post;
            DisplayMessage = i_Post.Message;
        }

        /// <summary>
        /// Gets Original facebook post
        /// </summary>
        public Post OriginalPost
        {
            get { return m_OriginalPost; } 
        }

        /// <summary>
        /// Gets or sets the message
        /// </summary>
        public string DisplayMessage
        {
            get
            {
                return m_DisplayMessage;
            }

            set
            {
                if (m_OriginalPost == null)
                {
                    m_DisplayMessage = DateTime.Now + ": " + value;
                }
                else if (value != null)
                {
                    m_DisplayMessage = m_OriginalPost.UpdateTime + ": " + value;
                }
                else if (m_OriginalPost.Caption != null)
                {
                    m_DisplayMessage = m_OriginalPost.UpdateTime + ": " + m_OriginalPost.Caption;
                }
                else
                {
                    m_DisplayMessage = string.Format(m_OriginalPost.UpdateTime + ": " + "[{0}]", m_OriginalPost.Type);
                }
            }
        }
    }
}
