using CafeDemoLib.Data;
using CafeOtomasyonuApp.Formlar;
using System;
using System.Windows.Forms;

namespace CafeDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private CafeContext _context;
        private void Form1_Load(object sender, EventArgs e)
        {
            _context = new CafeContext();
            _context.Load();
        }

        private FrmMasalar frmMasalar;
        private void btnMasalar_Click(object sender, EventArgs e)
        {
            if (frmMasalar == null || frmMasalar.IsDisposed)
            {
                frmMasalar = new FrmMasalar();
            }
            frmMasalar.Context = _context;
            frmMasalar.MdiParent = this;
            frmMasalar.WindowState = FormWindowState.Maximized;
            frmMasalar.Show();
        }


        private FrmMenuAyar frmMenuAyar;
        private void btnRaporlar_Click(object sender, EventArgs e)
        {
            if (frmMenuAyar == null || frmMenuAyar.IsDisposed)
            {
                frmMenuAyar = new FrmMenuAyar();
            }
            frmMenuAyar.Context = _context;
            frmMenuAyar.MdiParent = this;
            frmMenuAyar.WindowState = FormWindowState.Maximized;
            frmMenuAyar.Show();
        }

        private FrmKatAyar frmKatAyar;
        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            if (frmKatAyar == null || frmKatAyar.IsDisposed)
            {
                frmKatAyar = new FrmKatAyar();
            }
            frmKatAyar.Context = _context;
            frmKatAyar.MdiParent = this;
            frmKatAyar.WindowState = FormWindowState.Maximized;
            frmKatAyar.Show();
        }



        private void btnKapat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Kapatmak istediğinize emin misiniz? ", "Uygulamayı Kapatmak Üzeresiniz!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
