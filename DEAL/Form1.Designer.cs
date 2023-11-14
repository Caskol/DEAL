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
            textBoxKeyDES = new TextBox();
            textBoxKeyDEAL = new TextBox();
            labelKeys = new Label();
            textBoxOutput = new TextBox();
            textBoxInput = new TextBox();
            buttonDecrypt = new Button();
            buttonEncrypt = new Button();
            SuspendLayout();
            // 
            // textBoxKeyDES
            // 
            textBoxKeyDES.Location = new Point(12, 27);
            textBoxKeyDES.Name = "textBoxKeyDES";
            textBoxKeyDES.PlaceholderText = "Введите 7 символов ключа DES";
            textBoxKeyDES.Size = new Size(371, 23);
            textBoxKeyDES.TabIndex = 1;
            // 
            // textBoxKeyDEAL
            // 
            textBoxKeyDEAL.Location = new Point(389, 27);
            textBoxKeyDEAL.Name = "textBoxKeyDEAL";
            textBoxKeyDEAL.PlaceholderText = "Введите 16 символов ключа DEAL";
            textBoxKeyDEAL.Size = new Size(353, 23);
            textBoxKeyDEAL.TabIndex = 6;
            // 
            // labelKeys
            // 
            labelKeys.AutoSize = true;
            labelKeys.Location = new Point(12, 9);
            labelKeys.Name = "labelKeys";
            labelKeys.Size = new Size(45, 15);
            labelKeys.TabIndex = 8;
            labelKeys.Text = "Ключи";
            // 
            // textBoxOutput
            // 
            textBoxOutput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxOutput.Location = new Point(12, 222);
            textBoxOutput.Multiline = true;
            textBoxOutput.Name = "textBoxOutput";
            textBoxOutput.ReadOnly = true;
            textBoxOutput.ScrollBars = ScrollBars.Vertical;
            textBoxOutput.Size = new Size(729, 209);
            textBoxOutput.TabIndex = 9;
            // 
            // textBoxInput
            // 
            textBoxInput.Location = new Point(12, 56);
            textBoxInput.Multiline = true;
            textBoxInput.Name = "textBoxInput";
            textBoxInput.ScrollBars = ScrollBars.Both;
            textBoxInput.Size = new Size(564, 160);
            textBoxInput.TabIndex = 10;
            // 
            // buttonDecrypt
            // 
            buttonDecrypt.Location = new Point(582, 143);
            buttonDecrypt.Name = "buttonDecrypt";
            buttonDecrypt.Size = new Size(159, 73);
            buttonDecrypt.TabIndex = 11;
            buttonDecrypt.Text = "Расшифровать текст";
            buttonDecrypt.UseVisualStyleBackColor = true;
            buttonDecrypt.Click += buttonDecrypt_Click;
            // 
            // buttonEncrypt
            // 
            buttonEncrypt.Location = new Point(582, 56);
            buttonEncrypt.Name = "buttonEncrypt";
            buttonEncrypt.Size = new Size(159, 71);
            buttonEncrypt.TabIndex = 12;
            buttonEncrypt.Text = "Зашифровать текст";
            buttonEncrypt.UseVisualStyleBackColor = true;
            buttonEncrypt.Click += buttonEncrypt_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(754, 443);
            Controls.Add(buttonEncrypt);
            Controls.Add(buttonDecrypt);
            Controls.Add(textBoxInput);
            Controls.Add(textBoxOutput);
            Controls.Add(labelKeys);
            Controls.Add(textBoxKeyDEAL);
            Controls.Add(textBoxKeyDES);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            Text = "DEAL-128 Cipher";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBoxKeyDES;
        private TextBox textBoxKeyDEAL;
        private Label labelKeys;
        private TextBox textBoxOutput;
        private TextBox textBoxInput;
        private Button buttonDecrypt;
        private Button buttonEncrypt;
    }
}