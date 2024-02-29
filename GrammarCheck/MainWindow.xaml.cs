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
            string input = UserInput.Text;
            var task = Task.Run(() => CheckGrammars(input));
            task.Wait();
            CheckResult.Content = task.Result;
        }

        private async Task<string> CheckGrammars(string input)
        {
            string result = string.Empty;
            bool rule1 = await Task.Run(() => CheckGrammar1(input));
            //bool rule2 = await Task.Run(CheckGrammar2);
            //bool rule3 = await Task.Run(CheckGrammar3);
            //bool rule4 = await Task.Run(CheckGrammar4);
            //bool rule5 = await Task.Run(CheckGrammar5);
            List<int> rules = new();
            if (rule1)
                rules.Add(1);
            //if (rule2)
            //    rules.Add(2);
            //if (rule3)
            //    rules.Add(3);
            //if (rule4)
            //    rules.Add(4);
            //if (rule5)
            //    rules.Add(5);
            result = string.Join(',', rules);
            return result;    
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
            bool result = false;
            return result;
        }
        private bool CheckGrammar3(string input)
        {
            bool result = false;
            return result;
        }
        private bool CheckGrammar4(string input)
        {
            bool result = true;
            return result;
        }
        private bool CheckGrammar5(string input)
        {
            bool result = false;
            return result;
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
