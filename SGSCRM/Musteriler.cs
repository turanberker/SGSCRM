using Businness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSCRM
{
    public partial class Musteriler : Form
    {
        public Musteriler(Main Anaform)
        {
            InitializeComponent();
            anaform = Anaform;
        }
        Main anaform;
        DataTable musterilerdt;
        private void Musteriler_Load(object sender, EventArgs e)
        {
            DataGridDoldur();
        }
        public void DataGridDoldur()
        {
            using (CustomerBS bs = new CustomerBS())
            {
                musterilerdt = bs.SelectAsDataTable();
                grdMusteriler.DataSource = musterilerdt;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form f = new Helper.Tools().Varmi("MusteriGuncelleEkle", anaform);
            if (f == null)
            {
                f = new MusteriGuncelleEkle(this);
                f.MdiParent = anaform;
                f.Show();
            }
            else
            {
                f.BringToFront();
                this.ActivateMdiChild(f);
            }
        }
        protected void Filtrele()
        {
            DataTable gMusterilerdt ;
            if(!string.IsNullOrEmpty(tstxtAdi.Text))
            {
                using (DataView dw = new DataView(musterilerdt))
                {
                    dw.RowFilter=string.Format( "FNAME like '%{0}%'",tstxtAdi.Text);
                    gMusterilerdt = dw.ToTable();
                }                
            }
            else
            {
                gMusterilerdt = musterilerdt;
            }
            if (!string.IsNullOrEmpty(tstxtSoyadi.Text))
            {
                using (DataView dw = new DataView(gMusterilerdt))
                {
                    dw.RowFilter = string.Format("LNAME like '%{0}%'", tstxtSoyadi.Text);
                    gMusterilerdt = dw.ToTable();
                }
            }
            if (!string.IsNullOrEmpty(tstxtCepTel.Text))
            {
                using (DataView dw = new DataView(gMusterilerdt))
                {
                    dw.RowFilter = string.Format("Phone_Number like '%{0}%'", tstxtCepTel.Text);
                    gMusterilerdt = dw.ToTable();
                }
            }
            if (!string.IsNullOrEmpty(tstxtTCNo.Text))
            {
                using (DataView dw = new DataView(gMusterilerdt))
                {
                    dw.RowFilter = string.Format("TC_Number like '%{0}%'", tstxtTCNo.Text);
                    gMusterilerdt = dw.ToTable();
                }
            }
            if (!string.IsNullOrEmpty(tstxtMaslegi.Text))
            {
                using (DataView dw = new DataView(gMusterilerdt))
                {
                    dw.RowFilter = string.Format("Occupation_Type_Name like '%{0}%'", tstxtMaslegi.Text);
                    gMusterilerdt = dw.ToTable();
                }
            }
            if (!string.IsNullOrEmpty(tstxtCildi.Text))
            {
                using (DataView dw = new DataView(gMusterilerdt))
                {
                    dw.RowFilter = string.Format("Skin_Type_Name like '%{0}%'", tstxtCildi.Text);
                    gMusterilerdt = dw.ToTable();
                }
            }
            DateTime tarihbas;
            if (!string.IsNullOrEmpty(tstxtTarihBas.Text) && DateTime.TryParse(tstxtTarihBas.Text, out tarihbas))
            {
                using (DataView dw = new DataView(gMusterilerdt))
                {
                    dw.RowFilter = string.Format("Birth_Date >= '{0}'", tarihbas);
                    gMusterilerdt = dw.ToTable();
                }
            }
            DateTime tarihBit;
            if (!string.IsNullOrEmpty(tstxtTarihBit.Text) && DateTime.TryParse(tstxtTarihBit.Text, out tarihBit))
            {
                using (DataView dw = new DataView(gMusterilerdt))
                {
                    dw.RowFilter = string.Format("Birth_Date <= '{0}'", tarihBit);
                    gMusterilerdt = dw.ToTable();
                }
            }
            grdMusteriler.DataSource = gMusterilerdt;
           
        }
    }
}
