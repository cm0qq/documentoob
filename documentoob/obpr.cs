using iTextSharp.text.pdf;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Xceed.Words.NET;
using static documentoob.Form1;

namespace documentoob
{
    public partial class obpr : Form
    {
        public obpr()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            column = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            i = e.RowIndex;
            selectedRowIndex = e.RowIndex;
        }

        private void obpr_Load(object sender, EventArgs e)
        {
            string connectionString = "Host=localhost;Username=postgres;Password=1111;Database=documentoob";

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT id, название, срок_обучения, квалификация, стоимость_обучения FROM Образовательная_программа";

                    using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(selectQuery, connection))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            label1.Text = Login.UserLogin;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pname = dataGridView1[1, i].Value.ToString();
            duration = dataGridView1[2, i].Value.ToString();
            qualification = dataGridView1[3, i].Value.ToString();
            tuition_cost = dataGridView1[4, i].Value.ToString();

            try
            {
                string filePath = "C:\\Users\\Дмитрий\\Documents\\Договор_новый.txt";

                if (File.Exists(filePath))
                {
                    string fileContent = File.ReadAllText(filePath);
                    fileContent = fileContent.Replace("#pname#", pname);
                    File.WriteAllText(filePath, fileContent);
                }
                else
                {
                    MessageBox.Show("Файл не найден.", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка");
            }


            try
            {
                string templateFilePath = "C:\\Users\\Дмитрий\\Documents\\Договор_новый.docx";

                if (File.Exists(templateFilePath))
                {
                    using (DocX doc = DocX.Load(templateFilePath))
                    {
                        doc.ReplaceText("#pname#", pname);
                        doc.Save();
                    }
                }
                else
                {
                    MessageBox.Show("Файл-шаблон не найден.", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка");
            }

            try
            {
                string templateFilePath = "C:\\Users\\Дмитрий\\Documents\\Квитанция_новый.docx";

                if (File.Exists(templateFilePath))
                {
                    using (DocX doc = DocX.Load(templateFilePath))
                    {
                        doc.ReplaceText("#tuition_cost#", tuition_cost);
                        doc.Save();
                    }
                }
                else
                {
                    MessageBox.Show("Файл-шаблон не найден.", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка");
            }

            gend gend = new gend();
            gend.Show();
            this.Hide();
        }

        private int selectedRowIndex = -1;
        public int i;
        public string pname;
        public string duration;
        public string qualification;
        public string tuition_cost;
        public string column;

    }
}
