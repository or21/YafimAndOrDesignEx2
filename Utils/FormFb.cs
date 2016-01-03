//-----------------------------------------------------------------------
// <copyright file="FormFb.cs" company="A16_Ex02">
// Yafim Vodkov 308973882 Or Brand id 302521034
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Utils
{
    /// <summary>
    /// FaceBook UI form
    /// </summary>
    public class FormFb : Form
    {
        /// <summary>
        /// Name of the application
        /// </summary>
        private const string k_AppName = "FaceBoo";

        /// <summary>
        /// Label header
        /// </summary>
        private readonly Label r_LabelHeader = new Label();

        /// <summary>
        /// Facebook icon
        /// </summary>
        private readonly PictureBox r_PictureBoxFbIcon = new PictureBox();

        /// <summary>
        /// Facebook blue header
        /// </summary>
        private readonly Label r_LabelFbTextHeader = new Label();

        /// <summary>
        /// Button margin 
        /// </summary>
        private int m_ButtonMargin = 35;

        /// <summary>
        /// Gets or sets button margin property
        /// </summary>
        public int ButtonMargin
        {
            get { return m_ButtonMargin; }
            set { this.m_ButtonMargin = value; }
        }

        /// <summary>
        /// Gets Label header
        /// </summary>
        public Label LabelHeader
        {
            get { return this.r_LabelHeader; }
        }

        /// <summary>
        /// Initializes a new instance of the FormFb class.
        /// </summary>
        public FormFb()
        {
            initializeForm();
        }

        /// <summary>
        /// Initializes the form
        /// </summary>
        private void initializeForm()
        {
            // labelHeader
            r_LabelHeader.Location = new Point(0, 0);
            r_LabelHeader.Name = "labelHeader";
            r_LabelHeader.BackColor = Color.FromArgb(58, 87, 149);

            // pictureBoxFbIcon
            r_PictureBoxFbIcon.Image = Properties.Resources.facebook_box_white;
            r_PictureBoxFbIcon.Location = new Point(6, 5);
            r_PictureBoxFbIcon.Name = "pictureBoxFbIcon";
            r_PictureBoxFbIcon.Size = new Size(21, 21);
            r_PictureBoxFbIcon.TabIndex = 1;
            r_PictureBoxFbIcon.TabStop = false;
            r_PictureBoxFbIcon.BackColor = Color.FromArgb(58, 87, 149);

            // labelFbTextHeader
            r_LabelFbTextHeader.Font = new Font("Tahoma", 10F, FontStyle.Bold, GraphicsUnit.Point, 177);
            r_LabelFbTextHeader.ForeColor = Color.White;
            r_LabelFbTextHeader.Location = new Point(28, 5);
            r_LabelFbTextHeader.BackColor = Color.FromArgb(58, 87, 149);
            r_LabelFbTextHeader.Name = "labelFbTextHeader";
            r_LabelFbTextHeader.Size = new Size(77, 19);
            r_LabelFbTextHeader.TabIndex = 2;
            r_LabelFbTextHeader.Text = k_AppName;

            // FbForm
            BackColor = Color.White;
            Controls.Add(r_PictureBoxFbIcon);
            Controls.Add(r_LabelFbTextHeader);
            Controls.Add(r_LabelHeader);
            Name = "FbForm";
            ResumeLayout(false);
        }

        /// <summary>
        /// resize the header on load
        /// </summary>
        /// <param name="i_Event">The event</param>
        protected override void OnLoad(EventArgs i_Event)
        {
            r_LabelHeader.Size = new Size(Width, 29);
        }
    }
}
