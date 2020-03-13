using BLL_Client;
using BO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace IHM.UserControls
{
    public enum EModeDetailsOffre
    {
        READ_ONLY,
        CREATION,
        MODIFICATION
    }
    public partial class DetailsOffre : UserControl
    {
        public EModeDetailsOffre mode = EModeDetailsOffre.READ_ONLY;

        private TableLayoutPanel layout = new TableLayoutPanel();
        private FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
        private ButtonBase buttonSupprimer = new Button() { Text = "SUPPRIMER", Size = new Size(100, 23) };
        private Button buttonModifier = new Button() { Text = "MODIFIER", Size = new Size(100, 23) };
        private Button buttonADD = new Button() { Text = "AJOUTER", Size = new Size(100, 23) };


        public event EventHandler<Offre> OffreChanged;


        private Offre _Offre;
        public Offre Offre
        {
            get { return this._Offre; }
            set
            {
                this._Offre = value;
                if (value == null && mode == EModeDetailsOffre.READ_ONLY)
                    this.Enabled = false;
                else
                {
                    this.RefreshForm();
                    this.Enabled = true;
                 }

             
            }
        }

        public Dictionary<string, Control> formControls = new Dictionary<string, Control>();

        public DetailsOffre()
        {
            InitializeComponent();
            Initialize();
            InitializeForm();
            this.Enabled = false;
        }

        public DetailsOffre(Offre offreIn) : this()
        {
            if (offreIn == null)
            {
                mode = EModeDetailsOffre.CREATION;
                Offre = offreIn;
            }
            else {
                mode = EModeDetailsOffre.MODIFICATION;
                Offre = offreIn;
            }
        }
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (Parent == null) return;
            switch(mode)
            {
                case EModeDetailsOffre.CREATION:
                    ParentForm.Text = "Ajouter une offre";

                    buttonSupprimer.Visible = false;
                    buttonModifier.Visible = false;

                    break;
                case EModeDetailsOffre.MODIFICATION:
                    ParentForm.Text = "Modification de l'Offre";

                    buttonADD.Visible = false;
                    buttonSupprimer.Visible = false;

                    break;
            }
            ParentForm.Size = new System.Drawing.Size(830, 260);
            ParentForm.MinimumSize = new System.Drawing.Size(830, 260);

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

            this.Controls.Add(layout);
            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 2;
            layout.RowCount = 9;

            layout.Controls.Add(flowLayoutPanel, 1, 9);
            flowLayoutPanel.Dock = DockStyle.Fill;

            buttonModifier.Click += ButtonModifier_Click;
            buttonModifier.TabIndex = 0;
            flowLayoutPanel.Controls.Add(buttonModifier);

            buttonADD.Click += ButtonADD_Click;
            buttonADD.TabIndex = 1;
            flowLayoutPanel.Controls.Add(buttonADD);

            buttonSupprimer.Click += ButtonSupprimer_Click;
            flowLayoutPanel.Controls.Add(buttonSupprimer);

        }

        private void ButtonADD_Click(object sender, EventArgs e)
        {
            if (mode == EModeDetailsOffre.READ_ONLY)
            {
                OpenPopup(null);
            }
            else
            {
                if (MessageBox.Show(Properties.Resources.MsgAdd,
                Properties.Resources.MsgTitre,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (true)
                    {

                    }
                }
            }
        }

        private void ButtonSupprimer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.MsgSup,
                Properties.Resources.MsgTitre,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {

            }
        }

        private void ButtonModifier_Click(object sender, EventArgs e)
        {
            if (mode == EModeDetailsOffre.READ_ONLY)
            {
                OpenPopup(Offre);
            }
            else
            {
                if (MessageBox.Show(Properties.Resources.MsgModif,
                Properties.Resources.MsgTitre,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {

                }   
            }
        }

        private void InitializeForm()
        {
            List<string> Labels = new List<string>() { "Titre", "Description", "Région", "Type de Contrat", "Type de Poste", "Date Publication", "Lien" };
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
                control.Dock = DockStyle.Fill;
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
                    if (mode != EModeDetailsOffre.READ_ONLY) kp.Value.Enabled = true;
                    else kp.Value.Enabled = false;
                }

                this.formControls["Titre"].Text = Offre.Titre;
                this.formControls["Description"].Text = Offre.Description;
                this.formControls["Région"].Text = Offre.Region.Nom;
                this.formControls["Type de Contrat"].Text = Offre.Contrat.Type;
                this.formControls["Type de Poste"].Text = Offre.Poste.Type;
                ((DateTimePicker)this.formControls["Date Publication"]).Value = Offre.Creation.Value;
                this.formControls["Lien"].Text = Offre.Lien;

                //  ((ComboBox)this.formControls["Region"]).SelectedValue = Offre.Region.Nom;
            }
            else
            {
                foreach (KeyValuePair<string, Control> kp in formControls)
                {
                    kp.Value.ResetText();
                }
            }
            //this.formControls["Type de Poste"].Enabled = false;
        }

        private void OpenPopup(Offre o)
        {
            using (Popup form2 = new Popup())
            {
                using (DetailsOffre details = new DetailsOffre(o))
                {
                    form2.MainLayout2.Controls.Add(details);
                    form2.ShowDialog();
                }
            }
        }
    }
    
}
