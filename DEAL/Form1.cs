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

            deal.DEALGenerateKeys(DEALKey, DESKey); //���������� ��������� ����� DEAL �� ����� DES � DEAL

            string inputText = textBoxInput.Text;
            while (inputText.Length % 8 != 0)
                inputText += " ";

            BitArray inputBinaryText = new BitArray(Encoding.Unicode.GetBytes(inputText));
            BitArray[] resultInBin = deal.DEALDecrypt(inputBinaryText); //�������� ������������� �����
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

            deal.DEALGenerateKeys(DEALKey, DESKey);//��������� ��������� ������

            string inputText = textBoxInput.Text;
            while (inputText.Length % 8 != 0)
                inputText += " ";
            
            BitArray inputBinaryText = new BitArray(Encoding.Unicode.GetBytes(inputText)); //�������� ����� ������
            BitArray[] resultInBin = deal.DEALEncrypt(inputBinaryText); //�������� ������������� �����
            byte[] result = new byte[resultInBin.GetLength(0) * 16]; //������ 16? ������ ��� UTF-16 ����� ������ ������� 16 ���
            for (int i = 0; i < resultInBin.GetLength(0); i++)
                resultInBin[i].CopyTo(result, i * 16);
            textBoxOutput.Text = Encoding.Unicode.GetString(result);
        }
        private void GetKeysFromForm()
        {
            //����� ����������� � ������� ASCII ��-�� ����, ��� ������ ASCII ����� 1 �����, � ������ ����� ��� ������ ������ �� ��������� 56/128 ���.
            //����� ��� ������������� �������� UTF-16 ����� ���� �� �������� ������� ������ ��������
            //��������� ���� ��� DES, ������� ����������� ����� DEAL
            string key = textBoxKeyDES.Text;
            if (key.Length > 7)
            {
                MessageBox.Show("������ ������������ ���� DES", "��������� ������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (key.Length < 7)
            {
                DialogResult dr = MessageBox.Show("����� ������������ ����. ���� DES ������ �������� �������� �� 7 �������� �������� ASCII" +
                    Environment.NewLine + "������ ��������� ���� ���� ��������� � �����?", "��������� ������", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dr == DialogResult.Yes)
                    while (key.Length < 7)
                        key += " ";
                else
                    return;
            }
            DESKey = new BitArray(Encoding.ASCII.GetBytes(key));


            //��������� ���� ��� DEAL
            key = textBoxKeyDEAL.Text;
            if (key.Length > 16)
            {
                MessageBox.Show("������ ������������ ���� DEAL", "��������� ������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (key.Length < 16)
            {
                DialogResult dr = MessageBox.Show("����� ������������ ����. ���� DEAL ������ �������� �������� �� 16 �������� �������� ASCII" +
                    Environment.NewLine + "������ ��������� ���� ���� ��������� � �����?", "��������� ������", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
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