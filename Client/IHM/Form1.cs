using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BO;
using BLL_Client;
using IHM.UserControls;
using BO.DTO;
using System.Threading.Tasks;

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
        private DataGridViewOptimized dataGridViewOffre = new DataGridViewOptimized();
        private BindingSource regionSource = new BindingSource();
        private BindingSource contratSource = new BindingSource();
        private BindingSource posteSource = new BindingSource();
        private BindingSource offreSource = new BindingSource();
        private DetailsOffre detailsOffre = new DetailsOffre();
        private FiltersOffreRequest filtersOffreRequest = new FiltersOffreRequest();

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

            RefreshOffreSelectioned();
        }


        /// <summary>
        /// Paramètrage du GroupBox 
        /// </summary>
        private void InitializeFiltre()
        {
            filtres.Controls.Add(comboBoxRegion);

            filtres.Controls.Add(comboBoxContrat);

            filtres.Controls.Add(comboBoxPoste);

            filtres.Controls.Add(dateTimePickerCreationStart);
            dateTimePickerCreationStart.Location = new System.Drawing.Point(393, 13);
            dateTimePickerCreationStart.TabIndex = 3;

            filtres.Controls.Add(dateTimePickerCreationEnd);
            dateTimePickerCreationEnd.Location = new System.Drawing.Point(599, 13);
            dateTimePickerCreationEnd.TabIndex = TabIndex = 4; 

            MainLayout.Controls.Add(filtres, 0, 0);
            filtres.Size = new System.Drawing.Size(794, 50);
            filtres.Dock = DockStyle.Fill;
            filtres.Location = new System.Drawing.Point(3, 3);
            filtres.TabIndex = 0;
            filtres.TabStop = false;
            filtres.Text = "Filtres";
            filtres.BackColor = System.Drawing.Color.AntiqueWhite;

        }

        /// <summary>
        /// Rafraichi la source des Régions
        /// </summary>
        private void RefreshSourceRegion()
        {
            List<Region> region = new List<Region>();
            region.Add(new Region() { Id = null, Nom = "ALL" });
            region.AddRange(controller.GetRegion());
            regionSource.DataSource = region;
        }

        /// <summary>
        /// Rafraichi la source des Contrats
        /// </summary>
        private void RefreshSourceContrat()
        {
            List<Contrat> contrat = new List<Contrat>();
            contrat.Add(new Contrat() { Id = null, Type = "ALL" });
            contrat.AddRange(controller.GetContrat());
            contratSource.DataSource = contrat;
        }

        /// <summary>
        /// Rafraichi la source des Postes
        /// </summary>
        private void RefreshSourcePoste()
        {
            List<Poste> poste = new List<Poste>();
            poste.Add(new Poste() { Id = null, Type = "ALL" });
            poste.AddRange(controller.GetPoste());
            posteSource.DataSource = poste;
        }

        public async void RefreshOffreSelectioned()
        {

            BO.Region region = (Region)comboBoxRegion.SelectedItem;
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
            detailsOffre.OffreChanged += DetailOffre_OffreChanged;
        }

        private void DetailOffre_OffreChanged(object sender, Offre e)
        {
            throw new NotImplementedException();
        }

    }
}
