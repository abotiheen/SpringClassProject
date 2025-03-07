using System;
using System.Drawing;
using System.Windows.Forms;

public class DashboardForm : Form
{
    private MenuStrip menuStrip;
    private ToolStrip toolStrip;
    private Panel contentPanel;
    private GroupBox quickActionsGroup;
    private Button btnAddStudent;
    private Button btnViewStudents;
    private Button btnSearchStudents;

    public DashboardForm()
    {
        this.Text = "Student Dashboard";
        this.Size = new Size(700, 550);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.BackColor = ColorTranslator.FromHtml("#3A0F3A");

        menuStrip = new MenuStrip()
        {
            BackColor = ColorTranslator.FromHtml("#4A154B"),
            Font = new Font("Arial", 14, FontStyle.Bold),
            ForeColor = Color.White
        };

        ToolStripMenuItem fileMenu = new ToolStripMenuItem("File") { ForeColor = Color.White };
        ToolStripMenuItem helpMenu = new ToolStripMenuItem("Help") { ForeColor = Color.White };

        menuStrip.Items.Add(fileMenu);
        menuStrip.Items.Add(helpMenu);
        this.MainMenuStrip = menuStrip;
        this.Controls.Add(menuStrip);

        toolStrip = new ToolStrip()
        {
            Dock = DockStyle.Top,
            BackColor = ColorTranslator.FromHtml("#4A154B"),
            Height = 40,
            GripStyle = ToolStripGripStyle.Hidden
        };

        ToolStripButton toolAddStudent = CreateSmallToolStripButton("➕", "Add Student");
        ToolStripButton toolViewStudents = CreateSmallToolStripButton("📄", "View Students");
        ToolStripButton toolSearchStudents = CreateSmallToolStripButton("🔍", "Search Students");

        toolStrip.Items.Add(toolAddStudent);
        toolStrip.Items.Add(toolViewStudents);
        toolStrip.Items.Add(toolSearchStudents);
        this.Controls.Add(toolStrip);

        contentPanel = new Panel()
        {
            Size = new Size(640, 270),
            Location = new Point(30, 90),
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.White
        };
        this.Controls.Add(contentPanel);

        quickActionsGroup = new GroupBox()
        {
            Text = "Quick Actions",
            Size = new Size(640, 120),
            Location = new Point(30, 380),
            BackColor = ColorTranslator.FromHtml("#6A1B6A"),
            Font = new Font("Arial", 16, FontStyle.Bold),
            ForeColor = Color.White
        };

        btnAddStudent = CreateActionButton("Add Student", new Point(40, 50));
        btnViewStudents = CreateActionButton("View Students", new Point(240, 50));
        btnSearchStudents = CreateActionButton("Search Students", new Point(440, 50));

        btnAddStudent.Click += (sender, e) =>
        {
            AddStudentForm addStudentForm = new AddStudentForm();
            addStudentForm.ShowDialog();
        };

        btnViewStudents.Click += (sender, e) =>
        {
            ViewStudentsForm viewStudentsForm = new ViewStudentsForm();
            viewStudentsForm.ShowDialog();
        };

        btnSearchStudents.Click += (sender, e) =>
        {
            ViewStudentsForm viewStudentsForm = new ViewStudentsForm();
            viewStudentsForm.ShowDialog();
        };

        toolAddStudent.Click += (sender, e) =>
        {
            AddStudentForm addStudentForm = new AddStudentForm();
            addStudentForm.ShowDialog();
        };

        toolViewStudents.Click += (sender, e) =>
        {
            ViewStudentsForm viewStudentsForm = new ViewStudentsForm();
            viewStudentsForm.ShowDialog();
        };

        toolSearchStudents.Click += (sender, e) =>
        {
            ViewStudentsForm viewStudentsForm = new ViewStudentsForm();
            viewStudentsForm.ShowDialog();
        };


        quickActionsGroup.Controls.Add(btnAddStudent);
        quickActionsGroup.Controls.Add(btnViewStudents);
        quickActionsGroup.Controls.Add(btnSearchStudents);
        this.Controls.Add(quickActionsGroup);
    }

    private ToolStripButton CreateSmallToolStripButton(string icon, string tooltip)
    {
        return new ToolStripButton()
        {
            Text = icon,
            ToolTipText = tooltip,
            Font = new Font("Arial", 12, FontStyle.Bold),
            ForeColor = Color.White,
            BackColor = ColorTranslator.FromHtml("#E3B34C"),
            DisplayStyle = ToolStripItemDisplayStyle.Text,
            Padding = new Padding(5),
            Margin = new Padding(3),
            Width = 40
        };
    }

    private Button CreateActionButton(string text, Point location)
    {
        return new Button()
        {
            Text = text,
            Size = new Size(180, 50),
            Location = location,
            Font = new Font("Arial", 14, FontStyle.Bold),
            BackColor = ColorTranslator.FromHtml("#5BB381"),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
    }
}
