using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace GrammarCheck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UserInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            var CorrectBrush = new SolidColorBrush(Colors.Green);
            var IncorrectBrush = new SolidColorBrush(Colors.White);
            string input = UserInput.Text;
            var task = Task.Run(() => CheckGrammars(input));
            task.Wait();
            var items = task.Result;
            Rule1Border.Background = items.Contains(1) ? CorrectBrush : IncorrectBrush;
            Rule2Border.Background = items.Contains(2) ? CorrectBrush : IncorrectBrush;
            Rule3Border.Background = items.Contains(3) ? CorrectBrush : IncorrectBrush;
            Rule4Border.Background = items.Contains(4) ? CorrectBrush : IncorrectBrush;
            Rule5Border.Background = items.Contains(5) ? CorrectBrush : IncorrectBrush;
            CheckResult.Content = string.Join(',',task.Result);

        }

        private async Task<List<int>> CheckGrammars(string input)
        {
            string result = string.Empty;
            bool rule1 = await Task.Run(() => CheckGrammar1(input));
            bool rule2 = await Task.Run(() => CheckGrammar2(input));
            bool rule3 = await Task.Run(() => CheckGrammar3(input));
            bool rule4 = await Task.Run(() => CheckGrammar4(input));
            bool rule5 = await Task.Run(() => CheckGrammar5(input));
            List<int> rules = new();
            if (rule1)
            {
                rules.Add(1);
            }
            if (rule2)
            {
                rules.Add(2);
            }
            if (rule3)
            {
                rules.Add(3);
            }
            if (rule4)
            {
                rules.Add(4);
            }
            if (rule5)
            {
                rules.Add(5);
            }


            return rules;    
        }

        private bool CheckGrammar1(string input)
        {
            var alphabet = new char[] {'a', 'b'};
            char[] chars = input.ToCharArray();

            if (!IsSubset(alphabet, chars))
                return false;
            int len = chars.Length;
            int iters = len / 2;
            for(int i = 0; i < iters && len > 1; i++)
            {
                if (chars[i] != chars[len - i - 1])
                    return false;
            }
            
            return true;
        }



        private bool CheckGrammar2(string input)
        {
            var alphabet = new char[] { '0', '1' };
            char[] chars = input.ToCharArray();
            int len = chars.Length;
            if (!IsSubset(alphabet, chars) || len == 0)
                return false;
            if (input != "" && chars[0] != '1')
                return false;
            else
            {
                for(int i = 1;  i < len; i++)
                {
                    if (chars[i - 1] == '1' && chars[i] != '0')
                        return false;
                }    
            }
            return true;
        }
        private bool CheckGrammar3(string input)
        {
            var alphabet = new char[] { 'a', 'b' };
            char[] chars = input.ToCharArray();
            int len = chars.Length;
            if (!IsSubset(alphabet, chars))
                return false;
            if(len > 4 && chars.Count(a => a == 'a') == 0)
                return false;
            return true;
        }
        private bool CheckGrammar4(string input)
        {
            var alphabet = new char[] { '0', '1' };
            char[] chars = input.ToCharArray();
            int len = chars.Length;
            if (!IsSubset(alphabet, chars) || len < 2 || (len == 2 && input != "01") || chars[0] != '0' || len == 0)
                return false;
            return true;
        }
        private bool CheckGrammar5(string input)
        {
            var alphabet = new char[] { '0', '1' };
            char[] chars = input.ToCharArray();
            int len = chars.Length;
            if (!IsSubset(alphabet, chars) || (len == 2 && input != "10") || len == 0)
                return false;
            return true;
        }

        private bool IsSubset(char[] chars1, char[] chars2)
        {
            var query = from sym in chars2.Except(chars1)
                        select sym;
            foreach(char c in query)
            {
                return false;
            }
            return true;
        }
    }
}
