using Microsoft.VisualBasic.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace DEAL
{
    internal class DEAL
    {
        //public string logString = "";
        private BitArray[] DESkeys;
        private BitArray[] DEALkeys;
        readonly int[] IPplacements = new int[] //массив, содержащий номера битов в блоке так, как они должны будут быть переставлены в исходном тексте T
            {
                58, 50, 42, 34, 26, 18, 10, 2,
                60, 52, 44, 36, 28, 20, 12, 4,
                62, 54, 46, 38, 30, 22, 14, 6,
                64, 56, 48, 40, 32, 24, 16, 8,
                57, 49, 41, 33, 25, 17, 9, 1,
                59, 51, 43, 35, 27, 19, 11, 3,
                61, 53, 45, 37, 29, 21, 13, 5,
                63, 55, 47, 39, 31, 23, 15, 7
            };
        readonly int[] reverseIPplacements = new int[]
        {
            40 , 8 ,  48 , 16 , 56 , 24 , 64 , 32 , 39 , 7 ,  47 , 15 , 55 , 23 , 63 , 31,
            38 , 6 ,  46 , 14 , 54 , 22 , 62 , 30 , 37 , 5 ,  45 , 13 , 53 , 21 , 61 , 29,
            36 , 4 ,  44 , 12 , 52 , 20 , 60 , 28 , 35 , 3 ,  43 , 11 , 51 , 19 , 59 , 27,
            34 , 2 ,  42 , 10 , 50 , 18 , 58 , 26 , 33 , 1 ,  41 , 9  , 49 , 17 , 57 , 25
        };

        readonly int[] Eplacements = new int[] //массив, содержащий номера битов в блоке так, как они должны будут расширены в функции Фейстеля (f)
        {
                32, 1, 2, 3, 4, 5,
                4, 5, 6, 7, 8, 9,
                8, 9, 10, 11, 12, 13,
                12, 13, 14, 15, 16, 17,
                16, 17, 18, 19, 20, 21,
                20, 21, 22, 23, 24, 25,
                24, 25, 26, 27, 28, 29,
                28, 29, 30, 31, 32, 1
        };
        readonly int[] CDPlacements = new int[]
        {
            57 , 49 ,  41 , 33 , 25 , 17 , 9 ,  1 ,  58,  50,  42,  34,  26,  18,
            10 , 2 ,  59 , 51 , 43 , 35 , 27 , 19 , 11 , 3  , 60 , 52 , 44 , 36,
            63 , 55 , 47 , 39 , 31  ,23 , 15,  7 ,  62 ,  54 , 46 , 38, 30 , 22,
            14 , 6 ,  61 ,  53 , 45 , 37 , 29 , 21  ,13 , 5 ,  28 , 20,  12,  4
        };
        readonly int[] KeyPlacements = new int[]
        {
            14,  17,  11 , 24 , 1  , 5  , 3  , 28 , 15 , 6  , 21 , 10 , 23 , 19 , 12 , 4,
            26 , 8 ,  16 , 7 ,  27 , 20 , 13 , 2 ,  41 , 52 , 31 , 37 , 47 , 55 , 30 , 40,
            51 , 45 , 33 , 48 , 44 , 49 , 39 , 56 , 34 , 53 , 46 , 42 , 50 , 36 , 29 , 32
        };
        readonly int[,,] Splacements = new int[,,]
        {
            {
                { 14 , 4  , 13 , 1 ,  2  , 15 , 11 , 8 ,  3 ,  10 , 6 ,  12 , 5 ,  9 ,  0  , 7 },
                { 0 ,  15 , 7  , 4  , 14 , 2  , 13 , 1 ,  10 , 6  , 12 , 11 , 9 ,  5 ,  3 ,  8 },
                { 4 ,  1  , 14 , 8  , 13 , 6  , 2  , 11 , 15 , 12 , 9 ,  7 ,  3  , 10 , 5  , 0 },
                { 15 , 12 , 8 ,  2 ,  4 ,  9 ,  1 ,  7  , 5  , 11 , 3 ,  14 , 10 , 0 ,  6 ,  13 }
            },
            {
                { 15 , 1 ,  8 ,  14 , 6  , 11 , 3  , 4 ,  9 ,  7 ,  2 ,  13 , 12 , 0 ,  5 ,  10 },
                { 3 ,  13,  4  , 7  , 15 , 2 ,  8  , 14 , 12 , 0  , 1  , 10 , 6  , 9 ,  11,  5 },
                { 0 ,  14 , 7  , 11 , 10 , 4  , 13 , 1 ,  5  , 8 ,  12 , 6 ,  9 ,  3 ,  2 ,  15 },
                { 13 , 8  , 10 , 1  , 3 ,  15 , 4 ,  2 ,  11 , 6 ,  7 ,  12 , 0 ,  5 ,  14 , 9 }
            },
            {
                { 10 , 0 ,  9 ,  14 , 6  , 3  , 15 , 5 ,  1  , 13 , 12 , 7 ,  11 , 4 ,  2 ,  8 },
                { 13 , 7 ,  0,   9 ,  3  , 4  , 6  , 10 , 2  , 8  , 5 ,  14 , 12 , 11 , 15,  1 },
                { 13 , 6 ,  4 ,  9 ,  8  , 15 , 3 ,  0,   11,  1 ,  2  , 12 , 5  , 10 , 14 , 7 },
                { 1  , 10 , 13 , 0 ,  6  , 9 ,  8  , 7 ,  4  , 15 , 14 , 3 ,  11 , 5 ,  2 ,  12 }
            },
            {
                { 7 ,  13,  14,  3  , 0  , 6 ,  9  , 10 , 1  , 2  , 8 ,  5 ,  11 , 12 , 4  , 15 },
                { 13 , 8 ,  11 , 5  , 6 ,  15 , 0 ,  3,   4 ,  7 ,  2 ,  12 , 1 ,  10 , 14, 9 },
                { 10 , 6 ,  9 ,  0 ,  12 , 11 , 7 ,  13 , 15,  1 ,  3 ,  14 , 5,   2 ,  8 ,  4 },
                { 3 ,  15 , 0 ,  6 ,  10 , 1 ,  13,  8 ,  9 ,  4 ,  5  , 11 , 12 , 7 ,  2 ,  14 }
            },
            {
                { 2  , 12,  4 ,  1 ,  7  , 10 , 11 , 6 ,  8  , 5  , 3 ,  15 , 13,  0 ,  14 , 9 },
                { 14 , 11 , 2  , 12 , 4  , 7 ,  13 , 1  , 5 ,  0 ,  15 , 10 , 3 ,  9  , 8 ,  6 },
                { 4  , 2 ,  1 ,  11 , 10,  13,  7 ,  8 ,  15 , 9  , 12,  5 ,  6  , 3,   0,   14 },
                { 11,  8 ,  12 , 7  , 1  , 14,  2 ,  13 , 6 ,  15 , 0 ,  9 ,  10,  4 ,  5 ,  3 }
            },
            {
                { 12 , 1 ,  10 , 15 , 9 ,  2 ,  6 ,  8 ,  0  , 13 , 3 ,  4 ,  14 , 7  , 5 ,  11 },
                { 10 , 15 , 4 ,  2  , 7  , 12 , 9  , 5 ,  6 ,  1 ,  13 , 14,  0 ,  11 , 3  , 8 },
                { 9 ,  14 , 15,  5,   2 ,  8  , 12 , 3 ,  7 ,  0,   4 ,  10 , 1 ,  13 , 11 , 6 },
                { 4  , 3  , 2  , 12 , 9  , 5  , 15 , 10 , 11,  14 , 1 ,  7 ,  6,   0 ,  8 ,  13 }
            },
            {
                { 4 ,  11 , 2 ,  14 , 15 , 0  , 8 ,  13,  3 ,  12 , 9 ,  7 ,  5  , 10 , 6 ,  1 },
                { 13 , 0  , 11,  7 ,  4  , 9  , 1  , 10 , 14 , 3,   5 ,  12 , 2  , 15,  8 ,  6 },
                { 1 ,  4 ,  11,  13 , 12,  3 ,  7  , 14 , 10 , 15 , 6 ,  8 ,  0 ,  5 ,  9 ,  2 },
                { 6  , 11 , 13 , 8 ,  1  , 4  , 10 , 7 ,  9  , 5  , 0  , 15 , 14 , 2 ,  3  , 12 }
            },
            {
                { 13 , 2 ,  8 ,  4  , 6 ,  15 , 11 , 1  , 10 , 9 ,  3 ,  14 , 5  , 0 ,  12 , 7 },
                { 1  , 15 , 13,  8 ,  10,  3,   7 ,  4 ,  12 , 5  , 6 ,  11 , 0 ,  14 , 9  , 2 },
                { 7  , 11 , 4 ,  1 ,  9  , 12 , 14 , 2 ,  0  , 6  , 10 , 13  ,15,  3 ,  5,   8 },
                { 2 ,  1 ,  14,  7 ,  4 ,  10 , 8  , 13 , 15 , 12 , 9  , 0  , 3 ,  5  , 6 ,  11 }
            }
        };
        readonly int[] Pplacements = new int[]
        {
            16 , 7 ,  20,  21,  29 , 12,  28 , 17,
            1 ,  15 , 23 , 26 , 5 ,  18 , 31 , 10,
            2 ,  8 ,  24 , 14 , 32,  27,  3 ,  9,
            19 , 13 , 30 , 6  , 22 , 11 , 4  , 25
        };
        /// <summary>
         /// Начальная перестановка битов шифра DES
         /// </summary>
         /// <param name="input">Входной массив битов, которые нужно переставить</param>
        private BitArray IP(BitArray input)
        {
            BitArray output = new BitArray(input.Length); //создаем массив битов
            for (int i=0;i<output.Length;i++) //по циклу перебираем 
                output[i] = input[IPplacements[i]-1]; //-1 т.к. массивы начинаются с 0. В выходной массив битов помещаем на место элементов, указанных в массиве placements, значения элеvентов input по порядку
            //logString += $"Была произведена первоначальная перестановка текста. Теперь текст имеет следующий вид: {ShowBitArrayInString(output)}"+Environment.NewLine;
            return output;
        }
        /// <summary>
        /// Метод генерации ключей RKi для DEAL
        /// </summary>
        /// <param name="key">128-битный ключ</param>
        /// <param name="DESkey">Ключ DES, относительно которого будут генерироваться ключи DEAL</param>
        public void DEALGenerateKeys(BitArray key, BitArray DESkey)
        {
            BitArray k1=new BitArray(64), k2=new BitArray(64); //разбиваем 128-битный ключ на 2 подключа по 64 бита для шифрования DES'ом
            for (int i=0;i<key.Length;i++) //цикл разбиение 128-битного ключа на 2 64-битных ключа
            {
                if (i < 64) //первая половина ключа
                    k1[i] = key[i];
                else
                    k2[i - 64] = key[i];
            }
            BitArray k1Copy, k2Copy, i1,i2,i4,i8;
            i1 = new BitArray(64); //Инициализируем массив для "Replicant constants" 1 (не знаю, как объяснить)
            i1[0] = true;
            i2 = new BitArray(64); //Инициализируем массив для 2
            i2[1] = true;
            i4 = new BitArray(64);//Инициализируем массив для 4
            i4[2] = true;
            i8 = new BitArray(64);
            i8[3] = true;
            k1Copy = Copy(k1); //копируем ключи, т.к. в ходе XOR они могут утеряться
            k2Copy = Copy(k2);
            DEALkeys = new BitArray[6];//инициализируем массив ключей DEAL
            DEALkeys[0] = DESEncryption(k1,DESkey,false); //Первый ключ DEAL получается путем шифрования первой половины исходного ключа
            DEALkeys[1] = DESEncryption(k2Copy.Xor(DEALkeys[0]), DESkey,true); //Второй ключ DEAL получается путем шифрования результата XOR второй половины ключа на готовый первый ключ DEAL
            k2Copy = Copy(k2); //Результат XOR записывается в массив битов по умолчанию, нужно копировать каждый раз после XOR
            DEALkeys[2] = DESEncryption(k1Copy.Xor(i1.Xor(DEALkeys[1])), DESkey,true); //Третий ключ получается путем шифрования XORа первой половины исходного ключа на XOR i-го числа на готовый второй ключ DEAL
            k1Copy = Copy(k1);
            DEALkeys[3] = DESEncryption(k2Copy.Xor(i2.Xor(DEALkeys[2])), DESkey,true);//Четвертый ключ по той же механике, что и 3 - только идет XOR других элементов
            k2Copy = Copy(k2);
            DEALkeys[4] = DESEncryption(k1Copy.Xor(i4.Xor(DEALkeys[3])), DESkey, true);//Пятый ключ
            k1Copy = Copy(k1);
            DEALkeys[5] = DESEncryption(k2Copy.Xor(i8.Xor(DEALkeys[4])), DESkey, true);//Последний ключ DEAL
            ////logString += $"Был получен 128-битный ключ {ShowBitArrayInString(key)}" + Environment.NewLine + "Он был разбит на 2 подключа по 64-бита каждый:" + Environment.NewLine + $"1 половина ключа имеет вид:{ShowBitArrayInString(k1)}" + Environment.NewLine + $"2 половина ключа имеет вид: {ShowBitArrayInString(k2)}" + Environment.NewLine;
            ////logString += $"Были сформированы 6 подключей DEAL: 1 путем шифрования DES первой половины исходного ключа, 2 путем шифрования DES результата XOR второй половины исходного ключа на первый ключ DEAL" + Environment.NewLine + $"1 ключ: {ShowBitArrayInString(DEALkeys[0])}" + Environment.NewLine + $"2 ключ: {ShowBitArrayInString(DEALkeys[1])}" + Environment.NewLine + $"3 ключ: {ShowBitArrayInString(DEALkeys[2])}" + Environment.NewLine + $"4 ключ: {ShowBitArrayInString(DEALkeys[3])}" + Environment.NewLine + $"5 ключ: {ShowBitArrayInString(DEALkeys[4])}" + Environment.NewLine + $"6 ключ: {ShowBitArrayInString(DEALkeys[5])}";
        }
        /// <summary>
        /// Основная логика шифрования DEAL. Поскольку нет никакой информации о механизме шифрования E, то на блоки текста будет проведен XOR раундового ключа
        /// </summary>
        /// <param name="inputText"></param>
        public BitArray[] DEALEncrypt(BitArray inputText)
        {
            BitArray[] result = new BitArray[inputText.Length / 128];
            BitArray[,] blocks = new BitArray[inputText.Length / 128,2]; //инициализируем блоки текста по 128 бит
            string leftBlock, rightBlock;
            BitArray oldL; //Массив битов для хранения левой части блока
            //logString += $"На зашифрование поступил блок, длиной {inputText.Length}. Он был поделен на {inputText.Length / 2} блоков по 128 бит для дальнейшего шифрования" + Environment.NewLine;
            for (int i=0;i<blocks.GetLength(0); i++)
            {
                //В цикле инициализируем 2 блока, равные половине длины блока шифртекста
                blocks[i, 0] = new BitArray(64); 
                blocks[i, 1] = new BitArray(64);
                for (int j=0;j<128;j++) //помещаем в поделенные блоки информацию из основного блока
                {
                    if (j < 64)
                        blocks[i, 0][j] = inputText[j + (128 * i)]; //каждые 64 блока текста + порядковый номер
                    else
                        blocks[i, 1][j-64] = inputText[j + (128 * i)];
                }
                for (int j=0;j<6;j++) //Цикл шифрования
                {
                    oldL = Copy(blocks[i, 0]); //копируем левый блок
                    //logString += $"Проводится операция шифрования E (XOR) - левый блок половины исходного ключа на ключ DEAL под номером {j + 1}" + Environment.NewLine + $"Текущее значение левого блока: {ShowBitArrayInString(blocks[i, 0])}" + Environment.NewLine + $"Текущее значение ключа DEAL: {ShowBitArrayInString(DEALkeys[j])}" + Environment.NewLine;
                    blocks[i, 0] = DESEncryption(blocks[i, 0], DEALkeys[j],false);//проводим шифрование E - шифрование левого 64-битного блока ключом DEAL при помощи функции DES
                    //logString += $"Результат XOR левого блока на {j + 1}ключ DEAL: {ShowBitArrayInString(blocks[i, 0])}" + Environment.NewLine;
                    //logString += $"Сейчас будет проведена операция XOR полученного значение левого блока на значение правого блока. Правый блок имеет вид: {ShowBitArrayInString(blocks[i, 1])}" + Environment.NewLine;
                    blocks[i, 0].Xor(blocks[i, 1]);//Затем финальный XOR - зашифрованный левый блок XORится на правый блок
                    blocks[i, 1] = oldL;
                    //logString += $"Результат {j + 1}-го раунда шифрования:" + Environment.NewLine + $"Левый блок имеет вид: {ShowBitArrayInString(blocks[i, 0])}" + Environment.NewLine + $"Правый блок имеет вид: {ShowBitArrayInString(blocks[i, 1])}" + Environment.NewLine;
                }
                leftBlock = ShowBitArrayInString(blocks[i, 0]);
                rightBlock = ShowBitArrayInString(blocks[i, 1]);
                result[i] = ShowStringInBitArray(leftBlock+rightBlock);
                //logString += $"Зашифрованный {i + 1} блок имеет вид: {ShowBitArrayInString(result[i])}";
            }
            return result;
        }
        public BitArray[] DEALDecrypt(BitArray inputText)
        {
            BitArray[] result = new BitArray[inputText.Length / 128];
            BitArray[,] blocks = new BitArray[inputText.Length / 128, 2]; //инициализируем блоки текста по 128 бит
            string leftBlock, rightBlock;
            BitArray oldR = new BitArray(64); //Массив битов для хранения правой части блока
            //logString += $"На дешифрование поступил блок, длиной {inputText.Length}. Он был поделен на {inputText.Length / 2} блоков по 128 бит для дальнейшего шифрования" + Environment.NewLine;
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                //В цикле инициализируем 2 блока, равные половине длины блока шифртекста
                blocks[i, 0] = new BitArray(64);
                blocks[i, 1] = new BitArray(64);
                for (int j = 0; j < 128; j++) //помещаем в поделенные блоки информацию из основного блока
                {
                    if (j<64)
                    blocks[i, 0][j] = inputText[j+(128*i)]; //каждые 64 блока текста + порядковый номер и сдвиг на 128 элементов относительно того, какого размера пришел изначальный текст
                    else
                    blocks[i, 1][j-64] = inputText[j+(128*i)];
                }
                for (int j = 5; j > -1; j--) //Цикл расшифровки
                {
                    oldR = Copy(blocks[i, 1]); //копируем правый блок
                    //logString += $"Проводится операция шифрования E (XOR) - левый блок половины исходного ключа на ключ DEAL под номером {j + 1}" + Environment.NewLine + $"Текущее значение левого блока: {ShowBitArrayInString(blocks[i, 0])}" + Environment.NewLine + $"Текущее значение ключа DEAL: {ShowBitArrayInString(DEALkeys[j])}" + Environment.NewLine;
                    blocks[i, 1] = DESEncryption(blocks[i, 1], DEALkeys[j],false);//проводим дешифрование E - дешифрование левого 64-битного блока ключом DEAL при помощи функции DES
                    blocks[i, 1].Xor(blocks[i, 0]);//Затем финальный XOR - зашифрованный правый блок XORится на левый блок
                                                   ////logString += $"Результат XOR левого блока на {j + 1}ключ DEAL: {ShowBitArrayInString(blocks[i, 0])}" + Environment.NewLine;
                                                   ////logString += $"Сейчас будет проведена операция XOR полученного значение левого блока на значение правого блока. Правый блок имеет вид: {ShowBitArrayInString(blocks[i, 1])}" + Environment.NewLine;

                    blocks[i, 0] = oldR;
                    //logString += $"Результат {j + 1}-го раунда шифрования:" + Environment.NewLine + $"Левый блок имеет вид: {ShowBitArrayInString(blocks[i, 0])}" + Environment.NewLine + $"Правый блок имеет вид: {ShowBitArrayInString(blocks[i, 1])}" + Environment.NewLine;
                }
                leftBlock = ShowBitArrayInString(blocks[i, 0]);
                rightBlock = ShowBitArrayInString(blocks[i, 1]);
                result[i] = ShowStringInBitArray(leftBlock + rightBlock);
                //logString += $"Расшифрованный {i + 1} блок имеет вид: {ShowBitArrayInString(result[i])}";
            }
            return result;
        }
        /// <summary>
        /// Конечная перестановка IP^-1
        /// </summary>
        /// <param name="input">Входной блок 64 бит</param>
        /// <returns>64-битный итоговый блок</returns>
        private BitArray reverseIP(BitArray input)
        {
            BitArray output = new BitArray(input.Length); //создаем массив битов
            for (int i = 0; i < output.Length; i++) //по циклу перебираем 
                output[i] = input[reverseIPplacements[i]-1]; //в выходной массив битов помещаем на место элементов, указанных в массиве reverseIPplacements, значения элеvентов input по порядку
            //logString += $"Была произведена конечная перестановка текста. Теперь текст имеет следующий вид: {ShowBitArrayInString(output)}" + Environment.NewLine;
            return output;
        }
        /// <summary>
        /// Функция шифрования DES
        /// </summary>
        /// <param name="input">Входной массив битов 64-битного блока</param>
        public BitArray DESEncryption(BitArray input, BitArray input_key,bool same_key)
        {
            //logString += $"На шифрование пришел блок, который имеет вид: {ShowBitArrayInString(input)}" + Environment.NewLine;
            input = IP(input);//производим начальную перестановку
            //logString += $"Произведена первоначальная перестановка исходного текста по IP. Перестановленный текст выглядит так: {ShowBitArrayInString(input)}" + Environment.NewLine;
            BitArray L = new BitArray(input.Length / 2); //инициализируем левый блок текста
            BitArray R = new BitArray(input.Length / 2); //инициализируем правый блок текста
            for (int i=0;i<input.Length/2;i++) //в цикле распределяем текст на 2 блока. Первые 32 бита - в левый блок, оставшиеся 32 бита в правый блок
            {
                L[i] = input[i];
            }
            for (int i=32;i<input.Length;i++)
            {
                R[i-32] = input[i];
            }
            BitArray oldL = null;
            if (same_key==false) //если используется тот же ключ (например, при генерации ключей для DEAL)
            GenerateKeysForDes(input_key); //генерируем 16 раундовых ключей
            for (int i=0;i<16;i++) //16 раундов шифрования DES
            {
                //logString += $"Начинается {i + 1}-й раунд шифрования DES. Блок L имеет вид {ShowBitArrayInString(L)}" + Environment.NewLine + $"Блок R имеет вид {ShowBitArrayInString(R)}" + Environment.NewLine;
                oldL = Copy(L); //запоминаем старый L
                L = Copy(R); //Новый L - старый R
                R = oldL.Xor(FeistelFunction(R, DESkeys[i])); //новый R это результат XOR старого L на функцию Фейстеля
                //logString += $"Произведена замена левого блока на правый. Блок L получил значения R и теперь имеет вид {ShowBitArrayInString(L)}" + Environment.NewLine + $"Блок R получил результат операции XOR на предыдущий массив значений блока L {ShowBitArrayInString(R)}"+Environment.NewLine;
            }
            string resultL,resultR,result; //создаем строки, хранящие финальное содержимое работы функций
            resultL = ShowBitArrayInString(L);
            resultR = ShowBitArrayInString(R);
            result = resultL + resultR;
            //logString += $"Конечные блоки шифрования L и R имеют вид: L = {resultL}" + Environment.NewLine + $"R={resultR}"+Environment.NewLine+$"Соединяем левый блок с правым и получаем результирующий блок: {result}"+Environment.NewLine+"Начинается конечная перестановка IP^-1"+Environment.NewLine;
            BitArray cipherBlock = new BitArray(64); //инициализируем финальный зашифрованный блок
            cipherBlock = reverseIP(ShowStringInBitArray(result));
            return cipherBlock;
        }
        /// <summary>
        /// Деширофка DES. Нужна только для проверки верности шифрования DES
        /// </summary>
        /// <param name="input">Входной текст</param>
        /// <param name="input_key">Входной ключ</param>
        /// <returns>Дешифрованный 64-битный блок</returns>
        public BitArray DESDecryption(BitArray input, BitArray input_key)
        {
            //logString += $"На дешифрование пришел блок, который имеет вид: {ShowBitArrayInString(input)}" + Environment.NewLine;
            //MessageBox.Show($"Itog = {ShowBitArrayInString(input)}");
            input = IP(input);//производим начальную перестановку
            //logString += $"Произведена первоначальная перестановка исходного текста по IP. Перестановленный текст выглядит так: {ShowBitArrayInString(input)}" + Environment.NewLine;
            BitArray L = new BitArray(input.Length / 2); //инициализируем левый блок текста
            BitArray R = new BitArray(input.Length / 2); //инициализируем правый блок текста
            for (int i = 0; i < input.Length / 2; i++) //в цикле распределяем текст на 2 блока. Первые 32 бита - в левый блок, оставшиеся 32 бита в правый блок
            {
                L[i] = input[i];
            }
            for (int i = 32; i < input.Length; i++)
            {
                R[i - 32] = input[i];
            }
            BitArray oldR = null;
            GenerateKeysForDes(input_key);
            for (int i = 15; i > -1; i--) //16 раундов шифрования DES
            {
                //logString += $"Начинается {i + 1}-й раунд дешифрования DES. Блок L имеет вид {ShowBitArrayInString(L)}" + Environment.NewLine + $"Блок R имеет вид {ShowBitArrayInString(R)}" + Environment.NewLine;
                oldR = Copy(R);//запоминаем старый блок R
                R = Copy(L);//Новый R - это старый L
                L = oldR.Xor(FeistelFunction(L, DESkeys[i])); //Новый L - результат XOR старого L на функцию Фейстеля
                //logString += $"Произведена замена правого блока на левый. Блок R получил значения L и теперь имеет вид {ShowBitArrayInString(L)}" + Environment.NewLine + $"Блок L получил результат операции XOR на предыдущий массив значений блока R {ShowBitArrayInString(R)}" + Environment.NewLine;
            }
            string resultL, resultR, result; //создаем строки, хранящие финальное содержимое работы функций
            resultL = ShowBitArrayInString(L);
            resultR = ShowBitArrayInString(R);
            result = resultL + resultR;
            //logString += $"Конечные блоки шифрования L и R имеют вид: L = {resultL}" + Environment.NewLine + $"R={resultR}" + Environment.NewLine + $"Соединяем левый блок с правым и получаем результирующий блок: {result}" + Environment.NewLine + "Начинается конечная перестановка IP^-1" + Environment.NewLine;
            BitArray cipherBlock = new BitArray(64); //инициализируем финальный зашифрованный блок
            cipherBlock = reverseIP(ShowStringInBitArray(result));
            return cipherBlock;
        }
        /// <summary>
        /// Функция Фейстеля (f)
        /// </summary>
        /// <param name="input">Входной блок, который будет зашифрован функцией Фейстеля</param>
        /// <param name="key">Ключ для шифрования</param>
        private BitArray FeistelFunction(BitArray input, BitArray key)
        {
            BitArray expandedInput = new BitArray(48); //создаем новый битовый массив, который будет содержать в себе расширенный вектор R
            for (int i=0;i<expandedInput.Length;i++)
                expandedInput[i] = input[Eplacements[i]-1]; //переставляем биты по таблице функции расширения E. -1 т.к. массив начинается с 0, а значения битов с 1
            //logString += $"Входящий блок R со значениями {ShowBitArrayInString(input)} был расширен и теперь имеет вид {ShowBitArrayInString(expandedInput)}" + Environment.NewLine;
            expandedInput.Xor(key);
            //logString += $"Была выполнения операция XOR с ключом {ShowBitArrayInString(key)}" + Environment.NewLine + $"Результат операции XOR: {ShowBitArrayInString(expandedInput)}" + Environment.NewLine;
            BitArray[] Bblocks = new BitArray[8];//инициализируем массив из 8 блоков B по 6 бит каждый
            BitArray[] B4bitblocks = new BitArray[8];//инициализируем массив из 8 блоков B' по 4 бита каждый
            int currentBlock = 0;//переменная, хранящая текущий номер блока
            StringBuilder a = new StringBuilder();//инициализируем строки, содержащие двоичные значения a и b, по которым будет искаться номер в блоке
            StringBuilder b=new StringBuilder();
            int A, B;
            string B4bitblock; //строка, хранящая текущий 4 битный блок B'
            for (int i = 0; i < expandedInput.Length;i+=6)
            {
                Bblocks[currentBlock] = new BitArray(6); //каждый блок B инициализируем по 6 бит
                for (int j = 0; j < 6; j++) //от 1 до 7 для верного умножения
                {
                    Bblocks[currentBlock][j] = expandedInput[i + j]; //во временную строковую переменную вставляем 
                }
                //logString += $"Блок B{currentBlock + 1} имеет вид: {ShowBitArrayInString(Bblocks[currentBlock])}" + Environment.NewLine;
                a.Append(Convert.ToInt32(Bblocks[currentBlock][0]));//добавляем в число A битовое значение 1 бита блока
                a.Append(Convert.ToInt32(Bblocks[currentBlock][5]));//добавляем в число A битовое значение последнего бита блока
                for (int j = 1; j < 5; j++)
                    b.Append(Convert.ToInt32(Bblocks[currentBlock][j])); //добавляем в число B битовые значение 4 разрядов
                //logString += $"Число a блока B{currentBlock+1} имеет двоичный вид : {a}" + Environment.NewLine + $"Число b блока B{currentBlock+1} имеет двоичный вид: {b}" + Environment.NewLine;
                A = Convert.ToInt32(a.ToString(), 2); //получаем десятичное представление чисел A и B
                B = Convert.ToInt32(b.ToString(), 2);
                //logString += $"Число a блока B{currentBlock+1} имеет десятичный вид : {A}" + Environment.NewLine + $"Число b блока B{currentBlock} имеет десятичный вид: {B}" + Environment.NewLine;
                //logString += $"Этот элемент в таблице S{currentBlock+1} имеет число {Splacements[currentBlock, A,B]}" + Environment.NewLine;
                B4bitblock = Convert.ToString(Splacements[currentBlock, A ,B], 2);
                if (B4bitblock.Length<4) //если двоичный вид числа оказался меньше 4 бит, то нужно добавить незначащие нули
                {
                    StringBuilder sb = new StringBuilder();
                    while(sb.Length!= 4 - B4bitblock.Length)//дополняем нулями в начале
                        sb.Append('0');
                    sb.Append(B4bitblock); //добавляем старые значения
                    B4bitblock= sb.ToString(); //получаем строку 4 бит
                }
                B4bitblocks[currentBlock] = ShowStringInBitArray(B4bitblock); //преобразовываем строку в массив битов
                //logString += $"Преобразовываем число {Splacements[currentBlock, A, B]} в двоичный вид. Блок B'{currentBlock} имеет вид: {B4bitblock}" + Environment.NewLine;
                currentBlock++; //увеличиваем номер блока
                a.Clear();
                b.Clear();
            }
            StringBuilder B1B8 = new StringBuilder(32); //Инициализируем 32 битный блок, сочетающий в себе все блоки B'
            for (int i=0;i<8;i++)
            {
                B1B8.Append(ShowBitArrayInString(B4bitblocks[i]));
            }
            //logString += $"32-битный блок B'1-B'8 имеет вид: {B1B8}";
            BitArray output = new BitArray(32);//инициализируем итоговый массив с результатом. Результатом является перестановленные элементы блока B'1-B'8
            BitArray temp = ShowStringInBitArray(B1B8.ToString()); //создаем временный битовый массив, содержащий значения блока B'1-B'8
            for (int i=0;i<32;i++)
            {
                output[i] = temp[Pplacements[i]-1];
            }
            //logString += $"Выходной блок из функции Фейстеля (блоки B'1-B'8 со смещениями P): {ShowBitArrayInString(output)}"+Environment.NewLine;
            return output;
        }
        /// <summary>
        /// Функция для генерации ключей ki шифра DES
        /// </summary>
        /// <param name="input_key">56-битный или 64-битный ключ (обязательно)</param>
        public void GenerateKeysForDes(BitArray input_key)
        {
            StringBuilder key=new StringBuilder();
            int trueCount = 0; //переменная, хранящая информацию о количестве единиц
            for (int i=1;i<input_key.Length+1;i++) //получаем блок из 7 бит 8 раз
            { 
                if (input_key[i-1]==false)
                    key.Append('0');//присваиваем значение в итоговый 64-битный ключ
                if (input_key[i-1] == true)
                {
                    key.Append('1');
                    trueCount++;
                } 
                if (i % 7 == 0 && input_key.Length!=64)//если уже достигнут 7 символ, то добавляем проверочный бит (только для ключа 56 бит!!!)
                {
                    if (trueCount % 2 == 0) //если количество единиц четное
                        key.Append('1'); //то делаем так, чтобы они стали нечетными, добавляя в блок дополнительную единицу
                    else
                        key.Append('0'); //иначе если их количество уже нечетное, то добавляем ноль
                    trueCount = 0;//обнуляем счетчик
                }
            }
            //if (input_key.Length!=64)
            //logString += $"Получен 56 битный ключ. Он был расширен до 64 бит. Теперь ключ имеет вид {key}"+Environment.NewLine;
            StringBuilder C, D; //создаем временные блоки в виде строк
            C = new StringBuilder();
            D = new StringBuilder();
            for (int i=0;i<CDPlacements.Length;i++)
            {
                if (i < 28) //если i еще в первой половине, то помещаем в блок C0 символ из расширенного ключа
                {
                    C.Append(key[CDPlacements[i]-1]); //ВАЖНО! Вычитается единица, поскольку значения в CDPlacements начинаются с 1, а массив - с нуля
                }
                else //иначе помещаем в блок D0 символ
                    D.Append(key[CDPlacements[i]-1]);
            }
            //logString += $"Были созданы блоки C0 D0 с перестановленным содержимым. Содержимое блока C0: {C}" + Environment.NewLine+ $"Содержимое блока D0: {D}" +Environment.NewLine;
            string C0D0 = C.ToString() + D;
            //logString += $"Нулевой вектор C0D0 имеет вид: {C0D0}"+Environment.NewLine;
            BitArray[] vectorsForkeys = new BitArray[16];//создаем битовый массив, хранящий вектора CD
            string[,] blocks=new string[16,2]; //создаем двумерный строковый массив - хранилище блоков
            string[] vectors = new string[16];//создаем строковый массив векторов
            BitArray tempC, tempD; //создаем временные массивы битов
            for (int i=0;i<16;i++) //генерируем векторы CD
            {
                vectorsForkeys[i] = new BitArray(56); //инициализируем вектор
                if (i == 0) //если это самый первый элемент, то берем блоки C0D0
                {
                    tempC = ShowStringInBitArray(C.ToString());
                    tempD = ShowStringInBitArray(D.ToString());
                }   
                else //иначе берем предыдущий вектор
                {
                    tempC = ShowStringInBitArray(blocks[i - 1, 0]);
                    tempD = ShowStringInBitArray(blocks[i - 1, 1]);
                }
                if (i==0 || i==1||i==8||i==15) //это итерации, в которых происходит всего 1 сдвиг влево
                {
                    //сдвигаем у обоих блоков значения
                    this.LeftShift(ref tempC, 1);
                    this.LeftShift(ref tempD, 1);
                }
                else //иначе сдвигаем 2 раза влево
                {
                    //сдвигаем у обоих блоков значения
                    this.LeftShift(ref tempC, 2);
                    this.LeftShift(ref tempD, 2);
                }
                blocks[i, 0] = ShowBitArrayInString(tempC);//заносим в строковый массив блоков значения массива битов
                blocks[i, 1] = ShowBitArrayInString(tempD);
                //logString += $"Получаем {i+1}-е блоки:"+Environment.NewLine+$"C{i + 1} = {blocks[i, 0]}" + Environment.NewLine + $"D{i + 1} = {blocks[i, 1]}" + Environment.NewLine;
                vectors[i] = blocks[i, 0] + blocks[i, 1]; //для отображения данных
                //logString += $"{i + 1}-й вектор имеет вид: {vectors[i]}"+Environment.NewLine;
            }
            DESkeys = new BitArray[16]; //инициализируем хранилище ключей
            for (int i=0;i<16;i++) //создаем 16 ключей DES
            {
                key.Clear();
                for (int j=0;j<KeyPlacements.Length;j++) //В цикле размещаем в первый ключ значения из первого вектора, и делаем перестановку
                {
                    key.Append(vectors[i][KeyPlacements[j]-1]);//ВАЖНО! Вычитается единица, поскольку значения в CDPlacements начинаются с 1, а массив - с нуля
                }
                DESkeys[i] = ShowStringInBitArray(key.ToString()); //преобразовываем ключ из строк в набор нулей и единиц
                //logString += $"{i + 1}-й ключ имеет вид: {ShowBitArrayInString(DESkeys[i])}" + Environment.NewLine;
            }
        }
        /// <summary>
        /// Конвертирует входной массив битов в строку string
        /// </summary>
        /// <param name="input">Массив битов в логическом виде</param>
        /// <returns>Строка с битам</returns>
        public string ShowBitArrayInString(BitArray input)
        {
            StringBuilder bits = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == true)
                    bits.Append("1");
                else
                    bits.Append("0");
            }
            return bits.ToString();
        }
        private BitArray ShowStringInBitArray(string input)
        {
            BitArray bits=new BitArray(input.Length);
            for (int i=0;i<input.Length;i++)
            {
                if (input[i] == '1')
                    bits.Set(i, true);
                else
                    bits.Set(i, false);
            }
            return bits;
        }
        //public string getLog()
        //{
        //    return logString;
        //}
        //public void clearLog()
        //{
        //    logString = "";
        //}
        /// <summary>
        /// Выполняет операцию левого сдвига на count символов
        /// </summary>
        /// <param name="input">Входной массив, в который будет записан результат</param>
        /// <param name="count">Количество сдвигов влево</param>
        private void LeftShift(ref BitArray input,int count)
        {
            //Неприятная особенность встроенного метода LeftShift в BitArray - он не помещает в конец значение сдвинутого бита. Этот метод исправляет это
            bool first_bool;
            for (int i=0;i<count;i++)
            {
                first_bool = input[0];
                for (int j = 1; j < input.Count; j++)
                {
                    input[j - 1] = input[j];
                }

                input[input.Count - 1] = first_bool;
            }
        }
        /// <summary>
        /// Копирует массив BitArray
        /// </summary>
        /// <param name="input">Массив, который необходимо скопировать</param>
        /// <returns>Скопированный массив</returns>
        private BitArray Copy(BitArray input)
        {
            BitArray output = new BitArray(input.Length);
            for (int i=0;i<input.Length;i++) 
                output[i] = input[i];
            return output;
        }

    }
    
}
