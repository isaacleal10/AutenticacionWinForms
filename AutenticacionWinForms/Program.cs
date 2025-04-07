using AutenticacionWinForms.Formularios;
using System;
using System.Windows.Forms;

namespace AutenticacionWinForms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
        }
    }
}
