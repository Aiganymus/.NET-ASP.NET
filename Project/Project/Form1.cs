using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;


namespace Project
{
    public partial class Form1 : Form
    {
        private string path = @"C:\Users\Admin\source\repos\Project\data.csv";

        public Form1()
        {
            InitializeComponent();
            Shown += Form1_Shown;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');

                        dataGridView1.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5]);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = tbName.Text;
            string gender = rb1.Checked ? "Female" : "Male";
            string date = dtpDate.Text;
            string surname = tbSurname.Text;
            string city = cbCity.Text;
            string email = tbEmail.Text;

            bool hasSameValue = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && ((row.Cells[0].Value.ToString() == name && row.Cells[1].Value.ToString() == surname && row.Cells[5].Value.ToString() == gender) || row.Cells[4].Value.ToString() == email))
                {                    
                    hasSameValue = true;
                    break;
                }
            }

            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            
            if (isEmail)
            {
                if (!hasSameValue)
                {
                    if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(surname) && !string.IsNullOrWhiteSpace(city))
                    {
                        dataGridView1.Rows.Add(name, surname, date, city, email, gender);

                        string student = String.Format("{0};{1};{2};{3};{4};{5}", name, surname, date, city, email, gender);
                        StringBuilder csv = new StringBuilder();
                        csv.AppendLine(student);
                        File.AppendAllText(path, csv.ToString());
                        
                    }
                    else MessageBox.Show("Please, fill all the fields!!!");
                }
                else MessageBox.Show("This student already exists in data base!!!");
            }
            else MessageBox.Show("Please, write valid email!!!");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == dataGridView1.NewRowIndex || e.RowIndex < 0)
                return;

            int index = dataGridView1.SelectedCells[0].RowIndex;
            dataGridView1.Rows.RemoveAt(index);

            DataGridViewRow selectedRow = dataGridView1.Rows[index];

            string email = Convert.ToString(selectedRow.Cells["gEmail"].Value);

            var lines = new List<string>();
            var file = new System.IO.StreamReader(path);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }
            file.Close();

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains(email))
                {
                    lines.RemoveAt(i);
                    break;
                }
            }

            File.WriteAllLines(path, lines);
        }
    }
}
