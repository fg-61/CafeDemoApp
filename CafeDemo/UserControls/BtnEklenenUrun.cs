using System;
using System.Windows.Forms;

namespace CafeOtomasyonuApp.UserControls
{
    public delegate void ArttirClicked(object sender, EventArgs e);
    public delegate void AzaltClicked(object sender, EventArgs e);
    public partial class BtnEklenenUrun : UserControl
    {

        public BtnEklenenUrun()
        {
            InitializeComponent();
        }

        public string SiparisIsmi
        {
            get => txtSiparisIsmi.Text;
            set => txtSiparisIsmi.Text = value;
        }

        public int Adet
        {
            get => int.Parse(txtUrunAdet.Text);
            set => txtUrunAdet.Text = value.ToString();
        }

        public decimal BirimFiyat { get; set; }

        public decimal ToplamTutar
        {
            get => BirimFiyat * Adet;
        }

        public event ArttirClicked ArttirClicked;
        public void btnYemekAdetArttir_Click(object sender, EventArgs e)
        {
            Adet++;
            txtUrunAdet.Text = Adet.ToString();
            txtAraToplamTutar.Text = ToplamTutar.ToString();
            ArttirClicked?.Invoke(this, e);
        }

        public event AzaltClicked AzaltClicked;
        public void btnYemekAdetAzalt_Click(object sender, EventArgs e)
        {
            if (Adet > 1)
            {
                Adet--;
                txtUrunAdet.Text = Adet.ToString();
                txtAraToplamTutar.Text = ToplamTutar.ToString();
                AzaltClicked?.Invoke(this, e);
            }
            else return;

        }

    }

}
