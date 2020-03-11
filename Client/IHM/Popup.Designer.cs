namespace IHM
{
    partial class Popup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainLayout2 = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // MainLayout2
            // 
            this.MainLayout2.AutoSize = true;
            this.MainLayout2.ColumnCount = 1;
            this.MainLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainLayout2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayout2.Location = new System.Drawing.Point(0, 0);
            this.MainLayout2.Name = "MainLayout2";
            this.MainLayout2.RowCount = 1;
            this.MainLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainLayout2.Size = new System.Drawing.Size(678, 292);
            this.MainLayout2.TabIndex = 0;
            // 
            // Popup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 292);
            this.Controls.Add(this.MainLayout2);
            this.Name = "Popup";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TableLayoutPanel MainLayout2;
    }
}