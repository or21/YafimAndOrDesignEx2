//-----------------------------------------------------------------------
// <copyright file="FormLogin.Designer.cs" company="A16_Ex02">
// Yafim Vodkov 308973882 Or Brand id 302521034
// </copyright>
//-----------------------------------------------------------------------
namespace AppUI
{
    /// <summary>
    /// Login form class
    /// </summary>
    public partial class FormLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelHeadLine = new System.Windows.Forms.Label();
            this.fbBlueButtonLogin = new Utils.ButtonFbBlue();
            this.SuspendLayout();
            // 
            // labelHeadLine
            // 
            this.labelHeadLine.AutoSize = true;
            this.labelHeadLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelHeadLine.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labelHeadLine.Location = new System.Drawing.Point(14, 56);
            this.labelHeadLine.Name = "labelHeadLine";
            this.labelHeadLine.Size = new System.Drawing.Size(406, 46);
            this.labelHeadLine.TabIndex = 0;
            this.labelHeadLine.Text = "Welcome to FaceBoo";
            // 
            // fbBlueButtonLogin
            // 
            this.fbBlueButtonLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(122)))), ((int)(((byte)(172)))));
            this.fbBlueButtonLogin.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.fbBlueButtonLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fbBlueButtonLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.fbBlueButtonLogin.ForeColor = System.Drawing.Color.White;
            this.fbBlueButtonLogin.Location = new System.Drawing.Point(143, 133);
            this.fbBlueButtonLogin.Name = "fbBlueButtonLogin";
            this.fbBlueButtonLogin.Size = new System.Drawing.Size(132, 49);
            this.fbBlueButtonLogin.TabIndex = 1;
            this.fbBlueButtonLogin.Text = "Login";
            this.fbBlueButtonLogin.UseVisualStyleBackColor = false;
            this.fbBlueButtonLogin.Click += new System.EventHandler(this.fbBlueButtonLogin_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 224);
            this.Controls.Add(this.fbBlueButtonLogin);
            this.Controls.Add(this.labelHeadLine);
            this.Name = "MainWindow";
            this.Text = "Welcome";
            this.Controls.SetChildIndex(this.labelHeadLine, 0);
            this.Controls.SetChildIndex(this.fbBlueButtonLogin, 0);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        /// <summary>
        /// Label head line
        /// </summary>
        private System.Windows.Forms.Label labelHeadLine;

        /// <summary>
        /// Facebook button
        /// </summary>
        private Utils.ButtonFbBlue fbBlueButtonLogin;
    }
}