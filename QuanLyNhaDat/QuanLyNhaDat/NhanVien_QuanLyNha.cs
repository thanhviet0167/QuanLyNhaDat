﻿using System;
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
    public partial class NhanVien_QuanLyNha : Form
    {
        public NhanVien_QuanLyNha()
        {
            InitializeComponent();
           
        }
        SqlConnection sqlCon = null;
        String MaNha = "";
        private void GetAll()
        {
            String strConnect = @"Data Source=DESKTOP-EIVACRQ\SQLEXPRESS;Initial Catalog=QuanLyNhaDat;Persist Security Info=True;User ID=NhanVien;Password=C";

            sqlCon = new SqlConnection(strConnect);
            sqlCon.Open();

            string sqlSelect = "SELECT * FROM NHA";
            SqlCommand cmd = new SqlCommand(sqlSelect, sqlCon);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["MaNha"].Visible = false;
            dataGridView1.Columns["MaChuNha"].Visible = false;
            dataGridView1.Columns["MaLN"].Visible = false;
            dataGridView1.Columns["MaNV"].Visible = false;
            dataGridView1.Columns["MaCN"].Visible = false;

            sqlCon.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if(this.MaNha.Length > 0)
            {
                String strConnect = @"Data Source=DESKTOP-EIVACRQ\SQLEXPRESS;Initial Catalog=QuanLyNhaDat;Integrated Security=True";
                sqlCon = new SqlConnection(strConnect);
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("UpdateTienThue_T1_fix", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@manha", SqlDbType.NVarChar).Value = this.MaNha;
                
                cmd.ExecuteNonQuery();
                sqlCon.Close();
                this.GetAll();

            }
        }

        private void SaveThongTin(String _MaNha)
        {
            String strConnect = @"Data Source=DESKTOP-EIVACRQ\SQLEXPRESS;Initial Catalog=QuanLyNhaDat;Persist Security Info=True;User ID=NhanVien;Password=C";

            sqlCon = new SqlConnection(strConnect);
            sqlCon.Open();
            int tinhtrang ;
            if (checkTinhTrang0.Checked)
            {
                tinhtrang = 0;
            }
            else
            {
                tinhtrang = 1;
            }
            string sqlUpdate = "UPDATE NHA SET DiaChi=N'" + txtDiaChi.Text + "', ThongTin = N'" + txtThongTin.Text
                + "', NgayDangKy = N'" + txtNgayDangKy.Text + "', TinhTrang = " + tinhtrang + ", NgayHetHan = N'" +
                txtNgayHetHan.Text + "', TienThue = N'" + Convert.ToDouble(txtTienThue.Text) + "' Where MaNha = N'" + _MaNha + "'";
            SqlCommand cmd = new SqlCommand(sqlUpdate, sqlCon);
            cmd.ExecuteNonQuery();
            sqlCon.Close();

        }

        private void NhanVien_ThueNha_Load(object sender, EventArgs e)
        {
            buttonSave.Visible = false;
            txtDiaChi.Enabled = false;
            txtThongTin.Enabled = false;
            txtTienThue.Enabled = false;
            checkTinhTrang0.Enabled = false;
            checkTinhTrang1.Enabled = false;
            numericSoPhong.Enabled = false;
            txtNgayDangKy.Enabled = false;
            txtNgayHetHan.Enabled = false;
            this.GetAll();
         
            

        }

        private void buttonChinhSua_Click(object sender, EventArgs e)
        {
            buttonSave.Visible = true;
            txtDiaChi.Enabled = true;
            txtThongTin.Enabled = true;
            txtTienThue.Enabled = true;
            checkTinhTrang0.Enabled = true;
            checkTinhTrang1.Enabled = true;
            numericSoPhong.Enabled = true;
            txtNgayDangKy.Enabled = true;
            txtNgayHetHan.Enabled = true;
            buttonChinhSua.Visible = false;
            buttonGiam.Visible = false;
            buttonTang.Visible = false;
            buttonThemNha.Visible = false;
            button1.Visible = false;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDiaChi.Text =  dataGridView1.Rows[e.RowIndex].Cells["DiaChi"].FormattedValue.ToString();
            txtThongTin.Text = dataGridView1.Rows[e.RowIndex].Cells["ThongTin"].FormattedValue.ToString();
            txtTienThue.Text = dataGridView1.Rows[e.RowIndex].Cells["TienThue"].FormattedValue.ToString();
            txtNgayDangKy.Text = dataGridView1.Rows[e.RowIndex].Cells["NgayDangKy"].FormattedValue.ToString();
            txtNgayHetHan.Text = dataGridView1.Rows[e.RowIndex].Cells["NgayHetHan"].FormattedValue.ToString();
            numericSoPhong.Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["SoPhong"].FormattedValue.ToString());
            this.MaNha = dataGridView1.Rows[e.RowIndex].Cells["MaNha"].FormattedValue.ToString();
           
            if (dataGridView1.Rows[e.RowIndex].Cells["TinhTrang"].FormattedValue.ToString() == "False")
            {
                checkTinhTrang0.Checked = true;
                checkTinhTrang1.Checked = false;
            }
            else
            {
                checkTinhTrang0.Checked = false;
                checkTinhTrang1.Checked = true;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Xoa")
            {
                if (MessageBox.Show("Ban muon xoa ngoi nha nay?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    if(this.MaNha.Length > 0)
                    {
                        String strConnect = @"Data Source=DESKTOP-EIVACRQ\SQLEXPRESS;Initial Catalog=QuanLyNhaDat;Integrated Security=True";
                        sqlCon = new SqlConnection(strConnect);
                        sqlCon.Open();
                        string sqldelete = "DELETE FROM NHA WHERE MaNha = N'" + this.MaNha + "'";
                        SqlCommand cmd = new SqlCommand(sqldelete, sqlCon);
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                        MessageBox.Show("Xoa nha thanh cong");
                        this.GetAll();
                    }
                    else
                    {
                        MessageBox.Show("Xoa nha that bai");
                    }
                    
                }

            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "XemTinhTrang")
            {
                String strConnect = @"Data Source=DESKTOP-EIVACRQ\SQLEXPRESS;Initial Catalog=QuanLyNhaDat;Integrated Security=True";
                sqlCon = new SqlConnection(strConnect);
                sqlCon.Open();
                
                SqlCommand cmd = new SqlCommand("Xem_Tinh_Trang_T2_fix", sqlCon);
             //   cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@manha", SqlDbType.NVarChar).Value = this.MaNha;
                cmd.Parameters.Add("@tinhtrang", SqlDbType.Bit).Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Tinh trang(0 chua thue, 1 da thue): " + cmd.Parameters["@tinhtrang"].Value);
                Console.Out.WriteLine(cmd.Parameters["@tinhtrang"].Value);
                Console.Out.WriteLine("Hello");
                sqlCon.Close();
               
            }    
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Boolean check = true;
            if(txtDiaChi.Text.Length <= 0 || txtNgayDangKy.Text.Length <= 0 || txtNgayHetHan.Text.Length <= 0
                || txtThongTin.Text.Length <= 0 || txtTienThue.Text.Length <= 0 || 
                (!checkTinhTrang1.Checked && !checkTinhTrang0.Checked))
            {
                check = false;
             
            }
            if (check)
            {
                Error.Text = "";

                this.SaveThongTin(this.MaNha);
                this.GetAll();
                buttonSave.Visible = false;
                txtDiaChi.Enabled = false;
                txtThongTin.Enabled = false;
                txtTienThue.Enabled = false;
                checkTinhTrang0.Enabled = false;
                checkTinhTrang1.Enabled = false;
                numericSoPhong.Enabled = false;
                txtNgayDangKy.Enabled = false;
                txtNgayHetHan.Enabled = false;
                buttonChinhSua.Visible = false;
                buttonGiam.Visible = true;
                buttonTang.Visible = true;
                buttonThemNha.Visible = true;
                buttonChinhSua.Visible = true;
                button1.Visible = true;
            }
            else
            {
                Error.Text = "Thieu thong tin";
            }
        }

        private void checkTinhTrang0_Click(object sender, EventArgs e)
        {
           
            checkTinhTrang1.Checked = false;
          
        }
        

        private void checkTinhTrang1_Click_1(object sender, EventArgs e)
        {
            checkTinhTrang0.Checked = false;
           
        }

        private void buttonThemNha_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            new ThemNhacs().Show();
        }

        private void buttonGiam_Click(object sender, EventArgs e)
        {
            if (this.MaNha.Length > 0)
            {
                String strConnect = @"Data Source=DESKTOP-EIVACRQ\SQLEXPRESS;Initial Catalog=QuanLyNhaDat;Integrated Security=True";
                sqlCon = new SqlConnection(strConnect);
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("UpdateTienThueNha_T2_fix", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@manha", SqlDbType.NVarChar).Value = this.MaNha;

                cmd.ExecuteNonQuery();
                sqlCon.Close();
                this.GetAll();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.MaNha.Length > 0)
            {
                String strConnect = @"Data Source=DESKTOP-EIVACRQ\SQLEXPRESS;Initial Catalog=QuanLyNhaDat;Integrated Security=True";
                sqlCon = new SqlConnection(strConnect);
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("UpdateTinhTrang_T1_fix", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@manha", SqlDbType.NVarChar).Value = this.MaNha;
               
                cmd.ExecuteNonQuery();
                
                sqlCon.Close();
                this.GetAll();

            }
        }
    }
}
