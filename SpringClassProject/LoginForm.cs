using System;
using System.Drawing;
using System.Windows.Forms;

public class LoginForm : Form
{
    private TextBox txtUsername;
    private TextBox txtPassword;
    private Button btnLogin;
    private Label lblTitle;
    private Label lblUsername;
    private Label lblPassword;
    private Panel panelContainer;

    public LoginForm()
    {
        this.Text = "Login - Slack Style";
        this.Size = new Size(500, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.BackColor = ColorTranslator.FromHtml("#3A0F3A");

        panelContainer = new Panel()
        {
            Size = new Size(400, 400),
            Location = new Point(50, 90),
            BackColor = ColorTranslator.FromHtml("#6A1B6A"),
            BorderStyle = BorderStyle.None
        };

        lblTitle = new Label()
        {
            Text = "Welcome Back!",
            Font = new Font("Arial", 24, FontStyle.Bold),
            ForeColor = Color.White,
            Size = new Size(320, 50),
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(40, 25)
        };

        lblUsername = new Label()
        {
            Text = "Username",
            ForeColor = Color.White,
            Font = new Font("Arial", 14),
            Location = new Point(40, 90),
            AutoSize = true
        };

        txtUsername = new TextBox()
        {
            Size = new Size(320, 50),
            Location = new Point(40, 120),
            Font = new Font("Arial", 14),
            BackColor = Color.White,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle
        };

        lblPassword = new Label()
        {
            Text = "Password",
            ForeColor = Color.White,
            Font = new Font("Arial", 14),
            Location = new Point(40, 190),
            AutoSize = true
        };

        txtPassword = new TextBox()
        {
            Size = new Size(320, 50),
            Location = new Point(40, 220),
            Font = new Font("Arial", 14),
            BackColor = Color.White,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle,
            UseSystemPasswordChar = true
        };

        btnLogin = new Button()
        {
            Text = "Log In",
            Size = new Size(320, 55),
            Location = new Point(40, 300),
            Font = new Font("Arial", 16, FontStyle.Bold),
            BackColor = ColorTranslator.FromHtml("#5BB381"),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.Click += BtnLogin_Click;

        panelContainer.Controls.Add(lblTitle);
        panelContainer.Controls.Add(lblUsername);
        panelContainer.Controls.Add(txtUsername);
        panelContainer.Controls.Add(lblPassword);
        panelContainer.Controls.Add(txtPassword);
        panelContainer.Controls.Add(btnLogin);

        this.Controls.Add(panelContainer);
    }

    private void BtnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string password = txtPassword.Text;

        // Admin credentials
        if (username == "dhrgam" && password == "1234")
        {
            UserSession.Role = "Admin";
            DashboardForm dashboard = new DashboardForm();
            dashboard.Show();
            this.Hide();
        }
        // Teacher credentials
        else if (username == "zain" && password == "1234")
        {
            UserSession.Role = "Teacher";
            DashboardForm dashboard = new DashboardForm();
            dashboard.Show();
            this.Hide();
        }
        else
        {
            MessageBox.Show("Invalid Username or Password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
