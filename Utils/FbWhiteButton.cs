//-----------------------------------------------------------------------
// <copyright file="FbWhiteButton.cs" company="A16_Ex02">
// Yafim Vodkov 308973882 Or Brand id 302521034
// </copyright>
//-----------------------------------------------------------------------
using System.Drawing;
using System.Windows.Forms;

namespace Utils
{
    /// <summary>
    /// Facebook UI white button
    /// </summary>
    public sealed class FbWhiteButton : Button
    {
        /// <summary>
        /// Initializes a new instance of the FbWhiteButton class.
        /// </summary>
        public FbWhiteButton()
        {
            setUniqueProperties();
        }

        /// <summary>
        /// Set some unique properties to the button
        /// </summary>
        private void setUniqueProperties()
        {
            FlatStyle = FlatStyle.Flat;
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 177);
            ForeColor = Color.Black;
            Name = "fbWhiteButton";
            UseVisualStyleBackColor = false;

            FlatAppearance.BorderColor = Color.Black;
            FlatAppearance.BorderSize = 1;
        }        
    }
}
