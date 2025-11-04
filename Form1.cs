using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices; // Needed for window dragging

namespace StudentGradeManagement
{
    public partial class Form1 : Form
    {
        // For custom window dragging 
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        // Dictionary to store student names and grades
        private Dictionary<string, int> students = new Dictionary<string, int>();

        public Form1()
        {
            InitializeComponent();
            
            // NEW: Add MouseDown event handler to the custom title bar for dragging
            pnlTitleBar.MouseDown += pnlTitleBar_MouseDown;
            lblTitle.MouseDown += pnlTitleBar_MouseDown;
        }

        // NEW: Method to allow dragging the borderless window
        private void pnlTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        
        // Add or update student
        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Student name must be provided"); 
                    return;
                }
// ... (rest of method remains the same for brevity)
                if (!int.TryParse(txtGrade.Text.Trim(), out int grade))
                {
                    MessageBox.Show("Invalid grade. Please enter a number between 0 and 100.");
                    return;
                }

                if (grade < 0 || grade > 100)
                {
                    MessageBox.Show("Grade must be between 0 and 100.");
                    return;
                }

                if (students.ContainsKey(name))
                {
                    students[name] = grade;
                    lblMessage.Text = $"Updated grade for '{name}' to {grade}.";
                }
                else
                {
                    students.Add(name, grade);
                    lblMessage.Text = $"Added student '{name}' with grade {grade}.";
                }

                txtName.Clear();
                txtGrade.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding student: {ex.Message}");
            }
        }

        // Display all students in DataGridView
        private void btnDisplayStudents_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                if (students.Count == 0)
                {
                    lblMessage.Text = "No students to show"; 
                    return;
                }

                int i = 1; // Start numbering at 1
                foreach (var student in students)
                {
                    // Added the index 'i' for the 'No' column
                    dataGridView1.Rows.Add(i++, student.Key, student.Value);
                }

                lblMessage.Text = $"Displayed {students.Count} students.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying students: {ex.Message}");
            }
        }

        // Search for a student
        private void btnSearchStudent_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                if (students.TryGetValue(name, out int grade))
                {
                    lblMessage.Text = $"Student '{name}' has a grade of {grade}.";
                }
                else
                {
                    lblMessage.Text = "No matching student found"; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching student: {ex.Message}");
            }
        }

        // Calculate average grade
        private void btnCalculateAverage_Click(object sender, EventArgs e)
        {
            try
            {
                if (students.Count == 0)
                {
                    lblMessage.Text = "No students available to compute average for"; 
                    return;
                }

                double average = students.Values.Average();
                lblMessage.Text = $"Average Grade: {average:F2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating average: {ex.Message}");
            }
        }

        // Find highest and lowest grades
        private void btnFindHighLow_Click(object sender, EventArgs e)
        {
            try
            {
                if (students.Count == 0)
                {
                    lblMessage.Text = "No student data available for analysis"; 
                    return;
                }

                int highest = students.Values.Max();
                int lowest = students.Values.Min();

                var highStudents = string.Join(", ", students.Where(s => s.Value == highest).Select(s => s.Key));
                var lowStudents = string.Join(", ", students.Where(s => s.Value == lowest).Select(s => s.Key));

                lblMessage.Text = $"Highest ({highest}): {highStudents} | Lowest ({lowest}): {lowStudents}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finding high/low grades: {ex.Message}");
            }
        }
    }
}
