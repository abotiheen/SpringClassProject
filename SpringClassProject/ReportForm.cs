using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

public class ReportForm : Form
{
    private RichTextBox rtbReport;
    private Button btnPrint;
    private Button btnSave;
    private Button btnClose;
    private PrintDocument printDoc;
    private PrintPreviewDialog previewDialog;

    public ReportForm()
    {
        this.Text = "Student Report";
        this.Size = new Size(700, 500);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.BackColor = ColorTranslator.FromHtml("#3A0F3A");

        Panel container = new Panel()
        {
            Size = new Size(640, 420),
            Location = new Point(30, 30),
            BackColor = ColorTranslator.FromHtml("#6A1B6A")
        };

        btnClose = CreateBackButton("⬅", new Point(10, 10));
        btnClose.Click += (sender, e) => this.Close();
        container.Controls.Add(btnClose);

        Label lblTitle = new Label()
        {
            Text = "Student Report",
            Font = new Font("Arial", 18, FontStyle.Bold),
            ForeColor = Color.White,
            Size = new Size(300, 30),
            Location = new Point(170, 10),
            TextAlign = ContentAlignment.MiddleCenter
        };
        container.Controls.Add(lblTitle);

        rtbReport = new RichTextBox()
        {
            Location = new Point(20, 60),
            Size = new Size(600, 300),
            Font = new Font("Arial", 12),
            BackColor = Color.White,
            ForeColor = Color.Black,
            ReadOnly = true
        };
        container.Controls.Add(rtbReport);

        btnPrint = CreateStyledButton("Print Report", new Point(20, 380));
        btnPrint.Click += BtnPrint_Click;
        container.Controls.Add(btnPrint);

        btnSave = CreateStyledButton("Save Report", new Point(160, 380));
        btnSave.Click += BtnSave_Click;
        container.Controls.Add(btnSave);

        this.Controls.Add(container);

        GenerateReport();
        InitializePrinting();
    }

    private Button CreateStyledButton(string text, Point location)
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
    private Button CreateBackButton(string text, Point location)
    {
        return new Button()
        {
            Text = text,
            Size = new Size(120, 35),
            Location = location,
            Font = new Font("Arial", 12, FontStyle.Bold),
            BackColor = ColorTranslator.FromHtml("#E3B34C"),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
    }

    private void GenerateReport()
    {
        var reportLines = StudentManager.Students.Select(s => $"{s.Name}\t{s.Age}\t{s.Class}");
        string header = "Name\tAge\tClass\n" + new string('-', 40) + "\n";
        rtbReport.Text = header + string.Join("\n", reportLines);
    }

    private void InitializePrinting()
    {
        printDoc = new PrintDocument();
        printDoc.PrintPage += PrintDoc_PrintPage;
        previewDialog = new PrintPreviewDialog();
        previewDialog.Document = printDoc;
    }

    private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
    {
        e.Graphics.DrawString(rtbReport.Text, rtbReport.Font, Brushes.Black, e.MarginBounds);
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
        if (previewDialog.ShowDialog() == DialogResult.OK)
        {
            printDoc.Print();
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        SaveFileDialog sfd = new SaveFileDialog
        {
            Filter = "Text Files (*.txt)|*.txt",
            FileName = "StudentReport.txt"
        };
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            System.IO.File.WriteAllText(sfd.FileName, rtbReport.Text);
            MessageBox.Show("Report saved successfully!", "Save Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
