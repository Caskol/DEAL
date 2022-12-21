namespace DEAL
{
    partial class Form1
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
            this.textBoxKeyDES = new System.Windows.Forms.TextBox();
            this.textBoxKeyDEAL1 = new System.Windows.Forms.TextBox();
            this.labelKeys = new System.Windows.Forms.Label();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.buttonDecrypt = new System.Windows.Forms.Button();
            this.buttonEncrypt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxKeyDES
            // 
            this.textBoxKeyDES.Location = new System.Drawing.Point(12, 27);
            this.textBoxKeyDES.Name = "textBoxKeyDES";
            this.textBoxKeyDES.PlaceholderText = "Введите 7 символов ключа DES";
            this.textBoxKeyDES.Size = new System.Drawing.Size(371, 23);
            this.textBoxKeyDES.TabIndex = 1;
            // 
            // textBoxKeyDEAL1
            // 
            this.textBoxKeyDEAL1.Location = new System.Drawing.Point(389, 27);
            this.textBoxKeyDEAL1.Name = "textBoxKeyDEAL1";
            this.textBoxKeyDEAL1.PlaceholderText = "Введите 16 символов ключа DEAL";
            this.textBoxKeyDEAL1.Size = new System.Drawing.Size(353, 23);
            this.textBoxKeyDEAL1.TabIndex = 6;
            // 
            // labelKeys
            // 
            this.labelKeys.AutoSize = true;
            this.labelKeys.Location = new System.Drawing.Point(12, 9);
            this.labelKeys.Name = "labelKeys";
            this.labelKeys.Size = new System.Drawing.Size(45, 15);
            this.labelKeys.TabIndex = 8;
            this.labelKeys.Text = "Ключи";
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxOutput.Location = new System.Drawing.Point(12, 222);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOutput.Size = new System.Drawing.Size(729, 209);
            this.textBoxOutput.TabIndex = 9;
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(12, 56);
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxInput.Size = new System.Drawing.Size(564, 160);
            this.textBoxInput.TabIndex = 10;
            // 
            // buttonDecrypt
            // 
            this.buttonDecrypt.Location = new System.Drawing.Point(582, 143);
            this.buttonDecrypt.Name = "buttonDecrypt";
            this.buttonDecrypt.Size = new System.Drawing.Size(159, 73);
            this.buttonDecrypt.TabIndex = 11;
            this.buttonDecrypt.Text = "Расшифровать текст";
            this.buttonDecrypt.UseVisualStyleBackColor = true;
            this.buttonDecrypt.Click += new System.EventHandler(this.buttonDecrypt_Click);
            // 
            // buttonEncrypt
            // 
            this.buttonEncrypt.Location = new System.Drawing.Point(582, 56);
            this.buttonEncrypt.Name = "buttonEncrypt";
            this.buttonEncrypt.Size = new System.Drawing.Size(159, 71);
            this.buttonEncrypt.TabIndex = 12;
            this.buttonEncrypt.Text = "Зашифровать текст";
            this.buttonEncrypt.UseVisualStyleBackColor = true;
            this.buttonEncrypt.Click += new System.EventHandler(this.buttonEncrypt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(754, 443);
            this.Controls.Add(this.buttonEncrypt);
            this.Controls.Add(this.buttonDecrypt);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.labelKeys);
            this.Controls.Add(this.textBoxKeyDEAL1);
            this.Controls.Add(this.textBoxKeyDES);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "DEAL-128 Cipher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox textBoxKeyDES;
        private TextBox textBoxKeyDEAL1;
        private Label labelKeys;
        private TextBox textBoxOutput;
        private TextBox textBoxInput;
        private Button buttonDecrypt;
        private Button buttonEncrypt;
    }
}