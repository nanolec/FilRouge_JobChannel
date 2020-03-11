using BLL_Client;
using BO;
using BO.DTO;
using IHM.UserControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IHM
{
    public partial class Form1 : Form
    {
        private ControllerC controller = new ControllerC();
        private GroupBox filtres = new GroupBox();
        private ComboBox comboBoxRegion = new ComboBox();
        private ComboBox comboBoxContrat = new ComboBox();
        private ComboBox comboBoxPoste = new ComboBox();
        private DateTimePicker dateTimePickerCreationStart = new DateTimePicker();
        private DateTimePicker dateTimePickerCreationEnd = new DateTimePicker();
        private Button razMokett = new Button();
        private TextBox compteur = new TextBox();
        private DataGridViewOptimized dataGridViewOffre = new DataGridViewOptimized();
        private BindingSource regionSource = new BindingSource();
        private BindingSource contratSource = new BindingSource();
        private BindingSource posteSource = new BindingSource();
        private BindingSource offreSource = new BindingSource();
        private DetailsOffre detailsOffre = new DetailsOffre();
        private FiltersOffreRequest filtersOffreRequest = new FiltersOffreRequest();
        private ToolTip toolTip = new ToolTip();

        //STATE
        private Offre _OffreSelected = null;
        public Offre OffreSelected
        {
            get
            {
                return this._OffreSelected;
            }
            set
            {
                detailsOffre.Offre = value;
                this._OffreSelected = value;
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

        public Form1()
        {
            InitializeComponent();

            Load += Form1_Load;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeFiltre();
            // Combox Régions
            RefreshSourceRegion();
            InitialiseComboBoxRegion();

            // Combox Contrats
            RefreshSourceContrat();
            InitialiseComboBoxContrat();

            // Combox Postes
            RefreshSourcePoste();
            InitialiseComboBoxPoste();

            // DateTimePicker Creation
            InitialiseDateTimePickerCreation();

            //InitDataGrid
            InitialiseDataGridOffre();

            //InitDetails
            InitialiseDetailsOffre();

            this.Text = "Liste des Offres";
            this.MinimumSize = new System.Drawing.Size(830, 500);

            // Set up the delays for the ToolTip.
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 300;
            toolTip.ReshowDelay = 1000;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip.ShowAlways = true;

            RefreshOffreSelectioned();
        }

        /// <summary>
        /// Paramètrage du GroupBox ... 
        /// Positionnement des filtres
        /// </summary>
        private void InitializeFiltre()
        {
            MainLayout.Controls.Add(filtres, 0, 0);
            filtres.Size = new System.Drawing.Size(794, 50);
            filtres.Dock = DockStyle.Fill;
            filtres.Location = new System.Drawing.Point(3, 3);
            filtres.TabIndex = 0;
            filtres.TabStop = false;
            filtres.Text = "Filtres";
            filtres.BackColor = System.Drawing.Color.AntiqueWhite;

            filtres.Controls.Add(comboBoxRegion);
            toolTip.SetToolTip(this.comboBoxRegion, "Fitre par Région");

            filtres.Controls.Add(comboBoxContrat);
            toolTip.SetToolTip(this.comboBoxContrat, "Fitre par Contrat");

            filtres.Controls.Add(comboBoxPoste);
            toolTip.SetToolTip(this.comboBoxPoste, "Fitre par Poste");

            filtres.Controls.Add(dateTimePickerCreationStart);
            dateTimePickerCreationStart.Location = new System.Drawing.Point(392, 13);
            dateTimePickerCreationStart.Size = new System.Drawing.Size(121, 24);
            dateTimePickerCreationStart.TabIndex = 3;
            toolTip.SetToolTip(this.dateTimePickerCreationStart, "Date de début");

            filtres.Controls.Add(dateTimePickerCreationEnd);
            dateTimePickerCreationEnd.Location = new System.Drawing.Point(519, 13);
            dateTimePickerCreationEnd.Size = new System.Drawing.Size(121, 24);
            dateTimePickerCreationEnd.TabIndex = TabIndex = 4;
            toolTip.SetToolTip(this.dateTimePickerCreationEnd, "Date de fin");

            filtres.Controls.Add(razMokett);
            razMokett.Location = new System.Drawing.Point(645, 12);
            razMokett.Size = new System.Drawing.Size(75, 23);
            razMokett.TabIndex = 5;
            razMokett.Text = "RAZ Filtres";
            razMokett.UseVisualStyleBackColor = true;
            razMokett.Click += new EventHandler(razMokett_Click);

            filtres.Controls.Add(compteur);
            compteur.Location = new System.Drawing.Point(780, 13);
            compteur.Size = new System.Drawing.Size(20, 23);
            //compteur.Dock = DockStyle.Right;
            toolTip.SetToolTip(this.compteur, "Nombre d'offres");
            //compteur.Text = dataGridViewOffre.RowCount.ToString();

        }

        /// <summary>
        /// Réinitialisation des filtres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void razMokett_Click(object sender, EventArgs e)
        {
            comboBoxRegion.SelectedIndex = 0;
            comboBoxPoste.SelectedIndex = 0;
            comboBoxContrat.SelectedIndex = 0;

            ResetIntervalle();
            InitialiseDateTimePickerCreation();
            RefreshOffreSelectioned();
        }

        /// <summary>
        /// Rafraichi la source des Régions
        /// </summary>
        private void RefreshSourceRegion()
        {
            List<Region> region = new List<Region>();
            region.Add(new Region() { Id = null, Nom = "ALL - Regions" });
            region.AddRange(controller.GetRegion());
            regionSource.DataSource = region;
        }

        /// <summary>
        /// Rafraichi la source des Contrats
        /// </summary>
        private void RefreshSourceContrat()
        {
            List<Contrat> contrat = new List<Contrat>();
            contrat.Add(new Contrat() { Id = null, Type = "ALL - Contrats" });
            contrat.AddRange(controller.GetContrat());
            contratSource.DataSource = contrat;
        }

        /// <summary>
        /// Rafraichi la source des Postes
        /// </summary>
        private void RefreshSourcePoste()
        {
            List<Poste> poste = new List<Poste>();
            poste.Add(new Poste() { Id = null, Type = "ALL - Poste" });
            poste.AddRange(controller.GetPoste());
            posteSource.DataSource = poste;
        }

        public async void RefreshOffreSelectioned()
        {

            Region region = (Region)comboBoxRegion.SelectedItem;
            Contrat contrat = (Contrat)comboBoxContrat.SelectedItem;
            Poste poste = (Poste)comboBoxPoste.SelectedItem;
            Intervalle intervalle = new Intervalle(dateTimePickerCreationStart.NullableValue(), dateTimePickerCreationEnd.NullableValue());

            // Fix Bug out of Range exception
            offreSource.DataSource = null;

            try
            {
            var t = controller.GetOffres(new FiltersOffreRequest(region, contrat, poste, intervalle));
            await t;
            offreSource.DataSource = t.Result;

            }
            catch { }

            if (dataGridViewOffre.RowCount > 0)
            {
            this.dataGridViewOffre.Columns["Id"].Visible = false;
            }

            compteur.Text = dataGridViewOffre.RowCount.ToString();
        }

        /// <summary>
        /// Initialise le comboBox pour choisir les régions
        /// </summary>
        public void InitialiseComboBoxRegion()
        {
            comboBoxRegion.Location = new System.Drawing.Point(9, 13);
            comboBoxRegion.TabIndex = 0;
            //comboBoxRegion.Dock = DockStyle.Left;

            comboBoxRegion.DataSource = regionSource;
            comboBoxRegion.DisplayMember = "Name";

            comboBoxRegion.SelectedValueChanged += ComboBoxRegion_SelectedValueChanged;
        }

        /// <summary>
        /// Initialise le comboBox pour choisir les contrats
        /// </summary>
        private void InitialiseComboBoxContrat()
        {
            comboBoxContrat.Location = new System.Drawing.Point(137, 13);
            comboBoxContrat.TabIndex = 1;
            //comboBoxContrat.Dock = DockStyle.Left;

            comboBoxContrat.DataSource = contratSource;
            comboBoxContrat.DisplayMember = "Type";

            comboBoxContrat.SelectedValueChanged += ComboBoxContrat_SelectedValueChanged;
        }

        /// <summary>
        /// Initialise le comboBox pour choisir les postes
        /// </summary>
        private void InitialiseComboBoxPoste()
        {
            comboBoxPoste.Location = new System.Drawing.Point(265, 13);
            comboBoxPoste.TabIndex = 2;
            //comboBoxPoste.Dock = DockStyle.Left;

            comboBoxPoste.DataSource = posteSource;
            comboBoxPoste.DisplayMember = "Type";

            comboBoxPoste.SelectedValueChanged += ComboBoxPoste_SelectedValueChanged;
        }

        /// <summary>
        /// Initialise le DateTimePicker pour choisir la date de création de l'offre
        /// </summary>
        private void InitialiseDateTimePickerCreation()
        {
            ResetIntervalle();
            dateTimePickerCreationStart.ValueChanged += DateTimePickerCreation_ValueChanged;
            dateTimePickerCreationEnd.ValueChanged += DateTimePickerCreation_ValueChanged;
        }

        private void ResetIntervalle()
        {
            dateTimePickerCreationStart.NullableValue(null);
            dateTimePickerCreationEnd.NullableValue(null);
        }

        private void DateTimePickerCreation_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            dtp.NullableValue(dtp.Value);
            RefreshOffreSelectioned();
        }

        private void ComboBoxRegion_SelectedValueChanged(object sender, EventArgs e)
        {
            Region region = (Region)comboBoxRegion.SelectedValue;
            if(region.Id != null)
            {
                filtersOffreRequest.region = region;
            }
            else filtersOffreRequest.region = null;

            RefreshOffreSelectioned();
        }

        private void ComboBoxContrat_SelectedValueChanged(object sender, EventArgs e)
        {
            RefreshOffreSelectioned();
        }

        private void ComboBoxPoste_SelectedValueChanged(object sender, EventArgs e)
        {
            RefreshOffreSelectioned();
        }

        /// <summary>
        /// Initialise la datagrid pour voir la liste des offres en fonction de la région choisi
        /// </summary>
        private void InitialiseDataGridOffre()
        {
            MainLayout.Controls.Add(dataGridViewOffre, 0, 1);
            dataGridViewOffre.Dock = DockStyle.Fill;

            dataGridViewOffre.ReadOnly = true;
            dataGridViewOffre.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //Source
            dataGridViewOffre.DataSource = offreSource;
            //Style
            dataGridViewOffre.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewOffre.RowHeadersVisible = false;
            //Bind 
            dataGridViewOffre.SelectionChanged += DataGridViewOffre_SelectionChanged;

            dataGridViewOffre.AllowUserToAddRows = false;
            dataGridViewOffre.AllowUserToResizeRows = false;
            dataGridViewOffre.MultiSelect = false;

        }

        /// <summary>
        /// Stratégie quand on sélectionne une région dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewOffre_SelectionChanged(object sender, EventArgs e)
        {
            var row = dataGridViewOffre.CurrentRow;
            if (row != null)
            {
                OffreSelected = (Offre)row.DataBoundItem;
            }
            else
            {
                OffreSelected = null;
            }
        }

        private void InitialiseDetailsOffre()
        {
            MainLayout.Controls.Add(detailsOffre, 0, 2);
        }
    }
}
