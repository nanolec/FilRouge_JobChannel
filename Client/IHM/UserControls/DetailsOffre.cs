using BLL_Client;
using BO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace IHM.UserControls
{
    /// <summary>
    /// Enumération Read            ==> Consultation des Offres
    ///             Creation        ==> Insert d'une offre
    ///             Modification    ==> Update d'une offre
    /// </summary>
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

        private ControllerC controller = new ControllerC();
        private Popup form2;

        public event EventHandler<Offre> OffreChanged;
        public event EventHandler RefreshListEvent;

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
        /// Affiche et type les colonnes de données
        /// </summary>
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

                    //((ComboBox)control).SelectedText = null;
                    //((ComboBox)control).SelectionLength = 0;
                    //((ComboBox)control).
                }

                formControls.Add(label, control);
                control.Dock = DockStyle.Fill;
                layout.Controls.Add(label_UI, 0, i);
                layout.Controls.Add(control, 1, i);
                i++;

            }
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

        /// <summary>
        /// Gestion du bouton Ajout de l'offre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        string titre = ((TextBox)formControls["Titre"]).Text;
                        string description = ((TextBox)formControls["Description"]).Text;
                        Poste poste = (Poste)((ComboBox)formControls["Type de Poste"]).SelectedItem;
                        Contrat contrat = (Contrat)((ComboBox)formControls["Type de Contrat"]).SelectedItem;
                        BO.Region region = (BO.Region)((ComboBox)formControls["Région"]).SelectedItem;
                        DateTime creation = (DateTime)((DateTimePicker)formControls["Date Publication"]).Value;
                        string lien = ((TextBox)formControls["Lien"]).Text;


                        Offre offre = new Offre(titre, description, poste, contrat, region, creation, lien );
                        int result = controller.InsertOffre(offre);

                        if (result == 1)
                        {
                            MessageBox.Show($"{result} offre a été ajoutée");
                        }
                        else
                        {
                            MessageBox.Show("Aucune offre n'a été ajoutée");
                        }
                        this.ParentForm.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Gestion du bouton Modification de l'offre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    if (true)
                    {
                        int? id = Offre.Id;
                        string titre = ((TextBox)formControls["Titre"]).Text;
                        string description = ((TextBox)formControls["Description"]).Text;
                        Poste poste = (Poste)((ComboBox)formControls["Type de Poste"]).SelectedItem;
                        Contrat contrat = (Contrat)((ComboBox)formControls["Type de Contrat"]).SelectedItem;
                        BO.Region region = (BO.Region)((ComboBox)formControls["Région"]).SelectedItem;
                        DateTime creation = (DateTime)((DateTimePicker)formControls["Date Publication"]).Value;
                        string lien = ((TextBox)formControls["Lien"]).Text;

                        Offre offre = new Offre(id, poste, contrat, region, titre, description, creation, lien);
                        int result = controller.UpdateOffre(offre);

                        if (result == 1)
                        {
                            MessageBox.Show($"{result} offre a été modifiéé");
                        }
                        else
                        {
                            MessageBox.Show("Aucune offre n'a été modifiée");
                        }
                        this.ParentForm.Close();
                    }
                }   
            }
        }

        /// <summary>
        /// Gestion du bouton suppression de l'offre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSupprimer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.MsgSup,
                Properties.Resources.MsgTitre,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (true)
                {
                    int result = controller.DeleteOffre(Offre.Id);

                    if (result == 1)
                    {
                        MessageBox.Show($"{result} offre a été supprimée");
                    }
                    else
                    {
                        MessageBox.Show("Aucune offre n'a été supprimée");
                    }
                RefreshListEvent(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Rafraichissement des données dans le tableau
        /// </summary>
        private void RefreshForm()
        {
            if (Offre != null)
            {
                foreach (KeyValuePair<string, Control> kp in formControls)
                {
                    if (mode != EModeDetailsOffre.READ_ONLY) kp.Value.Enabled = true;
                    else { 
                        kp.Value.Enabled = false;
                        // Règle pb affichage en bleu
                        if (kp.Value is ComboBox)
                        {
                            ((ComboBox)kp.Value).SelectedText = null;
                        }
                    }
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
                    // Règle pb affichage en bleu
                    if (kp.Value is ComboBox)
                    {
                        ((ComboBox)kp.Value).SelectedText = null;
                    }
                }
            }
            //this.formControls["Type de Poste"].Enabled = false;
        }

        /// <summary>
        /// Affichage de la Popup
        /// </summary>
        /// <param name="o"> Reçoit une offre</param>
        private void OpenPopup(Offre o)
        {
            using (form2 = new Popup())
            {
                using (DetailsOffre details = new DetailsOffre(o))
                {
                    form2.MainLayout2.Controls.Add(details);
                    form2.FormClosed += Form2_FormClosed;
                    form2.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Raffraichie le formulaire principal à la fermeture du formulaire secondaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            RefreshListEvent(this, new EventArgs());
        }
    }
    
}
