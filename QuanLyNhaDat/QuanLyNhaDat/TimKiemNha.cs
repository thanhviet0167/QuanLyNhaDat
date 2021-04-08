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
    public partial class TimKiemNha : Form
    {
        public TimKiemNha()
        {
            InitializeComponent();
        }
        SqlConnection sqlCon = null;

        private void GetAll()
        {
            String strConnect = @"Data Source=DESKTOP-EIVACRQ\SQLEXPRESS;Initial Catalog=QuanLyNhaDat;Persist Security Info=True;User ID=KhachHang;Password=A";

            sqlCon = new SqlConnection(strConnect);
            sqlCon.Open();

            string sqlSelect = "SELECT * FROM NHA Where TinhTrang=0";
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
        private void TimKiemNha_Load(object sender, EventArgs e)
        {
            List<string> listprice= new List<string>() {"Duoi 12 tr" ,"Tu 12tr đen 25tr", "Tren 25tr"};
            comboBoxGiaNha.DataSource = listprice;
            List<string> listtype = new List<string>() { "Loai 1", "Loai 2", "Loai 3","Loai 4" };
            comboBoxLoaiNha.DataSource = listtype;
            List<string> address = new List<string>() { "Quan 1", "Quan 2", "Quan 3", "Quan 4" , "Quan 5", "Quan 7", "Quan 8", "Quan 10"};
            comboBoxKhuVuc.DataSource = address;
            this.GetAll();
        //    dataGridView1.Visible = false;

        
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private Boolean check_GiaNha(double gia, string str)
        {
            if(str == "Duoi 12 tr")
            {
                if(gia < 12000000)
                {
                    return true;
                }
            }
            else
            {
                if (str == "Tu 12tr đen 25tr")
                {
                    if (gia >= 12000000 && gia <= 25000000)
                    {
                        return true;
                    }
                }
                else
                {
                    if (str == "Tren 25tr")
                    {
                        if (gia > 25000000)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private Boolean check_khuvuc(string khuvuc, string str)
        {
            string convert = khuvuc.ToLower();
            string str_convert = str.ToLower();
            if(str == "Quan 1" || str == "Quan2" || str == "Quan 3" || str == "Quan 4" || str == "Quan 5" ||
                str == "Quan 7" || str == "Quan 8" || str == "Quan 10")
            {
                if (convert.Contains(str_convert))
                {
                    return true;
                }
            }
            return false;
        }
        private Boolean check_LoaiNha(string MaLN, string str)
        {
            if(str == "Loai 1")
            {
                if(MaLN == "LN001")
                {
                    return true;
                }
            }
            if (str == "Loai 2")
            {
                if (MaLN == "LN002")
                {
                    return true;
                }
            }
            if (str == "Loai 3")
            {
                if (MaLN == "LN003")
                {
                    return true;
                }
            }
            if (str == "Loai 4")
            {
                if (MaLN == "LN004")
                {
                    return true;
                }
            }

            return false;
        }

        private Boolean check_SoPhong(int sp, int sl)
        {
            if(sp == sl)
            {
                return true;
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var index = new List<int>();

            int check = 0;
            if (comboBoxGiaNha.Text != "")
            {
                check += 1;
            }
            if(comboBoxKhuVuc.Text != "")
            {
                check += 1;
            }
            if(comboBoxLoaiNha.Text != "")
            {
                check += 1;
            }
            if(numericUpDownSoPhong.Value > 0)
            {
                check += 1;
            }


            String strConnect = @"Data Source=DESKTOP-EIVACRQ\SQLEXPRESS;Initial Catalog=QuanLyNhaDat;Persist Security Info=True;User ID=KhachHang;Password=A";

            sqlCon = new SqlConnection(strConnect);
            sqlCon.Open();

            string sqlSelect = "SELECT * FROM NHA Where TinhTrang=0";
            SqlCommand cmd = new SqlCommand(sqlSelect, sqlCon);
            SqlDataReader dr = cmd.ExecuteReader();
            int k = 0;
            while (dr.Read())
            {
                int tmp_check = 0;
                if (check_GiaNha(Convert.ToDouble(dr["TienThue"]), comboBoxGiaNha.Text))
                {
                    tmp_check += 1;
                    Console.Out.WriteLine("Gia nha dung");
                }
                if (check_khuvuc(Convert.ToString(dr["DiaChi"]), comboBoxKhuVuc.Text))
                {
                    tmp_check += 1;
                    Console.Out.WriteLine("Khu vuc dung");
                }
                if(check_LoaiNha(Convert.ToString(dr["MaLN"]), comboBoxLoaiNha.Text))
                {
                    tmp_check += 1;
                    Console.Out.WriteLine("Ma loai nha dung");
                }
                var sp = numericUpDownSoPhong.Value;
                if (check_SoPhong(Convert.ToInt32(dr["SoPhong"]), (int)sp))
                {
                    tmp_check += 1;
                    Console.Out.WriteLine("So phong dung");
                }
                if(check <= tmp_check)
                {

                }
                else
                {
                    index.Add(k);
                }
                k++;
            }
            this.GetAll();
            CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dataGridView1.DataSource];
            currencyManager1.SuspendBinding();
            for (var i = 0; i < index.Count; i++)
            {
                dataGridView1.Rows[index[i]].Visible = false;
            }
            currencyManager1.ResumeBinding();

            sqlCon.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.Columns[e.ColumnIndex].Name == "Mua")
            {
                if(MessageBox.Show("Ban muon mua ngoi nha nay?","Message",MessageBoxButtons.YesNo,MessageBoxIcon.Question)
                    ==DialogResult.Yes)
                {
                    var index = dataGridView1.Columns[e.RowIndex].Index;
                    
                    Console.Out.WriteLine(dataGridView1.Rows[e.RowIndex].Cells["MaNha"].FormattedValue.ToString());
                   

                }

            }
            else
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "ThueNha")
                {
                    if (MessageBox.Show("Ban muon thue ngoi nha nay?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                    {
                        var index = dataGridView1.Columns[e.RowIndex].Index;

                        Console.Out.WriteLine(dataGridView1.Rows[e.RowIndex].Cells["MaNha"].FormattedValue.ToString());


                    }

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new KhachHang().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.GetAll();
        }
    }
}
