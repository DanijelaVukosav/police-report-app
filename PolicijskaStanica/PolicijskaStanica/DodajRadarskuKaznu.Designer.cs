
namespace PolicijskaStanica
{
    partial class DodajRadarskuKaznu
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
            this.label1 = new System.Windows.Forms.Label();
            this.registracijskeTablice = new System.Windows.Forms.TextBox();
            this.prekoracenjeBrzine = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.vrijemeKazne = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dodajKaznu = new System.Windows.Forms.Button();
            this.izmijeniKaznu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Registracijske tablice:";
            // 
            // registracijskeTablice
            // 
            this.registracijskeTablice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registracijskeTablice.Location = new System.Drawing.Point(79, 43);
            this.registracijskeTablice.Name = "registracijskeTablice";
            this.registracijskeTablice.Size = new System.Drawing.Size(244, 26);
            this.registracijskeTablice.TabIndex = 1;
            // 
            // prekoracenjeBrzine
            // 
            this.prekoracenjeBrzine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prekoracenjeBrzine.Location = new System.Drawing.Point(79, 102);
            this.prekoracenjeBrzine.Name = "prekoracenjeBrzine";
            this.prekoracenjeBrzine.Size = new System.Drawing.Size(244, 26);
            this.prekoracenjeBrzine.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Prekoracenje brzine:";
            // 
            // vrijemeKazne
            // 
            this.vrijemeKazne.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vrijemeKazne.Location = new System.Drawing.Point(79, 169);
            this.vrijemeKazne.Name = "vrijemeKazne";
            this.vrijemeKazne.Size = new System.Drawing.Size(244, 26);
            this.vrijemeKazne.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Vrijeme(dd-MM-yyyy HH:mm):";
            // 
            // dodajKaznu
            // 
            this.dodajKaznu.Location = new System.Drawing.Point(247, 244);
            this.dodajKaznu.Name = "dodajKaznu";
            this.dodajKaznu.Size = new System.Drawing.Size(89, 34);
            this.dodajKaznu.TabIndex = 6;
            this.dodajKaznu.Text = "Dodaj kaznu";
            this.dodajKaznu.UseVisualStyleBackColor = true;
            this.dodajKaznu.Click += new System.EventHandler(this.dodajKaznu_Click);
            // 
            // izmijeniKaznu
            // 
            this.izmijeniKaznu.Location = new System.Drawing.Point(247, 284);
            this.izmijeniKaznu.Name = "izmijeniKaznu";
            this.izmijeniKaznu.Size = new System.Drawing.Size(89, 35);
            this.izmijeniKaznu.TabIndex = 7;
            this.izmijeniKaznu.Text = "Izmijeni kaznu";
            this.izmijeniKaznu.UseVisualStyleBackColor = true;
            this.izmijeniKaznu.Click += new System.EventHandler(this.izmijeniKaznu_Click);
            // 
            // DodajRadarskuKaznu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(386, 346);
            this.Controls.Add(this.izmijeniKaznu);
            this.Controls.Add(this.dodajKaznu);
            this.Controls.Add(this.vrijemeKazne);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.prekoracenjeBrzine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.registracijskeTablice);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DodajRadarskuKaznu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dodaj radarsku kaznu";
            this.Load += new System.EventHandler(this.DodajRadarskuKaznu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox registracijskeTablice;
        private System.Windows.Forms.TextBox prekoracenjeBrzine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox vrijemeKazne;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button dodajKaznu;
        private System.Windows.Forms.Button izmijeniKaznu;
    }
}