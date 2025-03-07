using System;
using System.Windows.Forms;

class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new LoginForm());
        // Application.Run(new DashboardForm());
        // Application.Run(new AddStudentForm());
       // Application.Run(new ViewStudentsForm());

    }
}
