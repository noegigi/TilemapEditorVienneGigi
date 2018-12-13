namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.TileCanvas = new System.Windows.Forms.PictureBox();
            this.MapCanvas = new System.Windows.Forms.PictureBox();
            this.LoadTileMap = new System.Windows.Forms.Button();
            this.LoadMap = new System.Windows.Forms.Button();
            this.Erase = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.TileWidthLabel = new System.Windows.Forms.Label();
            this.LabelTileHeight = new System.Windows.Forms.Label();
            this.LabelMarginHeight = new System.Windows.Forms.Label();
            this.LabelMarginWidth = new System.Windows.Forms.Label();
            this.TileWidth = new System.Windows.Forms.NumericUpDown();
            this.TileHeight = new System.Windows.Forms.NumericUpDown();
            this.MarginWidth = new System.Windows.Forms.NumericUpDown();
            this.MarginHeight = new System.Windows.Forms.NumericUpDown();
            this.ValidateTileMapProp = new System.Windows.Forms.Button();
            this.TileMapProperties = new System.Windows.Forms.GroupBox();
            this.PanelProperties = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.TileCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TileWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TileHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarginWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarginHeight)).BeginInit();
            this.TileMapProperties.SuspendLayout();
            this.PanelProperties.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TileCanvas
            // 
            this.TileCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TileCanvas.Location = new System.Drawing.Point(0, 0);
            this.TileCanvas.Name = "TileCanvas";
            this.TileCanvas.Size = new System.Drawing.Size(165, 396);
            this.TileCanvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.TileCanvas.TabIndex = 0;
            this.TileCanvas.TabStop = false;
            this.TileCanvas.Click += new System.EventHandler(this.TileCanvas_Click);
            this.TileCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TileCanvas_MouseMove);
            // 
            // MapCanvas
            // 
            this.MapCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MapCanvas.Location = new System.Drawing.Point(183, 42);
            this.MapCanvas.Name = "MapCanvas";
            this.MapCanvas.Size = new System.Drawing.Size(608, 396);
            this.MapCanvas.TabIndex = 1;
            this.MapCanvas.TabStop = false;
            this.MapCanvas.Click += new System.EventHandler(this.MapCanvas_Click);
            this.MapCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapCanvas_MouseDown);
            this.MapCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapCanvas_MouseMove);
            this.MapCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MapCanvas_MouseUp);
            // 
            // LoadTileMap
            // 
            this.LoadTileMap.Location = new System.Drawing.Point(50, 12);
            this.LoadTileMap.Name = "LoadTileMap";
            this.LoadTileMap.Size = new System.Drawing.Size(75, 23);
            this.LoadTileMap.TabIndex = 2;
            this.LoadTileMap.Text = "Charger";
            this.LoadTileMap.UseVisualStyleBackColor = true;
            this.LoadTileMap.Click += new System.EventHandler(this.LoadTileMap_Click);
            // 
            // LoadMap
            // 
            this.LoadMap.Location = new System.Drawing.Point(250, 12);
            this.LoadMap.Name = "LoadMap";
            this.LoadMap.Size = new System.Drawing.Size(75, 23);
            this.LoadMap.TabIndex = 3;
            this.LoadMap.Text = "Charger";
            this.LoadMap.UseVisualStyleBackColor = true;
            this.LoadMap.Click += new System.EventHandler(this.LoadMap_Click);
            // 
            // Erase
            // 
            this.Erase.Location = new System.Drawing.Point(369, 12);
            this.Erase.Name = "Erase";
            this.Erase.Size = new System.Drawing.Size(75, 23);
            this.Erase.TabIndex = 4;
            this.Erase.Text = "Effacer";
            this.Erase.UseVisualStyleBackColor = true;
            this.Erase.Click += new System.EventHandler(this.Erase_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(501, 12);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 5;
            this.Cancel.Text = "Annuler";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(628, 12);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(80, 23);
            this.Save.TabIndex = 6;
            this.Save.Text = "Sauvegarder";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // TileWidthLabel
            // 
            this.TileWidthLabel.AutoSize = true;
            this.TileWidthLabel.Location = new System.Drawing.Point(32, 27);
            this.TileWidthLabel.Name = "TileWidthLabel";
            this.TileWidthLabel.Size = new System.Drawing.Size(55, 13);
            this.TileWidthLabel.TabIndex = 1;
            this.TileWidthLabel.Text = "Tile Width";
            // 
            // LabelTileHeight
            // 
            this.LabelTileHeight.AutoSize = true;
            this.LabelTileHeight.Location = new System.Drawing.Point(102, 27);
            this.LabelTileHeight.Name = "LabelTileHeight";
            this.LabelTileHeight.Size = new System.Drawing.Size(58, 13);
            this.LabelTileHeight.TabIndex = 3;
            this.LabelTileHeight.Text = "Tile Height";
            // 
            // LabelMarginHeight
            // 
            this.LabelMarginHeight.AutoSize = true;
            this.LabelMarginHeight.Location = new System.Drawing.Point(102, 84);
            this.LabelMarginHeight.Name = "LabelMarginHeight";
            this.LabelMarginHeight.Size = new System.Drawing.Size(73, 13);
            this.LabelMarginHeight.TabIndex = 7;
            this.LabelMarginHeight.Text = "Margin Height";
            // 
            // LabelMarginWidth
            // 
            this.LabelMarginWidth.AutoSize = true;
            this.LabelMarginWidth.Location = new System.Drawing.Point(32, 84);
            this.LabelMarginWidth.Name = "LabelMarginWidth";
            this.LabelMarginWidth.Size = new System.Drawing.Size(70, 13);
            this.LabelMarginWidth.TabIndex = 5;
            this.LabelMarginWidth.Text = "Margin Width";
            // 
            // TileWidth
            // 
            this.TileWidth.Location = new System.Drawing.Point(35, 44);
            this.TileWidth.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.TileWidth.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.TileWidth.Name = "TileWidth";
            this.TileWidth.Size = new System.Drawing.Size(52, 20);
            this.TileWidth.TabIndex = 8;
            this.TileWidth.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // TileHeight
            // 
            this.TileHeight.Location = new System.Drawing.Point(105, 44);
            this.TileHeight.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.TileHeight.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.TileHeight.Name = "TileHeight";
            this.TileHeight.Size = new System.Drawing.Size(52, 20);
            this.TileHeight.TabIndex = 9;
            this.TileHeight.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // MarginWidth
            // 
            this.MarginWidth.Location = new System.Drawing.Point(35, 101);
            this.MarginWidth.Name = "MarginWidth";
            this.MarginWidth.Size = new System.Drawing.Size(52, 20);
            this.MarginWidth.TabIndex = 10;
            // 
            // MarginHeight
            // 
            this.MarginHeight.Location = new System.Drawing.Point(105, 101);
            this.MarginHeight.Name = "MarginHeight";
            this.MarginHeight.Size = new System.Drawing.Size(52, 20);
            this.MarginHeight.TabIndex = 11;
            // 
            // ValidateTileMapProp
            // 
            this.ValidateTileMapProp.Location = new System.Drawing.Point(56, 182);
            this.ValidateTileMapProp.Name = "ValidateTileMapProp";
            this.ValidateTileMapProp.Size = new System.Drawing.Size(75, 23);
            this.ValidateTileMapProp.TabIndex = 12;
            this.ValidateTileMapProp.Text = "Confirm";
            this.ValidateTileMapProp.UseVisualStyleBackColor = true;
            this.ValidateTileMapProp.Click += new System.EventHandler(this.ValidateTileMapProp_Click);
            // 
            // TileMapProperties
            // 
            this.TileMapProperties.Controls.Add(this.ValidateTileMapProp);
            this.TileMapProperties.Controls.Add(this.LabelMarginHeight);
            this.TileMapProperties.Controls.Add(this.MarginHeight);
            this.TileMapProperties.Controls.Add(this.TileWidthLabel);
            this.TileMapProperties.Controls.Add(this.MarginWidth);
            this.TileMapProperties.Controls.Add(this.LabelTileHeight);
            this.TileMapProperties.Controls.Add(this.TileHeight);
            this.TileMapProperties.Controls.Add(this.LabelMarginWidth);
            this.TileMapProperties.Controls.Add(this.TileWidth);
            this.TileMapProperties.Location = new System.Drawing.Point(5, 3);
            this.TileMapProperties.Name = "TileMapProperties";
            this.TileMapProperties.Size = new System.Drawing.Size(200, 222);
            this.TileMapProperties.TabIndex = 13;
            this.TileMapProperties.TabStop = false;
            this.TileMapProperties.Text = "Tilemap Properties";
            // 
            // PanelProperties
            // 
            this.PanelProperties.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelProperties.Controls.Add(this.TileMapProperties);
            this.PanelProperties.Location = new System.Drawing.Point(313, 116);
            this.PanelProperties.Name = "PanelProperties";
            this.PanelProperties.Size = new System.Drawing.Size(213, 230);
            this.PanelProperties.TabIndex = 14;
            this.PanelProperties.Visible = false;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.TileCanvas);
            this.panel1.Location = new System.Drawing.Point(12, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 397);
            this.panel1.TabIndex = 15;
            this.panel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel1_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PanelProperties);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Erase);
            this.Controls.Add(this.LoadMap);
            this.Controls.Add(this.LoadTileMap);
            this.Controls.Add(this.MapCanvas);
            this.Name = "Form1";
            this.Text = "TileMap Editor";
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.TileCanvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapCanvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TileWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TileHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarginWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarginHeight)).EndInit();
            this.TileMapProperties.ResumeLayout(false);
            this.TileMapProperties.PerformLayout();
            this.PanelProperties.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox TileCanvas;
        private System.Windows.Forms.PictureBox MapCanvas;
        private System.Windows.Forms.Button LoadTileMap;
        private System.Windows.Forms.Button LoadMap;
        private System.Windows.Forms.Button Erase;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.NumericUpDown MarginHeight;
        private System.Windows.Forms.NumericUpDown MarginWidth;
        private System.Windows.Forms.NumericUpDown TileHeight;
        private System.Windows.Forms.NumericUpDown TileWidth;
        private System.Windows.Forms.Label LabelMarginHeight;
        private System.Windows.Forms.Label LabelMarginWidth;
        private System.Windows.Forms.Label LabelTileHeight;
        private System.Windows.Forms.Label TileWidthLabel;
        private System.Windows.Forms.Button ValidateTileMapProp;
        private System.Windows.Forms.GroupBox TileMapProperties;
        private System.Windows.Forms.Panel PanelProperties;
        private System.Windows.Forms.Panel panel1;
    }
}

