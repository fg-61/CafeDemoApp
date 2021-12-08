using CafeDemoLib.Business;
using CafeDemoLib.Data;
using CafeDemoLib.Model;
using CafeOtomasyonuApp.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CafeOtomasyonuApp.Formlar
{


    public partial class FrmSiparis : Form
    {
        public FrmSiparis()
        {
            InitializeComponent();
        }

        public Masa SeciliMasa { get; set; }
        public List<Siparis> MasaninSiparisleri { get; set; }
        public CafeContext Context { get; set; }
        private KategoriRepository _kategoriRepository;
        private SiparisRepository _siparisRepository;
        decimal toplamTutar = 0;

        private void FrmSiparis_Load(object sender, EventArgs e)
        {
            _kategoriRepository = new KategoriRepository() { Context = Context };
            _siparisRepository = new SiparisRepository() { Context = Context };
            List<Kategori> kategoriler = _kategoriRepository.GetAll(x =>
                x.Urunler.Count > 0)
                .OrderBy(x => x.Sira)
                .ToList();
            flpKategori.Controls.Clear();

            int kategoriBtnYukseklik = 100;
            int kategoriBtnGenislik = flpKategori.Width - 30;
            foreach (Kategori kategori in kategoriler)
            {
                Button btn = new Button()
                {
                    Text = kategori.Ad,
                    Size = new Size(kategoriBtnGenislik, kategoriBtnYukseklik),
                    Tag = kategori
                };
                btn.Click += KategoriBtn_Click;
                flpKategori.Controls.Add(btn);
            }
        }
        private Kategori _seciliKategori;
        private void KategoriBtn_Click(object sender, EventArgs e)
        {
            Button seciliButon = sender as Button;
            _seciliKategori = seciliButon.Tag as Kategori;
            List<Urun> urunler = _seciliKategori.Urunler;

            flpYemekEkleme.Controls.Clear(); // olmassa basına this. ekle

            int urunBtnYukseklik = 120;
            int urunBtnGenislik = flpYemekEkleme.Width / 3 - 15;
            foreach (Urun urun in urunler)
            {
                BtnYemekSecme btn = new BtnYemekSecme()
                {
                    YemekIsmi = urun.Ad,
                    Fiyat = (urun.Fiyat).ToString(),
                    Size = new Size(urunBtnGenislik, urunBtnYukseklik),
                    //BackgroundImage = Image.FromStream(new MemoryStream(urun.Fotograf)),
                    //BackgroundImageLayout = ImageLayout.Stretch

                    Tag = urun
                };
                btn.OrderClicked += BtnYemekSecme_OrderClicked;
                flpYemekEkleme.Controls.Add(btn);
            }
        }


        private Urun _seciliUrun;
        private void BtnYemekSecme_OrderClicked(object sender, EventArgs e)
        {
            BtnYemekSecme seciliButon = sender as BtnYemekSecme;
            _seciliUrun = seciliButon.Tag as Urun;
            SeciliMasa.MasaDurumu = MasaDurumlar.Dolu;
            bool varMi = false;
            Siparis seciliSiparis = new Siparis();
            foreach (Siparis siparis in MasaninSiparisleri)
            {
                if (siparis.Urun.Id == _seciliUrun.Id)
                {
                    seciliSiparis = siparis;
                    varMi = true;
                    break;
                }
            }

            if (varMi)
            {
                seciliSiparis.Adet++;
                foreach (BtnEklenenUrun item in flpSiparisEkrani.Controls)
                {
                    if (item.SiparisIsmi == _seciliUrun.Ad)
                    {
                        item.Adet = seciliSiparis.Adet;
                        item.txtUrunAdet.Text = item.Adet.ToString();
                        item.txtAraToplamTutar.Text = seciliSiparis.AraToplam.ToString();
                        item.ArttirClicked += BtnArttir_ArttirClicked;
                        item.AzaltClicked += BtnAzalt_AzaltClicked;
                    }
                }
                _siparisRepository.Update();
            }

            else
            {
                Siparis yeniSiparis = new Siparis()
                {
                    Adet = 1,
                    Urun = _seciliUrun,
                    Fiyat = _seciliUrun.Fiyat,
                    Masa = SeciliMasa
                };

                BtnEklenenUrun btn = new BtnEklenenUrun()
                {
                    Size = new Size(flpSiparisEkrani.Width - 30, 45),
                    SiparisIsmi = _seciliUrun.Ad,
                    Adet = yeniSiparis.Adet,
                    BirimFiyat = yeniSiparis.Fiyat,
                };

                
                flpSiparisEkrani.Controls.Add(btn);
                btn.ArttirClicked += BtnArttir_ArttirClicked;
                btn.AzaltClicked += BtnAzalt_AzaltClicked;
                btn.txtAraToplamTutar.Text = yeniSiparis.AraToplam.ToString();
                _siparisRepository.Add(yeniSiparis);
                ToplamaHesapla();
            }

            toplamTutar += seciliSiparis.AraToplam;
            txtToplamTutar.Text = toplamTutar.ToString();

            MasaninSiparisleri = _siparisRepository.GetAll(x =>
               x.Masa.Id == SeciliMasa.Id && 
               x.Masa.MasaDurumu == MasaDurumlar.Dolu);
        }
        
        private void BtnAzalt_AzaltClicked(object sender, EventArgs e)
        {
            CikarmaHesapla();
        }

        private void BtnArttir_ArttirClicked(object sender, EventArgs e)
        {
            ToplamaHesapla();
        }




        private void ToplamaHesapla()
        {
            decimal total = 0;
            foreach (BtnEklenenUrun item in flpSiparisEkrani.Controls)
            {
                total += item.ToplamTutar;
            }
            toplamTutar = total;
            txtToplamTutar.Text = toplamTutar.ToString();
        }
        private void CikarmaHesapla()
        {
            decimal total = 0;
            foreach (BtnEklenenUrun item in flpSiparisEkrani.Controls)
            {
                total += item.ToplamTutar;
            }
            toplamTutar = total;
            txtToplamTutar.Text = toplamTutar.ToString();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHesapIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

    }

}
