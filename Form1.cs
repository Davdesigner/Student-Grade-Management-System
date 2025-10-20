using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGMSWinForms
{
    public partial class Form1 : Form
    {
        private Dictionary<string, int> students = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        public Form1()
        {
            InitializeComponent();
            Seed();
            RefreshList();
        }

        private void Seed()
        {
            students["Alice"] = 85;
            students["Bob"] = 72;
        }

        private void RefreshList()
        {
            lstStudents.Items.Clear();
            foreach (var kv in students.OrderBy(k => k.Key))
            {
                lstStudents.Items.Add($"{kv.Key} - {kv.Value}");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Enter name"); return; }
            if (students.ContainsKey(name)) { MessageBox.Show("Student exists. Use Update."); return; }
            if (!int.TryParse(txtGrade.Text.Trim(), out int g) || g < 0 || g > 100) { MessageBox.Show("Invalid grade 0-100"); return; }
            students[name] = g;
            RefreshList();
            MessageBox.Show($"Added {name} ({g})");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Enter name"); return; }
            if (!students.ContainsKey(name)) { MessageBox.Show("Student does not exist."); return; }
            if (!int.TryParse(txtGrade.Text.Trim(), out int g) || g < 0 || g > 100) { MessageBox.Show("Invalid grade 0-100"); return; }
            students[name] = g;
            RefreshList();
            MessageBox.Show($"Updated {name} ({g})");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Enter name"); return; }
            if (students.TryGetValue(name, out int g)) MessageBox.Show($"{name}: {g}");
            else MessageBox.Show($"{name} not found.");
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            if (students.Count == 0) { MessageBox.Show("No students"); return; }
            double avg = students.Values.Average();
            int hi = students.Values.Max();
            int lo = students.Values.Min();
            var top = students.Where(kv => kv.Value == hi).Select(kv => kv.Key);
            var bottom = students.Where(kv => kv.Value == lo).Select(kv => kv.Key);
            MessageBox.Show($"Average: {avg:F2}\nHighest: {hi} - {string.Join(", ", top)}\nLowest: {lo} - {string.Join(", ", bottom)}");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Enter name"); return; }
            if (!students.ContainsKey(name)) { MessageBox.Show("Student does not exist."); return; }
            students.Remove(name);
            RefreshList();
            MessageBox.Show($"Deleted {name}");
        }

        private void lstStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstStudents.SelectedItem == null) return;
            var text = lstStudents.SelectedItem.ToString();
            if (string.IsNullOrEmpty(text)) return;
            var parts = text.Split('-');
            if (parts.Length >= 2)
            {
                txtName.Text = parts[0].Trim();
                txtGrade.Text = parts[1].Trim();
            }
        }
    }
}
