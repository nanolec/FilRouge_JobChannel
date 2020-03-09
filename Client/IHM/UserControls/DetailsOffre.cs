using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BO;
using BLL_Client;

namespace IHM.UserControls
{
    public partial class DetailsOffre : UserControl
    {
        private TableLayoutPanel layout = new TableLayoutPanel();

        public event EventHandler<Offre> OffreChanged;

        private Offre _Offre;
        public Offre Offre
        {
            get { return this._Offre; }
            set
            {
                this._Offre = value;
                this.RefreshForm();
            }
        }

        public Dictionary<string, Control> formControls = new Dictionary<string, Control>();

        public DetailsOffre()
        {
            InitializeComponent();
            Initialize();
            InitializeForm();
        }

        /// <summary>
        /// Active le double buffer
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private ControllerC controller = new ControllerC();

        private void Initialize()
        {
            // Configuration
            this.Dock = DockStyle.Fill;
            this.AutoSize = true;
            //

            this.Controls.Add(layout);
            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 2;
            layout.RowCount = 9;

            Button buttonModifier = new Button() { Text = "MODIFIER" };

            buttonModifier.Click += ButtonModifier_Click;
            buttonModifier.Dock = DockStyle.Right;
            layout.Controls.Add(buttonModifier, 1, 7);
        }

        private void ButtonModifier_Click(object sender, EventArgs e)
        {
            OffreChanged?.Invoke(this, Offre);
        }

        private void InitializeForm()
        {
            List<string> Labels = new List<string>() { "Titre", "Description", "Type de Poste", "Type de Contrat", "Date Publication", "Lien", "Région" };
            int i = 0;

            foreach (string label in Labels)
            {
                Label label_UI = new Label() { Text = label + " :" };
                label_UI.Dock = DockStyle.Top;


                Control control = new TextBox();
                if (label == "Date Publication")
                {
                    control = new DateTimePicker();
                }
                else if (label == "Type de Poste" || label == "Type de Contrat" || label == "Région")
                {
                    BindingSource bs = new BindingSource();

                    control = new ComboBox();
                    ((ComboBox)control).DataSource = bs;
                    ((ComboBox)control).DisplayMember = "Name";
                    ((ComboBox)control).ValueMember = "Id";

                    if (label == "Type de Poste")
                    {
                        bs.DataSource = controller.GetPoste();
                    }
                    else if (label == "Type de Contrat")
                    {
                        bs.DataSource = controller.GetContrat();
                    }
                    else if (label == "Région")
                    {
                        bs.DataSource = controller.GetRegion();
                    }
                    else
                        bs.DataSource = controller.GetOffres();
                }

                formControls.Add(label, control);
                layout.Controls.Add(label_UI, 0, i);
                layout.Controls.Add(control, 1, i);
                i++;

            }
        }

        private void RefreshForm()
        {
            if (Offre != null)
            {
                foreach (KeyValuePair<string, Control> kp in formControls)
                {
                    kp.Value.Enabled = true;
                }

                //"Titre", "Description", "Type de Poste", "Type de Contrat", "Date Publication", "Lien", "Région"

                this.formControls["Titre"].Text = Offre.Titre;
                this.formControls["Description"].Text = Offre.Description;
                this.formControls["Type de Poste"].Text = Offre.Poste.Type;
                this.formControls["Type de Contrat"].Text = Offre.Contrat.Type;
                ((DateTimePicker)this.formControls["Date Publication"]).Value = Offre.Creation.Value;
                this.formControls["Lien"].Text = Offre.Lien;
                this.formControls["Région"].Text = Offre.Region.Nom;

              //  ((ComboBox)this.formControls["Region"]).SelectedValue = Offre.Region.Nom;
            }
            else
            {
                foreach (KeyValuePair<string, Control> kp in formControls)
                {
                    kp.Value.ResetText();
                    kp.Value.Enabled = false;
                }
            }
            this.formControls["Type de Poste"].Enabled = false;
        }
    }
    
}
