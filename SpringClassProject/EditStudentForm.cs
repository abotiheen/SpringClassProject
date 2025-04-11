using System;
using System.Drawing;
using System.Windows.Forms;

public class EditStudentForm : Form
{
    private Student student;
    private TextBox txtName;
    private TextBox txtAge;
    private ComboBox cmbClass;
    private Button btnSave;

    public EditStudentForm(Student student)
    {
        this.student = student;
        this.Text = "Edit Student";
        this.Size = new Size(500, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.BackColor = ColorTranslator.FromHtml("#3A0F3A");

        Panel formContainer = new Panel()
        {
            Size = new Size(420, 460),
            Location = new Point(40, 30),
            BackColor = ColorTranslator.FromHtml("#6A1B6A")
        };

        Label lblTitle = new Label()
        {
            Text = "Edit Student Details",
            Font = new Font("Arial", 20, FontStyle.Bold),
            ForeColor = Color.White,
            Size = new Size(300, 40),
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(60, 20)
        };

        Label lblName = CreateLabel("Name:", new Point(40, 80));
        txtName = CreateTextBox(new Point(40, 110));
        txtName.Text = student.Name;

        Label lblAge = CreateLabel("Age:", new Point(40, 160));
        txtAge = CreateTextBox(new Point(40, 190));
        txtAge.Text = student.Age.ToString();

        Label lblClass = CreateLabel("Class:", new Point(40, 240));
        cmbClass = new ComboBox()
        {
            Size = new Size(320, 30),
            Location = new Point(40, 270),
            Font = new Font("Arial", 14),
            BackColor = Color.White,
            ForeColor = Color.Black,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cmbClass.Items.AddRange(new string[] { "Computer Science", "Physics", "Archeology", "History", "Psychology" });
        cmbClass.SelectedIndex = GetClassIndex(student.Class);

        btnSave = new Button()
        {
            Text = "Save Changes",
            Size = new Size(320, 50),
            Location = new Point(40, 330),
            Font = new Font("Arial", 14, FontStyle.Bold),
            BackColor = ColorTranslator.FromHtml("#5BB381"),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnSave.Click += BtnSave_Click;

        formContainer.Controls.Add(lblTitle);
        formContainer.Controls.Add(lblName);
        formContainer.Controls.Add(txtName);
        formContainer.Controls.Add(lblAge);
        formContainer.Controls.Add(txtAge);
        formContainer.Controls.Add(lblClass);
        formContainer.Controls.Add(cmbClass);
        formContainer.Controls.Add(btnSave);

        this.Controls.Add(formContainer);
    }

    private int GetClassIndex(string className)
    {
        string[] classes = { "Computer Science", "Physics", "Archeology", "History", "Psychology" };
        for (int i = 0; i < classes.Length; i++)
        {
            if (classes[i].Equals(className, StringComparison.OrdinalIgnoreCase))
                return i;
        }
        return 0;
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

    private TextBox CreateTextBox(Point location)
    {
        return new TextBox()
        {
            Size = new Size(320, 30),
            Location = location,
            Font = new Font("Arial", 14),
            BackColor = Color.White,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle
        };
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            MessageBox.Show("Name cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (!int.TryParse(txtAge.Text, out int age) || age < 18 || age > 32)
        {
            MessageBox.Show("Age must be a number between 18 and 32.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (cmbClass.SelectedIndex == -1)
        {
            MessageBox.Show("Please select a class.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        student.Name = txtName.Text;
        student.Age = age;
        student.Class = cmbClass.SelectedItem.ToString();
        MessageBox.Show("Student record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        this.Close();
    }
}
