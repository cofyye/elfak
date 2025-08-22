using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Kartica
{
    public partial class Form_Kartica_AddUpdate : Form
    {
        private readonly string type = "";
        private readonly int racunId = -1;
        private readonly ATM_WinForm.DTOs.KarticaBasic kartica = null;

        public Form_Kartica_AddUpdate(string type, ATM_WinForm.DTOs.KarticaBasic kartica, int racunId)
        {
            InitializeComponent();

            TipComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            TipComboBox.SelectedIndex = 0;

            this.type = type;
            this.kartica = kartica;
            this.racunId = racunId;


            // Subscribe to the SelectedIndexChanged event
            //TipComboBox.SelectedIndexChanged += TipComboBox_SelectedIndexChanged;

            //MaxIznosZaduzenjaTxtBx.Enabled = false;
            //MaxDatumVracanjaDugaDateTimePicker.Enabled = false;

            switch (this.type)
            {
                case "add":
                    {
                        AkcijaKarticeBtn.Text = "Dodaj Karticu";

                        this.Text = "ATM - Dodaj Karticu";

                        break;
                    }
                case "update":
                    {
                        AkcijaKarticeBtn.Text = "Izmeni Karticu";

                        DatumIzdavanjaDateTimePicker.Text = kartica.Datum_izdavanje.ToString();
                        DatumIstekaDateTimePicker.Text = kartica.Datum_isteka.ToString();
                        DnevniLimitTxtBx.Text = kartica.Dnevni_limit;
                        TipComboBox.Text = kartica.Tip;
                        TipComboBox.Enabled = false;

                        if(kartica.Tip == "kreditna")
                        {
                            MaxIznosZaduzenjaTxtBx.Text = kartica.Max_iznos_zaduzenja;
                            MaxDatumVracanjaDugaDateTimePicker.Text = kartica.Max_datum_vracanja_duga.ToString();
                        }

                        this.Text = "ATM - Izmeni Karticu";

                        break;
                    }
                default: break;
            }
        }

        private void AkcijaKarticeBtn_Click(object sender, EventArgs e)
        {

            try
            {
                if (DatumIzdavanjaDateTimePicker.Text == "" ||
                   DatumIstekaDateTimePicker.Text == "" || DnevniLimitTxtBx.Text == ""
                   || TipComboBox.Text == ""
                   )
                {
                    MessageBox.Show("Polja ne smeju biti prazna!");
                    return;
                }

                switch (this.type)
                {
                    case "add":
                        {
                            DTOs.KarticaBasic kartica = new DTOs.KarticaBasic
                            {
                                Datum_izdavanje = DatumIzdavanjaDateTimePicker.Value,
                                Datum_isteka = DatumIstekaDateTimePicker.Value,
                                Dnevni_limit = DnevniLimitTxtBx.Text,
                                Tip = TipComboBox.SelectedItem.ToString(),
                                Max_iznos_zaduzenja = MaxIznosZaduzenjaTxtBx.Text,
                                Max_datum_vracanja_duga = MaxDatumVracanjaDugaDateTimePicker.Value,
                                Odgovara = DTOManager.VratiRacun(racunId)
                            };

                            DTOManager.DodajKarticu(kartica);

                            MessageBox.Show("Uspesno ste dodali karticu!");

                            break;
                        }
                    case "update":
                        {
                            this.kartica.Datum_izdavanje = DatumIzdavanjaDateTimePicker.Value;
                            this.kartica.Datum_isteka = DatumIstekaDateTimePicker.Value;
                            this.kartica.Dnevni_limit = DnevniLimitTxtBx.Text;
                            this.kartica.Tip = TipComboBox.SelectedItem.ToString();

                            if (this.kartica.Tip == "kreditna")
                            {
                                this.kartica.Max_iznos_zaduzenja = MaxIznosZaduzenjaTxtBx.Text;
                                this.kartica.Max_datum_vracanja_duga = MaxDatumVracanjaDugaDateTimePicker.Value;
                            }
                            this.kartica.Odgovara = DTOManager.VratiRacun(racunId);

                            DTOManager.IzmeniKarticu(this.kartica);

                            MessageBox.Show("Uspesno ste izmenili karticu!");

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

        private void TipComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(TipComboBox.SelectedItem.ToString() == "kreditna")
            {
                MaxIznosZaduzenjaTxtBx.Enabled = true;
                MaxDatumVracanjaDugaDateTimePicker.Enabled = true;
            }
            else
            {
                MaxIznosZaduzenjaTxtBx.Enabled = false;
                MaxDatumVracanjaDugaDateTimePicker.Enabled = false;
            }
        }
    }
}
