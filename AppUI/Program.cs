//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="A16_Ex02">
// Yafim Vodkov 308973882 Or Brand id 302521034
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.IO;
using System.Windows.Forms;

namespace AppUI
{
    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point for the application
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
