using CafeDemoLib.Business;
using CafeDemoLib.Data;
using CafeDemoLib.Model;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
namespace CafeOtomasyonuApp.Formlar
{
    public partial class FrmMenuAyar : Form
    {
        public FrmMenuAyar()
        {
            InitializeComponent();
        }

        public CafeContext Context { get; set; }
        private KategoriRepository _kategoriRepo;
        private IRepository<Urun> _urunRepo;
        private void MenuForm_Load(object sender, EventArgs e)
        {
            _kategoriRepo = new KategoriRepository
            {
                Context = Context
            };
            _urunRepo = new UrunRepository();
            lstKategori.DataSource = _kategoriRepo.GetAll();
        }

        private Kategori _seciliKategori;
        private void lstKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstKategori.SelectedItem == null) return;

            _seciliKategori = lstKategori.SelectedItem as Kategori;
            lstUrunler.DataSource = _seciliKategori.Urunler;
        }

        private void btnKategoriKaydet_Click(object sender, EventArgs e)
        {
            Kategori yeniKategori = new Kategori()
            {
                Ad = txtKategoriAdi.Text
            };
            _kategoriRepo.Add(yeniKategori);
            lstKategori.DataSource = null;
            lstKategori.DataSource = _kategoriRepo.GetAll();
        }

        private void btnUrunKaydet_Click(object sender, EventArgs e)
        {
            Urun yeniUrun = new Urun()
            {
                Ad = txtUrunAdi.Text,
                Fiyat = nFiyat.Value
            };

            _kategoriRepo.AddProduct(_seciliKategori, yeniUrun);
            lstUrunler.DataSource = null;
            lstUrunler.DataSource = _seciliKategori.Urunler;
        }
    }
}
