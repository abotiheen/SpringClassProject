using System;
using System.Drawing;
using System.Windows.Forms;

public class AddStudentForm : Form
{
    private TextBox txtName;
    private TextBox txtAge;
    private ComboBox cmbClass;
    private DateTimePicker dtpEnrollmentDate;
    private Button btnSave;

    public AddStudentForm()
    {
        this.Text = "Add Student";
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
            Text = "Add New Student",
            Font = new Font("Arial", 20, FontStyle.Bold),
            ForeColor = Color.White,
            Size = new Size(300, 40),
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(60, 20)
        };

        Label lblName = CreateLabel("Name:", new Point(40, 80));
        txtName = CreateTextBox(new Point(40, 110));

        Label lblAge = CreateLabel("Age:", new Point(40, 160));
        txtAge = CreateTextBox(new Point(40, 190));

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

        Label lblDate = CreateLabel("Enrollment Date:", new Point(40, 320));
        dtpEnrollmentDate = new DateTimePicker()
        {
            Size = new Size(320, 30),
            Location = new Point(40, 350),
            Font = new Font("Arial", 14),
            Format = DateTimePickerFormat.Short
        };

        btnSave = new Button()
        {
            Text = "Save Student",
            Size = new Size(320, 50),
            Location = new Point(40, 400),
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
        formContainer.Controls.Add(lblDate);
        formContainer.Controls.Add(dtpEnrollmentDate);
        formContainer.Controls.Add(btnSave);

        this.Controls.Add(formContainer);
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

        Student newStudent = new Student(txtName.Text, age, cmbClass.SelectedItem.ToString());
        StudentManager.AddStudent(newStudent);

        MessageBox.Show("Student record saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        this.Close();     }
    }
