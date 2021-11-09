
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppQLTraSua.dataAcessLayer.models;
namespace QuanLyTiemTraSuaUWU
{
    public partial class frmNhanVien : Form
    {
        private int maNV = 0;
        public frmNhanVien()
        {
            InitializeComponent();
            TaiDanhSachNhanVien();
        }

        private void TaiDanhSachNhanVien()
        {
            List<NHANVIEN> danhSachNhanVien;

            using (var dbcontext = new dbQLtrasuauwu())
            {
                danhSachNhanVien = dbcontext.NHANVIENs.ToList();
            }

            int soThuTu = 1;
            dgvNhanVien.Rows.Clear();
            //Kiểm tra danh sách rỗng
            if (danhSachNhanVien.Count <= 0) return;
            //Đổ dữ liệu ra bảng
            foreach (var nv in danhSachNhanVien)
            {
                int indexRow = dgvNhanVien.Rows.Add();
                dgvNhanVien.Rows[indexRow].Cells[0].Value = soThuTu++;
                dgvNhanVien.Rows[indexRow].Cells[1].Value = nv.HoTen;
                dgvNhanVien.Rows[indexRow].Cells[2].Value = nv.SDT;
                dgvNhanVien.Rows[indexRow].Cells[3].Value = nv.DiaCHi;
                dgvNhanVien.Rows[indexRow].Cells[4].Value = nv.NamSinh;
                dgvNhanVien.Rows[indexRow].Cells[5].Value = nv.GioiTinh;
                dgvNhanVien.Rows[indexRow].Cells[6].Value = nv.MaChucVu;
                dgvNhanVien.Rows[indexRow].Cells[7].Value = nv.TaiKhoan;
                dgvNhanVien.Rows[indexRow].Cells[8].Value = nv.MatKhau;
                
            }
            dgvNhanVien.Rows[0].Selected = false;

        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (dgvNhanVien.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;

            dgvNhanVien.CurrentRow.Selected = true;
            
            txtHoten.Text = dgvNhanVien.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            txtSDT.Text = dgvNhanVien.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            txtDiaChi.Text = dgvNhanVien.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
            dateTimePicker1.Text = dgvNhanVien.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
            comboBox1.Text = dgvNhanVien.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
            comboBox2.Text = dgvNhanVien.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
            txtUser.Text = dgvNhanVien.Rows[e.RowIndex].Cells[7].FormattedValue.ToString();
            txtPass.Text = dgvNhanVien.Rows[e.RowIndex].Cells[8].FormattedValue.ToString();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var nv = new NHANVIEN();
            nv.HoTen = txtHoten.Text;
            nv.SDT = txtSDT.Text;
            nv.DiaCHi = txtDiaChi.Text;
            nv.NamSinh = DateTime.Parse(dateTimePicker1.Text);
            nv.GioiTinh = comboBox1.Text;
            nv.MaChucVu = comboBox2.Text;
            nv.TaiKhoan = txtUser.Text;
            nv.MatKhau = txtPass.Text;

            try
            {
                using (var dbcontext = new dbQLtrasuauwu())
                {
                    dbcontext.NHANVIENs.Add(nv);
                    dbcontext.SaveChanges();// done
                }

                TaiDanhSachNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dbcontext = new dbQLtrasuauwu())
                {
                    NHANVIEN updatenv = dbcontext.NHANVIENs.FirstOrDefault(p => p.HoTen == txtHoten.Text);

                    if (updatenv == null)
                    {
                        MessageBox.Show("Không cập nhật được !!!!");
                        return;
                    }

                    var nv = dbcontext.NHANVIENs.FirstOrDefault(p => p.HoTen == txtHoten.Text);
                    //nv.HoTen = txtHoten.Text;
                    nv.SDT = txtSDT.Text;
                    nv.DiaCHi = txtDiaChi.Text;
                    nv.NamSinh = DateTime.Parse(dateTimePicker1.Text);
                    nv.GioiTinh = comboBox1.Text;
                    nv.MaChucVu = comboBox2.Text;
                    nv.TaiKhoan = txtUser.Text;
                    nv.MatKhau = txtPass.Text;
                    dbcontext.SaveChanges();
                    MessageBox.Show("Cập nhật thành công !!!!");
                }

                TaiDanhSachNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (var context = new dbQLtrasuauwu())
            {
                NHANVIEN deletenv = context.NHANVIENs.FirstOrDefault(p => p.HoTen == txtHoten.Text);
                if (deletenv != null)
                {
                    DialogResult result = MessageBox.Show($"Bạn có đồng ý xóa nhân viên {deletenv.HoTen} không ?", "Thông báo cho bạn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        context.NHANVIENs.Remove(deletenv);
                        context.SaveChanges();

                        loadform();
                        loadDGV();

                        MessageBox.Show($"Xóa nhân viên {deletenv.HoTen} thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                }
            }
                
        }

        private void loadform()
        {
            txtHoten.Clear();
            txtDiaChi.Clear();
            txtPass.Clear();
            txtSDT.Clear();
            txtUser.Clear();
            txtHoten.Focus();
        }

        private void loadDGV()
        {
            using (var context = new dbQLtrasuauwu())
            {
                List<NHANVIEN> listkhachhang = context.NHANVIENs.ToList();
                TaiDanhSachNhanVien();
            }
                
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("bạn muốn thoát ???", " thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {

        }
    }
}