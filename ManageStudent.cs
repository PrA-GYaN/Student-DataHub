﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Student_Management_System
{
    public partial class ManageStudent : Form
    {
        StudentClass student = new StudentClass();
        public ManageStudent()
        {
            InitializeComponent();
        }

        private void ManageStudent_Load(object sender, EventArgs e)
        {
            showTable();
        }
        // To show student list in DataGridVIEW
        public void showTable()
        {
            // Bind the data to DataGridView
            DataGridView_student.DataSource = student.getStudentlist();

            // Ensure the DataGridView control is initialized properly
            if (DataGridView_student != null && DataGridView_student.ThemeStyle != null)
            {
                // Set alternating row style
                DataGridView_student.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
                DataGridView_student.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty; // You can specify a color if needed
                DataGridView_student.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty; // Same for Selection color
                DataGridView_student.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty; // Same for Selection text color

                // Set the general back color for the grid
                DataGridView_student.ThemeStyle.BackColor = System.Drawing.Color.DimGray;

                // Grid color for better visibility
                DataGridView_student.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(231, 229, 255);

                // Header style customization
                DataGridView_student.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(100, 88, 255);
                DataGridView_student.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
                DataGridView_student.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular);
                DataGridView_student.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
                DataGridView_student.ThemeStyle.HeaderStyle.Height = 24;
                DataGridView_student.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                // Row style customization
                DataGridView_student.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
                DataGridView_student.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
                DataGridView_student.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular);
                DataGridView_student.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
                DataGridView_student.ThemeStyle.RowsStyle.Height = 80;
                DataGridView_student.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
                DataGridView_student.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            }

            // Apply image layout for the image column (column index 7 in this case)
            if (DataGridView_student.Columns.Count > 7)
            {
                DataGridViewImageColumn imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7];
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
        }

        // Display student data from student to textbox
        private void DataGridView_student_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_ID.Text = DataGridView_student.CurrentRow.Cells[0].Value.ToString();
            textbox_Fname.Text = DataGridView_student.CurrentRow.Cells[1].Value.ToString();
            textbox_Lname.Text = DataGridView_student.CurrentRow.Cells[2].Value.ToString();

            dateTimePicker1.Value = (DateTime)DataGridView_student.CurrentRow.Cells[3].Value;
            if (DataGridView_student.CurrentRow.Cells[4].Value.ToString() == "Male")
                radioButton_Male.Checked = true;

            textbox_contactnum.Text = DataGridView_student.CurrentRow.Cells[5].Value.ToString();
            textBox_address.Text = DataGridView_student.CurrentRow.Cells[6].Value.ToString();
            byte[] img = (byte[])DataGridView_student.CurrentRow.Cells[7].Value;
            MemoryStream ms = new MemoryStream(img);
            picturebox_student.Image = Image.FromStream(ms);

        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            // Clear Information
            textbox_Fname.Clear();
            textBox_ID.Clear();
            textbox_Lname.Clear();
            textbox_contactnum.Clear();
            textBox_address.Clear();
            textBox_ID.Clear();
            radioButton_Male.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            picturebox_student.Image = null;
        }

        private void button_upload_Click(object sender, EventArgs e)
        {
            // Upload Photo
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
                picturebox_student.Image = Image.FromFile(opf.FileName);
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_student.DataSource = student.searchStudent(textBox_search.Text);
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }
        // Create a function to verify
        bool verify()
        {
            if ((textbox_Fname.Text == "") || (textbox_Lname.Text == "") ||
                (textbox_contactnum.Text == "") || (textBox_address.Text == "") ||
                (picturebox_student.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private void button_update_Click(object sender, EventArgs e)
        {
            // Update Student
            int id = Convert.ToInt32(textBox_ID.Text);
            string fname = textbox_Fname.Text;
            string lname = textbox_Lname.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phone = textbox_contactnum.Text;
            string address = textBox_address.Text;
            string gender = radioButton_Male.Checked ? "Male" : "Female";

            // Now to get photo from picture box
            MemoryStream ms = new MemoryStream();
            picturebox_student.Image.Save(ms, picturebox_student.Image.RawFormat);
            byte[] img = ms.ToArray();

            // Check student age is between 10 to 100
            int year_born = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - year_born) < 10 || (this_year - year_born) > 100)
            {
                MessageBox.Show("Student age must be between 10 and 100", "Invalid Date of Birth", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                try
                {
                    if (student.updateStudent(id, fname, lname, bdate, gender, phone, address, img))
                    {
                        showTable();
                        MessageBox.Show("Student Details Updated Successfully", "Update Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)

                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Update student details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {

        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
