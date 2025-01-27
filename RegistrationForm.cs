using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System
{
    public partial class RegistrationForm : Form
    {
        StudentClass student = new StudentClass();

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void button_upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog
            {
                Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif"
            };

            if (opf.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    picturebox_student.Image = Image.FromFile(opf.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_add_Click_1(object sender, EventArgs e)
        {
            string fname = textbox_Fname.Text;
            string lname = textbox_Lname.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phone = textbox_contactnum.Text;
            string address = textBox_address.Text;
            string gender = radioButton_Male.Checked ? "Male" : "Female";

            byte[] img = null;
            if (picturebox_student.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    picturebox_student.Image.Save(ms, picturebox_student.Image.RawFormat);
                    img = ms.ToArray();
                }
            }

            int yearBorn = dateTimePicker1.Value.Year;
            int currentYear = DateTime.Now.Year;
            int age = currentYear - yearBorn;

            if (age < 10 || age > 100)
            {
                MessageBox.Show("Student age must be between 10 and 100.", "Invalid Date of Birth", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                try
                {
                    if (student.insertStudent(fname, lname, bdate, gender, phone, address, img))
                    {
                        showTable();
                        MessageBox.Show("New student added successfully.", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all required fields.", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool verify()
        {
            return !string.IsNullOrWhiteSpace(textbox_Fname.Text) &&
                   !string.IsNullOrWhiteSpace(textbox_Lname.Text) &&
                   !string.IsNullOrWhiteSpace(textbox_contactnum.Text) &&
                   !string.IsNullOrWhiteSpace(textBox_address.Text) &&
                   picturebox_student.Image != null;
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            showTable();
        }

        // To show student list in DataGridView
        private void showTable()
        {
            DataGridView_student.DataSource = student.getStudentlist();

            // Ensure that the DataGridView is properly initialized
            if (DataGridView_student != null && DataGridView_student.ThemeStyle != null)
            {
                // Modify the ThemeStyle safely only after initialization
                DataGridView_student.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
                DataGridView_student.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
                DataGridView_student.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
                DataGridView_student.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
                DataGridView_student.ThemeStyle.BackColor = System.Drawing.Color.DimGray;
                DataGridView_student.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
                DataGridView_student.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
                DataGridView_student.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
                DataGridView_student.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                DataGridView_student.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
                DataGridView_student.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                DataGridView_student.ThemeStyle.HeaderStyle.Height = 24;
                DataGridView_student.ThemeStyle.ReadOnly = false;
                DataGridView_student.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
                DataGridView_student.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
                DataGridView_student.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                DataGridView_student.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
                DataGridView_student.ThemeStyle.RowsStyle.Height = 80;
                DataGridView_student.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
                DataGridView_student.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));

                // Set up the image column layout
                if (DataGridView_student.Columns.Count > 7)
                {
                    DataGridViewImageColumn imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7];
                    imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                }
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textbox_Fname.Clear();
            textbox_Lname.Clear();
            textbox_contactnum.Clear();
            textBox_address.Clear();
            radioButton_Male.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            picturebox_student.Image = null;
        }
    }
}