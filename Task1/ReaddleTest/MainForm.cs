using System;
using System.Windows.Forms;
using System.Net;
using System.IO;
namespace ReaddleTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            comboBoxCurrency.SelectedIndex = 0;
        }
        /// <summary>
        /// Get currency USD to ... 
        /// </summary>
        /// <param name="currency">Country code</param>
        /// <returns></returns>
        private decimal GetCurrency(string currency)
        {
            var uri = String.Format("http://free.currencyconverterapi.com/api/v5/convert?q=USD_{0}&compact=ultra", currency);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "text/json";
            request.Method = "GET";
            using (HttpWebResponse httpWebResponse = (HttpWebResponse)request.GetResponse())
            {
                if (httpWebResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).",
                        httpWebResponse.StatusCode, httpWebResponse.StatusDescription));
                }
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    //Simple JSON encoding
                    string[] data = result.Split(new Char[] { '.', ':', '}' });
                    result = data[1] + ',' + data[2];
                    decimal cur;
                    try
                    {
                        cur = Convert.ToDecimal(result);
                        return cur;
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Problem with FormatOperation");
                        return 0;
                    }

                }
            }
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            decimal cur=0;
            string selectedCurrency = Convert.ToString(comboBoxCurrency.Items[comboBoxCurrency.SelectedIndex]);
            try
            {
                cur = GetCurrency(selectedCurrency);
            }
            catch (WebException exception)
            {
                textBoxAnswer.Text = exception.Message;
                return;
            }
            catch (Exception exception)
            {
                textBoxAnswer.Text = exception.ToString();
                return;
            }
            //Output
            textBoxAnswer.Text=String.Format("{0} USD to {1} is {2}", numericUpDownValue.Value, selectedCurrency, Convert.ToString(numericUpDownValue.Value*cur));

        }
    }
}
