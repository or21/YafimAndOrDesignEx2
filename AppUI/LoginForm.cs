//-----------------------------------------------------------------------
// <copyright file="LoginForm.cs" company="A16_Ex02">
// Yafim Vodkov 308973882 Or Brand id 302521034
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Windows.Forms;
using FacebookWrapper;
using Utils;

namespace AppUI
{
    /// <summary>
    /// Login form class.
    /// </summary>
    public partial class LoginForm : FbForm
    {
        /// <summary>
        /// Initializes a new instance of the LoginForm class.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Send http request to facebook and get the information.
        /// </summary>
        /// <param name="i_Sender">Object sender</param>
        /// <param name="i_Event">The event</param>
        private void fbBlueButtonLogin_Click(object i_Sender, EventArgs i_Event)
        {
            LoginResult result = FacebookService.Login(
                "904603836301816", 
                "user_friends", 
                "email", 
                "user_likes", 
                "publish_actions", 
                "user_posts",
                "public_profile", 
                "user_events", 
                "user_about_me", 
                "user_birthday", 
                "user_hometown", 
                "user_photos");

            if (!string.IsNullOrEmpty(result.AccessToken))
            {
                try
                {
                    Form appUi = new MainWindow(result);
                    this.Hide();
                    appUi.ShowDialog();
                }
                finally
                {
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            }
        }
    }
}
