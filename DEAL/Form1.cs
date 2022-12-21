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
            //��������� ���� ��� DES, ������� ����������� ����� DEAL
            string key = textBoxKeyDES.Text;
            string temp;
            while (key.Length * 8 < 56)
                key += " ";
            byte[] bytes = Encoding.ASCII.GetBytes(key); //�������� ����� ��������
            BitArray bt = new BitArray(bytes);
            if (bt.Length != 56)
            {
                MessageBox.Show("����� ������������ ����. ���� DES ������ ����������� �������� �� 7 �������� �������� ASCII");
                return;
            }

            //��������� ���� ��� DEAL
            BitArray DEALKey;
            key = textBoxKeyDEAL1.Text;
            while (key.Length * 8 < 128)
                key += " ";
            bytes = Encoding.ASCII.GetBytes(key); //�������� ����� ��������
            DEALKey = new BitArray(bytes);
            if (DEALKey.Length != 128)
            {
                MessageBox.Show("����� ������������ ����. ���� DEAL ������ ����������� �������� �� 16 �������� �������� ASCII");
                return;
            }
            deal.DEALGenerateKeys(DEALKey,bt); //���������� ��������� ����� DEAL �� ����� DES � DEAL


            string inputText = textBoxInput.Text;
            while (inputText.Length%8!=0)
            {
                inputText += " ";
            }
            bytes=Encoding.Unicode.GetBytes(inputText); //�������� ����� ������
            BitArray inputBinaryText = new BitArray(bytes);
            BitArray[] resultInBin = deal.DEALDecrypt(inputBinaryText); //�������� ������������� �����
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
            //��������� ���� ��� DES, ������� ����������� ����� DEAL
            string key = textBoxKeyDES.Text;
            while (key.Length * 8 < 56) //��������� ����� �� ������� �������
                key += " ";
            byte[] bytes = Encoding.ASCII.GetBytes(key); //�������� ����� ��������
            BitArray bt = new BitArray(bytes);
            if (bt.Length!=56)
            {
                MessageBox.Show("����� ������������ ����. ���� DES ������ ����������� �������� �� 7 �������� �������� ASCII");
                return;
            }

            //��������� ���� ��� DEAL
            BitArray DEALKey;
            key = textBoxKeyDEAL1.Text;
            while (key.Length * 8 < 128)
                key += " ";
            bytes = Encoding.ASCII.GetBytes(key); //�������� ����� ��������
            DEALKey = new BitArray(bytes);
            if (DEALKey.Length != 128)
            {
                MessageBox.Show("����� ������������ ����. ���� DEAL ������ ����������� �������� �� 16 �������� �������� ASCII");
                return;
            }
            deal.DEALGenerateKeys(DEALKey,bt);
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = "";
            //��������� ���� ��� DES, ������� ����������� ����� DEAL
            string key = textBoxKeyDES.Text;
            string temp;
            while (key.Length*8 < 56)
                key += " ";
            byte[] bytes = Encoding.ASCII.GetBytes(key); //�������� ����� ��������
            BitArray bt = new BitArray(bytes);
            if (bt.Length != 56)
            {
                MessageBox.Show("����� ������������ ����. ���� DES ������ ����������� �������� �������� �� 7 �������� �������� ASCII");
                return;
            }

            //��������� ���� ��� DEAL
            BitArray DEALKey;
            key = textBoxKeyDEAL1.Text;
            while (key.Length*8 < 128)
                key += " ";
            bytes = Encoding.ASCII.GetBytes(key); //�������� ����� ��������
            DEALKey = new BitArray(bytes);
            if (DEALKey.Length != 128)
            {
                MessageBox.Show("����� ������������ ����. ���� DEAL ������ ����������� �������� �������� �� 16 �������� �������� ASCII");
                return;
            }
            deal.DEALGenerateKeys(DEALKey, bt);


            string inputText = textBoxInput.Text;
            while (inputText.Length % 8 != 0)
                inputText += " ";
            bytes = Encoding.Unicode.GetBytes(inputText); //�������� ����� ������
            BitArray inputBinaryText = new BitArray(bytes);

            BitArray[] resultInBin = deal.DEALEncrypt(inputBinaryText); //�������� ������������� �����
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