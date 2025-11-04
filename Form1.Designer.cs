using System.Drawing;
using System.Windows.Forms;

namespace StudentGradeManagement
{
    partial class Form1
    {
        private TextBox txtName;
        private TextBox txtGrade;
        private Button btnAddStudent;
        private Button btnDisplayStudents;
        private Button btnSearchStudent;
        private Button btnCalculateAverage;
        private Button btnFindHighLow;
        private Label lblMessage;
        private DataGridView dataGridView1;
        private Panel pnlControls; 

        // NEW COMPONENTS FOR CUSTOM TITLE BAR
        private Panel pnlTitleBar; 
        private Label lblTitle;
        private Button btnMinimize;
        private Button btnClose;

        private void InitializeComponent()
        {
            // --- 1. CUSTOM TITLE BAR PANEL (GRASS GREEN) ---
            pnlTitleBar = new Panel
            {
                Top = 0,
                Left = 0,
                Height = 30, // Standard title bar height
                Width = 900, // Matches form width
                BackColor = System.Drawing.ColorTranslator.FromHtml("#3F923F") // Grass Green
            };
            
            // --- 1a. TITLE LABEL ---
            lblTitle = new Label
            {
                Text = "Student Grade Management System",
                Top = 7,
                Left = 10,
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            // --- 1b. CLOSE BUTTON ---
            btnClose = new Button
            {
                Text = "X",
                Top = 0,
                Left = 870, 
                Width = 30,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.Red }
            };
            btnClose.Click += (s, e) => Application.Exit(); 

            // --- 1c. MINIMIZE BUTTON ---
            btnMinimize = new Button
            {
                Text = "-",
                Top = 0,
                Left = 840, 
                Width = 30,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0, MouseOverBackColor = Color.Gray }
            };
            btnMinimize.Click += (s, e) => this.WindowState = FormWindowState.Minimized;


            // --- 2. LEFT CONTROLS PANEL (GREEN REMAINS FOR VISUAL STYLE) ---
            pnlControls = new Panel
            {
                Top = 30, // Starts below the title bar
                Left = 0,
                Width = 220,
                Height = 570, // Adjusted height (600 - 30)
                BackColor = System.Drawing.ColorTranslator.FromHtml("#3F923F") // Grass Green
            };

            //Textboxes for input student name and grade 
            txtName = new TextBox
            {
                PlaceholderText = "Enter student name",
                Top = 10,
                Left = 10,
                Width = 200
            };

            txtGrade = new TextBox
            {
                PlaceholderText = "Enter student grade",
                Top = 40,
                Left = 10,
                Width = 200
            };

            // Buttons 
            btnAddStudent = new Button
            {
                Text = "Add/Update Student",
                Top = 70,
                Left = 10,
                Width = 200
            };
            btnAddStudent.Click += btnAddStudent_Click;

            btnDisplayStudents = new Button
            {
                Text = "Display Students",
                Top = 100,
                Left = 10,
                Width = 200
            };
            btnDisplayStudents.Click += btnDisplayStudents_Click;

            btnSearchStudent = new Button
            {
                Text = "Search Student",
                Top = 130,
                Left = 10,
                Width = 200
            };
            btnSearchStudent.Click += btnSearchStudent_Click;

            btnCalculateAverage = new Button
            {
                Text = "Calculate Average Grade",
                Top = 160,
                Left = 10,
                Width = 200
            };
            btnCalculateAverage.Click += btnCalculateAverage_Click;

            btnFindHighLow = new Button
            {
                Text = "Find High/Low Grades",
                Top = 190,
                Left = 10,
                Width = 200
            };
            btnFindHighLow.Click += btnFindHighLow_Click;

            // Label text 
            lblMessage = new Label
            {
                Top = 230,
                Left = 10,
                AutoSize = true,
                ForeColor = Color.White 
            };

            // --- 3. DATAGRID VIEW ---
            dataGridView1 = new DataGridView
            {
                Top = 30, // Starts below the title bar
                Left = 220, // Next to pnlControls
                Width = 680, // Adjusted width (900 - 220)
                Height = 570, // Adjusted height (600 - 30)
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dataGridView1.Columns.Add("No", "No"); 
            dataGridView1.Columns.Add("StudentName", "Student Name");
            dataGridView1.Columns.Add("Grade", "Grade");
            dataGridView1.Columns["No"].FillWeight = 20;


            // --- 4. FORM SETTINGS ---
            Text = "Student Grade Management System";
            ClientSize = new System.Drawing.Size(900, 600); 
            FormBorderStyle = FormBorderStyle.None; // KEY: Removes native Windows border/title bar
            BackColor = System.Drawing.ColorTranslator.FromHtml("#333333"); // Dark Gray Background

            // Add all controls to Panel
            pnlControls.Controls.AddRange(new Control[]
            {
                txtName, txtGrade,
                btnAddStudent, btnDisplayStudents,
                btnSearchStudent, btnCalculateAverage, btnFindHighLow,
                lblMessage
            });

            // Add controls to Title Bar Panel
            pnlTitleBar.Controls.AddRange(new Control[]
            {
                lblTitle, btnMinimize, btnClose
            });


            // Add Panels and DataGridView to Form
            Controls.AddRange(new Control[]
            {
                pnlTitleBar, // Add the custom title bar first
                pnlControls, 
                dataGridView1
            });
        }
    }
}
