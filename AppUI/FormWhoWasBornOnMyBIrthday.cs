//-----------------------------------------------------------------------
// <copyright file="FormWhoWasBornOnMyBirthday.cs" company="A16_Ex02">
// Yafim Vodkov 308973882 Or Brand id 302521034
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Utils;

namespace AppUI
{
    /// <summary>
    /// Get information about famous people who was born on my birthday date
    /// </summary>
    public partial class FormWhoWasBornOnMyBirthday : FormFb
    {
        /// <summary>
        /// Path to Json file
        /// </summary>
        private readonly string m_PathToJsonFile = @"../../JSONFile/celeb-birthdays.JSON";

        /// <summary>
        /// Instance of Util class
        /// </summary>
        private readonly Utils.Utils r_Util;

        /// <summary>
        /// Birthday date before validation and parsing.
        /// </summary>
        private readonly string m_BirthdayDate;

        /// <summary>
        /// List of people who share the same birthday date.
        /// </summary>
        private List<string> m_ListOfPeopleWhoWasBornOnMyBirthday;

        /// <summary>
        /// Initializes a new instance of the FormWhoWasBornOnMyBirthday class.
        /// </summary>
        public FormWhoWasBornOnMyBirthday()
        {
            InitializeComponent();
            r_Util = Utils.Utils.Instance;
            m_BirthdayDate = FormMainWindow.Birthday;
        }

        /// <summary>
        /// 1. Get the list of people who was born on given date
        /// 2. Fetch birthdays
        /// 3. Initialize list box with all the birthdays
        /// </summary>
        /// <param name="i_Event">The event</param>
        protected override void OnLoad(EventArgs i_Event)
        {
            try
            {
                m_ListOfPeopleWhoWasBornOnMyBirthday = r_Util.GetListOfPeopleFromJSON(m_PathToJsonFile, m_BirthdayDate);
                fetchBirthdays();
                initListBox();
            }
            catch (FileNotFoundException fnfe)
            {
                MessageBox.Show(fnfe.Message);
                this.Dispose();
            }
            catch (WebException wes)
            {
                MessageBox.Show(wes.Message);
                this.Dispose();
            }
            catch (FormatException fe)
            {
                MessageBox.Show(fe.Message);
                this.Dispose();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                this.Dispose();
            }

            base.OnLoad(i_Event);
        }

        /// <summary>
        /// Initialize list box to select first item in the list
        /// </summary>
        private void initListBox()
        {
            listBoxWhoWasBorn.SelectedIndex = 0;
        }

        /// <summary>
        /// Insert birthday list into listBox
        /// </summary>
        private void fetchBirthdays()
        {
            foreach (string name in m_ListOfPeopleWhoWasBornOnMyBirthday)
            {
                listBoxWhoWasBorn.Items.Add(name);
            }
        }

        /// <summary>
        /// Close form
        /// </summary>
        /// <param name="i_Sender">Sender object</param>
        /// <param name="i_Event">Additional event arguments</param>
        private void fbWhiteButtonExit_Click(object i_Sender, EventArgs i_Event)
        {
            this.Close();
        }

        /// <summary>
        /// Update relevant information when item selected
        /// </summary>
        /// <param name="i_Sender">Sender object</param>
        /// <param name="i_Event">the event</param>
        private void listBoxWhoWasBorn_SelectedIndexChanged(object i_Sender, EventArgs i_Event)
        {
            labelName.Text = listBoxWhoWasBorn.Text;

            string celebFullName = listBoxWhoWasBorn.Text;

            // Load image and text asynchronously
            try
            {
                string celebWikiImagePath = r_Util.GetCelebPictureUrl(celebFullName);
                pictureBox.LoadAsync(celebWikiImagePath);
            }
            catch (NullReferenceException nre)
            {
                pictureBox.Image = Utils.Properties.Resources.attachment_unavailable;
            }
            finally
            {
                textBoxInfo.Text = r_Util.GetCelebInfo(celebFullName);
            }
        }
    }
}