using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace KRIP_DES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Shifr_Click(object sender, EventArgs e)
        {
            char[] text = Text.Text.ToCharArray();
            string shifr_text;
            int[] Q = new int[64];
            int[] table = { 57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18, 10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36, 63, 55, 47, 39, 31, 23, 15, 7, 62, 52, 46, 38, 30, 22, 14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4 };
            int[] Q_table = new int[56];          //  char[] binaryStr_key = new char[text.Length * 8];
            Random rnd = new Random();
            int value = 0;
            string Q_string = "0011000100110010001100110011010000110101001101100011011100111000";
            //for (int i = 0; i < Q.Length; i++)
            //{
            //    if ((i + 1) % 8 == 0)
            //    {
            //        Q[i] = 0;
            //    }
            //    else
            //    {
            //        value = rnd.Next(0, 2);
            //        Q[i] = value;
            //    }
            //    keyBox.Text = keyBox.Text + Q[i];
            //}

            for(int i =0; i < 64; i++)
            {
                if (Q_string[i] == '0')
                    Q[i] = 0;
                else
                    Q[i] = 1;
                keyBox.Text = keyBox.Text + Q[i];
            }

            int[] C_0 = new int[28];
            int[] D_0 = new int[28];
            for (int i = 0, j = 0; i < Q_table.Length; i++)
            {
                if (i < 28)
                {
                    C_0[i] = Q[table[i] - 1];
                }
                else
                {
                    D_0[j] = Q[table[i] - 1];
                    j++;
                }

            }

            int[][] C = new int[16][];
            int[][] D = new int[16][];

            C[0] = new int[28];
            C[1] = new int[28];
            C[2] = new int[28];
            C[3] = new int[28];
            C[4] = new int[28];
            C[5] = new int[28];
            C[6] = new int[28];
            C[7] = new int[28];
            C[8] = new int[28];
            C[9] = new int[28];
            C[10] = new int[28];
            C[11] = new int[28];
            C[12] = new int[28];
            C[13] = new int[28];
            C[14] = new int[28];
            C[15] = new int[28];

            D[0] = new int[28];
            D[1] = new int[28];
            D[2] = new int[28];
            D[3] = new int[28];
            D[4] = new int[28];
            D[5] = new int[28];
            D[6] = new int[28];
            D[7] = new int[28];
            D[8] = new int[28];
            D[9] = new int[28];
            D[10] = new int[28];
            D[11] = new int[28];
            D[12] = new int[28];
            D[13] = new int[28];
            D[14] = new int[28];
            D[15] = new int[28];

            for (int i = 0, p = 0; i < 16; i++) // сдвиг
            {
                for (int j = 0; j < C_0.Length; j++)
                {
                    if (i == 0)
                    {
                        if (j - 1 < 0)
                        {
                            p = j - 1 + 28;
                        }
                        else
                        {
                            p = j - 1;
                        }

                        C[i][p] = C_0[j];
                        D[i][p] = D_0[j];
                    }
                    else
                    {
                        if (i == 1 || i == 8 || i == 15)
                        {
                            if (j - 1 < 0)
                            {
                                p = j - 1 + 28;
                            }
                            else
                            {
                                p = j - 1;
                            }
                            C[i][p] = C[i - 1][j];
                            D[i][p] = D[i - 1][j];
                        }
                        else
                        {
                            if (j - 2 < 0)
                            {
                                p = j - 2 + 28;
                            }
                            else
                            {
                                p = j - 2;
                            }
                            C[i][p] = C[i - 1][j];
                            D[i][p] = D[i - 1][j];
                        }
                    }
                }
            }

            int[][] K = new int[16][];

            int[] K_table = { 14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32 };
            K[0] = new int[48];
            K[1] = new int[48];
            K[2] = new int[48];
            K[3] = new int[48];
            K[4] = new int[48];
            K[5] = new int[48];
            K[6] = new int[48];
            K[7] = new int[48];
            K[8] = new int[48];
            K[9] = new int[48];
            K[10] = new int[48];
            K[11] = new int[48];
            K[12] = new int[48];
            K[13] = new int[48];
            K[14] = new int[48];
            K[15] = new int[48];

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0, p = 0; j < K_table.Length; j++)
                {
                    if ((K_table[j] - 1) > 27)
                    {
                        p = D[i][(K_table[j] - 1) % 28];
                    }
                    else
                    {
                        p = C[i][K_table[j] - 1];
                    }
                    K[i][j] = p;
                }
            }
            string filename2 = " ";
            bool check_read_file = false;

            if (text.Length == 0)
            {
                check_read_file = true; 
                openFileDialog1.ShowDialog();
                string filename = openFileDialog1.FileName;
                string fileText = System.IO.File.ReadAllText(filename, Encoding.GetEncoding(1251));
                text = fileText.ToCharArray();                          
            }
            
            StringBuilder sb = new StringBuilder();
            foreach (byte b in System.Text.Encoding.Unicode.GetBytes(text))
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            string binaryStr_16 = sb.ToString();
          //  char[] binaryStr_16 = text; // входная последовательность
            
            int length = 0;

            if (binaryStr_16.Length % 64 != 0) // дополнение входной последовательности
            {
                length = binaryStr_16.Length + 64 - (binaryStr_16.Length % 64);
            }
            else
            {
                length = binaryStr_16.Length;
            }
            char[] binaryStr_charALL64 = new char[length];

            for(int i = 0; i < binaryStr_16.Length; i++)
            {
                if (binaryStr_16[i] == '0')
                    binaryStr_charALL64[i] = '0';
                else
                    binaryStr_charALL64[i] = '1';
            }

            int m = 0;
            char[] binaryStr_char16 = new char[64];
            char[] buff_IP = new char[64];
            while (m < binaryStr_charALL64.Length) // шифрование
            {
                for (int i = 0; i < 64; i++)
                {
                    buff_IP[i] = binaryStr_charALL64[m];
                    m++;
                }
                //   int[] binaryStr_int16 = new int[binaryStr_16.Length];
                int[] IP = { 58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4, 62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8, 57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3, 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7 };


                for(int i = 0; i < 64; i++)
                {
                    binaryStr_char16[i] = buff_IP[IP[i] - 1];
                }
                int[] L_0 = new int[32];
                int[] R_0 = new int[32];
                for (int i = 0, p = 0; i < 64; i++)
                {
                    if (i < 32)
                    {
                        if (binaryStr_char16[i] == '1')
                        {
                            L_0[i] = 1;
                        }
                        else
                            L_0[i] = 0;
                    }
                    else
                    {
                        if (binaryStr_char16[i] == '1')
                        {
                            R_0[p] = 1;
                            p++;
                        }
                        else
                        {
                            R_0[p] = 0;
                            p++;
                        }

                    }
                }

                int[][] L = new int[16][];
                int[][] R = new int[16][];

                L[0] = new int[32];
                L[1] = new int[32];
                L[2] = new int[32];
                L[3] = new int[32];
                L[4] = new int[32];
                L[5] = new int[32];
                L[6] = new int[32];
                L[7] = new int[32];
                L[8] = new int[32];
                L[9] = new int[32];
                L[10] = new int[32];
                L[11] = new int[32];
                L[12] = new int[32];
                L[13] = new int[32];
                L[14] = new int[32];
                L[15] = new int[32];

                R[0] = new int[32];
                R[1] = new int[32];
                R[2] = new int[32];
                R[3] = new int[32];
                R[4] = new int[32];
                R[5] = new int[32];
                R[6] = new int[32];
                R[7] = new int[32];
                R[8] = new int[32];
                R[9] = new int[32];
                R[10] = new int[32];
                R[11] = new int[32];
                R[12] = new int[32];
                R[13] = new int[32];
                R[14] = new int[32];
                R[15] = new int[32];

                int[] E = { 32, 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9, 8, 9, 10, 11, 12, 13, 12, 13, 14, 15, 16, 17, 16, 17, 18, 19, 20, 21, 20, 21, 22, 23, 24, 25, 24, 25, 26, 27, 28, 29, 28, 29, 30, 31, 32, 1 };
                int[] R_E = new int[48];
                int[] R_E_K = new int[48];


                int[,] S_table_1 = new int[,] { { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 }, { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 }, { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 }, { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 } };
                int[,] S_table_2 = new int[,] { { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 }, { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 }, { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 }, { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 } };
                int[,] S_table_3 = new int[,] { { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 }, { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 }, { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 }, { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 } };
                int[,] S_table_4 = new int[,] { { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 }, { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 }, { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 }, { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 } };
                int[,] S_table_5 = new int[,] { { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 }, { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 }, { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 }, { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 } };
                int[,] S_table_6 = new int[,] { { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 }, { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 }, { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 }, { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 } };
                int[,] S_table_7 = new int[,] { { 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 }, { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 }, { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 }, { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 } };
                int[,] S_table_8 = new int[,] { { 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 }, { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 }, { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 }, { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 } };
                //   char[] x_2 = new char[2];
                string x_2 = "  ";
                string x_4 = "    ";
                //  char[] x_4 = new char[4];
                string buff;
                int[] S_values = new int[8];
                string BinaryCode;
                int[] P_table = { 16, 7, 20, 21, 29, 12, 28, 17, 1, 15, 23, 26, 5, 18, 31, 10, 2, 8, 24, 14, 32, 27, 3, 9, 19, 13, 30, 6, 22, 11, 4, 25 };
                int[] P_table_1 = { 40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31, 38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29, 36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27, 34, 2, 42, 10, 50, 18, 28, 26, 33, 1, 41, 9, 49, 17, 57, 25 };
                int[] P = new int[32];
                int[] P_1 = new int[64];
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 32; j++)
                    {
                        if (i == 0)
                        {
                            L[i][j] = R_0[j];
                        }
                        else
                        {
                            L[i][j] = R[i - 1][j];
                        }
                    }
                    if (i == 0)
                    {
                        for (int k = 0; k < 48; k++) // расширение - E с 32х до 48
                        {
                            R_E[k] = R_0[E[k] - 1];
                            R_E_K[k] = (R_E[k] + K[i][k]) % 2; // сложение с ключом
                        }
                        for (int k = 0, p = 0, x2 = 0, x4 = 0, s = 0; k < 48; k++)
                        {
                            if (k % 6 == 0)
                            {
                                x_2 = R_E_K[k].ToString();
                            }
                            if (k % 6 == 5)
                            {
                                x_2 = x_2 + R_E_K[k].ToString();
                                p = 0;
                                buff = x_2;
                                x2 = Convert.ToInt32(buff, 2);
                                buff = x_4;
                                x4 = Convert.ToInt32(buff, 2);
                                if(s == 0)
                                    S_values[s] = S_table_1[x2, x4];
                                if (s == 1)
                                    S_values[s] = S_table_2[x2, x4];
                                if (s == 2)
                                    S_values[s] = S_table_3[x2, x4];
                                if (s == 3)
                                    S_values[s] = S_table_4[x2, x4];
                                if (s == 4)
                                    S_values[s] = S_table_5[x2, x4];
                                if (s == 5)
                                    S_values[s] = S_table_6[x2, x4];
                                if (s == 6)
                                    S_values[s] = S_table_7[x2, x4];
                                if (s == 7)
                                    S_values[s] = S_table_8[x2, x4];
                                s++;
                                //  z++;
                            }
                            else if (k % 6 != 0)
                            {
                                if (p == 0)
                                {
                                    x_4 = R_E_K[k].ToString();
                                    p++;
                                }
                                else
                                {
                                    x_4 = x_4 + R_E_K[k].ToString();
                                }

                            }
                        }
                        BinaryCode = Convert.ToString(S_values[0], 2).PadLeft(4, '0');
                        for (int k = 1; k < 8; k++)
                        {
                            BinaryCode = BinaryCode + Convert.ToString(S_values[k], 2).PadLeft(4, '0');
                        }
                        for (int k = 0; k < 32; k++)
                        {
                            if (BinaryCode[P_table[k] - 1] == '0')
                                P[k] = 0;
                            else
                                P[k] = 1;
                        }
                    }
                    else
                    {
                        for (int k = 0; k < 48; k++) // расширение - E с 32х до 48
                        {
                            R_E[k] = R[i - 1][E[k] - 1];
                            R_E_K[k] = (R_E[k] + K[i][k]) % 2; // сложение с ключом
                        }
                        for (int k = 0, p = 0, x2 = 0, x4 = 0, s = 0; k < 48; k++)
                        {
                            if (k % 6 == 0)
                            {
                                x_2 = R_E_K[k].ToString();
                            }
                            if (k % 6 == 5)
                            {
                                x_2 = x_2 + R_E_K[k].ToString();
                                p = 0;
                                buff = x_2;
                                x2 = Convert.ToInt32(buff, 2);
                                buff = x_4;
                                x4 = Convert.ToInt32(buff, 2);
                                if (s == 0)
                                    S_values[s] = S_table_1[x2, x4];
                                if (s == 1)
                                    S_values[s] = S_table_2[x2, x4];
                                if (s == 2)
                                    S_values[s] = S_table_3[x2, x4];
                                if (s == 3)
                                    S_values[s] = S_table_4[x2, x4];
                                if (s == 4)
                                    S_values[s] = S_table_5[x2, x4];
                                if (s == 5)
                                    S_values[s] = S_table_6[x2, x4];
                                if (s == 6)
                                    S_values[s] = S_table_7[x2, x4];
                                if (s == 7)
                                    S_values[s] = S_table_8[x2, x4];
                                s++;
                                //  z++;
                            }
                            else if (k % 6 != 0)
                            {
                                if (p == 0)
                                {
                                    x_4 = R_E_K[k].ToString();
                                    p++;
                                }
                                else
                                {
                                    x_4 = x_4 + R_E_K[k].ToString();
                                }

                            }
                        }
                        BinaryCode = Convert.ToString(S_values[0], 2).PadLeft(4, '0');
                        for (int k = 1; k < 8; k++)
                        {
                            BinaryCode = BinaryCode + Convert.ToString(S_values[k], 2).PadLeft(4, '0');
                        }
                        for (int k = 0; k < 32; k++)
                        {
                            if (BinaryCode[P_table[k] - 1] == '0')
                                P[k] = 0;
                            else
                                P[k] = 1;
                        }
                    }
                    for (int j = 0; j < 32; j++)
                    {
                        if (i == 0)
                        {
                            R[i][j] = (L_0[j] + P[j]) % 2;
                        }
                        else
                        {
                            R[i][j] = (L[i - 1][j] + P[j]) % 2;
                        }

                    }
                    if (i == 15)
                    {
                        for (int j = 0, p = 0; j < 64; j++)
                        {
                            if (P_table_1[j] - 1 >= 32)
                            {
                                p = L[i][P_table_1[j] - 1 - 32];
                            }
                            else
                            {
                                p = R[i][P_table_1[j] - 1];
                            }

                            P_1[j] = p;
                            shifr_text = P_1[j].ToString();
                            ShifrText.Text = ShifrText.Text + shifr_text;                            
                        }                                                                                            
                    }
                }
            }
            if (check_read_file)
            {
                saveFileDialog1.ShowDialog();
                filename2 = saveFileDialog1.FileName;
                StreamWriter streamwriter = new System.IO.StreamWriter(filename2, false, System.Text.Encoding.GetEncoding("utf-8"));
                streamwriter.Write(ShifrText.Text);
                streamwriter.Close();
            }           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_unShifr_Click(object sender, EventArgs e)
        {
            char[] text = ShifrText.Text.ToCharArray();
            char[] Q_text = keyBox.Text.ToCharArray();
            int[] Q = new int[64];
            int[] table = { 57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18, 10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36, 63, 55, 47, 39, 31, 23, 15, 7, 62, 52, 46, 38, 30, 22, 14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4 };

            for (int i = 0; i < Q.Length; i++)
            {
                if (Q_text[i] == '0')
                    Q[i] = 0;
                else
                    Q[i] = 1;
            }


            int[] C_0 = new int[28];
            int[] D_0 = new int[28];
            for (int i = 0, j = 0; i < 56; i++)
            {
                if (i < 28)
                {
                    C_0[i] = Q[table[i] - 1];
                }
                else
                {
                    D_0[j] = Q[table[i] - 1];
                    j++;
                }

            }

            int[][] C = new int[16][];
            int[][] D = new int[16][];

            C[0] = new int[28];
            C[1] = new int[28];
            C[2] = new int[28];
            C[3] = new int[28];
            C[4] = new int[28];
            C[5] = new int[28];
            C[6] = new int[28];
            C[7] = new int[28];
            C[8] = new int[28];
            C[9] = new int[28];
            C[10] = new int[28];
            C[11] = new int[28];
            C[12] = new int[28];
            C[13] = new int[28];
            C[14] = new int[28];
            C[15] = new int[28];

            D[0] = new int[28];
            D[1] = new int[28];
            D[2] = new int[28];
            D[3] = new int[28];
            D[4] = new int[28];
            D[5] = new int[28];
            D[6] = new int[28];
            D[7] = new int[28];
            D[8] = new int[28];
            D[9] = new int[28];
            D[10] = new int[28];
            D[11] = new int[28];
            D[12] = new int[28];
            D[13] = new int[28];
            D[14] = new int[28];
            D[15] = new int[28];

            for (int i = 0, p = 0; i < 16; i++) // сдвиг
            {
                for (int j = 0; j < C_0.Length; j++)
                {
                    if (i == 0)
                    {
                        if (j - 1 < 0)
                        {
                            p = j - 1 + 28;
                        }
                        else
                        {
                            p = j - 1;
                        }

                        C[i][p] = C_0[j];
                        D[i][p] = D_0[j];
                    }
                    else
                    {
                        if (i == 1 || i == 8 || i == 15)
                        {
                            if (j - 1 < 0)
                            {
                                p = j - 1 + 28;
                            }
                            else
                            {
                                p = j - 1;
                            }
                            C[i][p] = C[i - 1][j];
                            D[i][p] = D[i - 1][j];
                        }
                        else
                        {
                            if (j - 2 < 0)
                            {
                                p = j - 2 + 28;
                            }
                            else
                            {
                                p = j - 2;
                            }
                            C[i][p] = C[i - 1][j];
                            D[i][p] = D[i - 1][j];
                        }
                    }
                }
            }

            int[][] K = new int[16][];

            int[] K_table = { 14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32 };
            K[0] = new int[48];
            K[1] = new int[48];
            K[2] = new int[48];
            K[3] = new int[48];
            K[4] = new int[48];
            K[5] = new int[48];
            K[6] = new int[48];
            K[7] = new int[48];
            K[8] = new int[48];
            K[9] = new int[48];
            K[10] = new int[48];
            K[11] = new int[48];
            K[12] = new int[48];
            K[13] = new int[48];
            K[14] = new int[48];
            K[15] = new int[48];

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0, p = 0; j < K_table.Length; j++)
                {
                    if ((K_table[j] - 1) > 27)
                    {
                        p = D[i][(K_table[j] - 1) % 28];
                    }
                    else
                    {
                        p = C[i][K_table[j] - 1];
                    }
                    K[i][j] = p;
                }
            }
            bool check_read_file = false;
            if (text.Length == 0)
            {
                check_read_file = true;
                openFileDialog1.ShowDialog();
                string filename = openFileDialog1.FileName;
                string fileText = System.IO.File.ReadAllText(filename, Encoding.GetEncoding(1251));
                text = fileText.ToCharArray();

            }

            char[] binaryStr_charALL = text;
            char[] binaryStr_char16 = new char[64];
            char[] buff_IP = new char[64];
            int m = 0;
            while (m < binaryStr_charALL.Length) // шифрование
            {
                for (int i = 0; i < 64; i++)
                {
                    buff_IP[i] = binaryStr_charALL[m];
                    m++;
                }

                
                //   int[] binaryStr_int16 = new int[binaryStr_16.Length];
                int[] IP = { 58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4, 62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8, 57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3, 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7 };
                int[] P_table_1 = { 40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31, 38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29, 36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27, 34, 2, 42, 10, 50, 18, 28, 26, 33, 1, 41, 9, 49, 17, 57, 25 };
                for (int i = 0; i < 64; i++)
                {
                    binaryStr_char16[i] = buff_IP[IP[i] - 1];
                }

                int[][] L = new int[16][];
                int[][] R = new int[16][];

                L[0] = new int[32];
                L[1] = new int[32];
                L[2] = new int[32];
                L[3] = new int[32];
                L[4] = new int[32];
                L[5] = new int[32];
                L[6] = new int[32];
                L[7] = new int[32];
                L[8] = new int[32];
                L[9] = new int[32];
                L[10] = new int[32];
                L[11] = new int[32];
                L[12] = new int[32];
                L[13] = new int[32];
                L[14] = new int[32];
                L[15] = new int[32];

                R[0] = new int[32];
                R[1] = new int[32];
                R[2] = new int[32];
                R[3] = new int[32];
                R[4] = new int[32];
                R[5] = new int[32];
                R[6] = new int[32];
                R[7] = new int[32];
                R[8] = new int[32];
                R[9] = new int[32];
                R[10] = new int[32];
                R[11] = new int[32];
                R[12] = new int[32];
                R[13] = new int[32];
                R[14] = new int[32];
                R[15] = new int[32];

                for (int i = 0, p = 0; i < 64; i++)
                {
                    if (i < 32)
                    {
                        if (binaryStr_char16[i] == '1')
                        {
                            R[15][i] = 1;
                        }
                        else
                            R[15][i] = 0;
                    }
                    else
                    {
                        if (binaryStr_char16[i] == '1')
                        {
                            L[15][p] = 1;
                            p++;
                        }
                        else
                        {
                            L[15][p] = 0;
                            p++;
                        }

                    }
                }

                int[] E = { 32, 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9, 8, 9, 10, 11, 12, 13, 12, 13, 14, 15, 16, 17, 16, 17, 18, 19, 20, 21, 20, 21, 22, 23, 24, 25, 24, 25, 26, 27, 28, 29, 28, 29, 30, 31, 32, 1 };
                int[] R_E = new int[48];
                int[] R_E_K = new int[48];


                int[,] S_table_1 = new int[,] { { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 }, { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 }, { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 }, { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 } };
                int[,] S_table_2 = new int[,] { { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 }, { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 }, { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 }, { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 } };
                int[,] S_table_3 = new int[,] { { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 }, { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 }, { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 }, { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 } };
                int[,] S_table_4 = new int[,] { { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 }, { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 }, { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 }, { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 } };
                int[,] S_table_5 = new int[,] { { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 }, { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 }, { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 }, { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 } };
                int[,] S_table_6 = new int[,] { { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 }, { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 }, { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 }, { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 } };
                int[,] S_table_7 = new int[,] { { 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 }, { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 }, { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 }, { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 } };
                int[,] S_table_8 = new int[,] { { 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 }, { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 }, { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 }, { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 } };

                string x_2 = "  ";
                string x_4 = "    ";

                string buff;
                int[] S_values = new int[8];
                string BinaryCode;
                int[] P_table = { 16, 7, 20, 21, 29, 12, 28, 17, 1, 15, 23, 26, 5, 18, 31, 10, 2, 8, 24, 14, 32, 27, 3, 9, 19, 13, 30, 6, 22, 11, 4, 25 };
                
                int[] P = new int[32];
                int[] P_1 = new int[64];

                int[] L_0 = new int[32];
                int[] R_0 = new int[32];
                for (int i = 15; i > -1; i--)
                {
                    for (int j = 0; j < 32; j++)
                    {
                        if( i == 0)
                        {
                            R_0[j] = L[i][j];
                        }
                        else
                            R[i - 1][j] = L[i][j];
                    }
                   
                        for (int k = 0; k < 48; k++) // расширение - E с 32х до 48
                        {
                            R_E[k] = L[i][E[k] - 1];
                            R_E_K[k] = (R_E[k] + K[i][k]) % 2; // сложение с ключом
                        }
                        for (int k = 0, p = 0, x2 = 0, x4 = 0, s = 0; k < 48; k++)
                        {
                            if (k % 6 == 0)
                            {
                                x_2 = R_E_K[k].ToString();
                            }
                            if (k % 6 == 5)
                            {
                                x_2 = x_2 + R_E_K[k].ToString();
                                p = 0;
                                buff = x_2;
                                x2 = Convert.ToInt32(buff, 2);
                                buff = x_4;
                                x4 = Convert.ToInt32(buff, 2);
                            if (s == 0)
                                S_values[s] = S_table_1[x2, x4];
                            if (s == 1)
                                S_values[s] = S_table_2[x2, x4];
                            if (s == 2)
                                S_values[s] = S_table_3[x2, x4];
                            if (s == 3)
                                S_values[s] = S_table_4[x2, x4];
                            if (s == 4)
                                S_values[s] = S_table_5[x2, x4];
                            if (s == 5)
                                S_values[s] = S_table_6[x2, x4];
                            if (s == 6)
                                S_values[s] = S_table_7[x2, x4];
                            if (s == 7)
                                S_values[s] = S_table_8[x2, x4];
                            s++;
                            //  z++;
                        }
                            else if (k % 6 != 0)
                            {
                                if (p == 0)
                                {
                                    x_4 = R_E_K[k].ToString();
                                    p++;
                                }
                                else
                                {
                                    x_4 = x_4 + R_E_K[k].ToString();
                                }

                            }
                        }
                        BinaryCode = Convert.ToString(S_values[0], 2).PadLeft(4, '0');
                        for (int k = 1; k < 8; k++)
                        {
                            BinaryCode = BinaryCode + Convert.ToString(S_values[k], 2).PadLeft(4, '0');
                        }
                        for (int k = 0; k < 32; k++)
                        {
                            if (BinaryCode[P_table[k] - 1] == '0')
                                P[k] = 0;
                            else
                                P[k] = 1;
                        }                                    
                 
                    for (int j = 0; j < 32; j++)
                    {
                        if (i == 0)
                        {
                            L_0[j] = (R[i][j] + P[j]) % 2;
                        }
                        else
                        {
                            L[i - 1][j] = (R[i][j] + P[j]) % 2;
                        }

                    }
                    if (i == 0)
                    {
                        char[] result_decrypt = new char[64];
                        for (int j = 0, p = 0; j < 64; j++)
                        {
                            if (P_table_1[j] - 1 >= 32)
                            {
                                p = R_0[P_table_1[j] - 1 - 32];
                            }
                            else
                            {
                                p = L_0[P_table_1[j] - 1];
                            }
                            P_1[j] = p;
                            if(P_1[j] == 0)
                            {
                                result_decrypt[j] = '0';
                            }
                            else
                            {
                                result_decrypt[j] = '1';
                            }                           
                        }
                        string text1 = new string(result_decrypt);
                        var stringArray = Enumerable.Range(0, text1.Length / 8).Select(k => Convert.ToByte(text1.Substring(k * 8, 8), 2)).ToArray();
                        var str = Encoding.Unicode.GetString(stringArray);

                        Text.Text = Text.Text + str;                        
                    }
                }
            }
            if (check_read_file)
            {
                saveFileDialog1.ShowDialog();
                string filename2 = saveFileDialog1.FileName;
                StreamWriter streamwriter = new System.IO.StreamWriter(filename2, false, System.Text.Encoding.GetEncoding("utf-8"));
                streamwriter.Write(Text.Text);
                streamwriter.Close();
            }
        }
    }
}
