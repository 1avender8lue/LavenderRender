using System;
using System.Drawing;
using System.Windows.Forms;

namespace LavenderRender
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _pictureBox = new PictureBox();
            _renderButton = new Button();
            _saveButton = new Button();
            ((System.ComponentModel.ISupportInitialize)_pictureBox).BeginInit();
            SuspendLayout();
            // 
            // _pictureBox
            // 
            _pictureBox.Location = new Point(0, 0);
            _pictureBox.Name = "_pictureBox";
            _pictureBox.Size = new Size(800, 800);
            _pictureBox.TabIndex = 0;
            _pictureBox.TabStop = false;
            // 
            // _renderButton
            // 
            _renderButton.Font = new Font("Segoe UI", 14F);
            _renderButton.Location = new Point(922, 742);
            _renderButton.Name = "_renderButton";
            _renderButton.Size = new Size(100, 35);
            _renderButton.TabIndex = 1;
            _renderButton.Text = "Render";
            _renderButton.UseVisualStyleBackColor = true;
            _renderButton.Click += RenderButton_Click;
            // 
            // _saveButton
            // 
            _saveButton.Font = new Font("Segoe UI", 14F);
            _saveButton.Location = new Point(1058, 742);
            _saveButton.Name = "_saveButton";
            _saveButton.Size = new Size(100, 35);
            _saveButton.TabIndex = 2;
            _saveButton.Text = "Save";
            _saveButton.UseVisualStyleBackColor = true;
            _saveButton.Click += SaveButton_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 861);
            Controls.Add(_saveButton);
            Controls.Add(_renderButton);
            Controls.Add(_pictureBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lavender Render";
            ((System.ComponentModel.ISupportInitialize)_pictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox _pictureBox;
        private Button _renderButton;
        private Button _saveButton;
    }
}
