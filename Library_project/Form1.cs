using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Library_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection connect = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\EMIR EL HAFIZ\OneDrive\Documents\kitaplik.mdb");

        void list()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * From kitaplar", connect);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            list();
        }

        private void BtnList_Click(object sender, EventArgs e)
        {
            list();
        }
        string status = "";

        private void BtnSave_Click(object sender, EventArgs e)
        {
            connect.Open();
            OleDbCommand command1 = new OleDbCommand("insert into Kitaplar (KitapAd,Yazar,Tur,Sayfa,Durum) values (@p1,@p2,@p3,@p4,@p5)", connect);
            command1.Parameters.AddWithValue("@p1", TxtBookName.Text);
            command1.Parameters.AddWithValue("@p2", TxtBookAuthor.Text);
            command1.Parameters.AddWithValue("@p3", CmbType.Text);
            command1.Parameters.AddWithValue("@p4", TxtPages.Text);
            command1.Parameters.AddWithValue("@p5", status);
            command1.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("The book added to the system", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            status = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            status = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int choose = dataGridView1.SelectedCells[0].RowIndex;
            TxtBookId.Text = dataGridView1.Rows[choose].Cells[0].Value.ToString();
            TxtBookName.Text = dataGridView1.Rows[choose].Cells[1].Value.ToString();
            TxtBookAuthor.Text = dataGridView1.Rows[choose].Cells[2].Value.ToString();
            CmbType.Text = dataGridView1.Rows[choose].Cells[3].Value.ToString();
            TxtPages.Text = dataGridView1.Rows[choose].Cells[4].Value.ToString();
            if (dataGridView1.Rows[choose].Cells[5].Value.ToString()=="True")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            connect.Open();
            OleDbCommand command = new OleDbCommand("Delete From Kitaplar Where Kitapid=@p1", connect);
            command.Parameters.AddWithValue("@p1", TxtBookId.Text);
            command.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("The book deleted from the list", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            list();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            connect.Open();
            OleDbCommand command = new OleDbCommand("update Kitaplar set KitapAd=@p1,Yazar=@p2,Tur=@p3,Sayfa=@p4,Durum=@p5 Where Kitapid=@p6", connect);
            command.Parameters.AddWithValue("@p1", TxtBookId.Text);
            command.Parameters.AddWithValue("@p2", TxtBookAuthor.Text);
            command.Parameters.AddWithValue("@p3", CmbType.Text);
            command.Parameters.AddWithValue("@p4", TxtPages.Text);
            if (radioButton1.Checked==true)
            {
                command.Parameters.AddWithValue("@p5", status);
            }
            if (radioButton2.Checked==true)
            {
                command.Parameters.AddWithValue("@p5", status);
            }
            command.Parameters.AddWithValue("@p6", TxtBookId.Text);
            command.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("List updated", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            list();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand command = new OleDbCommand("Select * From Kitaplar Where KitapAd=@p1", connect);
            command.Parameters.AddWithValue("@p1", TxtFind.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            OleDbCommand command = new OleDbCommand("Select * From Kitaplar Where KitapAd like '%" + TxtFind.Text + "%' ", connect);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}

//Provider=Microsoft.Jet.OLEDB.4.0;Data Source="C:\Users\EMIR EL HAFIZ\OneDrive\Documents\kitaplik.mdb"