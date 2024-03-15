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
    public partial class glav : Form
    {
        public glav()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            surname = dataGridView1[1, i].Value.ToString();
            name = dataGridView1[2, i].Value.ToString();
            patronymic = dataGridView1[3, i].Value.ToString();
            birthdate = dataGridView1[4, i].Value.ToString();
            passportseries = dataGridView1[5, i].Value.ToString();
            passportnumber = dataGridView1[6, i].Value.ToString();
            address = dataGridView1[7, i].Value.ToString();
            emale = dataGridView1[8, i].Value.ToString();
            phonenumber = dataGridView1[9, i].Value.ToString();
            position = dataGridView1[10, i].Value.ToString();
            workplace = dataGridView1[11, i].Value.ToString();
            {
                try
                {
                    string templateFilePath = "C:\\Users\\Дмитрий\\Documents\\Договор.docx";

                    if (File.Exists(templateFilePath))
                    {
                        string newFilePath = "C:\\Users\\Дмитрий\\Documents\\Договор_новый.docx";

                        File.Copy(templateFilePath, newFilePath, true);

                        using (DocX doc = DocX.Load(newFilePath))
                        {
                            doc.ReplaceText("#workplace#", workplace);
                            doc.ReplaceText("#surname#", surname);
                            doc.ReplaceText("#name#", name);
                            doc.ReplaceText("#patronymic#", patronymic);
                            doc.ReplaceText("#birthdate#", birthdate);
                            doc.ReplaceText("#address#", address);
                            doc.ReplaceText("#passportseries#", passportseries);
                            doc.ReplaceText("#passportnumber#", passportnumber);
                            doc.ReplaceText("#phonenumber#", phonenumber);
                            doc.Save();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка");
                }
            }

            try
            {
                string templateFilePath = "C:\\Users\\Дмитрий\\Documents\\Договор.txt";

                if (File.Exists(templateFilePath))
                {
                    string newTxtFilePath = "C:\\Users\\Дмитрий\\Documents\\Договор_новый.txt";
                    string templateContent = File.ReadAllText(templateFilePath);

                    templateContent = templateContent.Replace("#workplace#", workplace);
                    templateContent = templateContent.Replace("#surname#", surname);
                    templateContent = templateContent.Replace("name#", name);
                    templateContent = templateContent.Replace("#patronymic#", patronymic);
                    templateContent = templateContent.Replace("#birthdate#", birthdate);
                    templateContent = templateContent.Replace("#address#", address);
                    templateContent = templateContent.Replace("#passportseries#", passportseries);
                    templateContent = templateContent.Replace("#passportnumber#", passportnumber);
                    templateContent = templateContent.Replace("#phonenumber#", phonenumber);

                    File.WriteAllText(newTxtFilePath, templateContent);
                }
                else
                {
                    MessageBox.Show("Шаблон не найден.", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка");
            }

            try
            {
                string templateFilePath = "C:\\Users\\Дмитрий\\Documents\\Квитанция.docx";

                if (File.Exists(templateFilePath))
                {
                    string newFilePath = "C:\\Users\\Дмитрий\\Documents\\Квитанция_новый.docx";
                    File.Copy(templateFilePath, newFilePath, true);
                    using (DocX doc = DocX.Load(newFilePath))
                    {
                        doc.ReplaceText("#surname#", surname);
                        doc.ReplaceText("#name#", name);
                        doc.ReplaceText("#patronymic#", patronymic);
                        doc.ReplaceText("#address#", address);
                        doc.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка");
            }

            obpr obpr = new obpr();
            obpr.Show();
            this.Hide();
        }

        private void glav_Load(object sender, EventArgs e)
        {
            string connectionString = "Host=localhost;Username=postgres;Password=1111;Database=documentoob";

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT id, Фамилия, Имя, Отчество, Дата_рождения, Серия_паспорта, Номер_паспорта, Место_проживания, Электронная_почта, Номер_телефона, Должность, Место_работы FROM Физлицо";

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
            label2.Text = Login.UserLogin;
        }

        public int i;
        public string surname;
        public string name;
        public string patronymic;
        public string birthdate;
        public string passportseries;
        public string passportnumber;
        public string address;
        public string emale;
        public string phonenumber;
        public string position;
        public string workplace;
        public string column;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            column = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            i = e.RowIndex;
        }
    }
}
