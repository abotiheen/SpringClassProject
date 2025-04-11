using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class ViewStudentsForm : Form
{
    private DataGridView dgvStudents;
    private TextBox txtSearchName;
    private ComboBox cmbSearchClass;
    private Button btnSearch;
    private Button btnReset;
    private Button btnEdit;
    private Button btnDelete;

    public ViewStudentsForm()
    {
        this.Text = "View and Search Students";
        this.Size = new Size(700, 500);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.BackColor = ColorTranslator.FromHtml("#3A0F3A");

        Panel formContainer = new Panel()
        {
            Size = new Size(640, 420),
            Location = new Point(30, 30),
            BackColor = ColorTranslator.FromHtml("#6A1B6A")
        };

        Button btnBack = CreateCloseButton("⬅", new Point(40, 50));
        btnBack.Click += (sender, e) => this.Close();
        this.Controls.Add(btnBack);

        Label lblTitle = new Label()
        {
            Text = "View and Search Students",
            Font = new Font("Arial", 18, FontStyle.Bold),
            ForeColor = Color.White,
            Size = new Size(400, 30),
            Location = new Point(120, 20),
            TextAlign = ContentAlignment.MiddleCenter
        };

        Label lblSearchName = CreateLabel("Search by Name:", new Point(40, 70));
        txtSearchName = new TextBox()
        {
            Size = new Size(220, 30),
            Location = new Point(40, 100),
            Font = new Font("Arial", 14),
            BackColor = Color.White,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle
        };

        Label lblSearchClass = CreateLabel("Filter by Class:", new Point(290, 70));
        cmbSearchClass = new ComboBox()
        {
            Size = new Size(200, 30),
            Location = new Point(290, 100),
            Font = new Font("Arial", 14),
            BackColor = Color.White,
            ForeColor = Color.Black,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cmbSearchClass.Items.Add("All Classes");
        cmbSearchClass.Items.AddRange(new string[] { "Computer Science", "Physics", "Archeology", "History", "Psychology" });
        cmbSearchClass.SelectedIndex = 0;

        btnSearch = CreateButton("🔍 Search", new Point(500, 60));
        btnSearch.Click += BtnSearch_Click;

        btnReset = CreateButton("🔄 Reset", new Point(500, 100));
        btnReset.Click += BtnReset_Click;

        btnEdit = CreateButton("✎ Edit", new Point(500, 140));
        btnEdit.BackColor = ColorTranslator.FromHtml("#E3B34C");
        btnEdit.Click += BtnEdit_Click;

        btnDelete = CreateButton("✖ Delete", new Point(500, 180));
        btnDelete.BackColor = ColorTranslator.FromHtml("#EB344C");
        btnDelete.Click += BtnDelete_Click;

        dgvStudents = new DataGridView()
        {
            Size = new Size(560, 200),
            Location = new Point(40, 220),
            BackgroundColor = Color.White,
            ForeColor = Color.Black,
            Font = new Font("Arial", 12),
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            ReadOnly = true,
            RowHeadersVisible = false,
            AllowUserToAddRows = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            EnableHeadersVisualStyles = false
        };

        dgvStudents.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#E3B34C");
        dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvStudents.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvStudents.ColumnHeadersDefaultCellStyle.BackColor;
        dgvStudents.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor;

        formContainer.Controls.Add(lblTitle);
        formContainer.Controls.Add(lblSearchName);
        formContainer.Controls.Add(txtSearchName);
        formContainer.Controls.Add(lblSearchClass);
        formContainer.Controls.Add(cmbSearchClass);
        formContainer.Controls.Add(btnSearch);
        formContainer.Controls.Add(btnReset);
        formContainer.Controls.Add(btnEdit);
        formContainer.Controls.Add(btnDelete);
        formContainer.Controls.Add(dgvStudents);

        this.Controls.Add(formContainer);

        PopulateDataGridView();

        // Role-based access: if Teacher, hide Edit and Delete
        if (UserSession.Role.Equals("Teacher", StringComparison.OrdinalIgnoreCase))
        {
            btnEdit.Visible = false;
            btnDelete.Visible = false;
        }
    }

    private Label CreateLabel(string text, Point location)
    {
        return new Label()
        {
            Text = text,
            ForeColor = Color.White,
            Font = new Font("Arial", 14),
            Location = location,
            AutoSize = true
        };
    }

    private Button CreateButton(string text, Point location)
    {
        return new Button()
        {
            Text = text,
            Size = new Size(120, 35),
            Location = location,
            Font = new Font("Arial", 12, FontStyle.Bold),
            BackColor = ColorTranslator.FromHtml("#5BB381"),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
    }

    private Button CreateCloseButton(string text, Point location)
    {
        return new Button()
        {
            Text = text,
            Size = new Size(60, 35),
            Location = location,
            Font = new Font("Arial", 12, FontStyle.Bold),
            BackColor = ColorTranslator.FromHtml("#E3B34C"),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
    }

    private void PopulateDataGridView()
    {
        dgvStudents.DataSource = StudentManager.Students.Select(s => new
        {
            Name = s.Name,
            Age = s.Age,
            Class = s.Class
        }).ToList();
    }

    private void BtnSearch_Click(object sender, EventArgs e)
    {
        string searchName = txtSearchName.Text.ToLower();
        string selectedClass = cmbSearchClass.SelectedItem.ToString();

        var filteredStudents = StudentManager.Students
            .Where(s => (string.IsNullOrWhiteSpace(searchName) || s.Name.ToLower().Contains(searchName)) &&
                        (selectedClass == "All Classes" || s.Class == selectedClass))
            .Select(s => new { s.Name, s.Age, s.Class })
            .ToList();

        dgvStudents.DataSource = filteredStudents;

        if (filteredStudents.Count == 0)
        {
            MessageBox.Show("No matching records found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void BtnReset_Click(object sender, EventArgs e)
    {
        txtSearchName.Clear();
        cmbSearchClass.SelectedIndex = 0;
        PopulateDataGridView();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        if (dgvStudents.SelectedRows.Count > 0)
        {
            var row = dgvStudents.SelectedRows[0];
            string name = row.Cells["Name"].Value.ToString();
            Student student = StudentManager.Students.FirstOrDefault(s => s.Name == name);
            if (student != null)
            {
                EditStudentForm editForm = new EditStudentForm(student);
                editForm.ShowDialog();
                PopulateDataGridView();
            }
            else
            {
                MessageBox.Show("Student not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else
        {
            MessageBox.Show("Please select a student to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        if (dgvStudents.SelectedRows.Count > 0)
        {
            var row = dgvStudents.SelectedRows[0];
            string name = row.Cells["Name"].Value.ToString();
            Student student = StudentManager.Students.FirstOrDefault(s => s.Name == name);
            if (student != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete this student?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    StudentManager.Students.Remove(student);
                    PopulateDataGridView();
                }
            }
            else
            {
                MessageBox.Show("Student not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else
        {
            MessageBox.Show("Please select a student to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
