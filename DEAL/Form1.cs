using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

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

            deal.DEALGenerateKeys(DEALKey, DESKey); //генерируем раундовые ключи DEAL из ключа DES и DEAL

            string inputText = textBoxInput.Text;
            while (inputText.Length % 8 != 0)
                inputText += " ";

            BitArray inputBinaryText = new BitArray(Encoding.Unicode.GetBytes(inputText));
            BitArray[] resultInBin = deal.DEALDecrypt(inputBinaryText); //получаем дешифрованный текст
            byte[] result = new byte[resultInBin.GetLength(0) * 16];
            for (int i = 0; i < resultInBin.GetLength(0); i++)
                resultInBin[i].CopyTo(result, i * 16);
            textBoxOutput.Text = Encoding.Unicode.GetString(result);
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            DEAL deal = new DEAL();
            textBoxOutput.Text = "";

            GetKeysFromForm();

            deal.DEALGenerateKeys(DEALKey, DESKey);//генерация раундовых ключей

            string inputText = textBoxInput.Text;
            while (inputText.Length % 8 != 0)
                inputText += " ";
            
            BitArray inputBinaryText = new BitArray(Encoding.Unicode.GetBytes(inputText)); //получаем байты текста
            BitArray[] resultInBin = deal.DEALEncrypt(inputBinaryText); //получаем дешифрованный текст
            byte[] result = new byte[resultInBin.GetLength(0) * 16]; //почему 16? потому что UTF-16 имеет размер символа 16 бит
            for (int i = 0; i < resultInBin.GetLength(0); i++)
                resultInBin[i].CopyTo(result, i * 16);
            textBoxOutput.Text = Encoding.Unicode.GetString(result);
        }
        private void GetKeysFromForm()
        {
            //Ключи считываются в формате ASCII из-за того, что символ ASCII равен 1 байту, а размер блока для шифров должен не превышать 56/128 бит.
            //Иначе при использовании символов UTF-16 можно было бы вставить гораздо меньше символов
            //Считываем ключ для DES, который сгенерирует ключи DEAL
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


            //Считываем ключ для DEAL
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