using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;

namespace Utils
{
    /// <summary>
    /// Wrapper for Post object
    /// </summary>
    public class MyPost : Post
    {
        private Post m_OriginalPost ;
        private string m_DisplayMessage;

        public MyPost(string i_StatusWithoutPostObj)
        {
            DisplayMessage = i_StatusWithoutPostObj;
        }

        public MyPost(Post i_Post)
        {
            m_OriginalPost = i_Post;
            DisplayMessage = i_Post.Message;
        }

        public Post OriginalPost
        {
            get { return m_OriginalPost; } 
        }

        public string DisplayMessage
        {
            get { return m_DisplayMessage; }
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
