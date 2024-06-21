using System.Windows;
using System.Globalization;
using System.Windows.Forms;
using System;
using System.Numerics;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace vijenera
{
    public partial class MainWindow : Window
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        private byte[] Key;
        byte[] byte_keys;
        byte[] byte_ishod;
        byte[] res;
        byte[] res_2;

        public MainWindow()
        {
            InitializeComponent();
            textbox_shifr_text.IsReadOnly = true;
            textbox_deshifr_text.IsReadOnly = true;
            textbox_shifr_text_2.IsReadOnly = true;
            textbox_deshifr_text_2.IsReadOnly = true;
            textbox_ishod_2.IsReadOnly = true;
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_keys.Text == "" || textbox_ishod_text.Text == "")
            {
                System.Windows.MessageBox.Show("Введите данные");
                return;
            }
            char[] keys_text = textbox_keys.Text.ToLower().ToCharArray();
            char[] ishod_text = textbox_ishod_text.Text.ToLower().ToCharArray();

            byte_keys = Encoding.Unicode.GetBytes(keys_text);
            byte_ishod = Encoding.Unicode.GetBytes(ishod_text);
            res = new byte[byte_ishod.Length];

            for (int i = 0; i < byte_ishod.Length; i++)
            {
                res[i] = (byte)(byte_ishod[i] ^ byte_keys[i % byte_keys.Length]);
            }

            string shifr_2 = string.Join(" ", res.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
            string shifr = Encoding.Unicode.GetString(res);
            string keys_2 = string.Join(" ", byte_keys.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
            string text_2 = string.Join(" ", byte_ishod.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));

            textbox_shifr_text.Text = shifr;
            textbox_shifr_text_2.Text = shifr_2;
            textbox_keys_2.Text = keys_2;
            textbox_ishod_2.Text = text_2;

        }

        private void Rus_Click(object sender, RoutedEventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("ru-RU"));
            System.Windows.MessageBox.Show("Пишите пожалуйста весь текст только на русском");
        }

        private void Eng_Click(object sender, RoutedEventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("en-EN"));
            System.Windows.MessageBox.Show("Пишите пожалуйста весь текст только на английском");
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            textbox_shifr_text.Clear();
            textbox_shifr_text_2.Clear();
            textbox_deshifr_text.Clear();
            textbox_deshifr_text_2.Clear();
            textbox_ishod_2.Clear();
            textbox_keys_2.Clear();
            textbox_keys.Clear();
        }

        private void Deshifr_button_Click(object sender, RoutedEventArgs e)
        {
            res_2 = new byte[byte_ishod.Length];
            for (int i = 0; i < res_2.Length; i++)
            {
                res_2[i] = (byte)(res[i] ^ byte_keys[i % byte_keys.Length]);
            }
            string deshifr = Encoding.Unicode.GetString(res_2);
            string deshifr_2 = string.Join(" ", res_2.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));

            textbox_deshifr_text_2.Text = deshifr_2;
            textbox_deshifr_text.Text = deshifr;
        }

        private void Genn_keys_Click(object sender, RoutedEventArgs e)
        {
            byte[] arr = new byte[Encoding.Unicode.GetByteCount(textbox_ishod_text.Text)];
            rngCsp.GetBytes(arr);
            Key = arr;
            int t = GEN(Key);
        }
        public int GEN(byte[] Key)
        {
            byte[] arr = new byte[Encoding.Unicode.GetByteCount(textbox_ishod_text.Text)];
            rngCsp.GetBytes(arr);
            Key = arr;
            StringBuilder p = new StringBuilder();
            foreach (byte d in Key)
                p.Append(Convert.ToString(d, toBase: 2));

            Regex r = new Regex(@"1");
            int ones = r.Matches(p.ToString()).Count;
            int zeros = arr.Length * 8 - ones;
            if (ones != zeros)
                return GEN(Key);
            one_zero.Content = $"1: {ones} 0: {zeros}";

            string tt = Encoding.Unicode.GetString(arr);
            textbox_keys.Text = tt;

            string keys_2 = string.Join(" ", arr.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
            textbox_keys_2.Text = keys_2;
            return 0;
        }
    }
}

































//public byte[] GEN()
//{
//    byte[] arr = new byte[Encoding.UTF8.GetByteCount(textbox_ishod_text.Text)];
//    rngCsp.GetBytes(arr);
//    Key = arr;
//    StringBuilder p = new StringBuilder();
//    foreach (byte d in Key)
//        p.Append(Convert.ToString(d, toBase: 2));

//    return arr;
//}







//public int Proverka_keys(char[] Alfavit_1)
//{
//    keys = BigInteger.Parse(textbox_ishod_text.Text);
//    string keys_str = keys.ToString();

//    int size_keys = 0;

//    foreach (char h in keys_str)
//        if (h == '0' || h == '1')
//            size_keys++;

//    return size_keys;
//}





//public List<int> Ishod_text_v_dva(string[] test_words, char[] Alfavit_1)
//{
//    List<int> spicok = new List<int>();

//    //for (int i = 0; i < test_words.Length; i++)
//    //{
//    //    char[] one_word = test_words[i].ToCharArray();
//    //    foreach (char b in one_word)
//    //    {
//    //        for (int m = 0; m < Alfavit_1.Length; m++)
//    //        {
//    //            if (b == Alfavit_1[m])
//    //            {
//    //                int a = Array.IndexOf(Alfavit_1, Alfavit_1[m]);
//    //                spicok.Add(Convert.ToInt32(a));
//    //                textbox_ishod_2.Text += Convert.ToString(Convert.ToInt32(a), 2);
//    //            }
//    //        }

//    //    }
//    //}
//    return spicok;
//}






//if (Language(test_words, Alfavit_rus_a, Alfavit_angl_a) == 0)
//{
//    if (Proverka_keys(Alfavit_rus_a) == keys.Length)//язык русский
//    {

//        if (!Proverka_text(test_words, Alfavit_rus_a))
//            System.Windows.MessageBox.Show("Пишите только на русском!");
//        else
//            Shifr_gamma(keys, test_words, Alfavit_rus_a, k_rus);
//        language = "rus";
//    }
//    else
//    {
//        System.Windows.MessageBox.Show("Ключ не корректен, исправьте!");
//        return;
//    }

//}
//else if (Language(test_words, Alfavit_rus_a, Alfavit_angl_a) == 1)
//{
//    if (Proverka_keys(Alfavit_angl_a) == keys.Length)//язык англ
//    {
//        if (!Proverka_text(test_words, Alfavit_angl_a))
//            System.Windows.MessageBox.Show("Пишите только на английском!");
//        else
//            Shifr_gamma(keys, test_words, Alfavit_angl_a, k_angl);
//        language = "angl";
//    }
//    else
//    {
//        System.Windows.MessageBox.Show("Ключ не корректен, исправьте!");
//        return;
//    }
//}
//else if (Language(test_words, Alfavit_rus_a, Alfavit_angl_a) == 9)
//{
//    System.Windows.MessageBox.Show("Пишите русские или английские буквы!");
//}


















//    string test_shifr = textbox_ishod_text.Text;
//    string[] test_words = test_shifr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


//    byte[] text = Encoding.Unicode.GetBytes(test_shifr);
//    StringBuilder s = new StringBuilder(text.Length * 8);
//    for (int i = 0; i < text.Length; i++)
//        s.Append(Convert.ToString(text[i], toBase: 2));

//    textbox_ishod_2.Text += s.ToString();


//    byte[] keyss = Encoding.Unicode.GetBytes(textbox_keys.Text);
//    StringBuilder s_2 = new StringBuilder(text.Length * 8);
//    for (int j = 0; j < keyss.Length; j++)
//        s_2.Append(Convert.ToString(keyss[j], toBase: 2));

//    textbox_keys_2.Text += s_2.ToString();