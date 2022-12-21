using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace DEAL
{
    public partial class Form1 : Form
    {
        DEAL deal = new DEAL();
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = "";
            //Считываем ключ для DES, который сгенерирует ключи DEAL
            string key = textBoxKeyDES.Text;
            string temp;
            while (key.Length * 8 < 56)
                key += " ";
            byte[] bytes = Encoding.ASCII.GetBytes(key); //получаем байты символов
            BitArray bt = new BitArray(bytes);
            if (bt.Length != 56)
            {
                MessageBox.Show("Введён некорректный ключ. Ключ DES должен обязательно состоять из 7 символов алфавита ASCII");
                return;
            }

            //Считываем ключ для DEAL
            BitArray DEALKey;
            key = textBoxKeyDEAL1.Text;
            while (key.Length * 8 < 128)
                key += " ";
            bytes = Encoding.ASCII.GetBytes(key); //получаем байты символов
            DEALKey = new BitArray(bytes);
            if (DEALKey.Length != 128)
            {
                MessageBox.Show("Введён некорректный ключ. Ключ DEAL должен обязательно состоять из 16 символов алфавита ASCII");
                return;
            }
            deal.DEALGenerateKeys(DEALKey,bt); //генерируем раундовые ключи DEAL из ключа DES и DEAL


            string inputText = textBoxInput.Text;
            while (inputText.Length%8!=0)
            {
                inputText += " ";
            }
            bytes=Encoding.Unicode.GetBytes(inputText); //получаем байты текста
            BitArray inputBinaryText = new BitArray(bytes);
            BitArray[] resultInBin = deal.DEALDecrypt(inputBinaryText); //получаем дешифрованный текст
            byte[] result = new byte[resultInBin.GetLength(0) * 16];    
            for (int i=0;i<resultInBin.GetLength(0);i++)
            {
                resultInBin[i].CopyTo(result, i * 16);
            }
            string output = Encoding.Unicode.GetString(result);
            textBoxOutput.Text = output;
        }

        private void buttonGenerateKeys_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = "";
            //Считываем ключ для DES, который сгенерирует ключи DEAL
            string key = textBoxKeyDES.Text;
            while (key.Length * 8 < 56) //Расширяем ключи до нужного размера
                key += " ";
            byte[] bytes = Encoding.ASCII.GetBytes(key); //получаем байты символов
            BitArray bt = new BitArray(bytes);
            if (bt.Length!=56)
            {
                MessageBox.Show("Введён некорректный ключ. Ключ DES должен обязательно состоять из 7 символов алфавита ASCII");
                return;
            }

            //Считываем ключ для DEAL
            BitArray DEALKey;
            key = textBoxKeyDEAL1.Text;
            while (key.Length * 8 < 128)
                key += " ";
            bytes = Encoding.ASCII.GetBytes(key); //получаем байты символов
            DEALKey = new BitArray(bytes);
            if (DEALKey.Length != 128)
            {
                MessageBox.Show("Введён некорректный ключ. Ключ DEAL должен обязательно состоять из 16 символов алфавита ASCII");
                return;
            }
            deal.DEALGenerateKeys(DEALKey,bt);
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = "";
            //Считываем ключ для DES, который сгенерирует ключи DEAL
            string key = textBoxKeyDES.Text;
            string temp;
            while (key.Length*8 < 56)
                key += " ";
            byte[] bytes = Encoding.ASCII.GetBytes(key); //получаем байты символов
            BitArray bt = new BitArray(bytes);
            if (bt.Length != 56)
            {
                MessageBox.Show("Введён некорректный ключ. Ключ DES должен обязательно состоять максимум из 7 символов алфавита ASCII");
                return;
            }

            //Считываем ключ для DEAL
            BitArray DEALKey;
            key = textBoxKeyDEAL1.Text;
            while (key.Length*8 < 128)
                key += " ";
            bytes = Encoding.ASCII.GetBytes(key); //получаем байты символов
            DEALKey = new BitArray(bytes);
            if (DEALKey.Length != 128)
            {
                MessageBox.Show("Введён некорректный ключ. Ключ DEAL должен обязательно состоять максимум из 16 символов алфавита ASCII");
                return;
            }
            deal.DEALGenerateKeys(DEALKey, bt);


            string inputText = textBoxInput.Text;
            while (inputText.Length % 8 != 0)
                inputText += " ";
            bytes = Encoding.Unicode.GetBytes(inputText); //получаем байты текста
            BitArray inputBinaryText = new BitArray(bytes);

            BitArray[] resultInBin = deal.DEALEncrypt(inputBinaryText); //получаем дешифрованный текст
            byte[] result = new byte[resultInBin.GetLength(0)*16];
            for (int i = 0; i < resultInBin.GetLength(0); i++)
            {
                resultInBin[i].CopyTo(result, i*16);
            }
            string output = Encoding.Unicode.GetString(result);
            textBoxOutput.Text = output;
        }
    }
}