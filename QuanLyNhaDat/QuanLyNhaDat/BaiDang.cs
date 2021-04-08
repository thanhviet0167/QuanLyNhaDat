using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaDat
{
    public partial class BaiDang : Form
    {
        public BaiDang()
        {
            InitializeComponent();
            ThongTin.Visible = false;
            FormBaiDang.Visible = false;
            DataLoad.Visible = false;
        }
        SqlConnection sqlCon = null;
        private void button1_Click(object sender, EventArgs e)
        {
            DataLoad.Visible = false;
            FormBaiDang.Visible = false;
            ThongTin.Visible = true;
            
        }

       

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            DataLoad.Visible = false;
            ThongTin.Visible = false;
            FormBaiDang.Visible = true;
           
        }

        private void BaiDang_Load(object sender, EventArgs e)
        {
            txtMaChuNha.Enabled = false;
            txtHoTen.Enabled = false;
            txtSdt.Enabled = false;
            txtAdress.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ThongTin.Visible = false;
            FormBaiDang.Visible = false;
            DataLoad.Visible = false;
            txtDate.Clear();
            txtMaChuNha.Clear();
            txtMaNha.Clear();
        
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if(txtMCN.Text != "" && txtMaNha.Text != "" && txtDate.Text != "" && (radioButton1.Checked || radioButton2.Checked))
            {
                String strConnect = @"Data Source=DESKTOP-EIVACRQ\SQLEXPRESS;Initial Catalog=QuanLyNhaDat;Persist Security Info=True;User ID=ChuNha;Password=B";

                sqlCon = new SqlConnection(strConnect);
                sqlCon.Open();

                string sqlInsert = "INSERT INTO BAI_DANG VALUES (@Loai, @MaNha, @MaChuNha, @NgayDang)";
                SqlCommand cmd = new SqlCommand(sqlInsert, sqlCon);
                if (radioButton2.Checked)
                {
                    cmd.Parameters.AddWithValue("Loai", false);
                }
                else
                {
                    cmd.Parameters.AddWithValue("Loai", true);
                }
                cmd.Parameters.AddWithValue("MaNha", txtMaNha.Text);
                cmd.Parameters.AddWithValue("MaChuNha", txtMCN.Text);
                cmd.Parameters.AddWithValue("NgayDang", txtDate.Text);
                txtDate.Clear();
                txtMaChuNha.Clear();
                txtMaNha.Clear();
                MessageBox.Show("Tao bai dang thanh cong");
                cmd.ExecuteNonQuery();

                sqlCon.Close();
            }
            else
            {
                if(txtDate.Text == "")
                {
                    ErrorDate.Text =  "Ngay dang ki trong";
                }
                else
                {
                    ErrorDate.Clear();
                }

                if (txtMaNha.Text == "")
                {
                    ErrorMaNha.Text = "Ma nha trong";
                }
                else
                {
                    String strConnect = @"Data Source=DESKTOP-EIVACRQ\SQLEXPRESS;Initial Catalog=QuanLyNhaDat;Persist Security Info=True;User ID=ChuNha;Password=B";

                    sqlCon = new SqlConnection(strConnect);
                    sqlCon.Open();

                    string sqlSelect = "SELECT * FROM BAI_DANG";
                    SqlCommand cmd = new SqlCommand(sqlSelect, sqlCon);
                    SqlDataReader dr = cmd.ExecuteReader();
                    Boolean b = true;
                    while(dr.Read())
                    {

                        if (Convert.ToString(dr["MaNha"]).Equals(txtMaNha.Text))
                        {
                            b = false;
                        }
                    }
                    if(b)
                    {
                        ErrorMaNha.Clear();
                    }
                    else
                    {
                        ErrorMaNha.Text = "Ma nha da ton tai";
                    }
                    sqlCon.Close();

                }
                if (txtMaChuNha.Text == "")
                {
                    ErrorMCN.Text = "Ma chu nha trong";
                }
                else
                {
                    ErrorMCN.Clear();
                }
                if (!radioButton1.Checked && !radioButton2.Checked)
                {
                    ErrorDate.Text = "Loai bai dang chua duoc chon";
                }
                else
                {
                    ErrorLoai.Clear();
                }
            }
            
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           
            
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = false;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            
            ThongTin.Visible = false;
            FormBaiDang.Visible = false;
            DataLoad.Visible = true;
            txtDate.Clear();
            txtMaChuNha.Clear();
            txtMaNha.Clear();

            
            String strConnect = @"Data Source=DESKTOP-EIVACRQ\SQLEXPRESS;Initial Catalog=QuanLyNhaDat;Persist Security Info=True;User ID=ChuNha;Password=B";

            sqlCon = new SqlConnection(strConnect);
            sqlCon.Open();

            string sqlSelect = "SELECT * FROM BAI_DANG";
            SqlCommand cmd = new SqlCommand(sqlSelect, sqlCon);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            sqlCon.Close();
        }
    }
}
