using CafeDemoLib.Business;
using CafeDemoLib.Data;
using CafeDemoLib.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CafeOtomasyonuApp.Formlar
{
    public partial class FrmKatAyar : Form
    {
        public FrmKatAyar()
        {
            InitializeComponent();
        }

        public CafeContext Context { get; set; }
        private KatRepository _repository;
        private void btnKatKaydet_Click(object sender, EventArgs e)
        {
            Kat yeniKat = new Kat()
            {
                Ad = txtKatAdi.Text,
                Kod = txtKod.Text,
                Sira = int.Parse(txtSira.Text)
            };
            int adet = int.Parse(txtMasaAdeti.Text);

            for (int i = 0; i < adet; i++)
            {
                Masa yeniMasa = new Masa()
                {
                    Sira = i + 1,
                    Ad = $"{yeniKat.Kod}/Masa {i + 1}"
                };
                yeniKat.Masalar.Add(yeniMasa);
            }
            _repository.Add(yeniKat);
            lstKatlar.DataSource = null;
            lstKatlar.DataSource = _repository.GetAll().OrderBy(x => x.Sira).ToList();
        }

        private void KatAyarForm_Load(object sender, EventArgs e)
        {
            _repository = new KatRepository
            {
                Context = Context
            };
            lstKatlar.DataSource = _repository.GetAll().OrderBy(x => x.Sira).ToList();
        }
    }
}
