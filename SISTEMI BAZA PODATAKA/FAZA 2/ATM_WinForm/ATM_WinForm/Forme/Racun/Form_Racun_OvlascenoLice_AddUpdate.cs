using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Racun
{
    public partial class Form_Racun_OvlascenoLice_AddUpdate : Form
    {
        private readonly string type = "";
        private readonly int racunId = -1;
        private readonly ATM_WinForm.DTOs.RacunOvlascenoLiceBasic ovlascenoLice = null;
        public Form_Racun_OvlascenoLice_AddUpdate(string type, ATM_WinForm.DTOs.RacunOvlascenoLiceBasic ovlascenoLice, int racunId)
        {
            InitializeComponent();

            this.type = type;
            this.ovlascenoLice = ovlascenoLice;
            this.racunId = racunId;

            switch (this.type)
            {
                case "add":
                    {
                        AkcijaOvlascenoLiceBtn.Text = "Dodaj Ovlasceno Lice";

                        this.Text = "ATM - Dodaj Ovlasceno Lice";

                        labelaAddEditLBL.Text = "Unesi novo ovlasceno lice:";

                        break;
                    }
                case "update":
                    {
                        AkcijaOvlascenoLiceBtn.Text = "Izmeni Ovlasceno Lice";

                        labelaAddEditLBL.Text = "Izmeni ovlasceno lice:";
                        AddUpdateTxt.Text = ovlascenoLice.ImeOvlascenogLica;

                        this.Text = "ATM - Izmeni Ovlasceno Lice";

                        break;
                    }
                default: break;
            }
        }

        private void AkcijaOvlascenoLiceBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddUpdateTxt.Text == ""
                   )
                {
                    MessageBox.Show("Morate uneti ime ovlascenog lica koga zelite da dodate!");
                    return;
                }

                switch (this.type)
                {
                    case "add":
                        {
                            DTOs.RacunOvlascenoLiceBasic ovlascenoLice = new DTOs.RacunOvlascenoLiceBasic
                            {
                                ImeOvlascenogLica = AddUpdateTxt.Text,
                                PripadaRacunu = DTOManager.VratiRacun(racunId)
                            };

                            DTOManager.DodajOvlascenoLice(ovlascenoLice);

                            MessageBox.Show("Uspesno ste dodali novo ovlasceno lice!");

                            break;
                        }
                    case "update":
                        {
                            this.ovlascenoLice.ImeOvlascenogLica = AddUpdateTxt.Text;
                            this.ovlascenoLice.PripadaRacunu = DTOManager.VratiRacun(racunId);

                            DTOManager.IzmeniOvlascenoLice(this.ovlascenoLice);

                            MessageBox.Show("Uspesno ste izmenili ovlasceno lice!");

                            break;
                        }
                    default: break;
                }
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.ToString());
            }

            this.Close();
        }
    }
}
