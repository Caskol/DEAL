using System.Collections;
using System.Text;

namespace DEAL
{
    public partial class Form1 : Form
    {
        BitArray DESKey;
        BitArray DEALKey;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            DEAL deal = new DEAL();
            textBoxOutput.Text = "";

            GetKeysFromForm();
            if (DESKey == null || DEALKey == null)
                return;

            deal.DEALGenerateKeys(DEALKey, DESKey); //generate round keys (RK) DEAL from user's DES and DEAL keys
            
            string inputText = textBoxInput.Text;
            while (inputText.Length % 8 != 0)
                inputText += " ";

            BitArray inputBinaryText = new BitArray(Encoding.Unicode.GetBytes(inputText));
            BitArray[] resultInBin = deal.DEALDecrypt(inputBinaryText);
            byte[] result = new byte[resultInBin.GetLength(0) * 16];//16 - symbol size in UTF-16
            for (int i = 0; i < resultInBin.GetLength(0); i++)
                resultInBin[i].CopyTo(result, i * 16);
            textBoxOutput.Text = Encoding.Unicode.GetString(result);
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            DEAL deal = new DEAL();
            textBoxOutput.Text = "";

            GetKeysFromForm();
            if (DESKey == null || DEALKey == null)
                return;
            deal.DEALGenerateKeys(DEALKey, DESKey);//генерация раундовых ключей

            string inputText = textBoxInput.Text;
            while (inputText.Length % 8 != 0)
                inputText += " ";

            BitArray inputBinaryText = new BitArray(Encoding.Unicode.GetBytes(inputText));
            BitArray[] resultInBin = deal.DEALEncrypt(inputBinaryText);
            byte[] result = new byte[resultInBin.GetLength(0) * 16]; //16 - symbol size in UTF-16
            for (int i = 0; i < resultInBin.GetLength(0); i++)
                resultInBin[i].CopyTo(result, i * 16);
            textBoxOutput.Text = Encoding.Unicode.GetString(result);
        }
        private void GetKeysFromForm()
        {
            //ASCII symbol equals 1 byte. Block size in DES and DEAL cannot be bigger than 56/128 bits. 
            //So if there was a UTF-16 symbol (which equals 16 bytes) then block cannot be bigger than 3-5 UTF-16 symbols, which isn't good for cipher secure
            string key = textBoxKeyDES.Text;
            if (key.Length > 7)
            {
                MessageBox.Show("Введен недопустимый ключ DES", "Произошла ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (key.Length < 7)
            {
                DialogResult dr = MessageBox.Show("Введён некорректный ключ. Ключ DES должен состоять максимум из 7 символов алфавита ASCII" +
                    Environment.NewLine + "Хотите дополнить этот ключ пробелами в конце?", "Произошла ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dr == DialogResult.Yes)
                    while (key.Length < 7)
                        key += " ";
                else
                    return;
            }
            DESKey = new BitArray(Encoding.ASCII.GetBytes(key));


            key = textBoxKeyDEAL.Text;
            if (key.Length > 16)
            {
                MessageBox.Show("Введен недопустимый ключ DEAL", "Произошла ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (key.Length < 16)
            {
                DialogResult dr = MessageBox.Show("Введён некорректный ключ. Ключ DEAL должен состоять максимум из 16 символов алфавита ASCII" +
                    Environment.NewLine + "Хотите дополнить этот ключ пробелами в конце?", "Произошла ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dr == DialogResult.Yes)
                    while (key.Length < 16)
                        key += " ";
                else
                    return;
            }
            DEALKey = new BitArray(Encoding.ASCII.GetBytes(key));
        }
    }
}