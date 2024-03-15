using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using Npgsql;
using Spire.Doc;
using System.IO;

namespace documentoob
{
    public partial class gend : Form
    {
        public gend()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string docxFilePath = "C:\\Users\\Дмитрий\\Documents\\Квитанция_новый.docx";

                if (System.IO.File.Exists(docxFilePath))
                {
                    Process.Start(docxFilePath);
                }
                else
                {
                    MessageBox.Show("Документ не найден.", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string docxFilePath = "C:\\Users\\Дмитрий\\Documents\\Договор_новый.docx";

                if (System.IO.File.Exists(docxFilePath))
                {
                    Process.Start(docxFilePath);
                }
                else
                {
                    MessageBox.Show("Документ не найден.", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string txtFilePath = "C:\\Users\\Дмитрий\\Documents\\Договор_новый.txt";

                if (System.IO.File.Exists(txtFilePath))
                {
                    Process.Start(txtFilePath);
                }
                else
                {
                    MessageBox.Show("Документ не найден.", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void ConvertToPDF(string docxFilePath)
        {
            string pdfFilePath = Path.ChangeExtension(docxFilePath, ".pdf");

            try
            {
                Document document = new Document();
                document.LoadFromFile(docxFilePath);
                document.SaveToFile(pdfFilePath, FileFormat.PDF);

                // Открываем PDF файл
                Process.Start(pdfFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка");
            }
        }

        internal void button5_Click(object sender, EventArgs e)
        {
            string docxFilePath = "C:\\Users\\Дмитрий\\Documents\\Договор_новый.docx";

            if (File.Exists(docxFilePath))
            {
                ConvertToPDF(docxFilePath);
            }
            else
            {
                MessageBox.Show("Документ не найден.", "Ошибка");
            }
        }
    }
}
